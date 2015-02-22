using Appraisal.Class;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Appraisal
{
    public partial class Reminder : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ArrayList finallist = new ArrayList();
                ArrayList listofFunction = dbmanager.GetDistinctFunctions();

                foreach (string funct in listofFunction)
                {
                    ArrayList listofCompleted = dbmanager.GetDistinctNameUidCompletedAppraisal2(funct);

                    if (listofCompleted.Count != 0)
                    {
                        foreach (string s in listofCompleted)
                        {
                            finallist.Add(s);
                        }
                    }
                }

                ListBox1.DataSource = finallist;
                ListBox1.DataBind();
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            ArrayList listofSectionItems = new ArrayList();

            foreach (ListItem listItem in ListBox1.Items)
            {
                if (listItem.Selected == true)
                {
                    listofSectionItems.Add(listItem.Text);
                }
            }
            foreach (string s in listofSectionItems)
            {
                bool sentemail = email.SendMail("1st line" + Environment.NewLine + Environment.NewLine + "2nd line " + Environment.NewLine + Environment.NewLine + "3rd line", "1306543H@student.tp.edu.sg", "360° Leadership System New Password");
                //bool sentemail = email.SendMail("1st line" + Environment.NewLine + Environment.NewLine + "2nd line " + Environment.NewLine + Environment.NewLine + "3rd line", s + "@tp.edu.sg", "360° Leadership System New Password");

                if (sentemail == true)
                {
                    MessageBoxShow("Email send successfully.");
                    break;
                }
                else
                {
                    MessageBoxShow("Fail to add.");
                }
            }
        }

        private void MessageBoxShow(string message)
        {
            string strScript = "<script>";
            strScript += "alert('" + message + "');";
            strScript += "window.location='Reminder.aspx';";
            strScript += "</script>";
            this.ClientScript.RegisterStartupScript(this.GetType(), "Startup", strScript);
        }
    }
}