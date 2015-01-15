using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Text;
using System.Web.UI.WebControls;
using System.Collections;
using System.Web.Security;
using System.Security.Cryptography;
using Appraisal.Class;

namespace Appraisal
{
    public partial class LoginPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            staffnumber.Focus();
        }

        protected void login_button_Click(object sender, EventArgs e)
        {
            string staffnum = staffnumber.Text;
            string passw = password.Text;
            string normalhashpassw = "";

            if (passw.Length != 0)
            {
                normalhashpassw = FormsAuthentication.HashPasswordForStoringInConfigFile(passw, "sha1");

                bool result = dbmanager.ValidateLogin(staffnum, normalhashpassw);

                if (result == true)
                {
                    FormsAuthentication.SetAuthCookie(staffnum, false);
                    Session["LoginName"] = staffnum;
                    Response.Redirect("~/default.aspx");
                }
                else
                {
                    messagelbl.Text = "Invalid username or password.";
                }
            }
        }
    }
}
