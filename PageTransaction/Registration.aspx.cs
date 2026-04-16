using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;
using TIPIS3WebAdmin.TIPEmployeeMaster;
using System.Configuration;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Net.Mail;

public partial class PageTransaction_Registration : System.Web.UI.Page
{
    i_Employee emp = new cl_Employee();
    i_DataLayer dl = new cl_DataLayer();
    private static DataTable dtRFQ_Request = new DataTable();
    private static DataTable dtRFQ_Request_Upload = new DataTable();
    private static DataTable dtCurrency_List = new DataTable();
    private static DataTable dtBuyerIncharge_List = new DataTable();
    cl_RFQ_TransactionObject trans = new cl_RFQ_TransactionObject();
    cl_BP_PJMemberObject pmo = new cl_BP_PJMemberObject();

    cl_DataTransferObject dto = new cl_DataTransferObject();

    private static Boolean isAdmin = false;
    private static Boolean isRequestor = false;
    private static Boolean isMPDApprover = false;
    private static Boolean isBuyer = false;
    private const String DT_RFQ_REQUEST = "DT_RFQ_REQUEST";
    private const String DT_RFQ_REQUEST_FOR_UPLOAD = "DT_RFQ_REQUEST_FOR_UPLOAD";
    private const String STR_ALLOWED_CHARS = "STR_ALLOWED_CHARS";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            //Page.Form.Attributes.Add("enctype", "multipart/form-data");
            //Details.Portal.EMP emp = new Details.Portal.EMP(cl_DBConn.MSSQLEmp(), cl_Identity.Get_UserID());
            //hdnRefID.Value = (!string.IsNullOrEmpty(Request.QueryString["RefID"])) ? Request.QueryString["RefID"] : "0";
            //if (emp.HasRows)
            //{
            //    hdnUserEmpNo.Value = emp.EmpNo;
            //    hdnUserEmpName.Value = emp.EmpName;
            //    hdnUserDepCode.Value = emp.DepCode;
            //    hdnUserDepartment.Value = emp.Department;
            //    hdnUserDept.Value = emp.Dept;
            //    hdnUserSecCode.Value = emp.SecCode;
            //    txtRequestor_EmpName.Text = emp.EmpName;
            //    txtRequestor_Dept.Text = emp.Dept;

            //    isAdmin = (dl.Check_UserAccess(cl_ProvideFactory.getSqlFactory(), cl_DBConn.MSSQLTrans(), "is admin", "Administrator", emp.EmpNo));
            //    isRequestor = (dl.Check_UserAccess(cl_ProvideFactory.getSqlFactory(), cl_DBConn.MSSQLTrans(), "is requestor", "Requestor", emp.EmpNo));
            //    isMPDApprover = (dl.Check_UserAccess(cl_ProvideFactory.getSqlFactory(), cl_DBConn.MSSQLTrans(), "is MPD", "MPD Approver", emp.EmpNo));
            //    isBuyer = (dl.Check_UserAccess(cl_ProvideFactory.getSqlFactory(), cl_DBConn.MSSQLTrans(), "is Buyer", "Buyer In-charge", emp.EmpNo));

            //    if (!isAdmin)
            //    {
            //        ScriptManager.RegisterStartupScript(this, GetType(), "Javascript", "MenuHide('five-ddheader');", true);
            //    }
            //    if (!isRequestor && !isAdmin && !isMPDApprover && !isBuyer)
            //    {
            //        if (hdnRefID.Value == "0")
            //        {
            //            Response.Redirect("~/PortalAdmin/Unauthorized.aspx");
            //        }
            //    }

            //        Initialize_dtRFQ_Request();
            //    ViewState[STR_ALLOWED_CHARS] = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890-/,:.() ";
            //}
            //else
            //{
            //    Response.Redirect("~/PortalAdmin/Unauthorized.aspx");
            //}
            //emp.Dispose();
            //Load_ListofValues();
            //Load_NewRFQ_Approver("initial", "", gvApproval, gvApproval);

            //hdnRefID.Value = (!string.IsNullOrEmpty(Request.QueryString["RefID"])) ? Request.QueryString["RefID"] : "0";
            //if (hdnRefID.Value == "0")
            //{
            //    if (hdnRefID.Value == "0") hdnRefID.Value = TIPIS3WebAdmin.TIPDataStandard.Generation.GetReferenceNumber();

            //    divLogs.Visible = false;
            //}
            //else
            //{
            //    Load_Transaction("per refid", hdnRefID.Value);
            //    Load_HistoryLogs(hdnRefID.Value);

            //    Update_GVHeader();

            //}

        }
        else
        {
            if (gvRFQ_List.Rows.Count > 0) gvRFQ_List.HeaderRow.TableSection = TableRowSection.TableHeader;
            if (gvAttachment.Rows.Count > 0) gvAttachment.HeaderRow.TableSection = TableRowSection.TableHeader;
            if (gvApproval.Rows.Count > 0) gvApproval.HeaderRow.TableSection = TableRowSection.TableHeader;
            if (gvApproval1.Rows.Count > 0) gvApproval1.HeaderRow.TableSection = TableRowSection.TableHeader;
            if (gvHistoryLogs.Rows.Count > 0) gvHistoryLogs.HeaderRow.TableSection = TableRowSection.TableHeader;
            if (gvDetailsViewOnly.Rows.Count > 0) gvDetailsViewOnly.HeaderRow.TableSection = TableRowSection.TableHeader;

        }


    }
    #region Fill all dropdownlist thru maintenance
    private void Load_ListofValues()
    {
        // Location
        cl_Common.fill_DropDownList(ddlLocation, dl.getLOVMasterlist(cl_ProvideFactory.getSqlFactory(), cl_DBConn.MSSQLTrans(), "get location", "", ""), "ItemCode", "Description", "-- Please Select --");
        // Category
        cl_Common.fill_DropDownList(ddlCategory, dl.getLOVMasterlist(cl_ProvideFactory.getSqlFactory(), cl_DBConn.MSSQLTrans(), "get item category", "", ""), "ItemCode", "Description", "-- Please Select --");

        dtCurrency_List = dl.getLOVMasterlist(cl_ProvideFactory.getSqlFactory(), cl_DBConn.MSSQLTrans(), "per category", "Currency", "");
        dtBuyerIncharge_List = dl.getLOVMasterlist(cl_ProvideFactory.getSqlFactory(), cl_DBConn.MSSQLTrans(), "per category", "Buyer In-charge", "");
    }
    #endregion

    protected void Initialize_dtRFQ_Request()
    {
        dtRFQ_Request = new DataTable();
        if (dtRFQ_Request.Columns.Count == 0)
        {
            dtRFQ_Request.Columns.Add("ControlNo");
            dtRFQ_Request.Columns.Add("ItemDescription");
            dtRFQ_Request.Columns.Add("TempItemCode");
            dtRFQ_Request.Columns.Add("Quantity");
            dtRFQ_Request.Columns.Add("UOM");
            dtRFQ_Request.Columns.Add("Budget");
            dtRFQ_Request.Columns.Add("Currency");
            dtRFQ_Request.Columns.Add("Purpose");
            dtRFQ_Request.Columns.Add("Remarks");
            dtRFQ_Request.Columns.Add("BuyerInCharge");
            dtRFQ_Request.Columns.Add("BuyerInChargeEmpNo");
            dtRFQ_Request.Columns.Add("Status");
            dtRFQ_Request.Columns.Add("Valid");
            dtRFQ_Request.AcceptChanges();

            ViewState[DT_RFQ_REQUEST] = dtRFQ_Request;
        }
    }

    protected override void OnInit(EventArgs e)
    {
        //find the button control within the user control
        LinkButton btnAccept = (LinkButton)AlertMessage.FindControl("lbtnAccept");
        LinkButton btnDecline = (LinkButton)AlertMessage.FindControl("lbtnDecline");
        btnAccept.Click += new EventHandler(btnAccept_Click);
        btnDecline.Click += new EventHandler(btnDecline_Click);
        base.OnInit(e);
    }
    void btnAccept_Click(object sender, EventArgs e)
    {
        TextBox txtRemarks_Save = (TextBox)AlertMessage.FindControl("txtRemarks");
        switch (hdnAction.Value.ToLower())
        {
            case "refresh":
                Response.Redirect("Registration.aspx?RefID=" + hdnRefID.Value);
                hdnAction.Value = string.Empty;
                break;
            case "redirect":
                Response.Redirect("Registration.aspx");
                break;
            case "alert":
                hdnAction.Value = string.Empty;
                break;
            case "delete attachment":
                Delete_Attachment(txtRemarks_Save.Text.Trim().Replace("'", "`"));
                break;
            case "save":
                hdnAction.Value = "";
                if (hdnButton.Value == "saveButton") 
                {
                    Save_Request("SAVE REGISTRATION", txtRemarks_Save.Text);
                }
                else if (hdnButton.Value == "updateButton")
                {
                    Save_Request("UPDATE DETAILS", txtRemarks_Save.Text);
                }
                else if (hdnButton.Value == "assignButton")
                {
                    Save_Request("ASSIGN BUYERS", txtRemarks_Save.Text);
                }
                hdnButton.Value = "";
                break;
            case "edit app":
                hdnAction.Value = "";
                Edit_Approver();
                break;
            case "update rfq approver":
                Update_RFQ_Approver(txtRemarks_Save.Text);
                hdnAction.Value = String.Empty;
                break;
            case "update controls":
                UnlockRFQDetails();
                ButtonsForUpdate();
                hdnAction.Value = String.Empty;
                break;
            case "assign buyers controls":
                gvRFQ_List.Visible = true;
                gvDetailsViewOnly.Visible = false;
                int lastRow = gvRFQ_List.Rows.Count;
                gvRFQ_List.Rows[lastRow - 1].Visible = false;
                txtRequestor_LocalNo.Enabled = false;
                UnlockBuyerIncharge();
                ButtonsForAssignBuyer();
                hdnAction.Value = String.Empty;
                break;
            case "submit":
                LockRFQDetails();
                Save_Request("SUBMIT", txtRemarks_Save.Text);
                hdnAction.Value = "refresh";
                break;
            case "approve":
                LockRFQDetails();
                Approve_Deny("APPROVE", txtRemarks_Save.Text);
                hdnAction.Value = "refresh";
                break;
            case "resubmit":
                LockRFQDetails();
                ButtonsForResubmit();
                Resubmit_New_RFQ("RESUBMIT", txtRemarks_Save.Text);
                hdnAction.Value = "refresh";
                break;
            case "re-assign buyer":
                Reassign();
                ReassignNewBuyer();
                Edit_Buyer();
                hdnAction.Value = "refresh";
                break;
            case "update rfq buyer":
                Update_RFQ_Buyer(txtRemarks_Save.Text);
                hdnAction.Value = String.Empty;
                break;
            case "save re-assign buyer":
                LockRFQDetails();
                hdnAction.Value = "refresh";
                break;
            case "deny":
                LockRFQDetails();
                if (txtRemarks_Save.Text.Trim().Length == 0)
                {
                    AlertMessage.Show("warning", "Warning:", "Please provide remarks.");
                    hdnAction.Value = "alert"
;
                }
                else
                {
                    
                    Approve_Deny("DENY", txtRemarks_Save.Text);
                    //Response.Redirect("Registration.aspx?RefID=" + hdnRefID.Value);
                    //hdnAction.Value = String.Empty;
                    hdnAction.Value = "refresh";
                }
                
                break;
        }
        switch (hdnGV_Action.Value.ToLower())
        {
            case "delete item code":
                hdnGV_Action.Value = "";
                dtRFQ_Request = (DataTable)ViewState[DT_RFQ_REQUEST];
                foreach (DataRow row in dtRFQ_Request.Rows)
                {
                    if (row["TempItemCode"].ToString() == hdnDltRow.Value)
                    {
                        row.Delete();
                        break;
                    }
                }

                dtRFQ_Request.AcceptChanges();
                ViewState[DT_RFQ_REQUEST] = dtRFQ_Request;
                gvRFQ_List.DataSource = dtRFQ_Request;
                gvRFQ_List.DataBind();

                //NUMBER OF REQUEST
                string counts = gvRFQ_List.Rows.Count.ToString();
                int numOfReq = Int32.Parse(counts);
                int dif = numOfReq - 1;
                if (dif <= 0 || txtNumOfReq.Text == "0")
                {
                    txtNumOfReq.Text = "--Auto Generated--";
                    ddlCategory.SelectedIndex = 0;
                    lblNote.Visible = false;
                    gvRFQ_List.Visible = false;
                    divFileUpload.Visible = false;
                }
                else
                {
                    txtNumOfReq.Text = dif.ToString();
                }
                break;
            case "alert":
                hdnGV_Action.Value = "";
                Update_GVHeader();
                break;
        }

    }

    void btnDecline_Click(object sender, EventArgs e)
    {
        hdnAction.Value = "";
        upnlActionButton.Update();
    }


    #region Methods
    private void Delete_Attachment(string Remarks)
    {
        try
        {
            dto.ID = hdnAttachmentID.Value;
            dto.RefID = hdnRefID.Value;
            dto.FileName_Orig = "";
            dto.FileName_New = "";
            dto.FilePath = hdnAttachmentPath.Value;
            dto.ActionRemarks = Remarks;
            dto.CreatedBy_EmpNo = hdnUserEmpNo.Value;
            dto.CreatedBy_EmpName = hdnUserEmpName.Value;
            dto.Attribute1 = "-";
            dto.Attribute2 = "-";

            string result = dl.Insert_RFQ_Attachment(cl_ProvideFactory.getSqlFactory(), cl_DBConn.MSSQLTrans(), dto, "delete");
            if (result.ToLower() == "request timeout")
            {
                AlertMessage.Show("Warning", "Information:", "Request Timeout: Please try again!");
                hdnAction.Value = "alert";
            }
            else
            {
                string filePath = hdnAttachmentPath.Value;
                File.Delete(filePath);
                AlertMessage.Show("Warning", "Information:", "Attachment has been deleted!");
                hdnAction.Value = "alert";
            }
            Load_Attachment();
        }
        catch (Exception ex)
        {
            //
        }
    }
    protected void Save_Request(string TransType, string Remarks)
    {
        //transaction details
        dtRFQ_Request = (DataTable)ViewState[DT_RFQ_REQUEST];
        dtRFQ_Request_Upload = (DataTable)ViewState[DT_RFQ_REQUEST_FOR_UPLOAD];
        trans.RefID = hdnRefID.Value;
        trans.Reqname = txtRequestor_EmpName.Text;
        trans.Dept = txtRequestor_Dept.Text;
        trans.Local = txtRequestor_LocalNo.Text;
        trans.Location = ddlLocation.SelectedValue;
        trans.Category = ddlCategory.SelectedValue;
        trans.NoOfReq = txtNumOfReq.Text;
        trans.Attributes1 = Remarks;
        trans.CurrentUser = hdnUserEmpName.Value;
        trans.CurrentUserEmpNo = hdnUserEmpNo.Value;

        DataTable dt_rfq;
        if (hdnRFQ_Source.Value == "type")
        {
            DataTable dtRFQ_Request_ForSave = dtRFQ_Request.Clone();
            dtRFQ_Request = (DataTable)ViewState[DT_RFQ_REQUEST];
            for (int i = 0; i < dtRFQ_Request.Rows.Count - 1; i++)
            {
                DataRow r = dtRFQ_Request_ForSave.NewRow();
                r["ControlNo"] = dtRFQ_Request.Rows[i]["ControlNo"];
                r["ItemDescription"] = dtRFQ_Request.Rows[i]["ItemDescription"];
                r["TempItemCode"] = dtRFQ_Request.Rows[i]["TempItemCode"];
                r["Quantity"] = dtRFQ_Request.Rows[i]["Quantity"];
                r["UOM"] = dtRFQ_Request.Rows[i]["UOM"];
                r["Budget"] = dtRFQ_Request.Rows[i]["Budget"];
                r["Currency"] = dtRFQ_Request.Rows[i]["Currency"];
                r["Purpose"] = dtRFQ_Request.Rows[i]["Purpose"];
                r["Remarks"] = dtRFQ_Request.Rows[i]["Remarks"];
                r["BuyerInCharge"] = dtRFQ_Request.Rows[i]["BuyerInCharge"];
                r["BuyerInChargeEmpNo"] = dtRFQ_Request.Rows[i]["BuyerInChargeEmpNo"];
                r["Status"] = dtRFQ_Request.Rows[i]["Status"];

                dtRFQ_Request_ForSave.Rows.Add(r);
                dtRFQ_Request_ForSave.AcceptChanges();
            }

            dtRFQ_Request_ForSave.Columns.Remove("Valid");
            dt_rfq = dtRFQ_Request_ForSave.Copy();
        }
        else
        {
            DataTable dtRFQ_Request_Upload_ForSave = dtRFQ_Request.Clone();
            dtRFQ_Request_Upload = (DataTable)ViewState[DT_RFQ_REQUEST_FOR_UPLOAD];
            for (int i = 0; i < dtRFQ_Request_Upload.Rows.Count - 1; i++)
            {
                DataRow r = dtRFQ_Request_Upload_ForSave.NewRow();
                r["ControlNo"] = dtRFQ_Request.Rows[i]["ControlNo"];
                r["ItemDescription"] = dtRFQ_Request_Upload.Rows[i]["ItemDescription"];
                r["TempItemCode"] = dtRFQ_Request_Upload.Rows[i]["TempItemCode"];
                r["Quantity"] = dtRFQ_Request_Upload.Rows[i]["Quantity"];
                r["UOM"] = dtRFQ_Request_Upload.Rows[i]["UOM"];
                r["Budget"] = dtRFQ_Request_Upload.Rows[i]["Budget"];
                r["Currency"] = dtRFQ_Request_Upload.Rows[i]["Currency"];
                r["Purpose"] = dtRFQ_Request_Upload.Rows[i]["Purpose"];
                r["Remarks"] = dtRFQ_Request_Upload.Rows[i]["Remarks"];
                r["BuyerInCharge"] = dtRFQ_Request_Upload.Rows[i]["BuyerInCharge"];
                r["BuyerInChargeEmpNo"] = dtRFQ_Request_Upload.Rows[i]["BuyerInChargeEmpNo"];
                r["Status"] = dtRFQ_Request_Upload.Rows[i]["Status"];

                dtRFQ_Request_Upload_ForSave.Rows.Add(r);
                dtRFQ_Request_Upload_ForSave.AcceptChanges();
            }

            dtRFQ_Request_Upload_ForSave.Columns.Remove("Valid");
            dt_rfq = dtRFQ_Request_Upload_ForSave.Copy();
        }

        dt_rfq.Columns["ControlNo"].SetOrdinal(0);
        dt_rfq.Columns["ItemDescription"].SetOrdinal(1);
        dt_rfq.Columns["TempItemCode"].SetOrdinal(2);
        dt_rfq.Columns["Quantity"].SetOrdinal(3);
        dt_rfq.Columns["UOM"].SetOrdinal(4);
        dt_rfq.Columns["Budget"].SetOrdinal(5);
        dt_rfq.Columns["Currency"].SetOrdinal(6);
        dt_rfq.Columns["Purpose"].SetOrdinal(7);
        dt_rfq.Columns["Remarks"].SetOrdinal(8);
        dt_rfq.Columns["BuyerInCharge"].SetOrdinal(9);
        dt_rfq.Columns["BuyerInChargeEmpNo"].SetOrdinal(10);
        dt_rfq.Columns["Status"].SetOrdinal(11);
        dt_rfq.Columns.Add("Attribute1");
        dt_rfq.Columns.Add("Attribute2");
        dt_rfq.Columns.Add("Attribute3");



        string result = dl.Insert_RFQ_Request(cl_ProvideFactory.getSqlFactory(), cl_DBConn.MSSQLTrans(), trans, dt_rfq, TransType);

        if (result.ToLower() != "request timeout")
        {
            if (result.ToLower().Contains("completed"))
            {
                if(hdnButton.Value == "saveButton")
                {
                    Bulk_Insert_RFQ_Approvers();
                }
                AlertMessage.Show("Information", "Information:", result);
                hdnAction.Value = "refresh";
            }
            else
            {
                AlertMessage.Show("Information", "Information:", result);
                hdnAction.Value = "alert";
            }
        }
        else
        {
            AlertMessage.Show("Warning", "Information:", "Transaction Failed: Please check input details then try again!");
            hdnAction.Value = "alert";
        }
        Load_HistoryLogs(hdnRefID.Value);
    }
    private void Edit_Approver()
    {
        divApprover2.Visible = true;
        lbtnEditApprover.Visible = false;
        lbtnCancelEdit.Visible = true;
        lbtnSaveApprover.Visible = true;
        if (gvApproval1.Rows.Count > 0)
        {
            if (((HiddenField)gvApproval1.Rows[0].FindControl("hdnApprover_Status")).Value == "Approved")
            {
                ((DropDownList)gvApproval1.Rows[0].FindControl("ddlApprover")).Enabled = false;

            }
            else
            {
                ((DropDownList)gvApproval1.Rows[0].FindControl("ddlApprover")).Visible = true;
            }

            if (((HiddenField)gvApproval1.Rows[1].FindControl("hdnApprover_Status")).Value == "Approved")
            {
                ((DropDownList)gvApproval1.Rows[1].FindControl("ddlApprover")).Enabled = false;

            }
            else
            {
                ((DropDownList)gvApproval1.Rows[1].FindControl("ddlApprover")).Visible = true;
            }

            ((DropDownList)gvApproval1.Rows[0].FindControl("ddlApprover")).Visible = true; // APPROVER 1
            ((DropDownList)gvApproval1.Rows[1].FindControl("ddlApprover")).Visible = true; // APPROVER 2

            ((Label)gvApproval1.Rows[0].FindControl("lblApprover")).Visible = false; // APPROVER 1
            ((Label)gvApproval1.Rows[1].FindControl("lblApprover")).Visible = false; // APPROVER 2

        }
        uPnlApprover.Update();
    }
    private void Edit_Buyer()
    {
        if (dtRFQ_Request.Rows.Count > 0)
        {
            for (int i = 0; gvRFQ_List.Rows.Count > i; i++)
            {
                if (((HiddenField)gvRFQ_List.Rows[i].FindControl("hdnStats")).Value == "Partially closed")
                {
                    ((DropDownList)gvRFQ_List.Rows[i].FindControl("ddlBuyer")).Enabled = false;

                }
                else
                {
                    ((DropDownList)gvRFQ_List.Rows[i].FindControl("ddlBuyer")).Visible = true;
                }
                ((DropDownList)gvRFQ_List.Rows[i].FindControl("ddlBuyer")).Visible = true;
                ((Label)gvRFQ_List.Rows[i].FindControl("lblBuyer")).Visible = false;
            }
        }
        uPnlApprover.Update();
    }
    protected Boolean Update_RFQ_Approver(string txtRemarks_Save)
    {
        bool result = false;
        try
        {
            Label lblPosition;
            DropDownList ddlApprover;
            for (int i = 0; gvApproval1.Rows.Count > i; i++)
            {
                lblPosition = (Label)gvApproval1.Rows[i].FindControl("lblPosition");
                ddlApprover = (DropDownList)gvApproval1.Rows[i].FindControl("ddlApprover");

                string RefID = hdnRefID.Value;
                string Position = lblPosition.Text;
                string EmpNo = ddlApprover.SelectedValue;
                string EmpName = ddlApprover.SelectedItem.Text;

                string sqlQuery = "UPDATE [dbo].[tbl_RFQ_Approvers] SET [EmpNo]='" + EmpNo + "', [EmpName] = '" + EmpName + "' WHERE [RefID]='" + RefID + "' AND [Position]='" + Position + "'";


                using (SqlConnection con = new SqlConnection(cl_DBConn.MSSQLTrans()))
                {
                    SqlCommand command = new SqlCommand(sqlQuery, con);
                    con.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    con.Close();
                }
            }

            string sqlQueryLogs = "INSERT INTO [dbo].[tbl_RFQ_HistoryLogs]([RefID],[RFQNo],[ActionTaken],[Remarks],[CreatedBy],[CreatedDate],[UpdatedBy],[UpdatedDate]) VALUES ('" + hdnRefID.Value + "','" + txtControlNo.Text + "', 'CHANGE APPROVER','" + txtRemarks_Save + "','" + hdnUserEmpName.Value + "', '" + DateTime.Now + "', '', '')";
           
            using (SqlConnection con = new SqlConnection(cl_DBConn.MSSQLTrans()))
            {
                SqlCommand command = new SqlCommand(sqlQueryLogs, con);
                con.Open();
                SqlDataReader reader = command.ExecuteReader();
                con.Close();
            }

            if (isAdmin && hdnStatus.Value.ToLower() == "for approval")
            {
                using (SqlConnection con = new SqlConnection(cl_DBConn.MSSQLTrans()))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_Send_Email", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("@E_TransType", SqlDbType.VarChar).Value = "update app by admin";
                        cmd.Parameters.Add("@E_RefID", SqlDbType.VarChar).Value = hdnRefID.Value;
                        cmd.Parameters.Add("@E_Remarks", SqlDbType.VarChar).Value = txtRemarks_Save;
                        cmd.Parameters.Add("@E_UserName", SqlDbType.VarChar).Value = hdnUserEmpName.Value;

                        con.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
            }

            AlertMessage.Show("Information", "Information:", "Approvers updated successfully!");
            Load_RFQ_Approvers();
            Load_HistoryLogs(hdnRefID.Value);
            lbtnSaveApprover.Visible = false;
            lbtnEditApprover.Visible = true;
            lbtnCancelEdit.Visible = false;
            result = true;
        }
        catch
        {
            //
        }
        return result;
    }
    protected Boolean Update_RFQ_Buyer(string txtRemarks_Save)
    {
        bool result = false;
        try
        {
            string status = "";
            foreach (DataRow row in dtRFQ_Request.Rows)
            {
                status = row["Status"].ToString();
                if (status == "")
                {
                    DropDownList ddlBuyer;
                    Label TempItemCode;
                    for (int i = 0; gvRFQ_List.Rows.Count -1> i; i++)
                    {
                        ddlBuyer = (DropDownList)gvRFQ_List.Rows[i].FindControl("ddlBuyer");
                        TempItemCode = (Label)gvRFQ_List.Rows[i].FindControl("lblControlNo");
                        string RefID = hdnRefID.Value;
                        string EmpNo = ddlBuyer.SelectedValue;
                        string EmpName = ddlBuyer.SelectedItem.Text;

                        string sqlQuery = "UPDATE [dbo].[tbl_RFQ_DetailsOfRequest] SET [BuyerInCharge]='" + EmpName + "',[BuyerInChargeEmpNo]='" + EmpNo + "' WHERE [RefID]='" + RefID + "' AND [TempItemCode]='" + TempItemCode.Text + "'";


                        using (SqlConnection con = new SqlConnection(cl_DBConn.MSSQLTrans()))
                        {
                            SqlCommand command = new SqlCommand(sqlQuery, con);
                            con.Open();
                            SqlDataReader reader = command.ExecuteReader();
                            con.Close();
                        }
                    }

                }

            }

            string sqlQueryLogs = "INSERT INTO [dbo].[tbl_RFQ_HistoryLogs]([RefID],[RFQNo],[ActionTaken],[Remarks],[CreatedBy],[CreatedDate],[UpdatedBy],[UpdatedDate]) VALUES ('" + hdnRefID.Value + "','" + txtControlNo.Text + "', 'RE-ASSIGN BUYER','" + txtRemarks_Save + "','" + hdnUserEmpName.Value + "', '" + DateTime.Now + "', '', '')";

            using (SqlConnection con = new SqlConnection(cl_DBConn.MSSQLTrans()))
            {
                SqlCommand command = new SqlCommand(sqlQueryLogs, con);
                con.Open();
                SqlDataReader reader = command.ExecuteReader();
                con.Close();
            }

            using (SqlConnection con = new SqlConnection(cl_DBConn.MSSQLTrans()))
            {
                using (SqlCommand cmd = new SqlCommand("sp_Email_Per_Item", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@I_RefID", SqlDbType.VarChar).Value = hdnRefID.Value;

                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }

            AlertMessage.Show("Information", "Information:", "Buyers updated successfully!");
            gvDetailsViewOnly.Visible = true;
            Load_Transaction("per refid", hdnRefID.Value);
            Load_HistoryLogs(hdnRefID.Value);
            lbtnSaveNewBuyer.Visible = false;
            lbtnCancelUpdate.Visible = false;
            lbtnReAssignBuyer.Visible = true;
            result = true;
            hdnAction.Value = "refresh";
        }
        catch
        {
            //
        }
        return result;
    }

    private void Approve_Deny(string TransType, string Remarks)
    {
        trans.RefID = hdnRefID.Value;
        trans.CurrentUser = hdnUserEmpName.Value;
        trans.CurrentUserEmpNo = hdnUserEmpNo.Value;
        trans.isAdmin = isAdmin;
        string result = dl.Approve_Deny(cl_ProvideFactory.getSqlFactory(), cl_DBConn.MSSQLTrans(), trans, TransType, Remarks);
        if (result.ToLower() != "request timeout")
        {
            if (result.ToLower().Contains("completed"))
            {
                AlertMessage.Show("Information", "Information:", result);
                hdnAction.Value = "refresh";
            }
        }
        else
        {
            AlertMessage.Show("Warning", "Information:", "Transaction Failed: Request Timeout!");
            hdnAction.Value = "alert";
        }
        upnlDetailsOfReq.Update();
        Update_GVHeader();
    }
    private void Resubmit_New_RFQ(string TransType, string Remarks)
    {
        trans.RefID = hdnRefID.Value;
        trans.CurrentUser = hdnUserEmpName.Value;
        string result = dl.Resubmit(cl_ProvideFactory.getSqlFactory(), cl_DBConn.MSSQLTrans(), trans, TransType, Remarks);
        if (result.ToLower() != "request timeout")
        {
            if (result.ToLower().Contains("completed"))
            {
                AlertMessage.Show("Information", "Information:", result);
                hdnAction.Value = "refresh";
            }
        }
        else
        {
            AlertMessage.Show("Warning", "Information:", "Transaction Failed: Request Timeout!");
            hdnAction.Value = "alert";
        }
        upnlDetailsOfReq.Update();
    }

    #endregion


    #region Selection Methods and Buttons
    protected void lbtnEmpSearch_SendTo_Click(object sender, EventArgs e)
    {
        EmpSearch.Show("with username");

    }
    //protected void Selected_Employee_QA(string TransType, string EmpNo, string EmpEmail)
    //{
    //    cl_Employee emp = new cl_Employee();
    //    emp.Select_EmployeeMaster_Email(TransType, EmpNo, EmpEmail);

    //}
    //protected void Selected_Employee_MPD(string TransType, string EmpNo, string EmpEmail)
    //{
    //    cl_Employee emp = new cl_Employee();
    //    emp.Select_EmployeeMaster_Email(TransType, EmpNo, EmpEmail);

    //}
    //protected void Selected_Employee_CopyTo(string TransType, string EmpNo, string EmpEmail)
    //{
    //    cl_Employee emp = new cl_Employee();
    //    emp.Select_EmployeeMaster_Email_CopyTo(TransType, EmpNo, EmpEmail);
    //}

    #endregion

    #region Check if all fields have value
    private Boolean isComplete()
    {
        bool result = true;
        TextBox txtItemDesc = new TextBox();
        TextBox txtQty = new TextBox();
        TextBox txtUOM = new TextBox();
        TextBox txtBudget = new TextBox();
        DropDownList ddlCurrency = new DropDownList();
        TextBox txtPurpose = new TextBox();
        foreach (GridViewRow row in gvRFQ_List.Rows)
        {
            txtItemDesc = (TextBox)gvRFQ_List.Rows[row.RowIndex].FindControl("txtItemDesc");
            txtQty = (TextBox)gvRFQ_List.Rows[row.RowIndex].FindControl("txtQty");
            txtUOM = (TextBox)gvRFQ_List.Rows[row.RowIndex].FindControl("txtUOM");
            txtBudget = (TextBox)gvRFQ_List.Rows[row.RowIndex].FindControl("txtBudget");
            ddlCurrency = (DropDownList)gvRFQ_List.Rows[row.RowIndex].FindControl("ddlCurrency");
            txtPurpose = (TextBox)gvRFQ_List.Rows[row.RowIndex].FindControl("txtPurpose");
        }

        if (txtItemDesc.Text.Trim().Length == 0)
        {
            AlertMessage.Show("warning", "Information:", "Please input Item Description!");
            hdnAction.Value = "alert";
            if (txtItemDesc.Text.Trim().Length != 0)
            {
                txtItemDesc.Visible = false;
            }
            result = false;
        }
        else if (txtQty.Text.Trim().Length == 0)
        {
            AlertMessage.Show("warning", "Information:", "Please input Quantity!");
            hdnAction.Value = "alert";
            if (txtQty.Text.Trim().Length != 0)
            {
                txtQty.Visible = false;
            }
            result = false;
        }
        else if (txtUOM.Text.Trim().Length == 0)
        {
            AlertMessage.Show("warning", "Information:", "Please input UOM!");
            hdnAction.Value = "alert";
            if (txtUOM.Text.Trim().Length != 0)
            {
                txtUOM.Visible = false;
            }
            result = false;
        }
        else if (txtBudget.Text.Trim().Length == 0)
        {
            AlertMessage.Show("warning", "Information:", "Please input Budget!");
            hdnAction.Value = "alert";
            if (txtBudget.Text.Trim().Length != 0)
            {
                txtBudget.Visible = false;
            }
            result = false;
        }
        else if (ddlCurrency.SelectedIndex == 0)
        {
            AlertMessage.Show("warning", "Information:", "Please select Currency!");
            hdnAction.Value = "alert";
            if (ddlCurrency.SelectedIndex != 0)
            {
                ddlCurrency.Visible = false;
            }
            result = false;
        }
        else if (txtPurpose.Text.Trim().Length == 0)
        {
            AlertMessage.Show("warning", "Information:", "Please input Purpose!");
            hdnAction.Value = "alert";
            if (txtPurpose.Text.Trim().Length != 0)
            {
                txtPurpose.Visible = false;
            }
            result = false;
        }


        return result;

    }
    private Boolean isCompleteAll()
    {
        bool result = true;
        if (hdnButton.Value == "updateButton") 
        {
            if (txtRequestor_LocalNo.Text.Trim().Length == 0)
            {
                AlertMessage.Show("warning", "Information:", "Please input Local number!");
                hdnAction.Value = "alert";

                result = false;
            }
            else if (ddlLocation.SelectedIndex == 0)
            {
                AlertMessage.Show("warning", "Information:", "Please select Location!");
                hdnAction.Value = "alert";

                result = false;
            }
            else if (ddlCategory.SelectedIndex == 0)
            {
                AlertMessage.Show("warning", "Information:", "Please select Category!");
                hdnAction.Value = "alert";

                result = false;
            }
            else if (txtNumOfReq.Text == "" || txtNumOfReq.Text == "--Auto Generated--")
            {
                AlertMessage.Show("warning", "Information:", "Please input details of request!");
                hdnAction.Value = "alert";

                result = false;
            }
            else if (gvAttachment.Rows.Count == 0)
            {
                AlertMessage.Show("warning", "Information:", "Please attach file!");
                hdnAction.Value = "alert";

                result = false;
            }
        }
        else if (hdnButton.Value == "saveButton")
        {
            if (txtRequestor_LocalNo.Text.Trim().Length == 0)
            {
                AlertMessage.Show("warning", "Information:", "Please input Local number!");
                hdnAction.Value = "alert";

                result = false;
            }
            else if (ddlLocation.SelectedIndex == 0)
            {
                AlertMessage.Show("warning", "Information:", "Please select Location!");
                hdnAction.Value = "alert";

                result = false;
            }
            else if (ddlCategory.SelectedIndex == 0)
            {
                AlertMessage.Show("warning", "Information:", "Please select Category!");
                hdnAction.Value = "alert";

                result = false;
            }
            else if (txtNumOfReq.Text == "" || txtNumOfReq.Text == "--Auto Generated--")
            {
                AlertMessage.Show("warning", "Information:", "Please input details of request!");
                hdnAction.Value = "alert";

                result = false;
            }
            else if (gvAttachment.Rows.Count == 0)
            {
                AlertMessage.Show("warning", "Information:", "Please attach file!");
                hdnAction.Value = "alert";

                result = false;
            }
            else
            {
                Label lblPosition;
                DropDownList ddlApprover;
                string app1 = "1";
                string app2 = "2";

                for (int i = 0; gvApproval.Rows.Count > i; i++)
                {
                    lblPosition = (Label)gvApproval.Rows[i].FindControl("lblPosition");
                    ddlApprover = (DropDownList)gvApproval.Rows[i].FindControl("ddlApprover");
                    if (lblPosition.Text == "Approver1" || lblPosition.Text == "Approver2")
                    {
                        if (ddlApprover.SelectedIndex == 0)
                        {
                            if (gvApproval1.Rows.Count > 0)
                            {
                                if (i == 0) // Approver1
                                {
                                    if (((Label)gvApproval1.Rows[i].FindControl("lblStatus")).Text != "Approved")
                                    {
                                        AlertMessage.Show("Warning", "Information:", "Please select " + lblPosition.Text + "!");
                                        hdnAction.Value = "alert";
                                        return false;
                                    }
                                }
                                else if (i == 1) //Approver2
                                {
                                    if (((Label)gvApproval1.Rows[i].FindControl("lblStatus")).Text != "Approved")
                                    {
                                        AlertMessage.Show("Warning", "Information:", "Please select " + lblPosition.Text + "!");
                                        hdnAction.Value = "alert";
                                        return false;
                                    }
                                }
                            }

                        }

                        if (lblPosition.Text == "Approver1")
                        {
                            app1 = ddlApprover.SelectedValue;
                        }
                        else if (lblPosition.Text == "Approver2")
                        {
                            app2 = ddlApprover.SelectedValue;
                        }

                        if (app1 == app2)
                        {
                            AlertMessage.Show("Warning", "Information:", "Please select different name for Approvers!");
                            hdnAction.Value = "alert";
                            result = false;
                        }
                        if (ddlApprover.SelectedIndex == 0)
                        {
                            AlertMessage.Show("warning", "Information:", "Please select Approver!");
                            hdnAction.Value = "alert";

                            result = false;
                        }
                    }
                }
            }
        }
       

        return result;

    }
    private Boolean isCompleteEditApprovers() 
    {
        bool result = true;

        Label lblPosition;
        DropDownList ddlApprover;
        string app1 = "1";
        string app2 = "2";

        for (int i = 0; gvApproval1.Rows.Count > i; i++)
        {
            lblPosition = (Label)gvApproval1.Rows[i].FindControl("lblPosition");
            ddlApprover = (DropDownList)gvApproval1.Rows[i].FindControl("ddlApprover");
            if (lblPosition.Text == "Approver1" || lblPosition.Text == "Approver2")
            {
                if (ddlApprover.SelectedIndex == 0)
                {
                    if (gvApproval1.Rows.Count > 0)
                    {
                        if (i == 0) // Approver1
                        {
                            if (((Label)gvApproval1.Rows[i].FindControl("lblStatus")).Text != "Approved")
                            {
                                AlertMessage.Show("Warning", "Information:", "Please select " + lblPosition.Text + "!");
                                hdnAction.Value = "alert";
                                return false;
                            }
                        }
                        else if (i == 1) //Approver2
                        {
                            if (((Label)gvApproval1.Rows[i].FindControl("lblStatus")).Text != "Approved")
                            {
                                AlertMessage.Show("Warning", "Information:", "Please select " + lblPosition.Text + "!");
                                hdnAction.Value = "alert";
                                return false;
                            }
                        }
                    }

                }

                if (lblPosition.Text == "Approver1")
                {
                    app1 = ddlApprover.SelectedValue;
                }
                else if (lblPosition.Text == "Approver2")
                {
                    app2 = ddlApprover.SelectedValue;
                }

                if (app1 == app2)
                {
                    AlertMessage.Show("Warning", "Information:", "Please select different name for Approvers!");
                    hdnAction.Value = "alert";
                    result = false;
                }
                if (ddlApprover.SelectedIndex == 0)
                {
                    AlertMessage.Show("warning", "Information:", "Please select Approver!");
                    hdnAction.Value = "alert";

                    result = false;
                }
            }
        }

        return result;

    }

    private Boolean isCompleteBuyerIncharge()
    {
        bool result = true;

        DropDownList ddlBuyer;
        int rowsCount = gvRFQ_List.Rows.Count - 1;
        for (int i = 0; rowsCount > i; i++)
        {
            ddlBuyer = (DropDownList)gvRFQ_List.Rows[i].FindControl("ddlBuyer");

            if (ddlBuyer.SelectedIndex == 0)
            {
                AlertMessage.Show("warning", "Information:", "Please select Buyer In-charge!");
                hdnAction.Value = "alert";

                result = false;
            }
        }
        return result;
    }
        #endregion

        #region Attachment Buttons

        protected void lbtnUpload_Command(object sender, CommandEventArgs e)
    {
        upnlDetailsOfReq.Update();
        if (fuAttachment.FileName == "")
        {
            AlertMessage.Show("Warning", "Information:", "Please select file to attach!");
            hdnAction.Value = "alert";
        }
        else
        {
            try
            {
                string fileName = fuAttachment.FileName;
                string newFileName = hdnRefID.Value + "-" + fileName.Replace("#", "No.");
                int FileName_Length = fileName.Length;
                string FileName_Ext = fileName.Substring(fileName.LastIndexOf(".") + 1).ToLower();
                if (".gif.jpg.tiff.raw.gif.bmp.png.ppm.pam.tiff.pdf.xls.xlsx.doc.docx.ppt.pptx".ToLower().IndexOf(FileName_Ext) < 0)
                {
                    AlertMessage.Show("Warning", "Information:", "Invalid File Format!");
                    hdnAction.Value = "alert";
                    return;
                }
                else if (fuAttachment.FileBytes.Length > cl_DBConn.FileSize())
                {
                    AlertMessage.Show("Warning", "Information:", "File exceeds 10MB! Please compress.");
                    hdnAction.Value = "alert";
                    return;
                }
                else
                {
                    string FilePath = Server.MapPath(@"~/FileDirectory/Attachment/" + newFileName);

                    dto.ID = "";
                    dto.RefID = hdnRefID.Value;
                    dto.FileName_Orig = fuAttachment.FileName;
                    dto.FileName_New = newFileName;
                    dto.FilePath = FilePath;
                    dto.ActionRemarks = "";
                    dto.CreatedBy_EmpNo = hdnUserEmpNo.Value;
                    dto.CreatedBy_EmpName = hdnUserEmpName.Value;
                    dto.Attribute1 = "-";
                    dto.Attribute2 = "-";

                    string result = dl.Insert_RFQ_Attachment(cl_ProvideFactory.getSqlFactory(), cl_DBConn.MSSQLTrans(), dto, "save");
                    if (result.ToLower() == "request timeout")
                    {
                        AlertMessage.Show("Warning", "Information:", "Request Timeout: Please try again!");
                        hdnAction.Value = "alert";
                    }
                    else if (result.ToLower().Contains("exist"))
                    {
                        AlertMessage.Show("Information", "Information:", result);
                        hdnAction.Value = "alert";
                    }
                    else
                    {
                        fuAttachment.PostedFile.SaveAs(FilePath);
                    }
                    Load_Attachment();
                }

            }
            catch (Exception ex)
            {
                AlertMessage.Show("Warning", "Information:", "Request Timeout: Please try again!");
                hdnAction.Value = "alert";
            }
            fuAttachment.Dispose();
            upnlDetailsOfReq.Update();
        }
    }
    private void Load_Attachment()
    {
        DataTable dtAttachment = new DataTable();
        dtAttachment = dl.get_RFQ_Attachment(cl_ProvideFactory.getSqlFactory(), cl_DBConn.MSSQLTrans(), "per refid", hdnRefID.Value);
        gvAttachment.DataSource = dtAttachment;
        gvAttachment.DataBind();

        if (gvAttachment.Rows.Count > 0)
        {
            gvAttachment.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        Load_HistoryLogs(hdnRefID.Value);

    }
    protected void lbtnOpen_Command(object sender, CommandEventArgs e)
    {
        try
        {
            string filePath = e.CommandName.ToString();
            Response.ContentType = ContentType;
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(filePath));
            Response.WriteFile(filePath);
            Response.End();
        }
        catch (Exception ex)
        {
            //
        }
    }
    protected void lbtnDeleteAttachment_Command(object sender, CommandEventArgs e)
    {
        hdnAttachmentID.Value = e.CommandArgument.ToString();
        hdnAttachmentPath.Value = e.CommandName.ToString();
        upnlActionButton.Update();
        AlertMessage.Show("confirmationwithremarks", "Confirmation:", "Are you sure you delete this attachment?");
        hdnAction.Value = "delete attachment";
    }
    #endregion




    protected void lbtnExport_Click(object sender, EventArgs e)
    {
        AlertMessage.Show("Information", "Information:", "Transaction Failed: Please check input details then try again!");
        hdnAction.Value = "redirect";
    }
    //protected void grdMasterList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    //{
    //    //grdMasterList.PageIndex = e.NewPageIndex;
    //    //Load_Records();
    //}
    protected void lbtnAddItem_Command(object sender, CommandEventArgs e)
    {
        if (isComplete())
        {
            if (gvRFQ_List.Rows.Count > 0)
            {
                CopyRowsToDT_RFQList();
                AddRow_RFQList();
            }
            Update_GVHeader();
        }
    }
    protected void lbtnDeleteItem_Command(object sender, CommandEventArgs e)
    {
        hdnDltRow.Value = e.CommandArgument.ToString();

        AlertMessage.Show("confirmationwithremarks", "Confirmation:", "Are you sure you want to delete this row?");
        hdnGV_Action.Value = "delete item code";
        Update_GVHeader();
    }
    
    protected void lbtnReassignBuyer_Command(object sender, CommandEventArgs e)
    {
        hdnDltRow.Value = e.CommandArgument.ToString();

        AlertMessage.Show("confirmationwithremarks", "Confirmation:", "Are you sure you want to reaasign new buyer?");
        hdnGV_Action.Value = "reassign item code";
        Update_GVHeader();
    }
    protected void lbtnView_Command(object sender, CommandEventArgs e)
    {
        Response.Redirect("~/PageTransaction/ViewDetails.aspx?TempItemCode=" + e.CommandName.ToString());
    }




    protected void AddRow_RFQList()
    {
        
        hdnRFQ_Source.Value = "type";
        dtRFQ_Request = (DataTable)ViewState[DT_RFQ_REQUEST];
        DataRow dr = dtRFQ_Request.NewRow();

        dr["TempItemCode"] = "ITM" + DateTime.Now.ToString("yyyyMMddHHmmss");
        dtRFQ_Request.Rows.Add(dr);
        ViewState[DT_RFQ_REQUEST] = dtRFQ_Request;
        gvRFQ_List.DataSource = dtRFQ_Request;
        gvRFQ_List.DataBind();

        //NUMBER OF REQUEST
        string counts = gvRFQ_List.Rows.Count.ToString();
        int numOfReq = Int32.Parse(counts);
        int dif = numOfReq - 1;
        if (dif == 0 || txtNumOfReq.Text == "0")
        {
            txtNumOfReq.Text = "--Auto Generated--";
        }
        else
        {
            txtNumOfReq.Text = dif.ToString();
        }
        upnlDetailsOfReq.Update();
        upnlActionButton.Update();
    }
    protected void CopyRowsToDT_RFQList()
    {
        dtRFQ_Request.Rows.Clear();
        foreach (GridViewRow row in gvRFQ_List.Rows)
        {
            DataRow dr = dtRFQ_Request.NewRow();
            dr["ControlNo"] = ((HiddenField)row.FindControl("hdnControlNo")).Value;
            dr["ItemDescription"] = ((TextBox)row.FindControl("txtItemDesc")).Text;
            dr["TempItemCode"] = ((HiddenField)row.FindControl("hdnTempItemCode")).Value;
            dr["Quantity"] = ((TextBox)row.FindControl("txtQty")).Text;
            dr["UOM"] = ((TextBox)row.FindControl("txtUOM")).Text;
            dr["Budget"] = ((TextBox)row.FindControl("txtBudget")).Text;
            dr["Currency"] = ((DropDownList)row.FindControl("ddlCurrency")).SelectedValue;
            dr["Purpose"] = ((TextBox)row.FindControl("txtPurpose")).Text;
            dr["Remarks"] = ((TextBox)row.FindControl("txtRemarks")).Text;
            dr["BuyerInCharge"] = ((DropDownList)row.FindControl("ddlBuyer")).SelectedItem.Text;
            dr["BuyerInChargeEmpNo"] = ((DropDownList)row.FindControl("ddlBuyer")).SelectedValue;
            dr["Valid"] = "valid";

            dtRFQ_Request.Rows.Add(dr);
            dtRFQ_Request.AcceptChanges();
        }
        ViewState[DT_RFQ_REQUEST] = dtRFQ_Request;
    }
    protected void gvRFQ_List_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (hdnStatus.Value == "Open" )
        {
            gvRFQ_List.Columns[9].Visible = true;
            gvRFQ_List.Columns[10].Visible = false;
        }
       
        
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //Currency
            DropDownList GV_ddlCurrency = ((DropDownList)e.Row.FindControl("ddlCurrency"));
            Label GV_lblCurrency = ((Label)e.Row.FindControl("lblCurrency"));
            GV_ddlCurrency.DataSource = dtCurrency_List;
            GV_ddlCurrency.DataTextField = "Description";
            GV_ddlCurrency.DataValueField = "ItemCode";
            GV_ddlCurrency.DataBind();
            GV_ddlCurrency.Items.Insert(0, new ListItem("--Please Select--", "0"));
            //if (!GV_ddlCurrency.Items.Contains(new ListItem(GV_lblCurrency.Text, GV_lblCurrency.Text)) && GV_lblCurrency.Text.Length != 0)
            //{
            //    GV_ddlCurrency.Items.Insert(GV_ddlCurrency.Items.Count, new ListItem(GV_lblCurrency.Text, GV_lblCurrency.Text));
            //}

            GV_ddlCurrency.SelectedValue = GV_lblCurrency.Text.Length > 0 ? GV_lblCurrency.Text : "";


            //buyer in-charge
            DropDownList GV_ddlBuyerIncharge = ((DropDownList)e.Row.FindControl("ddlBuyer"));
            Label GV_lblBuyerIncharge = ((Label)e.Row.FindControl("lblBuyer"));
            HiddenField hdnBuyerEmpNo = ((HiddenField)e.Row.FindControl("hdnBuyerEmpNo"));
            GV_ddlBuyerIncharge.DataSource = dtBuyerIncharge_List;
            GV_ddlBuyerIncharge.DataTextField = "Description";
            GV_ddlBuyerIncharge.DataValueField = "ItemCode";
            GV_ddlBuyerIncharge.DataBind();
            GV_ddlBuyerIncharge.Items.Insert(0, new ListItem("--Please Select--", "0"));

            for (int i = 0; dtRFQ_Request.Rows.Count > i; i++)
            {
                if (string.IsNullOrEmpty(hdnBuyerEmpNo.Value) || hdnBuyerEmpNo.Value == "0")
                {
                    GV_ddlBuyerIncharge.SelectedValue = hdnBuyerEmpNo.Value;
                }
                else
                {
                    if (!string.IsNullOrEmpty(GV_lblBuyerIncharge.Text))
                    {
                        if (!GV_ddlBuyerIncharge.Items.Contains(new ListItem(GV_lblBuyerIncharge.Text, hdnBuyerEmpNo.Value)))
                        {
                            GV_ddlBuyerIncharge.Items.Insert(1, new ListItem(GV_lblBuyerIncharge.Text, hdnBuyerEmpNo.Value));
                            GV_ddlBuyerIncharge.SelectedValue = hdnBuyerEmpNo.Value;
                        }
                        else
                        {
                            GV_ddlBuyerIncharge.SelectedValue = hdnBuyerEmpNo.Value;
                        }
                    }
                    else
                    {
                        if (!GV_ddlBuyerIncharge.Items.Contains(new ListItem(hdnBuyerEmpNo.Value, hdnBuyerEmpNo.Value)))
                        {
                            GV_ddlBuyerIncharge.Items.Insert(1, new ListItem(hdnBuyerEmpNo.Value, hdnBuyerEmpNo.Value));
                            GV_ddlBuyerIncharge.SelectedValue = hdnBuyerEmpNo.Value;
                        }
                        else
                        {
                            GV_ddlBuyerIncharge.SelectedValue = hdnBuyerEmpNo.Value;
                        }
                    }
                }
            }
            
            //if (!GV_ddlBuyerIncharge.Items.Contains(new ListItem(GV_lblBuyerIncharge.Text, GV_lblBuyerIncharge.Text)) && GV_lblBuyerIncharge.Text.Length != 0)
            //{
            //    GV_ddlBuyerIncharge.Items.Insert(GV_ddlBuyerIncharge.Items.Count, new ListItem(GV_lblBuyerIncharge.Text, hdnBuyerEmpNo.Value));
            //}
           // GV_ddlBuyerIncharge.SelectedValue = GV_lblBuyerIncharge.Text.Length > 0 ? hdnBuyerEmpNo.Value : "";

            if (((HiddenField)e.Row.FindControl("hdnStats")).Value == "Partially closed")
            {
               ((Label)e.Row.FindControl("lblBuyer")).Visible = true;
               ((DropDownList)e.Row.FindControl("ddlBuyer")).Visible = false;
            }
            else
            {
                ((DropDownList)e.Row.FindControl("ddlBuyer")).Enabled = true;
            }


            if (((HiddenField)e.Row.FindControl("hdnValid")).Value == "valid")
            {
                //HIDE TEXTBOX SHOW LABEL
                ((TextBox)e.Row.FindControl("txtItemDesc")).Visible = false;
                ((Label)e.Row.FindControl("lblItemDesc")).Visible = true;
                ((TextBox)e.Row.FindControl("txtQty")).Visible = false;
                ((Label)e.Row.FindControl("lblQty")).Visible = true;
                ((TextBox)e.Row.FindControl("txtUOM")).Visible = false;
                ((Label)e.Row.FindControl("lblUOM")).Visible = true;
                ((TextBox)e.Row.FindControl("txtBudget")).Visible = false;
                ((Label)e.Row.FindControl("lblBudget")).Visible = true;
                ((DropDownList)e.Row.FindControl("ddlCurrency")).Visible = false;
                ((Label)e.Row.FindControl("lblCurrency")).Visible = true;
                ((TextBox)e.Row.FindControl("txtPurpose")).Visible = false;
                ((Label)e.Row.FindControl("lblPurpose")).Visible = true;
                ((TextBox)e.Row.FindControl("txtRemarks")).Visible = false;
                ((Label)e.Row.FindControl("lblRemarks")).Visible = true;

                if (((TextBox)e.Row.FindControl("txtRemarks")).Text == "")
                {
                    ((Label)e.Row.FindControl("lblRemarks")).Text = "N/A";
                }

                //HIDE ADD SHOW DELETE BUTTON
                ((LinkButton)e.Row.FindControl("lbtnAddItem")).Visible = false;
                ((LinkButton)e.Row.FindControl("lbtnDeleteItem")).Visible = true;
                e.Row.BackColor = System.Drawing.Color.Green;
            }

            // Check if it is the 31st row (index 30)
            if (e.Row.RowIndex >= 30)
            {
                //Hide row greater than 30
                e.Row.Visible = false;
            }

        }

        Update_GVHeader();
    }
    
    protected void gvDetailsViewOnly_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (hdnStatus.Value.ToLower() == "in-process" || hdnStatus.Value.ToLower() == "partially closed" || hdnStatus.Value.ToLower() == "closed")
        {
            gvDetailsViewOnly.Columns[1].Visible = true;
            gvDetailsViewOnly.Columns[10].Visible = true;
            gvDetailsViewOnly.Columns[11].Visible = true;
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (((HiddenField)e.Row.FindControl("hdnValid")).Value == "valid")
            {
                if (((Label)e.Row.FindControl("lblRemarks")).Text == "")
                {
                    ((Label)e.Row.FindControl("lblRemarks")).Text = "N/A";
                }
            }
            // Check if it is the 31st row (index 30)
            if (e.Row.RowIndex >= 30)
            {
                //Hide row greater than 30
                e.Row.Visible = false;
            }
            //Change color per status
            DataRowView drv = e.Row.DataItem as DataRowView;
            if (drv["Status"].ToString().Equals("In-process"))
            {
                e.Row.Cells[11].BackColor = System.Drawing.ColorTranslator.FromHtml("#e64a02");
            }
            else if (drv["Status"].ToString().Equals("Partially closed"))
            {
                e.Row.Cells[11].BackColor = System.Drawing.ColorTranslator.FromHtml("#cc0000");

            }
        }

        Update_GVHeader();
    }
    protected void Update_GVHeader()
    {
        if (gvRFQ_List.Rows.Count > 0)
        {
            gvRFQ_List.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        if (gvDetailsViewOnly.Rows.Count > 0)
        {
            gvDetailsViewOnly.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        uPnlRequestorInfo.Update();
        upnlDetailsOfReq.Update();
        uPnlApprover.Update();
        upnlReqAttach.Update();
        upnlLogs.Update();
        upnlActionButton.Update();
    }
    protected void OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataRowView drv = e.Row.DataItem as DataRowView;
            if (drv["Status"].ToString().Equals("Awaiting Approval"))
            {
                e.Row.Cells[3].BackColor = System.Drawing.ColorTranslator.FromHtml("#e64a02");
            }
            else if (drv["Status"].ToString().Equals("Denied"))
            {
                e.Row.Cells[3].BackColor = System.Drawing.ColorTranslator.FromHtml("#cc0000");
            }
            else if (drv["Status"].ToString().Equals("Approved"))
            {
                e.Row.Cells[3].BackColor = System.Drawing.ColorTranslator.FromHtml("#004a0a");
            }
        }

    }
    protected Boolean Bulk_Insert_RFQ_Approvers()
    {
        bool result = false;
        DataTable dt = new DataTable();
        try
        {
            dt.Columns.AddRange(new DataColumn[5] { new DataColumn("RefID", typeof(string)),
                                                    new DataColumn("Position", typeof(string)),
                                                    new DataColumn("EmpNo",typeof(string)),
                                                    new DataColumn("EmpName",typeof(string)),
                                                    new DataColumn("Attribute1", typeof(string))});
            
            Label lblPosition;
            DropDownList ddlApprover;
            for (int i = 0; gvApproval.Rows.Count > i; i++)
            {
                lblPosition = (Label)gvApproval.Rows[i].FindControl("lblPosition");
                ddlApprover = (DropDownList)gvApproval.Rows[i].FindControl("ddlApprover");

                string RefID = hdnRefID.Value;
                string Position = lblPosition.Text;
                string EmpNo = ddlApprover.SelectedValue;
                string EmpName = ddlApprover.SelectedItem.Text;
                string Attribute1 = i.ToString();
                dt.Rows.Add(RefID, Position, EmpNo, EmpName, Attribute1);


            }
            if (dt.Rows.Count > 0)
            {
                using (SqlConnection con = new SqlConnection(cl_DBConn.MSSQLTrans()))
                {
                    using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(con))
                    {
                        sqlBulkCopy.DestinationTableName = "dbo.tbl_RFQ_Approvers";

                        sqlBulkCopy.ColumnMappings.Add("RefID", "RefID");
                        sqlBulkCopy.ColumnMappings.Add("Position", "Position");
                        sqlBulkCopy.ColumnMappings.Add("EmpNo", "EmpNo");
                        sqlBulkCopy.ColumnMappings.Add("EmpName", "EmpName");
                        sqlBulkCopy.ColumnMappings.Add("Attribute1", "Attribute1");
                        con.Open();
                        sqlBulkCopy.WriteToServer(dt);
                        con.Close();
                    }
                }
            }
            result = true;
        }
        catch
        {
            //
        }
        return result;
    }


    #region Controls and Style methods
    private void RFQNoStyle()
    {
        txtControlNo.ReadOnly = true;
        txtControlNo.BackColor = System.Drawing.Color.Green;
        txtControlNo.BorderColor = System.Drawing.Color.Green;
        txtControlNo.ForeColor = System.Drawing.Color.White;
    }
    private void StatusStyleOrange()
    {
        txtReqStatus.ReadOnly = true;
        txtReqStatus.BackColor = System.Drawing.ColorTranslator.FromHtml("#e64a02");
        txtReqStatus.BorderColor = System.Drawing.ColorTranslator.FromHtml("#e64a02");
        txtReqStatus.ForeColor = System.Drawing.Color.White;
    }
    private void StatusStyleRed()
    {
        txtReqStatus.ReadOnly = true;
        txtReqStatus.BackColor = System.Drawing.ColorTranslator.FromHtml("#cc0000");
        txtReqStatus.BorderColor = System.Drawing.ColorTranslator.FromHtml("#cc0000");
        txtReqStatus.ForeColor = System.Drawing.Color.White;
    }
    private void ButtonsForSubmission()
    {
        lbtnEditApprover.Visible = true;
        lbtnSave.Visible = false;
        lbtnUpdate.Visible = true;
        lbtnSubmit.Visible = true;
    }
    private void ButtonsForResubmit()
    {
        lbtnReSubmit.Visible = false;
        lbtnSave.Visible = false;
        lbtnUpdate.Visible = true;
        lbtnSubmit.Visible = true;
        lbtnEditApprover.Visible = true;
    }
    private void ButtonsForUpdate()
    {
        lblNote.Visible = true;
        lbtnEditApprover.Visible = true;
        lbtnSaveUpdate.Visible = true;
        lbtnCancelUpdate.Visible = true;
        lbtnUpdate.Visible = false;
        lbtnSubmit.Visible = false; 
        lbtnSave.Visible = false;

    }
    private void ButtonsForAssignBuyer()
    {
        lbtnSaveBuyer.Visible = true;
        lbtnCancelUpdate.Visible = true;
        lbtnAssignBuyer.Visible = false;
        lbtnSave.Visible = false;
    }
    private void ButtonsForApproval()
    {
        lbtnEditApprover.Visible = false;
        lbtnSaveUpdate.Visible = false;
        lbtnCancelUpdate.Visible = false;
        lbtnUpdate.Visible = false;
        lbtnSubmit.Visible = false;
        lbtnSave.Visible = false;

        if (isRequestor)
        {
            lbtnApprove.Visible = false;
            lbtnDeny.Visible = false;
        }
        else if (isAdmin)
        {
            lbtnApprove.Visible = true;
            lbtnDeny.Visible = true;
        }

        Label lblPosition = new Label();
        HiddenField hdnApprover_EmpNo = new HiddenField();
        Label lblStatus = new Label();
        foreach (GridViewRow row in gvApproval1.Rows)
        {
            lblPosition = (Label)gvApproval1.Rows[row.RowIndex].FindControl("lblPosition");
            hdnApprover_EmpNo = (HiddenField)gvApproval1.Rows[row.RowIndex].FindControl("hdnApprover_EmpNo");
            lblStatus = (Label)gvApproval1.Rows[row.RowIndex].FindControl("lblStatus");

            if (lblPosition.Text == "Approver1" && hdnApprover_EmpNo.Value == hdnUserEmpNo.Value && lblStatus.Text == "Awaiting Approval")
            {
                lbtnApprove.Visible = true;
                lbtnDeny.Visible = true;
            }
            else if (lblPosition.Text == "Approver2" && hdnApprover_EmpNo.Value == hdnUserEmpNo.Value && lblStatus.Text == "Awaiting Approval")
            {
                lbtnApprove.Visible = true;
                lbtnDeny.Visible = true;
            }
            else if (lblPosition.Text == "3rd Approver (MPD Approver)" && isMPDApprover && lblStatus.Text == "Awaiting Approval")
            {
                lbtnApprove.Visible = true;
                lbtnDeny.Visible = true;
            }
        }

    }
    private void UnlockBuyerIncharge() 
    {
        DropDownList ddlBuyer = new DropDownList();
        foreach (GridViewRow row in gvRFQ_List.Rows)
        {
            ddlBuyer = (DropDownList)gvRFQ_List.Rows[row.RowIndex].FindControl("ddlBuyer");

            if (isMPDApprover || isAdmin)
            {
                ddlBuyer.Enabled = true;
            }
        }
    }
    private void ReassignNewBuyer()
    {
        int gvlastRow = gvRFQ_List.Rows.Count;
        gvRFQ_List.Rows[gvlastRow - 1].Visible = false;
        txtRequestor_LocalNo.Enabled = false;
        lbtnSaveNewBuyer.Visible = true;
        lbtnCancelUpdate.Visible = true;
        lbtnReAssignBuyer.Visible = false;
        gvRFQ_List.Columns[9].Visible = true;
        gvRFQ_List.Columns[10].Visible = false;

    }

    private void UnlockRFQDetails()
    {
        txtRequestor_LocalNo.Enabled = true;
        ddlLocation.Enabled = true;
        ddlCategory.Enabled = true;
        divFileUpload.Visible = true;

        //details of request view only
        gvRFQ_List.Visible = true;
        gvDetailsViewOnly.Visible = false;

        //Attachment 
        divUploadAttach.Visible = true;
        lblAtt.Visible = true;
        lblAtNote.Visible = true;
        LinkButton lbtnDelete = new LinkButton();
        foreach (GridViewRow row in gvAttachment.Rows)
        {
            lbtnDelete = (LinkButton)gvAttachment.Rows[row.RowIndex].FindControl("lbtnDelete");
            lbtnDelete.Visible = true;
        }
    }
    private void LockRFQDetails()
    {
        txtRequestor_LocalNo.Enabled = false;
        ddlLocation.Enabled = false;
        ddlCategory.Enabled = false;
        divFileUpload.Visible = false;

        //details of request view only
        gvRFQ_List.Visible = false;
        gvDetailsViewOnly.Visible = true;

        //Attachment 
        divUploadAttach.Visible = false;
        lblAtt.Visible = false;
        lblAtNote.Visible = false;
        LinkButton lbtnDelete = new LinkButton();
        foreach (GridViewRow row in gvAttachment.Rows)
        {
            lbtnDelete = (LinkButton)gvAttachment.Rows[row.RowIndex].FindControl("lbtnDelete");
            lbtnDelete.Visible = false;
        }
    }
    private void Reassign()
    {
        txtRequestor_LocalNo.Enabled = false;
        ddlLocation.Enabled = false;
        ddlCategory.Enabled = false;
        divFileUpload.Visible = false;

        //details of request view only
        gvRFQ_List.Visible = true;
        gvDetailsViewOnly.Visible = false;

        //Attachment 
        divUploadAttach.Visible = false;
        lblAtt.Visible = false;
        lblAtNote.Visible = false;
        LinkButton lbtnDelete = new LinkButton();
        foreach (GridViewRow row in gvAttachment.Rows)
        {
            lbtnDelete = (LinkButton)gvAttachment.Rows[row.RowIndex].FindControl("lbtnDelete");
            lbtnDelete.Visible = false;
        }
    }
    //private void AutomaticClosedRequest()
    //{
    //    Check status per item to close request
    //    Label lblStatus;
    //    int rowsCount = gvDetailsViewOnly.Rows.Count - 1;
    //    int partiallyClosedCount = 0;
    //    for (int i = 0; rowsCount > i; i++)
    //    {
    //        lblStatus = (Label)gvDetailsViewOnly.Rows[i].FindControl("lblStatus");

    //        if (lblStatus.Text.ToLower() == "partially closed")
    //        {
    //            partiallyClosedCount++;
    //        }
    //    }
    //    if (partiallyClosedCount == rowsCount)
    //    {
    //        trans.RefID = hdnRefID.Value;
    //        trans.CurrentUser = hdnUserEmpName.Value;

    //        string result = dl.Automatic_Closed(cl_ProvideFactory.getSqlFactory(), cl_DBConn.MSSQLTrans(), trans);
    //        if (result.ToLower() != "request timeout")
    //        {
    //            if (result.ToLower().Contains("completed"))
    //            {
    //                AlertMessage.Show("Information", "Information:", result);
    //                hdnAction.Value = "refresh";
    //            }
    //        }
    //        else
    //        {
    //            AlertMessage.Show("Warning", "Information:", "Transaction Failed: Request Timeout!");
    //            hdnAction.Value = "alert";
    //        }
    //        upnlDetailsOfReq.Update();
    //        Update_GVHeader();
    //    }


    //}

    #endregion

    #region Load methods
    private void Load_Transaction(string transType, string refID)
    {

        lblNote.Text = " Note: <br> 1. Row/s in <b>GREEN</b> is valid for submission. <br> 2. Kindly remember to upload any relevant files or documents before proceeding with manual input. <br>" +
                           " 3. Uploading new files or documents will overwrite any existing record. If you proceed with the upload, the current data will be replaced. ";
     
        DataSet ds = new DataSet();
        try
        {
            ds = dl.Select_Details_Of_Request(cl_ProvideFactory.getSqlFactory(), cl_DBConn.MSSQLTrans(), transType, refID, "", "", "");

            DataTable dtRFQTransaction = ds.Tables[0]; //Transaction Details
            DataTable dt_DetailOfRequest = ds.Tables[1]; //Details Of Request
            
            if (dtRFQTransaction != null && dtRFQTransaction.Rows.Count > 0)
            {
                txtControlNo.Text = !string.IsNullOrEmpty(dtRFQTransaction.Rows[0]["RFQControlNo"].ToString()) ? dtRFQTransaction.Rows[0]["RFQControlNo"].ToString() : "";
                hdnReqEmpNo.Value = !string.IsNullOrEmpty(dtRFQTransaction.Rows[0]["RequestorEmpNo"].ToString()) ? dtRFQTransaction.Rows[0]["RequestorEmpNo"].ToString() : "";
                txtRequestor_EmpName.Text = !string.IsNullOrEmpty(dtRFQTransaction.Rows[0]["RequestorName"].ToString()) ? dtRFQTransaction.Rows[0]["RequestorName"].ToString() : "";
                txtRequestor_Dept.Text = !string.IsNullOrEmpty(dtRFQTransaction.Rows[0]["Dept"].ToString()) ? dtRFQTransaction.Rows[0]["Dept"].ToString() : "";
                txtRequestor_LocalNo.Text = !string.IsNullOrEmpty(dtRFQTransaction.Rows[0]["LocalNo"].ToString()) ? dtRFQTransaction.Rows[0]["LocalNo"].ToString() : "";
                ddlLocation.SelectedValue = !string.IsNullOrEmpty(dtRFQTransaction.Rows[0]["Location"].ToString()) ? dtRFQTransaction.Rows[0]["Location"].ToString() : "";
                ddlCategory.Text = !string.IsNullOrEmpty(dtRFQTransaction.Rows[0]["Category"].ToString()) ? dtRFQTransaction.Rows[0]["Category"].ToString() : "";
                txtReqStatus.Text = !string.IsNullOrEmpty(dtRFQTransaction.Rows[0]["Status"].ToString()) ? dtRFQTransaction.Rows[0]["Status"].ToString() : "";
                hdnStatus.Value = !string.IsNullOrEmpty(dtRFQTransaction.Rows[0]["Status"].ToString()) ? dtRFQTransaction.Rows[0]["Status"].ToString() : "";
                
            }
            if (dt_DetailOfRequest != null && dt_DetailOfRequest.Rows.Count > 0)
            {

                dtRFQ_Request.Rows.Clear();
                dtRFQ_Request.AcceptChanges();
                //TODO
                for (int i = 0; i <= dt_DetailOfRequest.Rows.Count; i++)
                {
                    DataRow r = dtRFQ_Request.NewRow();

                    if (i < dt_DetailOfRequest.Rows.Count)
                    {
                        r["ControlNo"] = dt_DetailOfRequest.Rows[i]["TempItemCode"].ToString();
                        r["ItemDescription"] = dt_DetailOfRequest.Rows[i]["ItemDescription"].ToString();
                        r["TempItemCode"] = dt_DetailOfRequest.Rows[i]["TempItemCode"].ToString();
                        r["Quantity"] = dt_DetailOfRequest.Rows[i]["Quantity"].ToString();
                        r["UOM"] = dt_DetailOfRequest.Rows[i]["UOM"].ToString();
                        r["Budget"] = dt_DetailOfRequest.Rows[i]["Budget"].ToString();
                        r["Currency"] = dt_DetailOfRequest.Rows[i]["Currency"].ToString();
                        r["Purpose"] = dt_DetailOfRequest.Rows[i]["Purpose"].ToString();
                        r["Remarks"] = dt_DetailOfRequest.Rows[i]["Remarks"].ToString();
                        r["BuyerInCharge"] = dt_DetailOfRequest.Rows[i]["BuyerInCharge"].ToString();
                        r["BuyerInChargeEmpNo"] = dt_DetailOfRequest.Rows[i]["BuyerInChargeEmpNo"].ToString();
                        r["Status"] = dt_DetailOfRequest.Rows[i]["Status"].ToString();
                        r["Valid"] = "valid";
                    }
                    else
                    {
                        r["ControlNo"] = "ITM" + DateTime.Now.ToString("yyyyMMddHHmmss");
                        r["ItemDescription"] = "";
                        r["TempItemCode"] = "ITM" + DateTime.Now.ToString("yyyyMMddHHmmss");
                        r["Quantity"] = "";
                        r["UOM"] = "";
                        r["Budget"] = "";
                        r["Currency"] = "";
                        r["Purpose"] = "";
                        r["Remarks"] = "";
                        r["BuyerInCharge"] = "";
                        r["BuyerInChargeEmpNo"] = "";
                        r["Status"] = "";
                        r["Valid"] = "";
                    }

                    dtRFQ_Request.Rows.Add(r);
                    dtRFQ_Request.AcceptChanges();
                }
                
                //LinkButton lbtnReassignBuyer = new LinkButton();
                //LinkButton lbtnDeleteItem = new LinkButton();
                //foreach (GridViewRow row in gvRFQ_List.Rows)
                //{
                //    lbtnReassignBuyer = (LinkButton)gvRFQ_List.Rows[row.RowIndex].FindControl("lbtnReassignBuyer");
                //    lbtnDeleteItem = (LinkButton)gvRFQ_List.Rows[row.RowIndex].FindControl("lbtnDeleteItem");

                //    if (status == "")
                //    {
                //        lbtnReassignBuyer.Visible = true;
                //        lbtnDeleteItem.Visible = false;
                //    }
                //    else
                //    {
                //        lbtnReassignBuyer.Visible = false;
                //        lbtnDeleteItem.Visible = false;
                //    }
                //}
              
               
                Load_Details(dtRFQ_Request);
                if (isAdmin || isMPDApprover || isBuyer)
                {
                    lbtnExport.Visible = true;
                }
            }

            else
            {
                AlertMessage.Show("warning", "Information:", "Records not found.");
                hdnAction.Value = "redirect";
            }


        }
        catch (Exception ex)
        {
            AlertMessage.Show("warning", "Error:", "Error Loading: " + ex.Message);
            hdnAction.Value = "alert";
        }
        finally
        {
            upnlDetailsOfReq.Update();
            upnlReqAttach.Update();
            upnlLogs.Update();
            upnlActionButton.Update();
            upnlLogs.Update();
        }


        Load_Attachment();
        Load_RFQ_Approvers();
        divLogs.Visible = true;
        Load_HistoryLogs(hdnRefID.Value);

        LockRFQDetails();

        RFQNoStyle();
        
        //Per Status
        if (hdnStatus.Value.ToLower() == "for submission")
        {
            if (hdnUserEmpName.Value == txtRequestor_EmpName.Text || isAdmin)
            {
                ButtonsForSubmission();
            }
            lbtnSave.Visible = false;
            divApprover2.Visible = true;
            divApprover1.Visible = false;
            upnlDetailsOfReq.Update();
            StatusStyleOrange();
        }
        else if (hdnStatus.Value.ToLower() == "for approval" || hdnStatus.Value.ToLower() == "for mpd approval")
        {
            ButtonsForApproval();
            divApprover2.Visible = true;
            divApprover1.Visible = false;
            upnlDetailsOfReq.Update();
            StatusStyleOrange();
        }
        else if (hdnStatus.Value == "Open")
        {
            lbtnSave.Visible = false;
            divApprover2.Visible = true;
            divApprover1.Visible = false;
            upnlDetailsOfReq.Update();
            StatusStyleOrange();

            if (isAdmin || isMPDApprover)
            {
                lbtnAssignBuyer.Visible = true;
            }
        }
        else if (hdnStatus.Value.ToLower() == "denied")
        {
            ButtonsForApproval();
            divApprover2.Visible = true;
            divApprover1.Visible = false;
            upnlDetailsOfReq.Update();
            StatusStyleRed();
            if (txtRequestor_EmpName.Text == hdnUserEmpName.Value || isAdmin)
            {
                if (isAdmin)
                {
                    lbtnApprove.Visible = false;
                    lbtnDeny.Visible = false;
                    lbtnReSubmit.Visible = true;
                }
                else
                {
                    lbtnReSubmit.Visible = true;
                }

            }
        }
        else if (hdnStatus.Value.ToLower() == "in-process")
        {
            lbtnSave.Visible = false;
            divApprover2.Visible = true;
            divApprover1.Visible = false;
            upnlDetailsOfReq.Update();
            StatusStyleOrange();

        }
        else if (hdnStatus.Value.ToLower() == "partially closed")
        {
            lbtnSave.Visible = false;
            divApprover2.Visible = true;
            divApprover1.Visible = false;
            StatusStyleOrange();

            //AutomaticClosedRequest();
            upnlDetailsOfReq.Update();
            uPnlRequestorInfo.Update();
        }
        else if (hdnStatus.Value.ToLower() == "closed")
        {
            lbtnSave.Visible = false;
            divApprover2.Visible = true;
            divApprover1.Visible = false;
            upnlDetailsOfReq.Update();
            StatusStyleRed();

        }

        if (isAdmin)
        {
            if (hdnStatus.Value.ToLower() == "for submission" || hdnStatus.Value.ToLower() == "for approval") 
            {
                lbtnEditApprover.Visible = true;
            }
            else if (hdnStatus.Value.ToLower() == "in-process" || hdnStatus.Value.ToLower() == "partially closed")
            {
                lbtnReAssignBuyer.Visible = true;
            }
        }
    }
    private void Load_RFQ_Approvers()
    {
        DataTable dtTransApprover = new DataTable();
        dtTransApprover = dl.get_RFQ_Transaction_Approvers(cl_ProvideFactory.getSqlFactory(), cl_DBConn.MSSQLTrans(), "all approver", hdnRefID.Value);
        gvApproval1.DataSource = dtTransApprover;
        gvApproval1.DataBind();
        if (gvApproval1.Rows.Count > 0)
        {
            gvApproval1.HeaderRow.TableSection = TableRowSection.TableHeader;

            Label lblPosition;
            Label lblPosition1;
            Label lblPosition_App1;
            DropDownList ddlApprover;
            Label lblApprover;
            Label lblApprover1;
            HiddenField hdnApprover_EmpNo;
            HiddenField hdnDepCode;
            HiddenField hdnDepartment;
            Label lblApprover_gvApprovers1;

            for (int i = 0; gvApproval1.Rows.Count > i; i++)
            {

                lblPosition = (Label)gvApproval1.Rows[i].FindControl("lblPosition");
                lblPosition1 = (Label)gvApproval1.Rows[i].FindControl("lblPosition1");
                lblPosition_App1 = (Label)gvApproval1.Rows[i].FindControl("lblPosition");
                ddlApprover = (DropDownList)gvApproval1.Rows[i].FindControl("ddlApprover");
                lblApprover = (Label)gvApproval1.Rows[i].FindControl("lblApprover");
                lblApprover1 = (Label)gvApproval1.Rows[i].FindControl("lblApprover1");
                hdnApprover_EmpNo = (HiddenField)gvApproval1.Rows[i].FindControl("hdnApprover_EmpNo");
                lblApprover_gvApprovers1 = (Label)gvApproval1.Rows[i].FindControl("lblApprover");
                hdnDepCode = new HiddenField();
                hdnDepartment = new HiddenField();

                if (lblPosition.Text == "Approver1" || lblPosition.Text == "Approver2")
                {
                    cl_Common.getApprovers(lblPosition.Text, cl_Common.getDepCode(hdnReqEmpNo.Value), ddlApprover, "LastEmpName", "EmpNo");

                    if (lblPosition.Text == "Approver1")
                    {
                        lblPosition.Visible = false;
                        lblPosition1.Visible = true;
                        lblPosition1.Text = "1st Approver (Department SV/Manager)";
                    }
                    else if (lblPosition.Text == "Approver2")
                    {
                        lblPosition.Visible = false;
                        lblPosition1.Visible = true;
                        lblPosition1.Text = "2nd Approver (Manager/Department Head)";
                    }
                }

                if (lblPosition.Text == "Approver1" || lblPosition.Text == "Approver2")
                {
                    if (string.IsNullOrEmpty(hdnApprover_EmpNo.Value) || hdnApprover_EmpNo.Value == "0")
                    {
                        ddlApprover.SelectedValue = "0";
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(lblApprover_gvApprovers1.Text))
                        {
                            if (!ddlApprover.Items.Contains(new ListItem(lblApprover_gvApprovers1.Text, hdnApprover_EmpNo.Value)))
                            {
                                ddlApprover.Items.Insert(1, new ListItem(lblApprover_gvApprovers1.Text, hdnApprover_EmpNo.Value));
                                ddlApprover.SelectedValue = hdnApprover_EmpNo.Value;
                            }
                            else
                            {
                                ddlApprover.SelectedValue = hdnApprover_EmpNo.Value;
                            }
                        }
                        else
                        {
                            if (!ddlApprover.Items.Contains(new ListItem(hdnApprover_EmpNo.Value, hdnApprover_EmpNo.Value)))
                            {
                                ddlApprover.Items.Insert(1, new ListItem(hdnApprover_EmpNo.Value, hdnApprover_EmpNo.Value));
                                ddlApprover.SelectedValue = hdnApprover_EmpNo.Value;
                            }
                            else
                            {
                                ddlApprover.SelectedValue = hdnApprover_EmpNo.Value;
                            }
                        }
                    }
                }

                if (lblPosition.Text == "Approver1" || lblPosition.Text == "Approver2")
                {
                    if (string.IsNullOrEmpty(hdnApprover_EmpNo.Value) || hdnApprover_EmpNo.Value == "0")
                    {
                        ddlApprover.SelectedValue = "0";
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(lblApprover_gvApprovers1.Text))
                        {
                            if (!ddlApprover.Items.Contains(new ListItem(lblApprover_gvApprovers1.Text, hdnApprover_EmpNo.Value)))
                            {
                                ddlApprover.Items.Insert(1, new ListItem(lblApprover_gvApprovers1.Text, hdnApprover_EmpNo.Value));
                                ddlApprover.SelectedValue = hdnApprover_EmpNo.Value;
                            }
                            else
                            {
                                ddlApprover.SelectedValue = hdnApprover_EmpNo.Value;
                            }
                        }
                        else
                        {
                            if (!ddlApprover.Items.Contains(new ListItem(hdnApprover_EmpNo.Value, hdnApprover_EmpNo.Value)))
                            {
                                ddlApprover.Items.Insert(1, new ListItem(hdnApprover_EmpNo.Value, hdnApprover_EmpNo.Value));
                                ddlApprover.SelectedValue = hdnApprover_EmpNo.Value;
                            }
                            else
                            {
                                ddlApprover.SelectedValue = hdnApprover_EmpNo.Value;
                            }
                        }
                    }
                }
            }
        }


    }
    private void Load_NewRFQ_Approver(string transType, string keyword, GridView gvApprovers, GridView gvApprovers1)
    {
        //Details.Portal.EMP emp = new Details.Portal.EMP(cl_DBConn.MSSQLEmp(), cl_Login.hasAccess(hdnAccessor.Value));
        Details.Portal.EMP emp = new Details.Portal.EMP(cl_DBConn.MSSQLEmp(), cl_Identity.Get_UserID());
        DataTable dtRFQApprover = new DataTable();
        dtRFQApprover = dl.get_RFQ_Approvers(cl_ProvideFactory.getSqlFactory(), cl_DBConn.MSSQLTrans(), transType, keyword);
        gvApprovers.DataSource = dtRFQApprover;
        gvApprovers.DataBind();
        if (gvApprovers.Rows.Count > 0)
        {
            gvApprovers.HeaderRow.TableSection = TableRowSection.TableHeader;


            Label lblPosition;
            Label lblPosition1;
            Label lblPosition_App1;
            DropDownList ddlApprover;
            DropDownList ddlApprover1;
            Label lblApprover;
            Label lblApprover1;
            HiddenField hdnApprover_EmpNo;
            HiddenField hdnDepCode;
            HiddenField hdnDepartment;
            Label lblApprover_gvApprovers1;

            if (gvApprovers.Rows.Count > 0 && gvApprovers1.Rows.Count > 0)
            {
                for (int i = 0; gvApprovers.Rows.Count > i; i++)
                {
                    lblPosition = (Label)gvApprovers.Rows[i].FindControl("lblPosition");
                    lblPosition1 = (Label)gvApprovers.Rows[i].FindControl("lblPosition1");
                    lblPosition_App1 = (Label)gvApprovers1.Rows[i].FindControl("lblPosition");
                    ddlApprover = (DropDownList)gvApprovers.Rows[i].FindControl("ddlApprover");
                    ddlApprover1 = (DropDownList)gvApprovers1.Rows[i].FindControl("ddlApprover");
                    lblApprover = (Label)gvApprovers.Rows[i].FindControl("lblApprover");
                    lblApprover1 = (Label)gvApprovers.Rows[i].FindControl("lblApprover1");
                    hdnApprover_EmpNo = (HiddenField)gvApprovers1.Rows[i].FindControl("hdnApprover_EmpNo");
                    lblApprover_gvApprovers1 = (Label)gvApprovers1.Rows[i].FindControl("lblApprover");
                    hdnDepCode = new HiddenField();
                    hdnDepartment = new HiddenField();



                    if (transType == "initial")
                    {
                        hdnDepCode.Value = emp.DepCode;
                        hdnDepartment = hdnUserDepartment;
                    }
                    if (lblPosition.Text == "Approver1" || lblPosition.Text == "Approver2")
                    {
                        cl_Common.getApprovers(lblPosition.Text, hdnDepCode.Value, ddlApprover, "LastEmpName", "EmpNo");


                        if (lblPosition.Text == "Approver1")
                        {
                            lblPosition.Visible = false;
                            lblPosition1.Visible = true;
                            lblPosition1.Text = "1st Approver (Department SV/Manager)";
                        }
                        else if (lblPosition.Text == "Approver2")
                        {
                            lblPosition.Visible = false;
                            lblPosition1.Visible = true;
                            lblPosition1.Text = "2nd Approver (Manager/Department Head)";
                        }
                    }

                    if (lblPosition.Text == "Approver1" || lblPosition.Text == "Approver2")
                    {
                        if (string.IsNullOrEmpty(hdnApprover_EmpNo.Value) || hdnApprover_EmpNo.Value == "0")
                        {
                            ddlApprover.SelectedValue = "0";
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(lblApprover_gvApprovers1.Text))
                            {
                                if (!ddlApprover.Items.Contains(new ListItem(lblApprover_gvApprovers1.Text, hdnApprover_EmpNo.Value)))
                                {
                                    ddlApprover.Items.Insert(1, new ListItem(lblApprover_gvApprovers1.Text, hdnApprover_EmpNo.Value));
                                    ddlApprover.SelectedValue = hdnApprover_EmpNo.Value;
                                }
                                else
                                {
                                    ddlApprover.SelectedValue = hdnApprover_EmpNo.Value;
                                }
                            }
                            else
                            {
                                if (!ddlApprover.Items.Contains(new ListItem(hdnApprover_EmpNo.Value, hdnApprover_EmpNo.Value)))
                                {
                                    ddlApprover.Items.Insert(1, new ListItem(hdnApprover_EmpNo.Value, hdnApprover_EmpNo.Value));
                                    ddlApprover.SelectedValue = hdnApprover_EmpNo.Value;
                                }
                                else
                                {
                                    ddlApprover.SelectedValue = hdnApprover_EmpNo.Value;
                                }
                            }
                        }
                    }

                    if (lblPosition.Text == "Approver1" || lblPosition.Text == "Approver2")
                    {
                        if (string.IsNullOrEmpty(hdnApprover_EmpNo.Value) || hdnApprover_EmpNo.Value == "0")
                        {
                            ddlApprover.SelectedValue = "0";
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(lblApprover_gvApprovers1.Text))
                            {
                                if (!ddlApprover.Items.Contains(new ListItem(lblApprover_gvApprovers1.Text, hdnApprover_EmpNo.Value)))
                                {
                                    ddlApprover.Items.Insert(1, new ListItem(lblApprover_gvApprovers1.Text, hdnApprover_EmpNo.Value));
                                    ddlApprover.SelectedValue = hdnApprover_EmpNo.Value;
                                }
                                else
                                {
                                    ddlApprover.SelectedValue = hdnApprover_EmpNo.Value;
                                }
                            }
                            else
                            {
                                if (!ddlApprover.Items.Contains(new ListItem(hdnApprover_EmpNo.Value, hdnApprover_EmpNo.Value)))
                                {
                                    ddlApprover.Items.Insert(1, new ListItem(hdnApprover_EmpNo.Value, hdnApprover_EmpNo.Value));
                                    ddlApprover.SelectedValue = hdnApprover_EmpNo.Value;
                                }
                                else
                                {
                                    ddlApprover.SelectedValue = hdnApprover_EmpNo.Value;
                                }
                            }
                        }
                    }

                }
            }
        }
    }
    private void Load_Details(DataTable dtRFQ_Request)
    {
        gvRFQ_List.DataSource = dtRFQ_Request;
        gvRFQ_List.DataBind();

        gvDetailsViewOnly.DataSource = dtRFQ_Request;
        gvDetailsViewOnly.DataBind();
        
        

        int lastRow = gvDetailsViewOnly.Rows.Count;
        gvDetailsViewOnly.Rows[lastRow-1].Visible = false;

        //NUMBER OF REQUEST
        string counts = gvRFQ_List.Rows.Count.ToString();
        int numOfReq = Int32.Parse(counts);
        int dif = numOfReq - 1;
        if (dif == 0 || txtNumOfReq.Text == "0")
        {
            txtNumOfReq.Text = "--Auto Generated--";
        }
        else
        {
            txtNumOfReq.Text = dif.ToString();
        }
    }
    private void Load_RFQ_Upload_Details(string TransType)
    {
        dtRFQ_Request.Rows.Clear();
        if (TransType.ToLower() == "upload excel file" && fuRFQList.HasFile)
        {
            DataTable dtNew = new DataTable();
            dtNew.Columns.Add("ID", typeof(string));
            dtNew.Columns.Add("ItemDescription", typeof(string));
            dtNew.Columns.Add("Quantity", typeof(string));
            dtNew.Columns.Add("UOM", typeof(string));
            dtNew.Columns.Add("Budget", typeof(string));
            dtNew.Columns.Add("Currency", typeof(string));
            dtNew.Columns.Add("Purpose", typeof(string));
            dtNew.Columns.Add("Remarks", typeof(string));
            dtNew.Columns.Add("TempItemCode", typeof(string));
            dtNew.Columns.Add("ControlNo", typeof(string));
            dtNew.Columns.Add("Valid", typeof(string));
            dtNew.Columns.Add("BuyerInCharge", typeof(string));
            dtNew.Columns.Add("BuyerInChargeEmpNo", typeof(string));
            dtNew.Columns.Add("Status", typeof(string));
            dtNew.AcceptChanges();

            try
            {
                if (!Directory.Exists(Server.MapPath("~/FileTemp")))
                    Directory.CreateDirectory(Server.MapPath("~") + @"/FileTemp");

                string strFilePath = Server.MapPath("~/FileTemp/");
                string strFileName = DateTime.Now.ToString("MMddyyyyHHmmss");
                fuRFQList.SaveAs(strFilePath + strFileName);

                FileInfo fileToExport = new FileInfo(strFilePath + strFileName);
                using (ExcelPackage xlPackage = new ExcelPackage(fileToExport))
                {
                    ExcelWorksheet worksheet = xlPackage.Workbook.Worksheets[1];
                    var rowCnt = worksheet.Dimension.End.Row;
                    var colCnt = worksheet.Dimension.End.Column;
                    colCnt = 10;
                    int dtRowCount = 0;
                    for (int iRow = 2; iRow <= rowCnt; iRow++)
                    {
                        int iDRCount = 0;
                        DataRow dr = dtNew.NewRow();

                        for (int iCol = 1; iCol <= colCnt + 2; iCol++)
                        {
                            if (iCol == 9 || iCol == 10)
                            {
                                if (dtRowCount == 0) dr[iDRCount] = "ITM" + DateTime.Now.ToString("yyyyMMddHHmmss");
                                else
                                {
                                    do
                                    {
                                        dr[iDRCount] = "ITM" + DateTime.Now.ToString("yyyyMMddHHmmss");
                                    } while (dr[iDRCount].ToString() == dtNew.Rows[dtRowCount - 1]["TempItemCode"].ToString());
                                }
                            }
                            else if (iCol == 11) dr[iDRCount] = "valid";
                            else if (iCol == 12) dr[iDRCount] = "";
                            else dr[iDRCount] = worksheet.Cells[iRow, iCol].Value != null ? worksheet.Cells[iRow, iCol].Value.ToString().Trim() : "";

                            iDRCount++;
                        }
                        dtNew.Rows.Add(dr);
                        dtNew.AcceptChanges();
                        dtRowCount++;
                    }
                }
            }
            catch (Exception ex)
            {

            }

            dtNew.Columns.Remove("ID");
            bool exist = false;

            int count_uom_invalid = 0;
            foreach (DataRow row in dtNew.Rows)
            {
                exist = dl.Check_Currency(cl_ProvideFactory.getSqlFactory(), cl_DBConn.MSSQLTrans(), "check currency", row["Currency"].ToString());
                if (!exist) count_uom_invalid++;
            }

            //Check Item Description invalid characters
            bool has_invalid_char = false;
            string invalid_char = "";
            foreach (DataRow row in dtNew.Rows)
            {
                char[] desc_arry = row["ItemDescription"].ToString().ToCharArray();
                string allowed_chars = ViewState[STR_ALLOWED_CHARS] as string;

                for (int i = 0; i < desc_arry.Length; i++)
                {
                    if (!allowed_chars.Contains(desc_arry[i]))
                    {
                        has_invalid_char = true;
                        invalid_char = invalid_char + "(" + desc_arry[i] + ") ";
                    }
                }
            }

            //Check number of rows
            bool greaterThanThirthy = false;
            if (dtNew.Rows.Count>30)
            {
                greaterThanThirthy = true;
            }


            // CHECK IF DESCRIPTION FOR UPLOAD EXISTS IN GRIDVIEW
            TextBox txtDesc_temp;
            string desc_exist_onGV = "";
            int desc_exist_GV_count = 0;
            foreach (DataRow row in dtNew.Rows)
            {
                //TODO:
                int GVRows_count = 0;
                foreach (GridViewRow r in gvRFQ_List.Rows)
                {
                    GVRows_count++;
                    if (GVRows_count == gvRFQ_List.Rows.Count) break;
                    txtDesc_temp = (TextBox)gvRFQ_List.Rows[r.RowIndex].FindControl("txtItemDesc");
                    if (row["ItemDescription"].ToString() == txtDesc_temp.Text)
                    {
                        desc_exist_onGV = desc_exist_onGV + "{" + txtDesc_temp.Text + "} ";
                        desc_exist_GV_count++;
                    }
                }
            }

            if (!has_invalid_char)
            {
                if (count_uom_invalid > 0)
                {
                    AlertMessage.Show("warning", "Information:", "Invalid UOM. Please check upload details.");
                    hdnAction.Value = "alert";
                    hdnGV_Action.Value = "delete item code";
                    Update_GVHeader();
                    //Response.Redirect("Registration.aspx");
                    //CopyRowsToDT_RFQList();
                    //AddRow_RFQList();
                }
                else if (greaterThanThirthy) 
                {
                    AlertMessage.Show("warning", "Information:", "Maximum of 30 rows per upload.");
                    hdnAction.Value = "alert";
                    Update_GVHeader();
                }
                else
                {
                    DataTable dtRFQ_PrevList = (DataTable)ViewState[DT_RFQ_REQUEST];
                    hdnRFQ_Source.Value = "upload";

                    DataRow row_prevList;
                    //TODO: REMOVE LAST ROW OF PREVLIST
                    int count_prev_row = 0;
                    foreach (DataRow r in dtRFQ_PrevList.Rows)
                    {
                        count_prev_row++;
                        if (count_prev_row == dtRFQ_PrevList.Rows.Count) break;
                        row_prevList = dtNew.NewRow();
                        row_prevList["ItemDescription"] = r["ItemDescription"].ToString();
                        row_prevList["Quantity"] = r["Quantity"].ToString();
                        row_prevList["UOM"] = r["UOM"].ToString();
                        row_prevList["Budget"] = r["Budget"].ToString();
                        row_prevList["Currency"] = r["Currency"].ToString();
                        row_prevList["Purpose"] = r["Purpose"].ToString();
                        row_prevList["Remarks"] = r["Remarks"].ToString();
                        row_prevList["TempItemCode"] = r["TempItemCode"].ToString();
                        row_prevList["Valid"] = r["Valid"].ToString();

                        dtNew.Rows.Add(row_prevList);
                        dtNew.AcceptChanges();
                    }

                    DataRow row = dtNew.NewRow();

                    string temp_IC_new = "";
                    bool tempIC_exist;

                    do
                    {
                        tempIC_exist = false;
                        temp_IC_new = "ITM" + DateTime.Now.ToString("yyyyMMddHHmmss");
                        foreach (DataRow rowDTNew in dtNew.Rows)
                        {
                            if (temp_IC_new == rowDTNew["TempItemCode"].ToString())
                            {
                                tempIC_exist = true;
                                break;
                            }
                        }
                    } while (tempIC_exist);

                    row["TempItemCode"] = temp_IC_new;
                    dtNew.Rows.Add(row);
                    dtRFQ_Request_Upload = dtNew.Copy();
                    ViewState[DT_RFQ_REQUEST_FOR_UPLOAD] = dtRFQ_Request_Upload;
                    ViewState[DT_RFQ_REQUEST] = dtRFQ_Request_Upload;

                    DataTable dt = dtRFQ_Request_Upload;
                    var rows = dt.AsEnumerable().Take(30);
                    DataTable dtNew1 = rows.CopyToDataTable();
                    gvRFQ_List.DataSource = dtNew1;
                    gvRFQ_List.DataBind();

                    gvDetailsViewOnly.DataSource = dtNew1;
                    gvDetailsViewOnly.DataBind();

                    //NUMBER OF REQUEST
                    string counts = gvRFQ_List.Rows.Count.ToString();
                    int numOfReq = Int32.Parse(counts);
                    int dif = numOfReq -1;
                    if (dif == 0 || txtNumOfReq.Text == "0")
                    {
                        txtNumOfReq.Text = "--Auto Generated--";
                    }
                    else
                    {
                        txtNumOfReq.Text = dif.ToString();
                    }

                }


            }
            else
            {
                AlertMessage.Show("warning", "Information:", "Uploaded description/s has invalid character/s. "
                    + invalid_char);
                hdnAction.Value = "alert";
            }
        }
    }
    private void Load_HistoryLogs(string keyword)
    {
        DataTable dtLogs = new DataTable();
        dtLogs = dl.get_RFQ_HistoryLogs(cl_ProvideFactory.getSqlFactory(), cl_DBConn.MSSQLTrans(), keyword);
        gvHistoryLogs.DataSource = dtLogs;
        gvHistoryLogs.DataBind();
        if (gvHistoryLogs.Rows.Count > 0) gvHistoryLogs.HeaderRow.TableSection = TableRowSection.TableHeader;

    }
    #endregion
    #region Export Excel
    private void ExportToExcel(string ExportTemplate)
    {
        try
        {
            string strFilename = DateTime.Now.ToString("yyyy-MM-dd") + "-" + "Report_Per_Item.xlsx";
            string strExportFile = Server.MapPath("~/FileExport/") + strFilename;

            FileInfo fileTemplate = new FileInfo(ExportTemplate);
            FileInfo fileExport = new FileInfo(strExportFile);
            using (ExcelPackage xlPackage = new ExcelPackage(fileExport, fileTemplate))
            {
                ExcelWorksheet worksheet = xlPackage.Workbook.Worksheets[1];

                int iRow = 12;
                int iCol = 1;

                DataTable dtRecords = new DataTable();
                dtRecords = dl.get_RFQ_Per_Item(cl_ProvideFactory.getSqlFactory(), cl_DBConn.MSSQLTrans(), "extract per item",hdnRefID.Value);
               
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

                    #region RFQ No
                    worksheet.Cells[iRow, iCol].Value = dr["ControlNo"].ToString();
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

                    #region ItemDescription
                    worksheet.Cells[iRow, iCol].Value = dr["ItemDescription"].ToString();
                    worksheet.Cells[iRow, iCol].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                    worksheet.Cells[iRow, iCol].Style.Font.Size = 8;
                    worksheet.Cells[iRow, iCol].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                    iCol++;
                    #endregion

                    #region Quantity
                    worksheet.Cells[iRow, iCol].Value = dr["Quantity"].ToString();
                    worksheet.Cells[iRow, iCol].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                    worksheet.Cells[iRow, iCol].Style.Font.Size = 8;
                    worksheet.Cells[iRow, iCol].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                    iCol++;
                    #endregion

                    #region UOM
                    worksheet.Cells[iRow, iCol].Value = dr["UOM"].ToString();
                    worksheet.Cells[iRow, iCol].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                    worksheet.Cells[iRow, iCol].Style.Font.Size = 8;
                    worksheet.Cells[iRow, iCol].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                    iCol++;
                    #endregion

                    #region Budget
                    worksheet.Cells[iRow, iCol].Value = dr["Budget"].ToString();
                    worksheet.Cells[iRow, iCol].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                    worksheet.Cells[iRow, iCol].Style.Font.Size = 8;
                    worksheet.Cells[iRow, iCol].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                    iCol++;
                    #endregion

                    #region Currency
                    worksheet.Cells[iRow, iCol].Value = dr["Currency"].ToString();
                    worksheet.Cells[iRow, iCol].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                    worksheet.Cells[iRow, iCol].Style.Font.Size = 8;
                    worksheet.Cells[iRow, iCol].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                    iCol++;
                    #endregion

                    #region Purpose
                    worksheet.Cells[iRow, iCol].Value = dr["Purpose"].ToString();
                    worksheet.Cells[iRow, iCol].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                    worksheet.Cells[iRow, iCol].Style.Font.Size = 8;
                    worksheet.Cells[iRow, iCol].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                    iCol++;
                    #endregion

                    #region BuyerInCharge
                    worksheet.Cells[iRow, iCol].Value = dr["BuyerInCharge"].ToString();
                    worksheet.Cells[iRow, iCol].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                    worksheet.Cells[iRow, iCol].Style.Font.Size = 8;
                    worksheet.Cells[iRow, iCol].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                    iCol++;
                    #endregion

                    #region RequestorName
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

                    #region DateInProcess
                    worksheet.Cells[iRow, iCol].Value = dr["DateInProcess"].ToString();
                    worksheet.Cells[iRow, iCol].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                    worksheet.Cells[iRow, iCol].Style.Font.Size = 8;
                    worksheet.Cells[iRow, iCol].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                    iCol++;
                    #endregion

                    #region DatePartiallyClosed
                    worksheet.Cells[iRow, iCol].Value = dr["DatePartiallyClosed"].ToString();
                    worksheet.Cells[iRow, iCol].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                    worksheet.Cells[iRow, iCol].Style.Font.Size = 8;
                    worksheet.Cells[iRow, iCol].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                    iCol++;
                    #endregion

                    #region DateClosed
                    worksheet.Cells[iRow, iCol].Value = dr["DateClosed"].ToString();
                    worksheet.Cells[iRow, iCol].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                    worksheet.Cells[iRow, iCol].Style.Font.Size = 8;
                    worksheet.Cells[iRow, iCol].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                    iCol++;
                    #endregion

                    #region DateDenied
                    worksheet.Cells[iRow, iCol].Value = dr["DateDenied"].ToString();
                    worksheet.Cells[iRow, iCol].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                    worksheet.Cells[iRow, iCol].Style.Font.Size = 8;
                    worksheet.Cells[iRow, iCol].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                    iCol++;
                    #endregion

                    iRow++;

                }
                worksheet.Name = "RFQ Database System per Item";

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
    #endregion

    #region Buttons

    protected void gvHistoryLogs_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvHistoryLogs.PageIndex = e.NewPageIndex;
        Load_HistoryLogs(hdnRefID.Value);
    }
    protected void ddlCurrency_SelectedIndexChanged(object sender, EventArgs e)
    {
        foreach (GridViewRow row in gvRFQ_List.Rows)
        {
            ((Label)row.FindControl("lblCurrency")).Text = ((DropDownList)row.FindControl("ddlCurrency")).SelectedValue;
        }
        Update_GVHeader();
    }
    protected void ddlBuyer_SelectedIndexChanged(object sender, EventArgs e)
    {
        CopyRowsToDT_RFQList();
        foreach (GridViewRow row in gvRFQ_List.Rows)
        {
            ((Label)row.FindControl("lblBuyer")).Text = ((DropDownList)row.FindControl("ddlBuyer")).SelectedValue;
        }
        Update_GVHeader();
    }
    protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCategory.SelectedIndex != 0)
        {
            lblNote.Visible = true;
            divFileUpload.Visible = true;
            lblNote.Text = " Note: <br> 1. Row/s in <b>GREEN</b> is valid for submission. <br> 2.Kindly remember to upload any relevant files or documents before proceeding with manual input. <br>" +
                           " 3. Uploading new files or documents will overwrite any existing record. If you proceed with the upload, the current data will be replaced. ";
            dtRFQ_Request.Rows.Clear();
            if (gvRFQ_List.Rows.Count == 0) AddRow_RFQList();
            gvRFQ_List.Visible = true;

            //NUMBER OF REQUEST
            string counts = gvRFQ_List.Rows.Count.ToString();
            int numOfReq = Int32.Parse(counts);
            int dif = numOfReq - 1;
            if (dif == 0 || txtNumOfReq.Text == "0")
            {
                txtNumOfReq.Text = "--Auto Generated--";
            }
            else
            {
                txtNumOfReq.Text = dif.ToString();
            }
        }
        else if (ddlCategory.SelectedIndex == 0)
        {
            lblNote.Visible = false;
            gvRFQ_List.Visible = false;
            divFileUpload.Visible = false;
            //NUMBER OF REQ 
            txtNumOfReq.Text = "--Auto Generated--";

            upnlDetailsOfReq.Update();

        }
        if (gvRFQ_List.Rows.Count > 0)
        {
            gvRFQ_List.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
    }
    protected void lbtnUpload_RFQList_Click(object sender, EventArgs e)
    {
        upnlDetailsOfReq.Update();
        if (fuRFQList.HasFile)
        {
            CopyRowsToDT_RFQList();
            Load_RFQ_Upload_Details("upload excel file");
        }
        else
        {
            AlertMessage.Show("warning", "Information:", "Please select file to attach!");
            hdnAction.Value = "alert";
        }

        Update_GVHeader();
    }
    protected void lbtnDownloadTemplate_Click(object sender, EventArgs e)
    {
        string myfile = "RFQ Uploading Template.xlsx";
        Response.ContentType = "application/vnd.ms-excel";
        Response.AddHeader("Content-Disposition", "attachment;filename=" + myfile);
        Response.WriteFile(Server.MapPath("~/FileExport/Template/" + myfile), true);
        Response.End();

        Update_GVHeader();
    }
    protected void lbtnSave_Click(object sender, EventArgs e)
    {
        hdnButton.Value = "saveButton";
        if (isCompleteAll())
        {
            AlertMessage.Show("confirmationwithremarks", "Confirmation:", "Are you sure you want to save this request?");
            hdnAction.Value = "save";
            
        }

        Update_GVHeader();
    }
    protected void lbtnEditApprover_Click(object sender, EventArgs e)
    {
        AlertMessage.Show("confirmation", "Confirmation:", "Do you want to edit current selected Approver(s)?");
        hdnAction.Value = "edit app";
    }
    protected void lbtnCancelEdit_Click(object sender, EventArgs e)
    {
        Response.Redirect("Registration.aspx?RefID=" + hdnRefID.Value);
    }
    protected void lbtnSaveApprover_Click(object sender, EventArgs e)
    {
        if (isCompleteEditApprovers())
        {
            AlertMessage.Show("confirmationwithremarks", "Confirmation:", "Do you want to save all the currently selected Approver(s)?");
            hdnAction.Value = "update rfq approver";
        }

    }

    protected void lbtnUpdate_Click(object sender, EventArgs e)
    {
        AlertMessage.Show("confirmation", "Confirmation:", "Do you want to edit request details?");
        hdnAction.Value = "update controls";
    }

    protected void lbtnCancelUpdate_Click(object sender, EventArgs e)
    {
        Response.Redirect("Registration.aspx?RefID=" + hdnRefID.Value);
    }

    protected void lbtnSaveUpdate_Click(object sender, EventArgs e)
    {
        hdnButton.Value = "updateButton";
        if (isCompleteAll())
        {
            AlertMessage.Show("confirmationwithremarks", "Confirmation:", "Are you sure you want to update this request?");
            hdnAction.Value = "save";
        }
    }

    protected void lbtnSubmit_Click(object sender, EventArgs e)
    {
        AlertMessage.Show("confirmationwithremarks", "Confirmation:", "Are you sure you want to submit this request?");
        hdnAction.Value = "submit";
    }

    protected void lbtnApprove_Click(object sender, EventArgs e)
    {
        AlertMessage.Show("confirmationwithremarks", "Confirmation:", "Are you sure you want to approve this request?");
        hdnAction.Value = "approve";
    }

    protected void lbtnDeny_Click(object sender, EventArgs e)
    {
        AlertMessage.Show("confirmationwithremarks", "Confirmation:", "Are you sure you want to deny this request?");
        hdnAction.Value = "deny";
    }

    protected void lbtnReSubmit_Click(object sender, EventArgs e)
    {
        AlertMessage.Show("confirmationwithremarks", "Confirmation:", "Are you sure you want to re-submit this request?");
        hdnAction.Value = "resubmit";
    }

    protected void lbtnAssignBuyer_Click(object sender, EventArgs e)
    {
        AlertMessage.Show("confirmation", "Confirmation:", "Do you want to assign buyer(s)?");
        hdnAction.Value = "assign buyers controls";

    }

    protected void lbtnSaveBuyer_Click(object sender, EventArgs e)
    {
        if (isCompleteBuyerIncharge())
        {
            hdnButton.Value = "assignButton";
            AlertMessage.Show("confirmationwithremarks", "Confirmation:", "Are you sure you want to save this transaction?");
            hdnAction.Value = "save";
        }
    }

    protected void lbtnExport_Click1(object sender, EventArgs e)
    {
        //lbtnExport.Enabled = false;
        upnlDetailsOfReq.Update();
        if (gvDetailsViewOnly.Rows.Count == 0)
        {
            AlertMessage.Show("Information", "Information:", "No records found. Please search first.");
            hdnAction.Value = "alert";
        }
        else
        {
            ExportToExcel(Server.MapPath("~/FileExport/Template/Report Template_perItem.xlsx"));
        }
        ////lbtnExport.Enabled = true;
        upnlDetailsOfReq.Update();

    }
    protected void lbtnReAssignBuyer_Click(object sender, EventArgs e)
    {
        AlertMessage.Show("confirmation", "Confirmation:", "Are you sure you want to re-assign buyer incharge?");
        hdnAction.Value = "re-assign buyer";
    }
    protected void lbtnSaveNewBuyer_Click(object sender, EventArgs e)
    {
        AlertMessage.Show("confirmationwithremarks", "Confirmation:", "Do you want to save all the currently selected Buyer in-charge(s)?");
        hdnAction.Value = "update rfq buyer";

    }
    #endregion
}