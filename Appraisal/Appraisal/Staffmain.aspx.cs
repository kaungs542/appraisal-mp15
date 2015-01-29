using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Appraisal.Class;
using System.Collections;

namespace Appraisal
{
    public partial class Staffmain : System.Web.UI.Page
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            #region SelectedLinkBtn
            if (Session["SubmitAppraisalStaffLink"] != null)
            {
                SubmitLink.Style.Add("color", "Purple");
            }
            if (Session["ViewAppraisalStaffLink"] != null)
            {
                ViewAppraisalLink.Style.Add("color", "Purple");
            }
            if (Session["ViewIndividualAllLink"] != null)
            {
                ViewIndividualAllLink.Style.Add("color", "Purple");
            }
            if (Session["ViewAppraisalChartLink"] != null)
            {
                ViewAppraisalChartLink.Style.Add("color", "Purple");
            }
            if (Session["ViewGraphLink"] != null)
            {
                ViewGraphLink.Style.Add("color", "Purple");
            }


            #endregion
        }
        //public static string status;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Response.Clear();
                if (Session["Role"] != null)
                {
                    string role = Session["Role"].ToString();
                    if (role == "Admin")
                    {
                        Response.Redirect("accessdenied.aspx");
                    }
                    else
                    {
                        Session["Submitted"] = null;
                        Session["HomeClicked"] = true;
                        string uid = Session["UserID"].ToString();
                        string username = Session["Name"].ToString();

                        if (role == "Officer")
                        {
                            ArrayList listofsec = new ArrayList();
                            bool checkifappraisalsec = false;
                            ManageAppraisalPanel.Visible = true;
                            staffinfo stfinfo = dbmanager.GetStaffDetailsViaUid(uid);
                            if (stfinfo.Section != "ALL")
                            {
                                string[] arraysec = stfinfo.Section.Split(',');
                                foreach (string sec in arraysec)
                                {
                                    if (sec != "")
                                    {
                                        listofsec.Add(sec);
                                    }
                                }
                                checkifappraisalsec = dbmanager.CheckIfExistSection(listofsec);
                            }
                            else
                            {
                                checkifappraisalsec = dbmanager.CountAllAppraisal();
                            }
                            if (checkifappraisalsec == true)
                            {
                                ViewIndividualAllLbl.Text = "To view by section or individual staff report, click ";
                            }
                            else
                            {
                                ViewIndividualAllLbl.Text = "No staff evaluation of the same section is <b>found<b>";
                                ViewIndividualAllLink.Visible = false;
                            }

                            bool checkapp = dbmanager.CountAllAppraisal();
                            if (checkapp == true)
                            {
                                ViewAppraisalChart.Text = "To view peer evaluation chart, click ";
                                ViewAppraisalChartLink.Visible = true;
                                ViewGraph.Text = "To view section chart based on each question, click ";
                                ViewGraphLink.Visible = true;
                            }
                            else
                            {
                                ViewAppraisalChart.Text = "No chart is <b>found<b> ";
                                ViewAppraisalChartLink.Visible = false;
                                ViewGraph.Text = "No chart is <b>found<b> ";
                                ViewGraphLink.Visible = false;                                
                            }
                        }
                        else
                        {
                            ManageAppraisalPanel.Visible = false;
                        }

                        //staffName.Text = username;
                        //if appriasal submitted
                        bool result = dbmanager.CheckIfAppraisalSubmitted(uid);

                        //if appraisal started
                        Systemtime st = dbmanager.GetSystemTime();
                        DateTime today = DateTime.Today;
                        if (st != null)
                        {
                            int zzp = datediff.dateDiff(st.Enddate);
                            Session["EndTime"] = st.Enddate;

                            if (st.Startdate > today)
                            {
                                SubmitAppraisalLbl.Text = "System period <b>is closed<b>";
                                SubmitLink.Visible = false;
                            }
                            else if (zzp <= (-1))
                            {
                                SubmitAppraisalLbl.Text = "System period <b>is closed<b>";
                                SubmitLink.Visible = false;
                            }
                            else if (result == true)
                            {
                                Session["Submitted"] = true;
                                SubmitAppraisalLbl.Text = "Your evaluation has been <b>submitted<b>";
                                SubmitLink.Visible = false;
                            }
                            else
                            {
                                Session["Submitted"] = null;
                                SubmitAppraisalLbl.Text = "To start peer evaluation, click ";
                                SubmitLink.Visible = true;
                                
                            }
                        }
                        else
                        {
                            SubmitLink.Visible = false;
                        }

                        int countapp = dbmanager.GetCountYourAppraisal(uid);

                        if (countapp != 0)
                        {
                            //if appraisal submitted
                            //ViewAppraisalLbl.Text = "To view own evaluation report, click ";
                            ViewAppraisalLbl.Text = "To view own evaluation report, click here";
                            ViewAppraisalLink.Visible = false;
                        }
                        else
                        {
                            ViewAppraisalLbl.Text = "You have 0 evaluation(s) <b>received<b>";
                            ViewAppraisalLink.Visible = false;

                        }
                    }
                }
                else
                {
                    Response.Redirect("accessdenied.aspx");
                }
                //ViewAppraisalLbl.Visible = false;
                //ViewAppraisalLink.Visible = false;
            }
        }

        protected void SubmitAppraisalLink_Click(object sender, EventArgs e)
        {
            SubmitLink.Style.Add("color", "Purple");
            Session["SubmitAppraisalStaffLink"] = true;
            Response.Redirect("~/SubmitAppraisal.aspx");
        }

        protected void ViewAppraisalLink_Click(object sender, EventArgs e)
        {
            //view own appraisal
            Session["OperatorCheck"] = null;
            ViewAppraisalLink.Style.Add("color", "Purple");
            Session["ViewAppraisalStaffLink"] = true;
            Response.Redirect("~/ViewAppraisal.aspx");
        }

        protected void ViewIndividualAllLink_Click(object sender, EventArgs e)
        {
            Session["OperatorCheck"] = true;
            Session["PreviousPageOp"] = null;
            ViewIndividualAllLink.Style.Add("color", "Purple");
            Session["ViewIndividualAllLink"] = true;

            if (Session["Role"].ToString().Equals("Officer"))
            {
                Response.Redirect("~/ViewAppraisalAllHistory.aspx");
            }
            else
            {
                Response.Redirect("~/ViewAppraisal.aspx");
            }
        }

        protected void ViewAppraisalChartLink_Click(object sender, EventArgs e)
        {
            ViewAppraisalChartLink.Style.Add("color", "Purple");
            Session["ViewAppraisalChartLink"] = true;
            Response.Redirect("~/ViewHistoryChart.aspx");
        }

        protected void ViewGraphLink_Click(object sender, EventArgs e)
        {
            ViewGraphLink.Style.Add("color", "Purple");
            Session["ViewGraphLink"] = true;
            Response.Redirect("~/ViewGraph.aspx");
        }
    }
}
