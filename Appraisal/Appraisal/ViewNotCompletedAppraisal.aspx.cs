using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Appraisal.Class;
using System.Collections;
using System.IO;
using System.Globalization;

namespace Appraisal
{
    public partial class ViewCompletedAppraisal : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Session["Role"] != null)
                {
                    string role = Session["Role"].ToString();
                    if (role != "Admin")
                    {
                        Response.Redirect("accessdenied.aspx");
                    }
                    else
                    {

                        int count = dbmanager.CheckNotCompletedAppraisal();

                        if (count != 0)
                        {
                            #region populate result
                            string content = "";
                            content += "<table width='95%' border='0' style=padding-left:10px><tr><td>";

                            content += "<table width='100%' border='1' style='border-color:#000080' cellspacing='0'><tr style='border-color:#000080'><td style='border-color:#000080'>";
                            content += "<b>Total number of staffs who have not completed the peer evaluation:&nbsp;" + count + "</b>";
                            content += "</td></tr></table><br>";

                            ArrayList listofFunction = dbmanager.GetDistinctFunctions();

                            foreach (string funct in listofFunction)
                            {
                                ArrayList listofCompleted = dbmanager.GetDistinctNameUidCompletedAppraisal(funct);

                                if (listofCompleted.Count != 0)
                                {
                                    #region function by function
                                    content += "<b><font color='blue'>" + funct + "</font></b>";

                                    content += "<table width='60%' border='1' style='border-color:#000080' cellspacing='0'>";
                                    content += "<tr style='border-color:#000080' align='center'>";
                                    content += "<td style='border-color:#000080' width='40%'><b>Name</b></td><td style='border-color:#000080' width='20%'><b>User ID</b></td>"
                                                + "<td style='border-color:#000080' width='30%'><b>Section<></b/td></tr>";
                                    content += "<tr style='border-color:#000080' align='center'>";

                                    foreach (staffappraisal stfapp in listofCompleted)
                                    {
                                        string staffname = dbmanager.GetNameViaUserID(stfapp.Uid);
                                        staffinfo stf = dbmanager.GetStaffDetailsViaUid(stfapp.Uid);
                                        content += "<td width='40%' align='left' style='border-color:#000080'>" + staffname + "</td>";
                                        content += "<td width='20%' align='left' style='border-color:#000080'>" + stfapp.Uid.ToString() + "</td>";
                                        content += "<td width='30%' align='left' style='border-color:#000080'>" + stf.Section.ToString() + "</td>";
                                        content += "</tr>";
                                    }
                                    content += "</table><br>";
                                    #endregion
                                }
                            }
                            //content += "</td></tr></table>";
                            lblStaffSummary.Text += content;
                            #endregion
                        }
                        else
                        {
                            Response.Redirect("default.aspx");
                        }
                    }
                }
                else
                {
                    Response.Redirect("accessdenied.aspx");
                }
            }

        }

        protected void exportWord_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                Response.ClearContent();
                Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "StaffDetails.doc"));
                Response.Charset = "";
                Response.ContentType = "application/ms-word";
                StringWriter sw = new StringWriter();
                HtmlTextWriter htw = new HtmlTextWriter(sw);
                lblStaffSummary.RenderControl(htw);
                Response.Write(sw.ToString());
                Response.End();
            }
            catch (Exception ex)
            {
                MessageBoxShow(ex.Message.ToString());
            }
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }

        private void MessageBoxShow(string message)
        {
            string strScript = "<script>";
            strScript += "alert('" + message + "');";
            strScript += "</script>";
            this.ClientScript.RegisterStartupScript(this.GetType(), "Startup", strScript);
        }
    }
}