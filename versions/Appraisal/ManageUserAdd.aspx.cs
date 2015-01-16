using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Appraisal.Class;
using System.Collections;
using System.Globalization;
using System.Web.Security;

namespace Appraisal
{
    public partial class ManageUserAdd : System.Web.UI.Page
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            #region SelectedLinkBtn
            if (Session["BackBtnUserLink"] != null)
            {
                BackBtn.Style.Add("color", "Purple");
            }
            if (Session["BackBtnAddUserLink"] != null)
            {
                BackBtnLink.Style.Add("color", "Purple");
            }
            #endregion
        }

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
                        string legend = "";
                        legend += "<table align='left'><tr><td>";
                        legend += "<b>Legend of Add New System User</b><br>";
                        legend += "Adding new system user: All fields are mandatory fields. Sufficient and accurate information of user provided is needed.<br>";
                        legend += "</td></tr></table>";
                        lblLegend.Text = legend;

                        ArrayList listofSection = dbmanager.GetAllSection();
                        ArrayList listofFunction = dbmanager.GetAllFunction();
                        ArrayList listofRole = dbmanager.GetAllRole();

                        if (listofSection.Count != 0 && listofFunction.Count != 0 && listofRole.Count != 0)
                        {
                            NameTbx.Focus();
                            listSection.DataSource = listofSection;
                            listSection.DataBind();
                            ddlFunction.DataSource = listofFunction;
                            ddlFunction.DataBind();
                            ddlRole.DataSource = listofRole;
                            ddlRole.DataBind();
                            mainView.ActiveViewIndex = 0;
                        }
                    }
                }
                else
                {
                    Response.Redirect("accessdenied.aspx");
                }
            }
        }

        private void MessageBoxShow(string message)
        {
            string strScript = "<script>";
            strScript += "alert('" + message + "');";
            strScript += "window.location='ManageUserAdd.aspx';";
            strScript += "</script>";
            this.ClientScript.RegisterStartupScript(this.GetType(), "Startup", strScript);
        }

        protected void NextBtn_Click(object sender, EventArgs e)
        {
            ArrayList listofSectionItems = new ArrayList();
            bool chkresult = true;
            string name = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(NameTbx.Text.Trim());
            string uid = UserIdTbx.Text.ToLower().Trim();
            string sect = "";
            int count = 1;

            foreach (ListItem listItem in listSection.Items)
            {
                if (listItem.Selected == true)
                {
                    listofSectionItems.Add(listItem.Text);
                }
            }
            foreach (string s in listofSectionItems)
            {
                if (count == listofSectionItems.Count)
                {
                    sect += s;
                }
                else
                {
                    sect += s + ",";
                }
                count++;
            }
            if (chkresult == true)
            {
                lblValidatorSection.Text = "";
                string[] design = designationTbx.Text.Trim().Split('/');
                string designation = "";
                if (design.LongLength > 0)
                {
                    int counter = 0;
                    foreach (string de in design)
                    {
                        if (de != "")
                        {
                            if (counter > 0)
                            {
                                designation += "/" + CultureInfo.CurrentCulture.TextInfo.ToTitleCase(de.Trim());
                            }
                            else
                            {
                                designation += CultureInfo.CurrentCulture.TextInfo.ToTitleCase(de.Trim());
                            }
                            counter++;
                        }
                    }
                }
                else
                {
                    designation = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(designationTbx.Text.Trim());
                }
                string funct = ddlFunction.Text;
                string role = ddlRole.Text;

                staffinfo stf = null;

                if (name != "" && uid != "" && sect != "")
                {
                    bool chk = dbmanager.CheckUserIDExist(uid);
                    if (sect.Contains("ALL"))
                    {
                        sect = "ALL";
                    }
                    if (chk == false)
                    {
                        stf = new staffinfo(name, designation, sect, funct, uid, role);
                        Session["Staff"] = stf;
                        mainView.ActiveViewIndex = 1;
                        InformationLbl.Text = "Please preview what you have selected. If the information is incorrect, click 'Back' to re-select.";

                        string summary = "";
                        summary += "<table><tr><td><b>New system user information<b></td></tr></table>";
                        summary += "<table>";
                        summary += "<tr><td>Name:</td><td>" + name + "</td></tr>";
                        summary += "<tr><td>Designation:</td><td>" + designation + "</td></tr>";
                        summary += "<tr><td>Section:</td><td>" + sect.Replace(';', ',') + "</td></tr>";
                        summary += "<tr><td>Function:</td><td>" + funct + "</td></tr>";
                        summary += "<tr><td>User ID:</td><td>" + uid + "</td></tr>";
                        summary += "<tr><td>Role:</td><td>" + role + "</td></tr>";
                        summary += "</table>";

                        SummaryLbl.Text = summary;
                    }
                    else
                    {
                        lblValidatorUserId.ForeColor = System.Drawing.Color.Red;
                        lblValidatorUserId.Text = "Id already exist. Please enter another id.";
                    }
                }
            }
        }

        protected void ConfirmBtn_Click(object sender, EventArgs e)
        {
            staffinfo stf = (staffinfo)Session["Staff"];
            bool result = dbmanager.InsertStaffInformation(stf);
            string password = CreateRandomPassword(8);
            string hashpassw = FormsAuthentication.HashPasswordForStoringInConfigFile(password, "sha1");
            bool result2 = dbmanager.InsertUserIDandPassw(stf.Uid, hashpassw);

            if (result == true && result2 == true)
            {
                bool sentemail = email.SendMail("Thank you for using the 360° leadership system." + Environment.NewLine + Environment.NewLine + "Your new password for login to the system is: " + password + "" + Environment.NewLine + Environment.NewLine + "You are advised to change your password once you have logged in." + Environment.NewLine + "For login, please visit: " + "http://ascapps.tp.edu.sg/360Leadership/LoginPage.aspx" + Environment.NewLine + Environment.NewLine + "For any enquiries to the system, please contact asc webmaster or (x5376)", stf.Uid + "@tp.edu.sg", "360° Leadership System New Password");

                if (sentemail == true)
                {
                    MessageBoxShow("User added successfully.");
                }
            }
            else
            {
                MessageBoxShow("Fail to add.");
            }
        }

        private static string CreateRandomPassword(int passwordLength)
        {
            string allowedChars = "abcdefghijkmnopqrstuvwxyzABCDEFGHJKLMNOPQRSTUVWXYZ0123456789@$&?";
            char[] chars = new char[passwordLength];
            Random rd = new Random();

            for (int i = 0; i < passwordLength; i++)
            {
                chars[i] = allowedChars[rd.Next(0, allowedChars.Length)];
            }

            return new string(chars);
        }

        protected void BackBtn_Click(object sender, EventArgs e)
        {
            BackBtn.Style.Add("color", "Purple");
            Session["BackBtnUserLink"] = true;
            if (Session["ManageUserPage"] != null)
            {
                Response.Redirect(Session["ManageUserPage"].ToString());
            }
            else
            {
                Response.Redirect("ManageUser.aspx");
            }
        }

        protected void BackBtnLink_Click(object sender, EventArgs e)
        {
            BackBtnLink.Style.Add("color", "Purple");
            Session["BackBtnAddUserLink"] = true;
            mainView.ActiveViewIndex = 0;
        }
    }
}