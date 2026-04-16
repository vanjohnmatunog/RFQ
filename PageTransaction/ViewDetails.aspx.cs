using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Web.UI;
using System.Data;
using System.Web.UI.WebControls;
using TIPIS3WebAdmin.TIPEmployeeMaster;

public partial class PageTransaction_ViewDetails : System.Web.UI.Page
{
    i_DataLayer dl = new cl_DataLayer();
    private static Boolean isAdmin = false;
    private static DataTable dtRFQ_perItem = new DataTable();
    private const String DT_RFQ_perItem = "DT_RFQ_perItem";
    cl_RFQ_TransactionObject trans = new cl_RFQ_TransactionObject();
    cl_DataTransferObject dto = new cl_DataTransferObject();
    cl_RFQ_StatusperItemObject stat = new cl_RFQ_StatusperItemObject();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            string TempItemCode = Request.QueryString["TempItemCode"] == null ? "" : Request.QueryString["TempItemCode"].ToString();
            hdnTempItemCode.Value = TempItemCode;
            Details.Portal.EMP emp = new Details.Portal.EMP(cl_DBConn.MSSQLEmp(), cl_Identity.Get_UserID());
            if (emp.HasRows)
            {
                isAdmin = (dl.Check_UserAccess(cl_ProvideFactory.getSqlFactory(), cl_DBConn.MSSQLTrans(), "is admin", "Administrator", emp.EmpNo));

                if (!isAdmin) ScriptManager.RegisterStartupScript(this, GetType(), "Javascript", "MenuHide('five-ddheader');", true);
                hdnUserEmpNo.Value = emp.EmpNo;
                hdnUserEmpName.Value = emp.EmpName;

                Initialize_dtRFQ_perItem();
            }
            else
            {
                Unauthorized();
            }
            emp.Dispose();

            Load_Item("view item", TempItemCode);
            Load_Attachment();
            Load_MPDAttachment();
            Load_HistoryLogs(hdnRefID.Value,txtControlNo.Text);
            if (txtStatus.Text.ToLower() == "partially closed")
            {
                gvAttachmentReadOnly();
            }
            
            Update_GVHeader();
            if (gvHistoryLogs_MPD.Rows.Count > 0) divLogs.Visible = true;
        }
        else
        {
            if (gvAttachment.Rows.Count > 0) gvAttachment.HeaderRow.TableSection = TableRowSection.TableHeader;
            if (gvAttachmentMPD.Rows.Count > 0) gvAttachmentMPD.HeaderRow.TableSection = TableRowSection.TableHeader;
            if (gvDetailsViewOnly.Rows.Count > 0) gvDetailsViewOnly.HeaderRow.TableSection = TableRowSection.TableHeader;
            if (gvHistoryLogs_MPD.Rows.Count > 0) gvHistoryLogs_MPD.HeaderRow.TableSection = TableRowSection.TableHeader;
        }

    }
    private void Unauthorized()
    {
        Response.Redirect("~/PortalAdmin/Unauthorized.aspx");
    }
    protected void Update_GVHeader()
    {
        if (gvDetailsViewOnly.Rows.Count > 0)
        {
            gvDetailsViewOnly.HeaderRow.TableSection = TableRowSection.TableHeader;
        }

        upnlDetailsOfItem.Update();
        upnlReqAttach.Update();
        upnlMPDAttach.Update();
        upnlLogs.Update();
        upnlActionButton.Update();
    }
    protected void Initialize_dtRFQ_perItem()
    {
        dtRFQ_perItem = new DataTable();
        if (dtRFQ_perItem.Columns.Count == 0)
        {
            //dtRFQ_perItem.Columns.Add("RefID");
            //dtRFQ_perItem.Columns.Add("RFQControlNo");
            //dtRFQ_perItem.Columns.Add("RequestorName");
            //dtRFQ_perItem.Columns.Add("Dept");
            //dtRFQ_perItem.Columns.Add("LocalNo");
            //dtRFQ_perItem.Columns.Add("Location");
            //dtRFQ_perItem.Columns.Add("Category");
            //dtRFQ_perItem.Columns.Add("NoOfReq");
            //dtRFQ_perItem.Columns.Add("Status");
            //dtRFQ_perItem.Columns.Add("DateCreated");
            //dtRFQ_perItem.Columns.Add("ControlNo");
            dtRFQ_perItem.Columns.Add("ItemDescription");
           // dtRFQ_perItem.Columns.Add("TempItemCode");
            dtRFQ_perItem.Columns.Add("Quantity");
            dtRFQ_perItem.Columns.Add("UOM");
            dtRFQ_perItem.Columns.Add("Budget");
            dtRFQ_perItem.Columns.Add("Currency");
            dtRFQ_perItem.Columns.Add("Purpose");
            dtRFQ_perItem.Columns.Add("Remarks");
            dtRFQ_perItem.Columns.Add("BuyerInCharge");
            //dtRFQ_perItem.Columns.Add("FilePath");
            dtRFQ_perItem.AcceptChanges();

            ViewState[DT_RFQ_perItem] = dtRFQ_perItem;
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
                //Response.Redirect("Registration.aspx?RefID=" + hdnRefID.Value);
                Response.Redirect("ViewDetails.aspx?TempItemCode=" + hdnTempItemCode.Value);
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
            case "inprocess":
                Status_perItem("IN PROCESS", txtRemarks_Save.Text.Trim().Replace("'", "`"));
                break;
            case "partially close":
                Status_perItem("PARTIALLY CLOSED", txtRemarks_Save.Text.Trim().Replace("'", "`"));
		CheckStatusPerItem();
                break;
        }

    }

    void btnDecline_Click(object sender, EventArgs e)
    {
        hdnAction.Value = "";
        upnlActionButton.Update();
    }
    private void Load_Item(string transType, string TempItemCode)
    {
        DataTable dtItem = new DataTable();
        dtItem = dl.get_perItem(cl_ProvideFactory.getSqlFactory(), cl_DBConn.MSSQLTrans(), transType, TempItemCode);
        if (dtItem != null && dtItem.Rows.Count > 0)
        {
            txtControlNo.Text = !string.IsNullOrEmpty(dtItem.Rows[0]["ControlNo"].ToString()) ? dtItem.Rows[0]["ControlNo"].ToString() : "";
            ControlNoStyle();
            txtBuyer.Text = !string.IsNullOrEmpty(dtItem.Rows[0]["BuyerInCharge"].ToString()) ? dtItem.Rows[0]["BuyerInCharge"].ToString() : "";
            txtRequestor.Text = !string.IsNullOrEmpty(dtItem.Rows[0]["RequestorName"].ToString()) ? dtItem.Rows[0]["RequestorName"].ToString() : "";
            hdnRefID.Value = !string.IsNullOrEmpty(dtItem.Rows[0]["RefID"].ToString()) ? dtItem.Rows[0]["RefID"].ToString() : "";
            txtCategory.Text = !string.IsNullOrEmpty(dtItem.Rows[0]["Category"].ToString()) ? dtItem.Rows[0]["Category"].ToString() : "";
            txtStatus.Text = !string.IsNullOrEmpty(dtItem.Rows[0]["StatusPerItem"].ToString()) ? dtItem.Rows[0]["StatusPerItem"].ToString() : "";
            txtDateClose.Text = !string.IsNullOrEmpty(dtItem.Rows[0]["DateClosed"].ToString()) ? dtItem.Rows[0]["DateClosed"].ToString() : "";

            dtRFQ_perItem.Rows.Clear();
            dtRFQ_perItem.AcceptChanges();

            for (int i = 0; i <= dtItem.Rows.Count; i++)
            {
                DataRow r = dtRFQ_perItem.NewRow();

                if (i < dtItem.Rows.Count)
                {
                    r["ItemDescription"] = dtItem.Rows[i]["ItemDescription"].ToString();
                    r["Quantity"] = dtItem.Rows[i]["Quantity"].ToString();
                    r["UOM"] = dtItem.Rows[i]["UOM"].ToString();
                    r["Budget"] = dtItem.Rows[i]["Budget"].ToString();
                    r["Currency"] = dtItem.Rows[i]["Currency"].ToString();
                    r["Purpose"] = dtItem.Rows[i]["Purpose"].ToString();
                    r["Remarks"] = dtItem.Rows[i]["Remarks"].ToString();
                    r["BuyerInCharge"] = dtItem.Rows[i]["BuyerInCharge"].ToString();
                }
                else
                {
                    r["ItemDescription"] = "";
                    r["Quantity"] = "";
                    r["UOM"] = "";
                    r["Budget"] = "";
                    r["Currency"] = "";
                    r["Purpose"] = "";
                    r["Remarks"] = "";
                    r["BuyerInCharge"] = "";
                }

                dtRFQ_perItem.Rows.Add(r);
                dtRFQ_perItem.AcceptChanges();
            }
            Load_Details(dtRFQ_perItem);
           
        }
        else
        {
            AlertMessage.Show("warning", "Information:", "Records not found.");
            hdnAction.Value = "redirect";
        }

        if (hdnUserEmpName.Value == txtBuyer.Text || isAdmin)
        {
            lblAtt_MPD.Visible = true;
            lblAtNote_MPD.Visible = true;
            divUploadAttach_MPD.Visible = true;
            lbtnInprocess.Visible = true;
            lbtnPartiallyClosed.Visible = true;
        }
        if (txtStatus.Text.ToLower() == "partially closed")
        {
            txtStatus.ReadOnly = true;
            txtStatus.BackColor = System.Drawing.Color.Red;
            txtStatus.BorderColor = System.Drawing.Color.Red;
            txtStatus.ForeColor = System.Drawing.Color.White;

            lbtnInprocess.Visible = false;
            lbtnPartiallyClosed.Visible = false;

            upnlMPDAttach.Update();
        }
        else if (txtStatus.Text.ToLower() == "in-process")
        {
            txtStatus.ReadOnly = true;
            txtStatus.BackColor = System.Drawing.ColorTranslator.FromHtml("#e64a02");
            txtStatus.BorderColor = System.Drawing.ColorTranslator.FromHtml("#e64a02");
            txtStatus.ForeColor = System.Drawing.Color.White;
        }

    }
    private void Load_Details(DataTable dtRFQ_perItem)
    {
        gvDetailsViewOnly.DataSource = dtRFQ_perItem;
        gvDetailsViewOnly.DataBind();

        int lastRow = gvDetailsViewOnly.Rows.Count;
        gvDetailsViewOnly.Rows[lastRow - 1].Visible = false;

    }
    private void Delete_Attachment(string Remarks)
    {
        try
        {
            dto.ID = hdnAttachmentID.Value;
            dto.RefID = hdnRefID.Value;
            dto.ControlNo = txtControlNo.Text;
            dto.FileName_Orig = "";
            dto.FileName_New = "";
            dto.FilePath = hdnAttachmentPath.Value;
            dto.ActionRemarks = Remarks;
            dto.CreatedBy_EmpNo = hdnUserEmpNo.Value;
            dto.CreatedBy_EmpName = hdnUserEmpName.Value;
            dto.Attribute1 = "-";
            dto.Attribute2 = "-";

            string result = dl.Insert_RFQ_MPD_Attachment(cl_ProvideFactory.getSqlFactory(), cl_DBConn.MSSQLTrans(), dto, "delete");
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
            Load_MPDAttachment();
            Load_HistoryLogs(hdnRefID.Value, txtControlNo.Text);
        }
        catch (Exception ex)
        {
            //
        }
    }
    private void Status_perItem(string TransType, string Remarks)
    {
        stat.RefID = hdnRefID.Value;
        stat.Category = txtCategory.Text;
        stat.ControlNo = txtControlNo.Text;
        stat.BuyerIncharge = txtBuyer.Text;
        stat.CurrentUser = hdnUserEmpName.Value;

        string result = dl.Status_perItem(cl_ProvideFactory.getSqlFactory(), cl_DBConn.MSSQLTrans(), stat, TransType, Remarks);
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
        upnlDetailsOfItem.Update();
        Update_GVHeader();

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
        Load_HistoryLogs(hdnRefID.Value, txtControlNo.Text);
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
    protected void gvDetailsViewOnly_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (((Label)e.Row.FindControl("lblRemarks")).Text == "")
            {
                ((Label)e.Row.FindControl("lblRemarks")).Text = "N/A";
            }
           
            // Check if it is the 31st row (index 30)
            if (e.Row.RowIndex >= 30)
            {
                //Hide row greater than 30
                e.Row.Visible = false;
            }
        }

    }
    private void Load_HistoryLogs(string keyword, string ControlNo)
    {
        DataTable dtLogs = new DataTable();
        dtLogs = dl.get_RFQ_MPD_HistoryLogs(cl_ProvideFactory.getSqlFactory(), cl_DBConn.MSSQLTrans(), keyword, ControlNo);
        gvHistoryLogs_MPD.DataSource = dtLogs;
        gvHistoryLogs_MPD.DataBind();
        if (gvHistoryLogs_MPD.Rows.Count > 0) gvHistoryLogs_MPD.HeaderRow.TableSection = TableRowSection.TableHeader;
    }
    protected void gvHistoryLogs_MPD_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvHistoryLogs_MPD.PageIndex = e.NewPageIndex;
        Load_HistoryLogs(hdnRefID.Value, txtControlNo.Text);
    }


    #region MPD Attachment Buttons

    protected void lbtnUpload_MPD_Command(object sender, CommandEventArgs e)
    {
        upnlDetailsOfItem.Update();
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
                    dto.ControlNo = txtControlNo.Text;
                    dto.FileName_Orig = fuAttachment.FileName;
                    dto.FileName_New = newFileName;
                    dto.FilePath = FilePath;
                    dto.ActionRemarks = "";
                    dto.CreatedBy_EmpNo = hdnUserEmpNo.Value;
                    dto.CreatedBy_EmpName = hdnUserEmpName.Value;
                    dto.Attribute1 = "-";
                    dto.Attribute2 = "-";

                    string result = dl.Insert_RFQ_MPD_Attachment(cl_ProvideFactory.getSqlFactory(), cl_DBConn.MSSQLTrans(), dto, "save");
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
                    Load_MPDAttachment();
                    if (gvHistoryLogs_MPD.Rows.Count > 0) divLogs.Visible = true;
                }

            }
            catch (Exception ex)
            {
                AlertMessage.Show("Warning", "Information:", "Request Timeout: Please try again!");
                hdnAction.Value = "alert";
            }
            fuAttachment.Dispose();
            upnlDetailsOfItem.Update();
        }
    }
    private void Load_MPDAttachment()
    {
        DataTable dtAttachment = new DataTable();
        dtAttachment = dl.get_RFQ_MPD_Attachment(cl_ProvideFactory.getSqlFactory(), cl_DBConn.MSSQLTrans(), "per refid", hdnRefID.Value, txtControlNo.Text);
        gvAttachmentMPD.DataSource = dtAttachment;
        gvAttachmentMPD.DataBind();

        if (gvAttachmentMPD.Rows.Count > 0)
        {
            gvAttachmentMPD.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        Load_HistoryLogs(hdnRefID.Value, txtControlNo.Text);

    }
    protected void lbtnOpen_MPD_Command(object sender, CommandEventArgs e)
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
    protected void lbtnDeleteAttachment_MPD_Command(object sender, CommandEventArgs e)
    {
        hdnAttachmentID.Value = e.CommandArgument.ToString();
        hdnAttachmentPath.Value = e.CommandName.ToString();
        upnlActionButton.Update();
        AlertMessage.Show("confirmationwithremarks", "Confirmation:", "Are you sure you delete this attachment?");
        hdnAction.Value = "delete attachment";
    }
    #endregion

    private Boolean isCompleteAll()
    {
        bool result = true;

        if (gvAttachmentMPD.Rows.Count == 0)
        {
            AlertMessage.Show("warning", "Information:", "Please attach file!");
            hdnAction.Value = "alert";

            result = false;
        }
        return result;
    }
    private void gvAttachmentReadOnly()
    {
        //Attachment 
        divUploadAttach_MPD.Visible = false;
        lblAtt_MPD.Visible = false;
        lblAtNote_MPD.Visible = false;
        LinkButton lbtnDelete_MPD = new LinkButton();
        foreach (GridViewRow row in gvAttachmentMPD.Rows)
        {
            lbtnDelete_MPD = (LinkButton)gvAttachmentMPD.Rows[row.RowIndex].FindControl("lbtnDelete_MPD");
            lbtnDelete_MPD.Visible = false;
        }
    }
    private void CheckStatusPerItem()
    {
        DataTable dtDetailsPerRequest = new DataTable();
        dtDetailsPerRequest = dl.Automatic_Closed(cl_ProvideFactory.getSqlFactory(), cl_DBConn.MSSQLTrans(), "check status", hdnRefID.Value);

        int partiallyClosedCount = 0;
        for (int i = 0; dtDetailsPerRequest.Rows.Count > i; i++)
        {
            string status = dtDetailsPerRequest.Rows[i][12].ToString();

            if (status.ToLower() == "partially closed")
            {
                partiallyClosedCount++;
            }
        }
	if (partiallyClosedCount == dtDetailsPerRequest.Rows.Count)
            {
                trans.RefID = hdnRefID.Value;
                trans.CurrentUser = hdnUserEmpName.Value;

                string result = dl.Automatic_Closed(cl_ProvideFactory.getSqlFactory(), cl_DBConn.MSSQLTrans(), trans);
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
                upnlDetailsOfItem.Update();
                Update_GVHeader();
            }
    }
    private void ControlNoStyle()
    {
        txtControlNo.ReadOnly = true;
        txtControlNo.BackColor = System.Drawing.Color.Green;
        txtControlNo.BorderColor = System.Drawing.Color.Green;
        txtControlNo.ForeColor = System.Drawing.Color.White;
    }
    protected void lbtnInprocess_Click(object sender, EventArgs e)
    {
        if (isCompleteAll())
        {
            AlertMessage.Show("confirmationwithremarks", "Confirmation:", "Are you sure you want to save this transaction?");
            hdnAction.Value = "inprocess";
        }
    }

    protected void lbtnPartiallyClosed_Click(object sender, EventArgs e)
    {
        if (isCompleteAll())
        {
            AlertMessage.Show("confirmationwithremarks", "Confirmation:", "Are you sure you want to close this transaction?");
            hdnAction.Value = "partially close";
        }
    }

    protected void lbtnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("Registration.aspx?RefID=" + hdnRefID.Value);
    }
   
}