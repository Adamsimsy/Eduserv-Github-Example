using System;
using Sitecore.SecurityModel;

namespace Layouts.Sublayout_britishlibrary_shared_apiexample {
  
	/// <summary>
	/// Summary description for Sublayout_britishlibrary_shared_apiexampleSublayout
	/// </summary>
  public partial class Sublayout_britishlibrary_shared_apiexampleSublayout : System.Web.UI.UserControl 
	{
    private void Page_Load(object sender, EventArgs e) {
      // Put user code to initialize the page here
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        var item = Sitecore.Context.Item;
        //tbxInput.Text = item.Name;

        var master = Sitecore.Configuration.Factory.GetDatabase("master");

        //using (new SecurityDisabler())
        //{
            
        //    var home = master.GetItem("/sitecore/content/home");
        //    var sample = master.Templates["sample/sample item"];
        //    var myItem = home.Add(tbxInput.Text, sample);
        //}

        string query = String.Format("//*[@@templateid='{0}']", Sitecore.TemplateIDs.Folder);
        Sitecore.Data.Items.Item[] queried = master.SelectItems(query);

        if (queried != null)
        {
            foreach (var itm in queried)
            { //TODO: process item 
                litItems.Text = litItems.Text + "<p>" + itm.Paths.FullPath + "</p>";
            }
        }
 
    }
  }
}