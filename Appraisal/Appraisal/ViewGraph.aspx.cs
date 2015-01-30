using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Appraisal.Class;
using System.Collections;
using System.Web.UI.DataVisualization.Charting;
using System.IO;
using System.Data;

namespace Appraisal
{
    public partial class ViewGraph : System.Web.UI.Page
    {
        public static string lbl;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Session["Role"] != null)
                {
                    string role = Session["Role"].ToString();
                    if (role == "User")
                    {
                        Response.Redirect("accessdenied.aspx");
                    }
                    else
                    {
                        #region for Section Group Label
                        lbl = "select question:";
                        lblSelectQuestion.Text = lbl;
                        #endregion

                        bool checkifappraisalsec = dbmanager.CountAllAppraisal();
                        if (checkifappraisalsec == true)
                        {
                            //if (role == "Admin")
                            //{
                            //    BindDropDownListAdmin();
                            //}
                            if (role == "Officer")
                            {
                                BindDropDownListOfficer();
                            }
                            //LegendMessage();
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

        protected void BindDropDownListOfficer()
        {
            try
            {
                string uid = Session["UserID"].ToString();
                staffinfo stfinfo = dbmanager.GetStaffDetailsViaUid(uid);
                //if director
                //if (stfinfo.Section == "ALL")
                //{
                    //ArrayList listofsection = RemoveDups(dbmanager.GetAllSectionnByLimit());
                    //ArrayList listoffunction = dbmanager.GetAllFunctionByLimit();
                    ArrayList listofquestion = dbmanager.GetAllQuestionList();

                    //ArrayList listofselectfunction = new ArrayList();
                    //ArrayList listofselectsection = new ArrayList();

                    if (listofquestion.Count > 0)
                    {
                        //listofsection.Insert(0, "<----Please select one---->");
                        //listofsection.Insert(1, "<--------ASC School-------->");
                        //ddlSelectSection.DataSource = listofsection;
                        //ddlSelectSection.DataBind();
                        //for ddlselectQuestion
                        listofquestion.Insert(0, "<--------All question(s)------->");
                        ddlSelectQuestion.DataSource = listofquestion;
                        ddlSelectQuestion.DataBind();

                        /*listofselectfunction.Insert(0, "<--------Autoupdate-------->");
                        ddlFilterFunction.DataSource = listofselectfunction;
                        ddlFilterFunction.DataBind();
                        listoffunction.Insert(0, "<----Please select one---->");
                        listoffunction.Insert(1, "<--------ASC School-------->");
                        ddlSelectFunction.DataSource = listoffunction;
                        ddlSelectFunction.DataBind();
                        listofselectsection.Insert(0, "<--------Autoupdate-------->");
                        ddlFilterBySection.DataSource = listofselectsection;
                        ddlFilterBySection.DataBind();
                        SearchPanelFunctionViaSection.Visible = false;*/
                    }
                    else
                    {
                        MessageBoxShow("Error reading data from database.");
                    }
                //}
                /*else
                {
                    ArrayList listofsection = new ArrayList();
                    string[] arraysection = stfinfo.Section.Split(',');
                    if (arraysection.LongLength > 0)
                    {
                        foreach (string sect in arraysection)
                        {
                            listofsection.Add(sect);
                        }
                    }
                    ArrayList listoffunction = RemoveDups(dbmanager.GetAllFunctionByLimitViaOfficer(stfinfo.Section));
                    ArrayList listofquestion = dbmanager.GetAllQuestionList();

                    ArrayList listofselectfunction = new ArrayList();
                    ArrayList listofselectsection = new ArrayList();

                    if (listoffunction.Count > 0 && listofsection.Count > 0 && listofquestion.Count > 0)
                    {
                        listofsection.Insert(0, "<----Please select one---->");
                        ddlSelectSection.DataSource = listofsection;
                        ddlSelectSection.DataBind();
                        // for ddlselectQuestion
                        listofquestion.Insert(0, "<--------All question(s)------->");
                        ddlSelectQuestion.DataSource = listofquestion;
                        ddlSelectQuestion.DataBind();

                        listofselectfunction.Insert(0, "<--------Autoupdate-------->");
                        ddlFilterFunction.DataSource = listofselectfunction;
                        ddlFilterFunction.DataBind();
                        listoffunction.Insert(0, "<----Please select one---->");
                        ddlSelectFunction.DataSource = listoffunction;
                        ddlSelectFunction.DataBind();
                        listofselectsection.Insert(0, "<--------Autoupdate-------->");
                        ddlFilterBySection.DataSource = listofselectsection;
                        ddlFilterBySection.DataBind();
                        SearchPanelFunctionViaSection.Visible = false;
                    }
                    else
                    {
                        MessageBoxShow("Error reading data from database.");
                    }
                }*/
            }
            catch
            {

            }
        }

        private void MessageBoxShow(string message)
        {
            string strScript = "<script>";
            strScript += "alert('" + message + "');";
            strScript += "window.location='default.aspx';";
            strScript += "</script>";
            this.ClientScript.RegisterStartupScript(this.GetType(), "Startup", strScript);
        }

        protected void populateChartFunctionViaSection(string section, int questionID)
        {
            try
            {
                section = "PHS";
                questionID = 1;
 
                MultiView1.Visible = true;

                Chart1.Series.Clear();
                Chart1.ChartAreas[0].AxisX.MajorGrid.LineColor = System.Drawing.Color.LightGray;
                Chart1.ChartAreas[0].AxisY.MajorGrid.LineColor = System.Drawing.Color.LightGray;
                MultiView1.Visible = true;
                DataTable table = new DataTable();
                table.Columns.Add("FunctionGroup");
                table.Columns.Add("AverageGrading");
                table.Columns.Add("Date");

                ArrayList listofdates = dbmanager.GetListofDatesViaSection(section);
                //string[] functionsplit = functionlist.Split(',');

                ArrayList listofquestion = dbmanager.GetAllQuestion();
                ArrayList listofstaff = dbmanager.GetAllStaffDetails();

                foreach (DateTime date in listofdates)
                {
                    //foreach (string function in functionsplit)
                    //{
                        double result = 0.0;
                        double staffAverage = 0.0;
                        double staffAverageResult = 0.0;
                        int count = 0;

                        foreach (staffinfo staff in listofstaff)
                        {
                            if (staff.Section.Contains(section))
                            {
                                if (questionID == 0)
                                {
                                    foreach (Question qn in listofquestion)
                                    {
                                        staffAverage += dbmanager.GetAvgRating(staff.Uid, date, qn.QuestionID); // for all question
                                    }
                                    int countquestion = dbmanager.GetTotalCountQuestionInPeriod(staff.Uid, date);
                                    staffAverageResult = Math.Round((staffAverage / countquestion), 1);
                                    count++;
                                }
                                else
                                {
                                    staffAverage = dbmanager.GetAvgRating(staff.Uid, date, questionID); // for per question
                                    int countquestion = dbmanager.GetTotalCountQuestionInPeriod(staff.Uid, date);
                                    staffAverageResult = staffAverage;
                                    count++;
                                }
                                string monthname = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(date.Month);
                                table.Rows.Add(staff.Name, staffAverage, monthname.Substring(0, 3) + "/" + date.Year.ToString().Substring(2, 2));
                                //table.Rows.Add(staff.Name, staffAverage);
                            }
                            

                            result = 0.0;
                            staffAverage = 0.0;
                            staffAverageResult = 0.0;
                        }
                        //try
                        //{
                        //    result = Math.Round((staffAverageResult / count), 1);
                        //}
                        //catch
                        //{
                        //    result = 0.0;
                        //}

                        //string monthname = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(date.Month);
                        //table.Rows.Add(function, result);

                        //result = 0.0;
                        //staffAverage = 0.0;
                        //staffAverageResult = 0.0;
                    //}
                }

                bool display = false;
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    if (table.Rows[i].ItemArray[1].ToString() == "0")
                    {
                        display = false;
                    }
                    else
                    {
                        display = true;
                        break;
                    }
                }
                if (display == true)
                {
                    Chart1.Visible = true;
                    DataTableReader datareader = table.CreateDataReader();
                    foreach (Series cs in Chart1.Series) { cs.ChartType = SeriesChartType.Line; cs.IsValueShownAsLabel = true; }
                    Chart1.DataBindCrossTable(datareader, "Date","FunctionGroup", "AverageGrading", "");
                    Chart1.Legends.Add("Legend");
                    Chart1.ChartAreas[0].AxisX.Title = "Name of Staff";
                    Chart1.ChartAreas[0].AxisY.Title = "Average grade";
                    Chart1.Legends[0].Enabled = true;
                    Chart1.Legends[0].BackColor = System.Drawing.Color.Transparent;
                    Chart1.Width = 1000;
                    Chart1.Height = 600;
                    MultiView1.ActiveViewIndex = 0;
                }
                else
                {
                    Chart1.Visible = false;
                    MultiView1.ActiveViewIndex = 1;
                    lbDisplay.Text = "<b>No result found</b>";
                }
            }
            catch
            {
                MultiView1.Visible = false;
            }
        }

        protected void Display_Click(object sender, EventArgs e)
        {
            string sectionid = "PHS";
            int id = 1;
            populateChartFunctionViaSection(sectionid, id);
        }
    }
}