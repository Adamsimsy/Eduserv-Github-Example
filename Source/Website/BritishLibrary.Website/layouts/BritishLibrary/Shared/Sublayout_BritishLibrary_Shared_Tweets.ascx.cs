using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BritishLibrary.Components.Twitter;

namespace Layouts.Sublayout_britishlibrary_shared_tweets {
  
	/// <summary>
	/// Summary description for Sublayout_britishlibrary_shared_tweetsSublayout
	/// </summary>
  public partial class Sublayout_britishlibrary_shared_tweetsSublayout : System.Web.UI.UserControl 
	{
    private void Page_Load(object sender, EventArgs e) {
      // Put user code to initialize the page here
        TwitterClient client = new TwitterClient();

        string text = "";

        var tweets = client.GetTimelineTweets();

        tweets.ForEach(x => text += "<p>Tweet from: " + x.Key + " with message: " + x.Value + "</p>");

        litTweets.Text = text;
    }
  }
}