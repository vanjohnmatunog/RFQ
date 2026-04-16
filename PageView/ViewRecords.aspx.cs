using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Data;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;
using TIPIS3WebAdmin.TIPEmployeeMaster;

public partial class PageView_ViewRecords : System.Web.UI.Page
{
    i_DataLayer dl = new cl_DataLayer();
    private static Boolean isAdmin = false;
    private static Boolean isMPDApprover = false;
    private static Boolean isBuyer = false;
    private static Boolean isRequestor = false;
    private static DataTable dtRecords = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            //hdnUserType.Value = (!string.IsNullOrEmpty(Request.QueryString["user"])) ? Request.QueryString["user"] : "0";
            //if (hdnUserType.Value == "0") Unauthorized();

            //Details.Portal.EMP emp = new Details.Portal.EMP(cl_DBConn.MSSQLEmp(), cl_Identity.Get_UserID());
            //if (emp.HasRows)
            //{
            //    isAdmin = (dl.Check_UserAccess(cl_ProvideFactory.getSqlFactory(), cl_DBConn.MSSQLTrans(), "is admin", "Administrator", emp.EmpNo));
            //    isMPDApprover = (dl.Check_UserAccess(cl_ProvideFactory.getSqlFactory(), cl_DBConn.MSSQLTrans(), "is MPD", "MPD Approver", emp.EmpNo));
            //    isBuyer = (dl.Check_UserAccess(cl_ProvideFactory.getSqlFactory(), cl_DBConn.MSSQLTrans(), "is Buyer", "Buyer In-charge", emp.EmpNo));
            //    isRequestor = (dl.Check_UserAccess(cl_ProvideFactory.getSqlFactory(), cl_DBConn.MSSQLTrans(), "is requestor", "Requestor", emp.EmpNo));

            //    if (!isAdmin) ScriptManager.RegisterStartupScript(this, GetType(), "Javascript", "MenuHide('five-ddheader');", true);
            //    hdnUser_EmpNo.Value = emp.EmpNo;
            //    hdnUser_EmpName.Value = emp.EmpName;

            //    if (!isRequestor && !isAdmin && !isMPDApprover && !isBuyer)
            //    {
            //        Response.Redirect("~/PortalAdmin/Unauthorized.aspx");
            //    }
            //}
            //else
            //{
            //    Unauthorized();
            //}
            //emp.Dispose();

            ////if (hdnUserType.Value.ToLower() == "requestor")
            ////    cl_Common.fill_DropDownList(ddlStatus, dl.getLOVMasterlist(cl_ProvideFactory.getSqlFactory(), cl_DBConn.MSSQLTrans(), "get status", "", ""), "ItemCode", "Description", "-- Please Select --");
            ////else if (hdnUserType.Value.ToLower() == "approver")
            ////    cl_Common.fill_DropDownList(ddlStatus, dl.getLOVMasterlist(cl_ProvideFactory.getSqlFactory(), cl_DBConn.MSSQLTrans(), "get status", "", ""), "ItemCode", "Description", "-- Please Select --");
            ////else if (hdnUserType.Value.ToLower() == "administrator")
            ////    cl_Common.fill_DropDownList(ddlStatus, dl.getLOVMasterlist(cl_ProvideFactory.getSqlFactory(), cl_DBConn.MSSQLTrans(), "per category", "get status", "", ""), "ItemCode", "Description", "--Please select--");
            ////else if (hdnUserType.Value.ToLower() == "all")
            ////    cl_Common.fill_DropDownList(ddlStatus, dl.getLOVMasterlist(cl_ProvideFactory.getSqlFactory(), cl_DBConn.MSSQLTrans(), "get status", "", ""), "ItemCode", "Description", "-- Please Select --");

            //cl_Common.fill_DropDownList(ddlStatus, dl.getLOVMasterlist(cl_ProvideFactory.getSqlFactory(), cl_DBConn.MSSQLTrans(), "get status", "", ""), "ItemCode", "Description", "-- Please Select --");

            //string status = !string.IsNullOrEmpty(Request.QueryString["status"]) ? Request.QueryString["status"] : "0";
            //if (ddlStatus.Items.Contains(new ListItem(status, status))) ddlStatus.SelectedValue = status;
            //else ddlStatus.SelectedIndex = 0;

            //hdnKeyword.Value = (!string.IsNullOrEmpty(Request.QueryString["Keyword"])) ? Request.QueryString["Keyword"] : "";
            //if (hdnKeyword.Value.Trim().Length > 0) txtKeyword.Text = hdnKeyword.Value;
            //else txtKeyword.Text = "";

            //Session["KEYWORD"] = hdnKeyword.Value;
            //Session["FROMDATE"] = txtFromDate.Text;
            //Session["TODATE"] = txtToDate.Text;
            //Load_Records();

        }
        else
        {
            if (gvRecords.Rows.Count > 0) gvRecords.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
    }

    private DataTable GetData()
    {
        TIPIS3WebAdmin.TIPEmployeeMaster.Data.Portal.EMP allEmp = new TIPIS3WebAdmin.TIPEmployeeMaster.Data.Portal.EMP(cl_DBConn.MSSQLEmp());
        DataTable dt = new DataTable();
        allEmp.Fill(dt);
        return dt;
    }
    
    private void Unauthorized()
    {
        Response.Redirect("~/PortalAdmin/Unauthorized.aspx");
    }
    
    private void Load_Records()
    {
        hdnKeyword.Value = !string.IsNullOrEmpty(txtKeyword.Text.Trim()) ? txtKeyword.Text.Trim() : "";
        txtFromDate.Text = !string.IsNullOrEmpty(txtFromDate.Text.Trim()) ? txtFromDate.Text.Trim() : "";
        txtToDate.Text = !string.IsNullOrEmpty(txtToDate.Text.Trim()) ? txtToDate.Text.Trim() : "";

        string transType = "";

        if (hdnUserType.Value.ToLower() == "requestor") transType = "requestors view";
        else if (hdnUserType.Value.ToLower() == "approver") transType = "approvers view";
        else if (hdnUserType.Value.ToLower() == "administrator") transType = "administrator view";
        else if (hdnUserType.Value.ToLower() == "all") transType = "all";

        //DataTable dtRecords = new DataTable();
        string selectedStatus = "";
        if (ddlStatus.SelectedValue != "0") selectedStatus = ddlStatus.SelectedValue;
        dtRecords = dl.get_RFQ_Transaction(cl_ProvideFactory.getSqlFactory(), cl_DBConn.MSSQLTrans(), transType, selectedStatus, hdnKeyword.Value.Trim(), txtFromDate.Text.Trim(), txtToDate.Text.Trim(), hdnUser_EmpNo.Value);
        gvRecords.PageSize = int.Parse(ddlPageSize.SelectedValue);
        gvRecords.DataSource = dtRecords;
        gvRecords.DataBind();
        if (gvRecords.Rows.Count > 0)
        {
            lblCount.Text = "Record Count: " + dtRecords.Rows.Count.ToString();
            gvRecords.HeaderRow.TableSection = TableRowSection.TableHeader;
            if (isAdmin || isMPDApprover || isBuyer)
            {
                lbtnExport.Visible = true;
            }
        }
        else
        {
            lblCount.Text = "Record Count: 0";
            lbtnExport.Visible = false;
        }
       
        upnlRecords.Update();
        dtRecords.Dispose();

        Session["KEYWORD"] = hdnKeyword.Value;
        Session["FROMDATE"] = txtFromDate.Text;
        Session["TODATE"] = txtToDate.Text;
        //Session["DRSTATUS"] = selectedStatus;
    }
    protected void OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataRowView drv = e.Row.DataItem as DataRowView;
            
            if (drv["Status"].ToString().Equals("Denied") || drv["Status"].ToString().Equals("Closed"))
            {
                e.Row.Cells[12].BackColor = System.Drawing.ColorTranslator.FromHtml("#cc0000");
            }
            else
            {
                e.Row.Cells[12].BackColor = System.Drawing.ColorTranslator.FromHtml("#e64a02");
            }
        }

    }

    protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
    {
        Load_Records();
    }

    protected void grdRecords_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvRecords.PageIndex = e.NewPageIndex;
        Load_Records();
    }

    protected void lbtnView_Command(object sender, CommandEventArgs e)
    {
        Response.Redirect("~/PageTransaction/Registration.aspx?RefID=" + e.CommandName.ToString());
    }
    
    protected void lbtnSearch_Click(object sender, EventArgs e)
    {
        Load_Records();
    }

    protected void ddlParameter_SelectedIndexChanged(object sender, EventArgs e)
    {
        Load_Records();
    }

    private void ExportToExcel(string ExportTemplate)
    {
        try
        {
            string strFilename = DateTime.Now.ToString("yyyy-MM-dd") + "-" + "Report_Per_Request.xlsx";
            string strExportFile = Server.MapPath("~/FileExport/") + strFilename;

            FileInfo fileTemplate = new FileInfo(ExportTemplate);
            FileInfo fileExport = new FileInfo(strExportFile);
            using (ExcelPackage xlPackage = new ExcelPackage(fileExport, fileTemplate))
            {
                ExcelWorksheet worksheet = xlPackage.Workbook.Worksheets[1];

                int iRow = 12;
                int iCol = 1;

                //DataTable dtRecords = new DataTable();
                //string selectedStatus = "";
                //if (ddlStatus.SelectedValue != "0") selectedStatus = ddlStatus.SelectedValue;
                //dtRecords = dl.get_RFQ_Transaction(cl_ProvideFactory.getSqlFactory(), cl_DBConn.MSSQLTrans(), transType.Text +"extract per request", selectedStatus, hdnKeyword.Value.Trim(), txtFromDate.Text.Trim(), txtToDate.Text.Trim(), hdnUser_EmpNo.Value);
                gvRecords.PageSize = int.Parse(ddlPageSize.SelectedValue);

                foreach (DataRow dr in dtRecords.Rows)
                {
                    iCol = 1;
                    #region ID
                    //worksheet.Cells[iRow, iCol].Value = iRow - 11;
                    //worksheet.Cells[iRow, iCol].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    //worksheet.Cells[iRow, iCol].Style.Font.Size = 8;
                    //worksheet.Cells[iRow, iCol].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                    //iCol++;
                    #endregion

                    #region Ref ID
                    //worksheet.Cells[iRow, iCol].Value = dr["RefID"].ToString();
                    //worksheet.Cells[iRow, iCol].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                    //worksheet.Cells[iRow, iCol].Style.Font.Size = 8;
                    //worksheet.Cells[iRow, iCol].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                    //iCol++;
                    #endregion

                    #region Control Number
                    worksheet.Cells[iRow, iCol].Value = dr["RFQControlNo"].ToString();
                    worksheet.Cells[iRow, iCol].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                    worksheet.Cells[iRow, iCol].Style.Font.Size = 8;
                    worksheet.Cells[iRow, iCol].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                    iCol++;
                    #endregion

                    #region No. of Req
                    worksheet.Cells[iRow, iCol].Value = dr["NoOfReq"].ToString();
                    worksheet.Cells[iRow, iCol].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                    worksheet.Cells[iRow, iCol].Style.Font.Size = 8;
                    worksheet.Cells[iRow, iCol].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                    iCol++;
                    #endregion

                    #region Requestor Name
                    worksheet.Cells[iRow, iCol].Value = dr["RequestorName"].ToString();
                    worksheet.Cells[iRow, iCol].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                    worksheet.Cells[iRow, iCol].Style.Font.Size = 8;
                    worksheet.Cells[iRow, iCol].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                    iCol++;
                    #endregion

                    #region Dept
                    worksheet.Cells[iRow, iCol].Value = dr["Dept"].ToString();
                    worksheet.Cells[iRow, iCol].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                    worksheet.Cells[iRow, iCol].Style.Font.Size = 8;
                    worksheet.Cells[iRow, iCol].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                    iCol++;
                    #endregion

                    #region Category
                    worksheet.Cells[iRow, iCol].Value = dr["Category"].ToString();
                    worksheet.Cells[iRow, iCol].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                    worksheet.Cells[iRow, iCol].Style.Font.Size = 8;
                    worksheet.Cells[iRow, iCol].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                    iCol++;
                    #endregion

                    #region Location
                    worksheet.Cells[iRow, iCol].Value = dr["Location"].ToString();
                    worksheet.Cells[iRow, iCol].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                    worksheet.Cells[iRow, iCol].Style.Font.Size = 8;
                    worksheet.Cells[iRow, iCol].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                    iCol++;
                    #endregion

                    #region DateCreated
                    worksheet.Cells[iRow, iCol].Value = dr["DateCreated"].ToString();
                    worksheet.Cells[iRow, iCol].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                    worksheet.Cells[iRow, iCol].Style.Font.Size = 8;
                    worksheet.Cells[iRow, iCol].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                    iCol++;
                    #endregion

                    #region DateSubmitted
                    worksheet.Cells[iRow, iCol].Value = dr["DateSubmitted"].ToString();
                    worksheet.Cells[iRow, iCol].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                    worksheet.Cells[iRow, iCol].Style.Font.Size = 8;
                    worksheet.Cells[iRow, iCol].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                    iCol++;
                    #endregion

                    #region DateForwardedToMPD
                    worksheet.Cells[iRow, iCol].Value = dr["DateForwardedToMPD"].ToString();
                    worksheet.Cells[iRow, iCol].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                    worksheet.Cells[iRow, iCol].Style.Font.Size = 8;
                    worksheet.Cells[iRow, iCol].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                    iCol++;
                    #endregion

                    #region DateApprovedByMPD
                    worksheet.Cells[iRow, iCol].Value = dr["DateApprovedByMPD"].ToString();
                    worksheet.Cells[iRow, iCol].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                    worksheet.Cells[iRow, iCol].Style.Font.Size = 8;
                    worksheet.Cells[iRow, iCol].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                    iCol++;
                    #endregion

                    #region Status
                    worksheet.Cells[iRow, iCol].Value = dr["Status"].ToString();
                    worksheet.Cells[iRow, iCol].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                    worksheet.Cells[iRow, iCol].Style.Font.Size = 8;
                    worksheet.Cells[iRow, iCol].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                    iCol++;
                    #endregion

                    iRow++;

                }
                worksheet.Name = "RFQ Database System";

                // set category
                worksheet.Cells[6, 2].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                worksheet.Cells[6, 2].Value = ddlStatus.SelectedItem.Text;

                // set parameter
                worksheet.Cells[7, 2].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                worksheet.Cells[7, 2].Value = txtKeyword.Text.Trim();

                // set From Date
                worksheet.Cells[6, 5].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                worksheet.Cells[6, 5].Value = txtFromDate.Text.Trim();

                // set To Date
                worksheet.Cells[7, 5].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                worksheet.Cells[7, 5].Value = txtToDate.Text.Trim();

                //Set Number of records cell style
                worksheet.Cells[iRow, 1, iRow, iCol].Merge = true;
                worksheet.Cells[iRow, 1, iRow, iCol].Style.Font.Size = 8;
                worksheet.Cells[iRow, 1, iRow, iCol].Style.Font.Bold = true;
                worksheet.Cells[iRow, 1, iRow, iCol].Style.Border.Top.Style = ExcelBorderStyle.Dotted;
                worksheet.Cells[iRow, 1].Value = "Number of record(s) : " + (iRow - 12).ToString();

                //Set all columns to autofit
                //worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();

                xlPackage.Save();
            }

            fileExport = new FileInfo(strExportFile);
            Response.Clear();
            Response.ClearContent();
            Response.ClearHeaders();
            Response.AppendHeader("Content-Disposition", "attachments; filename=" + strFilename);
            Response.AppendHeader("Content-Length", fileExport.Length.ToString());
            Response.WriteFile(strExportFile);
            Response.Flush();
            File.Delete(strExportFile);
            Response.End();
        }
        catch (IOException ex) { string strError = ex.ToString(); }
    }

    protected void lbtnExport_Click(object sender, EventArgs e)
    {
        //lbtnExport.Enabled = false;
        upnlRecords.Update();
        if (gvRecords.Rows.Count == 0)
        {
            AlertMessage.Show("Information", "Information:", "No records found. Please search first.");
            hdnAction.Value = "alert";
        }
        else
        {
            ExportToExcel(Server.MapPath("~/FileExport/Template/Report Template_perRequest.xlsx"));
        }
        //lbtnExport.Enabled = true;
        upnlRecords.Update();
    }

    protected void lbtnSearchByDate_Click(object sender, EventArgs e)
    {
        if (isComplete())
        {
            DateTime fromDate = DateTime.Parse(txtFromDate.Text);
            DateTime toDate = DateTime.Parse(txtToDate.Text);
            if (fromDate > toDate)
            {
                AlertMessage.Show("warning", "Information:", "Please select proper date range!");
            }
            else 
            {
               Load_Records();
            }
        }
    }
    private Boolean isComplete()
    {
        bool result = true;
        if (txtFromDate.Text.Trim().Length == 0)
        {
            AlertMessage.Show("warning", "Information:", "Please select date from!");
            hdnAction.Value = "alert";
            result = false;
        }
        else if (txtToDate.Text.Trim().Length == 0)
        {
            AlertMessage.Show("warning", "Information:", "Please select date to!");
            hdnAction.Value = "alert";
            result = false;
        }
        return result;
    }
}