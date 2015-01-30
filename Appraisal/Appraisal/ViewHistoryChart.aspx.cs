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
    public partial class ViewHistoryChart : System.Web.UI.Page
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
                        lblSelectSection.Text = "select section group (chart title): ";
                        lblFilterByFunction.Text = "select function (chart components): ";
                        lblSelectedFunction.Text = "display function(s) chart by: ";
                        #endregion

                        bool checkifappraisalsec = dbmanager.CountAllAppraisal();
                        if (checkifappraisalsec == true)
                        {
                            if (role == "Admin")
                            {
                                BindDropDownListAdmin();
                            }
                            else if (role == "Officer")
                            {
                                BindDropDownListOfficer();
                            }
                            LegendMessage();
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

        protected void LegendMessage()
        {
            string legend = "";
            legend += "<table align='left'><tr><td>";
            legend += "<b>Legend on Viewing Evaluation Chart</b><br><br>";
            legend += "Average grade refers to overall final grading for the evaluation period.<br><br>";
            legend += "Please note that the format of date shown is: MONTH/YEAR.<br>";
            legend += "</td></tr></table>";
            lbllegendHistory.Text = legend;
        }

        public ArrayList RemoveDups(ArrayList items)
        {
            ArrayList noDups = new ArrayList();

            foreach (string strItem in items)
            {
                if (!noDups.Contains(strItem.Trim()))
                {
                    noDups.Add(strItem.Trim());
                }
            }
            noDups.Sort();
            return noDups;
        }

        protected void BindDropDownListAdmin()
        {
            try
            {

                ArrayList listofsection = RemoveDups(dbmanager.GetAllSectionnByLimit());
                ArrayList listoffunction = dbmanager.GetAllFunctionByLimit();
                ArrayList listofquestion = dbmanager.GetAllQuestionList();

                ArrayList listofselectfunction = new ArrayList();
                ArrayList listofselectsection = new ArrayList();
                ArrayList listofselectquestion = new ArrayList();

                if (listoffunction.Count > 0 && listofsection.Count > 0 && listofquestion.Count > 0)
                {
                    #region for section group
                    listofsection.Insert(0, "<----Please select one---->");
                    listofsection.Insert(1, "<--------ASC School-------->");
                    ddlSelectSection.DataSource = listofsection;
                    ddlSelectSection.DataBind();

                    listofquestion.Insert(0, "<--------All question(s)------->");
                    ddlSelectQuestion.DataSource = listofquestion;
                    ddlSelectQuestion.DataBind();

                    listofselectfunction.Insert(0, "<--------Autoupdate-------->");
                    ddlFilterFunction.DataSource = listofselectfunction;
                    ddlFilterFunction.DataBind();
                    #endregion

                    #region for function group
                    listoffunction.Insert(0, "<----Please select one---->");
                    listoffunction.Insert(1, "<--------ASC School-------->");
                    ddlSelectFunction.DataSource = listoffunction;
                    ddlSelectFunction.DataBind();

                    //listofquestion.Insert(0, "<---------All question-------->");
                    ddlFilterQuestion.DataSource = listofquestion;
                    ddlFilterQuestion.DataBind();

                    listofselectsection.Insert(0, "<--------Autoupdate-------->");
                    ddlFilterBySection.DataSource = listofselectsection;
                    ddlFilterBySection.DataBind();
                    SearchPanelFunctionViaSection.Visible = false;
                    #endregion
                }
                else
                {
                    MessageBoxShow("Error reading data from database.");
                }
            }
            catch
            {
            }
        }

        protected void BindDropDownListOfficer()
        {
            try
            {
                string uid = Session["UserID"].ToString();
                staffinfo stfinfo = dbmanager.GetStaffDetailsViaUid(uid);
                //if director
                if (stfinfo.Section == "ALL")
                {
                    ArrayList listofsection = RemoveDups(dbmanager.GetAllSectionnByLimit());
                    ArrayList listoffunction = dbmanager.GetAllFunctionByLimit();
                    ArrayList listofquestion = dbmanager.GetAllQuestionList();

                    ArrayList listofselectfunction = new ArrayList();
                    ArrayList listofselectsection = new ArrayList();

                    if (listoffunction.Count > 0 && listofsection.Count > 0 && listofquestion.Count > 0)
                    {
                        listofsection.Insert(0, "<----Please select one---->");
                        listofsection.Insert(1, "<--------ASC School-------->");
                        ddlSelectSection.DataSource = listofsection;
                        ddlSelectSection.DataBind();
                        //for ddlselectQuestion
                        listofquestion.Insert(0, "<--------All question(s)------->");
                        ddlSelectQuestion.DataSource = listofquestion;
                        ddlSelectQuestion.DataBind();

                        listofselectfunction.Insert(0, "<--------Autoupdate-------->");
                        ddlFilterFunction.DataSource = listofselectfunction;
                        ddlFilterFunction.DataBind();
                        listoffunction.Insert(0, "<----Please select one---->");
                        listoffunction.Insert(1, "<--------ASC School-------->");
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
                }
                else
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
                }
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

        private void MessageBox(string message)
        {
            string strScript = "<script>";
            strScript += "alert('" + message + "');";
            //strcript += "window.location='default.aspx';";
            strScript += "</script>";
            this.ClientScript.RegisterStartupScript(this.GetType(), "Startup", strScript);
        }

        private void MessageBoxShowWithoutredirect(string message)
        {
            string strScript = "<script>";
            strScript += "alert('" + message + "');";
            strScript += "</script>";
            this.ClientScript.RegisterStartupScript(this.GetType(), "Startup", strScript);
        }

        protected void SearchBtn_Click(object sender, EventArgs e)
        {
            if (SelectTbxFunction.Text == "")
            {
                MessageBox("Please click add button to insert function(s)!");
            }
            else
            {
                MultiView1.Visible = false;

                if (ddlSelectSection.Text == "<--------ASC School-------->")
                {
                    ddlFilterFunction.Items.Clear();
                    ddlFilterFunction.Items.Add("<--------Autoupdate-------->");
                    ddlFilterFunction.SelectedIndex = 0;
                    populateChartSectionViaFunctionALL();
                }
                else
                {
                    // for question ddl ()
                    int questionID = ddlSelectQuestion.SelectedIndex;
                    string section = ddlSelectSection.Text.Trim();
                    string functon = SelectTbxFunction.Text.Trim();
                    string allfunction = "";
                    int counter = 1;

                    if (functon.Length > 0)
                    {
                        if (functon == "All function")
                        {
                            ArrayList listofitems = new ArrayList();
                            foreach (ListItem funct in ddlFilterFunction.Items)
                            {
                                if (funct.Text != "All function" && funct.Text != "<----Please select one---->")
                                {
                                    listofitems.Add(funct);
                                }
                            }
                            foreach (ListItem functitem in listofitems)
                            {
                                if (counter == listofitems.Count)
                                {
                                    allfunction += functitem.Text;
                                }
                                else
                                {
                                    allfunction += functitem.Text + ",";
                                }
                                counter++;
                            }
                            populateChartFunctionViaSection(section, allfunction, questionID);
                        }
                        else
                        {
                            populateChartFunctionViaSection(section, functon, questionID);
                        }
                    }
                }
                ClearText();
            }
        }

        protected void populateChartSectionViaFunctionALL()
        {
            try
            {
                MultiView1.Visible = true;
                Chart1.Series.Clear();
                Chart1.ChartAreas[0].AxisX.MajorGrid.LineColor = System.Drawing.Color.LightGray;
                Chart1.ChartAreas[0].AxisY.MajorGrid.LineColor = System.Drawing.Color.LightGray;
                MultiView1.Visible = true;
                DataTable table = new DataTable();
                table.Columns.Add("AverageGrading");
                table.Columns.Add("Date");
                ArrayList listofdates = dbmanager.GetListofDatesViaAll();

                // decare and get items
                double result = 0.0;
                //double staffAverage = 0.0;
                //double staffAverageResult = 0.0;

                //ArrayList listofstaff = dbmanager.GetAllStaffDetails();
                //ArrayList listofquestion = dbmanager.GetAllQuestion();

                foreach (DateTime date in listofdates)
                {
                    //foreach (staffinfo staff in listofstaff)
                    //{
                        //foreach (Question qn in listofquestion)
                        //{
                            //staffAverage += dbmanager.GetAvgRating(staff.Uid, date, qn.QuestionID);
                            //staffAverage += dbmanager.GetAverageStaffPeriod(staff.Uid, date);
                        //}
                        //int countquestion = dbmanager.GetTotalCountQuestionInPeriod(staff.Uid, date);
                        //staffAverageResult = Math.Round((staffAverage / countquestion), 1);
                    //}
                    //result = Math.Round((staffAverageResult / listofstaff.Count), 1);
                    result = dbmanager.GetAverageAllStaffPeriod(date);
                    string monthname = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(date.Month);
                    table.Rows.Add(result, monthname.Substring(0, 3) + "/" + date.Year.ToString().Substring(2, 2));

                    result = 0.0;
                    //staffAverage = 0.0;
                    //staffAverageResult = 0.0;
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
                    Chart1.ChartAreas[0].AxisY.Title = "Average grade";
                    Chart1.DataBindCrossTable(datareader, "Date", "", "AverageGrading", "");
                    Chart1.Legends.Add("Legend");
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
            catch (Exception e)
            {
                MultiView1.Visible = false;
                MessageBoxShowWithoutredirect(e.Message);
            }
        }

        protected void populateChartFunctionViaSection(string section, string functionlist, int questionID)
        {
            try
            {
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
                string[] functionsplit = functionlist.Split(',');

                ArrayList listofquestion = dbmanager.GetAllQuestion();
                ArrayList listofstaff = dbmanager.GetAllStaffDetails();

                foreach (DateTime date in listofdates)
                {
                    foreach (string function in functionsplit)
                    {
                        double result = 0.0;
                        double staffAverage = 0.0;
                        double staffAverageResult = 0.0;
                        int count = 0;

                        foreach (staffinfo staff in listofstaff)
                        {
                            if (staff.Section.Contains(section) && staff.Function.Equals(function))
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
                                    staffAverage += dbmanager.GetAvgRating(staff.Uid, date, questionID); // for per question
                                    int countquestion = dbmanager.GetTotalCountQuestionInPeriod(staff.Uid, date);
                                    staffAverageResult = staffAverage;
                                    count++;
                                }
                            }
                        }
                        try
                        {
                            result = Math.Round((staffAverageResult / count), 1);
                        }
                        catch
                        {
                            result = 0.0;
                        }

                        string monthname = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(date.Month);
                        table.Rows.Add(function, result, monthname.Substring(0, 3) + "/" + date.Year.ToString().Substring(2, 2));

                        result = 0.0;
                        staffAverage = 0.0;
                        staffAverageResult = 0.0;
                    }
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
                    foreach (Series cs in Chart1.Series) 
                    { 
                        cs.ChartType = SeriesChartType.Line;
                        cs.IsValueShownAsLabel = true;
                    }
                    DataTableReader datareader = table.CreateDataReader();
                    Chart1.DataBindCrossTable(datareader, "FunctionGroup", "Date", "AverageGrading", "");
                    Chart1.Legends.Add("Legend");
                    Chart1.ChartAreas[0].AxisX.Title = "Period of evaluation";
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

        protected void populateChartSectionViaFunction(string function, string sectionlist, int questionID)
        {
            try
            {
                MultiView1.Visible = true;
                Chart1.Series.Clear();
                Chart1.ChartAreas[0].AxisX.MajorGrid.LineColor = System.Drawing.Color.LightGray;
                Chart1.ChartAreas[0].AxisY.MajorGrid.LineColor = System.Drawing.Color.LightGray;
                MultiView1.Visible = true;
                DataTable table = new DataTable();
                table.Columns.Add("SectionGroup");
                table.Columns.Add("AverageGrading");
                table.Columns.Add("Date");
                ArrayList listofdates = dbmanager.GetListofDatesViaFunction(function);
                string[] sectionsplit = sectionlist.Split(',');

                ArrayList listofquestion = dbmanager.GetAllQuestion();
                ArrayList listofstaff = dbmanager.GetAllStaffDetails();

                foreach (DateTime date in listofdates)
                {
                    foreach (string section in sectionsplit)
                    {
                        double result = 0.0;
                        double staffAverage = 0.0;
                        double staffAverageResult = 0.0;
                        int count = 0;

                        foreach (staffinfo staff in listofstaff)
                        {
                            if (staff.Section.Contains(section) && staff.Function.Equals(function))
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
                                    staffAverage += dbmanager.GetAvgRating(staff.Uid, date, questionID); // for per question
                                    int countquestion = dbmanager.GetTotalCountQuestionInPeriod(staff.Uid, date);
                                    staffAverageResult = staffAverage;
                                    count++;
                                }
                            }
                        }

                        try
                        {
                            result = Math.Round((staffAverageResult / count), 1);
                        }
                        catch
                        {
                            result = 0.0;
                        }

                        string monthname = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(date.Month);
                        table.Rows.Add(section, result, monthname.Substring(0, 3) + "/" + date.Year.ToString().Substring(2, 2));

                        result = 0.0;
                        staffAverage = 0.0;
                        staffAverageResult = 0.0;
                    }
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
                    Chart1.ChartAreas[0].AxisX.Title = "Period of evaluation";
                    Chart1.ChartAreas[0].AxisY.Title = "Average grade";
                    Chart1.DataBindCrossTable(datareader, "SectionGroup", "Date", "AverageGrading", "");
                    Chart1.Legends.Add("Legend");
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
            catch (Exception e)
            {
                MultiView1.Visible = false;
                MessageBoxShowWithoutredirect(e.Message);
            }
        }

        protected void SearchSwapBtn_Click(object sender, ImageClickEventArgs e)
        {
            MultiView1.Visible = false;
            string role = Session["Role"].ToString();
            if (Session["FunctionTrue"] != null)
            {
                if (role == "Admin")
                {
                    BindDropDownListAdmin();
                }
                else
                {
                    BindDropDownListOfficer();
                }
                SelectTbxFunction.Text = "";
                SearchPanelFunctionViaSection.Visible = false;
                SearchPanelSectionViaFunction.Visible = true;
                Session["FunctionTrue"] = null;
            }
            else
            {
                if (role == "Admin")
                {
                    BindDropDownListAdmin();
                }
                else
                {
                    BindDropDownListOfficer();
                }
                SelectTbxSection.Text = "";
                lblSelectFunction.Text = "select function group (chart title): ";
                lblFilterBySection.Text = "select section (chart components): ";
                lblFilterByQuestion.Text = lbl;
                lblSelectedSection.Text = "selected section(s): ";
                SearchPanelFunctionViaSection.Visible = true;
                SearchPanelSectionViaFunction.Visible = false;
                Session["FunctionTrue"] = true;
            }
        }

        protected void AddBtn_Click(object sender, EventArgs e)
        {
            MultiView1.Visible = false;
            if (SelectTbxFunction.Text != "All function")
            {
                if (ddlFilterFunction.SelectedIndex != 0 && Session["AdditionalSelectFunction"] == null)
                {
                    if (SelectTbxFunction.Text == "")
                    {
                        SelectTbxFunction.Text += ddlFilterFunction.Text;
                    }
                    else
                    {
                        SelectTbxFunction.Text += "," + ddlFilterFunction.Text;
                    }
                    //SelectTbxFunction.Text += ddlFilterFunction.Text;
                    Session["AdditionalSelectFunction"] = true;
                }
                else if (ddlFilterFunction.SelectedIndex != 0 && Session["AdditionalSelectFunction"] != null && ddlFilterFunction.Text != "All function")
                {
                    if (!SelectTbxFunction.Text.Contains("All function"))
                    {
                        if (!SelectTbxFunction.Text.Contains(ddlFilterFunction.Text))
                        {
                            SelectTbxFunction.Text += "," + ddlFilterFunction.Text;
                        }
                    }
                }
                else if (ddlFilterFunction.Text == "All function")
                {
                    SelectTbxFunction.Text = ddlFilterFunction.Text;
                }
            }
            else
            {
                MessageBoxShowWithoutredirect("Please clear search content.");
            }
        }

        protected void AddSectionBtnSection_Click(object sender, EventArgs e)
        {
            MultiView1.Visible = false;
            if (SelectTbxSection.Text != "All section")
            {
                if (ddlFilterBySection.SelectedIndex != 0 && Session["AdditionalSelectSection"] == null)
                {
                    SelectTbxSection.Text += ddlFilterBySection.Text;
                    Session["AdditionalSelectSection"] = true;
                }
                else if (ddlFilterBySection.SelectedIndex != 0 && Session["AdditionalSelectSection"] != null && ddlFilterBySection.Text != "All section")
                {
                    if (!SelectTbxSection.Text.Equals("All section"))
                    {
                        if (!SelectTbxSection.Text.Contains(ddlFilterBySection.Text))
                        {
                            SelectTbxSection.Text += "," + ddlFilterBySection.Text;
                        }
                    }
                }
                else if (ddlFilterBySection.Text == "All section")
                {
                    SelectTbxSection.Text = ddlFilterBySection.Text;
                }
            }
            else
            {
                MessageBoxShowWithoutredirect("Please clear search content.");

            }
        }

        protected void ClearBtn_Click(object sender, EventArgs e)
        {
            MultiView1.Visible = false;
            Session["AdditionalSelectFunction"] = null;
            Session["AdditionalSelectSection"] = null;
            SelectTbxFunction.Text = "";
            ddlFilterFunction.Items.Clear();
            ddlFilterFunction.Items.Add("<--------Autoupdate-------->");
            ddlSelectSection.SelectedIndex = 0;
            SelectTbxSection.Text = "";
            ddlFilterBySection.Items.Clear();
            ddlFilterBySection.Items.Add("<--------Autoupdate-------->");
            ddlSelectFunction.SelectedIndex = 0;
        }

        protected void ClearText()
        {
            //MultiView1.Visible = false;
            Session["AdditionalSelectFunction"] = null;
            Session["AdditionalSelectSection"] = null;
            SelectTbxFunction.Text = "";
            ddlFilterFunction.Items.Clear();
            ddlFilterFunction.Items.Add("<--------Autoupdate-------->");
            ddlSelectSection.SelectedIndex = 0;
            SelectTbxSection.Text = "";
            ddlFilterBySection.Items.Clear();
            ddlFilterBySection.Items.Add("<--------Autoupdate-------->");
            ddlSelectFunction.SelectedIndex = 0;
        }

        protected void ddlSelectSection_SelectedIndexChanged(object sender, EventArgs e)
        {
            MultiView1.Visible = false;
            if (ddlSelectSection.SelectedIndex != 0)
            {
                if (ddlSelectSection.Text == "<--------ASC School-------->")
                {
                    ddlFilterFunction.Items.Clear();
                    ddlFilterFunction.Items.Add("<--------Autoupdate-------->");
                    ddlFilterFunction.SelectedIndex = 0;
                    SelectTbxFunction.Text = "";
                    populateChartSectionViaFunctionALL();
                }
                else
                {
                    ArrayList listoffunctionviasection = RemoveDups(dbmanager.GetDistinctFunctionViaSection(ddlSelectSection.Text));
                    if (listoffunctionviasection.Count > 0)
                    {
                        MultiView1.Visible = false;
                        listoffunctionviasection.Insert(0, "<----Please select one---->");
                        if (listoffunctionviasection.Count > 2)
                        {
                            listoffunctionviasection.Insert(1, "All function");
                        }
                        ddlFilterFunction.DataSource = listoffunctionviasection;
                        ddlFilterFunction.DataBind();
                        SelectTbxFunction.Text = "";
                        Session["AdditionalSelectFunction"] = null;
                    }
                }
            }
        }

        protected void ddlSelectFunction_SelectedIndexChanged(object sender, EventArgs e)
        {
            MultiView1.Visible = false;
            if (ddlSelectFunction.SelectedIndex != 0)
            {
                if (ddlSelectFunction.Text == "<--------ASC School-------->")
                {
                    ddlFilterBySection.Items.Clear();
                    ddlFilterBySection.Items.Add("<--------Autoupdate-------->");
                    ddlFilterBySection.SelectedIndex = 0;
                    SelectTbxSection.Text = "";
                    populateChartSectionViaFunctionALL();
                }
                else
                {
                    ArrayList listofsectionviafunction = RemoveDups(dbmanager.GetDistinctSectionViaFunction(ddlSelectFunction.Text));
                    if (listofsectionviafunction.Count > 0)
                    {
                        MultiView1.Visible = false;
                        listofsectionviafunction.Insert(0, "<----Please select one---->");
                        if (listofsectionviafunction.Count > 2)
                        {
                            listofsectionviafunction.Insert(1, "All section");
                        }
                        ddlFilterBySection.DataSource = listofsectionviafunction;
                        ddlFilterBySection.DataBind();
                        SelectTbxSection.Text = "";
                        Session["AdditionalSelectSection"] = null;
                    }
                }
            }
        }

        protected void SearchBtnSection_Click(object sender, EventArgs e)
        {
            if (SelectTbxSection.Text == "")
            {
                MessageBox("Please click add button to insert section(s)!");
            }
            else
            {
                MultiView1.Visible = false;

                if (ddlSelectFunction.Text == "<--------ASC School-------->")
                {
                    ddlFilterBySection.Items.Clear();
                    ddlFilterBySection.Items.Add("<--------Autoupdate-------->");
                    ddlFilterBySection.SelectedIndex = 0;
                    populateChartSectionViaFunctionALL();
                }
                else
                {
                    // for question ddl ()
                    int questionID = ddlFilterQuestion.SelectedIndex;

                    string function = ddlSelectFunction.Text.Trim();
                    string section = SelectTbxSection.Text.Trim();
                    string allsection = "";
                    int counter = 1;

                    if (section.Length > 0)
                    {
                        if (section == "All section")
                        {
                            ArrayList listofitems = new ArrayList();
                            foreach (ListItem sect in ddlFilterBySection.Items)
                            {
                                if (sect.Text != "All section" && sect.Text != "<----Please select one---->")
                                {
                                    listofitems.Add(sect);
                                }
                            }
                            foreach (ListItem sectitem in listofitems)
                            {
                                if (counter == listofitems.Count)
                                {
                                    allsection += sectitem.Text;
                                }
                                else
                                {
                                    allsection += sectitem.Text + ",";
                                }
                                counter++;
                            }
                            populateChartSectionViaFunction(function, allsection, questionID);
                        }
                        else
                        {
                            populateChartSectionViaFunction(function, section, questionID);
                        }
                    }
                }
                ClearText();
            }
        }

    }
}