using System;

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
        Response.Write(tbxInput.Text);
    }
  }
}