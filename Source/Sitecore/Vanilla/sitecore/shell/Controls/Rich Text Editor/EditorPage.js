if (typeof (Sitecore) == "undefined") Sitecore = new Object();
if (typeof (Sitecore.Controls) == "undefined") Sitecore.Controls = new Object();

Sitecore.Controls.RichEditor = Class.create({
  initialize: function(editorId) {
    this.editorId = editorId;

    Event.observe(window, "resize", this.fitEditorToScreen.bind(this));
  },

  onClientLoad: function(editor) {
    editor.attachEventHandler("onkeydown", this.onKeyDown.bind(this));
    if (!scForm.browser.isIE) {
      this.getEditor().get_element().style.minHeight = '';
    }
    fixIeObjectTagBug();
    Event.observe($$('.reMode_design')[0], 'click', function () {
      setTimeout(fixIeObjectTagBug, 100);
    });

    if (Prototype.Browser.IE && editor.get_newLineMode() == Telerik.Web.UI.EditorNewLineModes.P) {
      editor.attachEventHandler("onkeydown", function (e) {
        if (e.keyCode == 13) {
          var oCmd = new Telerik.Web.UI.Editor.GenericCommand("Enter", editor.get_contentWindow(), editor);
          editor.executeCommand(oCmd);
        }
      });
    }

    this.oldValue = editor.get_html(true);

    this.fitEditorToScreen();
  },

  getEditor: function() {
    if (typeof ($find) == "function") {
      return $find(this.editorId);
    }

    return null;
  },

  fitEditorToScreen: function() {
    var editor = this.getEditor();
    if (!editor) {
      return;
    }

    var container = $$("form")[0];

    var width = 0;
    var height = 0;

    if (container.getHeight != null) {
      height = container.getHeight() - 28;
      width = container.getWidth();
    }
    else {
      width = container.offsetWidth;
      height = (container.offsetHeight - 28);
    }

    if (height < 0) {
      return;
    }

    editor.setSize(width, height);

    if (!scForm.browser.isIE) {
      if (height - 53 > 0) {
        $('EditorCenter').style.height = (height - $('EditorTop').offsetHeight - 27) + 'px';
      }
      else {
        $('EditorCenter').style.height = '0px';
      }
    }
  },

  saveRichText: function(html) {
    var w = scForm.browser.getParentWindow(window.frameElement.ownerDocument);
    var wFrameElement = w.frameElement;
    if (wFrameElement) {
      w = scForm.browser.getParentWindow(w.frameElement.ownerDocument);
      if (!w.scContent) {
        w = Element.adjacent(wFrameElement, '#' + wFrameElement.id.substring(0, wFrameElement.id.indexOf('overlayWindow')))[0].contentWindow;
      }
    }

    w.scContent.saveRichText(html);
  },

  setFocus: function() {
    var editor = this.getEditor();
    if (!editor) {
      return;
    }

    editor.setFocus();
  },

  setText: function(html) {
    var editor = this.getEditor();
    if (!editor) {
      return;
    }

    editor.set_html(html);
    fixIeObjectTagBug();
  },

  onKeyDown: function(evt) {
    var editor = this.getEditor();

    if (editor == null || evt == null) {
      return;
    }

    if (evt.ctrlKey && evt.keyCode == 13) {
      scSendRequest("editorpage:accept");
      return;
    }

    if (!scForm.isFunctionKey(evt, true)) {
      scForm.setModified(true);
    }
  }
});

function scCloseEditor() {
   var doc = window.top.document;
   
   // Field editor
   var w = doc.getElementById('feRTEContainer');

   if (w) {        
    $(w).hide();
   }
   else {
    // Page editor
    window.close();
  }

  var modalOverlay = Element.select(window.top.document, '#ModalOverlayRTE')[0];
  if (modalOverlay) {
    Element.remove(modalOverlay);
  }
}

function fixIeObjectTagBug() {
  var objects = Element.select($('Editor_contentIframe').contentWindow.document, 'object');
  var i;
  for (i = 0; i < objects.length; i++) {
    if (!objects[i].id || objects[i].id.indexOf('IE_NEEDS_AN_ID_') > -1) {
      objects[i].id = 'IE_NEEDS_AN_ID_' + i;
    }
  }
}

// Hack described here http://www.telerik.com/community/forums/sharepoint-2007/full-featured-editor/paragraph-style-names-don-t-match-config.aspx
function OnClientSelectionChange(editor, args) {
  var tool = editor.getToolByName("FormatBlock");
  if (tool) {
    setTimeout(function () {
      var defaultBlockSets = [
        ["p", "Normal"],
        ["h1", "Heading 1"],
        ["h2", "Heading 2"],
        ["h3", "Heading 3"],
        ["h4", "Heading 4"],
        ["h5", "Heading 5"],
        ["menu", "Menu list"],
        ["pre", "Formatted"],
        ["address", "Address"]];

      var value = tool.get_value();

      var block = Prototype.Browser.IE
        ? defaultBlockSets.find(function (element) { return element[1] == value; })
        : [value];

      if (block) {
        var tag = block[0];
        var correctBlock = editor._paragraphs.find(function (element) { return element[0].indexOf(tag) > -1; });
        if (correctBlock) {
          tool.set_value(correctBlock[1]);
        }
      }
    }, 0);
  }
}

// Fix mentioned here http://www.telerik.com/community/forums/aspnet-ajax/editor/html-entity-characters-are-not-escaped-on-hyperlink-editor-email-subject.aspx
function OnClientPasteHtml(sender, args) {
  var commandName = args.get_commandName();
  if (Prototype.Browser.IE && (commandName == "LinkManager" || commandName == "SetLinkProperties")) {
    var value = args.get_value();
    if (/<a[^>]*href=['|"]mailto:.*subject=/i.test(value)) {
      var hrefMarker = 'href=';

      // quote could be ' or " depending on subject content
      var quote = value.charAt(value.indexOf(hrefMarker) + hrefMarker.length);
      var regex = new RegExp(hrefMarker + quote + 'mailto:.*subject=.*' + quote, 'i');
      var fixedValue = value.replace(regex, function (str) { return str.replace(/</g, "&lt;").replace(/>/g, "&gt;"); });
      args.set_value(fixedValue);
    }
  }
}