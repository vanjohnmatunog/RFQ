using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

/// <summary>
/// Summary description for cl_DataLayer
/// </summary>
public class cl_DataLayer : i_DataLayer
{
    cl_DBLayer dbLayer = new cl_DBLayer();

    #region Project Information
    public bool Check_Currency(DbProviderFactory factory, string ConStr, string transType, string Currency)
    {
        SqlParameter[] parms = new SqlParameter[]
            {
                new SqlParameter("@TransType", transType),
                new SqlParameter("@ItemCode", Currency)
            };
        try
        {
            return Convert.ToBoolean(dbLayer.Scalar(factory, ConStr, "dbo.sp_Select_LOV_Masterlist", CommandType.StoredProcedure, parms));
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public DataTable get_RFQ_HistoryLogs(DbProviderFactory factory, string ConStr, string Keyword)
    {
        SqlParameter[] parms = new SqlParameter[]
            {
                new SqlParameter("@RefID", Keyword)
            };
        try
        {
            return dbLayer.getDataTable(factory, ConStr, "dbo.sp_Select_RFQ_HistoryLogs", CommandType.StoredProcedure, parms);
        }
        catch (Exception ex)
        {
            return null;
        }
    }
    public DataTable get_RFQ_MPD_HistoryLogs(DbProviderFactory factory, string ConStr, string Keyword, string ControlNo)
    {
        SqlParameter[] parms = new SqlParameter[]
            {
                new SqlParameter("@RefID", Keyword),
                new SqlParameter("@ControlNo", ControlNo)
            };
        try
        {
            return dbLayer.getDataTable(factory, ConStr, "dbo.sp_Select_RFQ_MPD_HistoryLogs", CommandType.StoredProcedure, parms);
        }
        catch (Exception ex)
        {
            return null;
        }
    }
    public DataTable get_RFQ_Approvers(DbProviderFactory factory, string ConStr, string TransType, string Keyword)
    {
        SqlParameter[] parms = new SqlParameter[]
            {
                new SqlParameter("@TransType", TransType),
                new SqlParameter("@Keyword", Keyword)
            };
        try
        {
            return dbLayer.getDataTable(factory, ConStr, "dbo.sp_Select_RFQ_Approver", CommandType.StoredProcedure, parms);
        }
        catch (Exception ex)
        {
            return null;
        }
    }
    public DataSet Select_Details_Of_Request(DbProviderFactory factory, string ConStr, string TransType, string RefID, string Keyword, string DateFrom, string DateTo)
    {
        SqlParameter[] parms = new SqlParameter[]
            {
                new SqlParameter("@TransType", TransType),
                new SqlParameter("@Status", ""),
                new SqlParameter("@Keyword", RefID),
                new SqlParameter("@FromDate", ""),
                new SqlParameter("@ToDate", ""),
                new SqlParameter("@UserEmpNo", "")
            };
        try
        {
            return dbLayer.getDataSet(factory, ConStr, "dbo.sp_Select_RFQ_Transaction", CommandType.StoredProcedure, parms);
        }
        catch (Exception ex)
        {
            return null;
        }
    }
    public DataTable get_RFQ_Transaction_Approvers(DbProviderFactory factory, string ConStr, string TransType, string Keyword)
    {
        SqlParameter[] parms = new SqlParameter[]
            {
                new SqlParameter("@TransType", TransType),
                new SqlParameter("@Keyword", Keyword)
            };
        try
        {
            return dbLayer.getDataTable(factory, ConStr, "dbo.sp_Select_RFQ_Transaction_Approvers", CommandType.StoredProcedure, parms);
        }
        catch (Exception ex)
        {
            return null;
        }
    }
    public DataTable get_perItem(DbProviderFactory factory, string ConStr, string TransType, string TempItemCode)
    {
        SqlParameter[] parms = new SqlParameter[]
            {
                new SqlParameter("@TransType", TransType),
                new SqlParameter("@TempItemCode", TempItemCode)
            };
        try
        {
            return dbLayer.getDataTable(factory, ConStr, "dbo.sp_Select_RFQ_perItem", CommandType.StoredProcedure, parms);
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    public DataTable get_RFQ_Transaction(DbProviderFactory factory, string ConStr, string TransType, string Status, string Keyword, string fromDate, string toDate, string userEmpNo)
    {
        SqlParameter[] parms = new SqlParameter[]
            {
                new SqlParameter("@TransType", TransType),
                new SqlParameter("@Status", Status),
                new SqlParameter("@Keyword", Keyword),
                new SqlParameter("@FromDate", fromDate),
                new SqlParameter("@ToDate", toDate),
                new SqlParameter("@UserEmpNo", userEmpNo)

            };
        try
        {
            return dbLayer.getDataTable(factory, ConStr, "dbo.sp_Select_RFQ_Transaction", CommandType.StoredProcedure, parms);
        }
        catch (Exception ex)
        {
            return null;
        }
    }
    public DataTable get_RFQ_Per_Item(DbProviderFactory factory, string ConStr, string TransType, string RefID)
    {
        SqlParameter[] parms = new SqlParameter[]
            {
                new SqlParameter("@TransType", TransType),
                new SqlParameter("@RefID", RefID)
            };
        try
        {
            return dbLayer.getDataTable(factory, ConStr, "dbo.sp_Select_RFQ_Per_Item", CommandType.StoredProcedure, parms);
        }
        catch (Exception ex)
        {
            return null;
        }
    }
    public DataTable Automatic_Closed(DbProviderFactory factory, string ConStr, string TransType, string RefID)
    {
        SqlParameter[] parms = new SqlParameter[]
            {
                new SqlParameter("@TransType", TransType),
                new SqlParameter("@RefID", RefID)
            };
        try
        {
            return dbLayer.getDataTable(factory, ConStr, "dbo.sp_Select_RFQ_Per_Item", CommandType.StoredProcedure, parms);
        }
        catch (Exception ex)
        {
            return null;
        }
    }
    public string Insert_RFQ_Request(DbProviderFactory factory, string ConStr, cl_RFQ_TransactionObject trans, DataTable dtRFQ_List, string transType)
    {
        try
        {
            return dbLayer.Scalar(factory, ConStr, "dbo.sp_Insert_RFQ_Request", CommandType.StoredProcedure, fillRFQRequest(trans, dtRFQ_List, transType)).ToString();
        }
        catch (Exception ex)
        {
            //return "Request Timeout";
            return ex.Message;
        }
    }

    SqlParameter[] fillRFQRequest(cl_RFQ_TransactionObject trans, DataTable dtRFQ_List, string transType)
    {
        return new SqlParameter[]
        {
            new SqlParameter("@TransType", transType),
            new SqlParameter("@RefID", trans.RefID),
            new SqlParameter("@ReqName", trans.Reqname),
            new SqlParameter("@ReqDept", trans.Dept),
            new SqlParameter("@Local", trans.Local),
            new SqlParameter("@Location", trans.Location),
            new SqlParameter("@Category", trans.Category),
            new SqlParameter("@NoOfReq", trans.NoOfReq),
            new SqlParameter("@Remarks", trans.Attributes1),
            new SqlParameter("@CurrentUser", trans.CurrentUser),
            new SqlParameter("@CurrentUserEmpNo", trans.CurrentUserEmpNo),

            new SqlParameter("@tbl_RFQ_ItemList", dtRFQ_List),
        };
    }
    public string Approve_Deny(DbProviderFactory factory, string ConStr, cl_RFQ_TransactionObject trans, string transType, string Remarks)
    {
        try
        {
            return dbLayer.Scalar(factory, ConStr, "dbo.sp_Approve_Deny_Transaction", CommandType.StoredProcedure, fillApproval(trans, transType, Remarks)).ToString();
        }
        catch (Exception ex)
        {
            return "Request Timeout";
        }
    }
    SqlParameter[] fillApproval(cl_RFQ_TransactionObject trans, string transType, string Remarks)
    {
        return new SqlParameter[]
        {
            new SqlParameter("@TransType", transType),
            new SqlParameter("@RefID", trans.RefID),
            new SqlParameter("@CurrentUser", trans.CurrentUser),
            new SqlParameter("@CurrentUserEmpNo", trans.CurrentUserEmpNo),
            new SqlParameter("@Remarks", Remarks),
            new SqlParameter("@isAdmin", trans.isAdmin)

        };
    }
    public string Resubmit(DbProviderFactory factory, string ConStr, cl_RFQ_TransactionObject trans, string transType, string Remarks)
    {
        try
        {
            return dbLayer.Scalar(factory, ConStr, "dbo.sp_Update_Resubmit_New_RFQ", CommandType.StoredProcedure, fillResubmit(trans, transType, Remarks)).ToString();
        }
        catch (Exception ex)
        {
            return "Request Timeout";
        }
    }
    SqlParameter[] fillResubmit(cl_RFQ_TransactionObject trans, string transType, string Remarks)
    {
        return new SqlParameter[]
        {
            new SqlParameter("@TransType", transType),
            new SqlParameter("@RefID", trans.RefID),
            new SqlParameter("@CurrentUser", trans.CurrentUser),
            new SqlParameter("@Remarks", Remarks)

        };
    }
    public string Status_perItem(DbProviderFactory factory, string ConStr, cl_RFQ_StatusperItemObject stat, string transType, string Remarks)
    {
        try
        {
            return dbLayer.Scalar(factory, ConStr, "dbo.sp_Insert_Status_perItem", CommandType.StoredProcedure, fillStatus(stat, transType, Remarks)).ToString();
        }
        catch (Exception ex)
        {
            return "Request Timeout";
        }
    }
    SqlParameter[] fillStatus(cl_RFQ_StatusperItemObject stat, string transType, string Remarks)
    {
        return new SqlParameter[]
        {
            new SqlParameter("@TransType", transType),
            new SqlParameter("@RefID",stat.RefID),
            new SqlParameter("@Category",stat.Category),
            new SqlParameter("@ControlNo", stat.ControlNo),
            new SqlParameter("@BuyerIncharge", stat.BuyerIncharge),
            new SqlParameter("@CurrentUser", stat.CurrentUser),
            new SqlParameter("@Remarks", Remarks)
        };
    }
    public string Automatic_Closed(DbProviderFactory factory, string ConStr, cl_RFQ_TransactionObject trans)
    {
        try
        {
            return dbLayer.Scalar(factory, ConStr, "dbo.sp_Automatic_Closed", CommandType.StoredProcedure, fillAutomatic_Closed(trans)).ToString();
        }
        catch (Exception ex)
        {
            return "Request Timeout";
        }
    }
    SqlParameter[] fillAutomatic_Closed(cl_RFQ_TransactionObject trans)
    {
        return new SqlParameter[]
        {
            new SqlParameter("@RefID", trans.RefID),
            new SqlParameter("@CurrentUser", trans.CurrentUser)

        };
    }

    #endregion

    #region Maintenance
    public string Insert_LOVMasterList(DbProviderFactory factory, string ConStr, cl_MaintenanceObject mo, string transType)
    {
        try
        {
            return dbLayer.Scalar(factory, ConStr, "dbo.sp_Insert_LOV_Masterlist", CommandType.StoredProcedure, fillLOVMasterList(mo, transType)).ToString();
        }
        catch (Exception ex)
        {
            return "Request Timeout";
        }
    }
    SqlParameter[] fillLOVMasterList(cl_MaintenanceObject mo, string transType)
    {
        return new SqlParameter[]
        {
            new SqlParameter("@TransType", transType),
            new SqlParameter("@ID", mo.ID),
            new SqlParameter("@Category", mo.Category),
            new SqlParameter("@ItemCode", mo.ItemCode),
            new SqlParameter("@Description", mo.Description),
            new SqlParameter("@Active", mo.Active),
            new SqlParameter("@CreatedBy", mo.CreatedBy_EmpName),
            new SqlParameter("@Attribute1", mo.Attribute1),
            new SqlParameter("@Attribute2", mo.Attribute2),
            new SqlParameter("@Attribute3", mo.Attribute3),
            new SqlParameter("@Attribute4", mo.Attribute4),
            new SqlParameter("@Attribute5", mo.Attribute5)
        };
    }

    public DataTable getLOVMasterlist(DbProviderFactory factory, string ConStr, string transType, string category, string EmpNo)
    {
        SqlParameter[] parms = new SqlParameter[]
            {
                new SqlParameter("@TransType", transType),
                new SqlParameter("@Category", category),
                new SqlParameter("@ItemCode", EmpNo)
            };
        try
        {
            return dbLayer.getDataTable(factory, ConStr, "dbo.sp_Select_LOV_Masterlist", CommandType.StoredProcedure, parms);
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    public bool Check_UserAccess(DbProviderFactory factory, string ConStr, string transType, string category, string EmpNo)
    {
        SqlParameter[] parms = new SqlParameter[]
            {
                new SqlParameter("@TransType", transType),
                new SqlParameter("@Category", category),
                new SqlParameter("@ItemCode", EmpNo)
            };
        try
        {
            return Convert.ToBoolean(dbLayer.Scalar(factory, ConStr, "dbo.sp_Select_LOV_Masterlist", CommandType.StoredProcedure, parms));
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    #endregion


    #region Attachment

    public DataTable get_RFQ_Attachment(DbProviderFactory factory, string ConStr, string transType, string Keyword)
    {
        SqlParameter[] parms = new SqlParameter[]
            {
                new SqlParameter("@TransType", transType),
                new SqlParameter("@Keyword", Keyword)
            };
        try
        {
            return dbLayer.getDataTable(factory, ConStr, "dbo.sp_Select_RFQ_Attachment", CommandType.StoredProcedure, parms);
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    public string Insert_RFQ_Attachment(DbProviderFactory factory, string ConStr, cl_DataTransferObject dto, string transType)
    {
        try
        {
            return dbLayer.Scalar(factory, ConStr, "dbo.sp_Insert_RFQ_Attachment", CommandType.StoredProcedure, fillDRAttachment(dto, transType)).ToString();
        }
        catch (Exception ex)
        {
            return "Request Timeout";
        }
    }
    

    SqlParameter[] fillDRAttachment(cl_DataTransferObject dto, string transType)
    {
        return new SqlParameter[]
        {
            new SqlParameter("@TransType",  transType),
            new SqlParameter("@ID",  dto.ID),
            new SqlParameter("@RefID",  dto.RefID),
            new SqlParameter("@FileName_Orig",  dto.FileName_Orig),
            new SqlParameter("@FileName_New",  dto.FileName_New),
            new SqlParameter("@FilePath",  dto.FilePath),
            new SqlParameter("@ActionRemarks",  dto.ActionRemarks),
            new SqlParameter("@CreatedBy_EmpNo",  dto.CreatedBy_EmpNo),
            new SqlParameter("@CreatedBy_EmpName",  dto.CreatedBy_EmpName),
            new SqlParameter("@Attribute1",  dto.Attribute1),
            new SqlParameter("@Attribute2",  dto.Attribute2)
        };
    }
    #endregion

    #region MPD Attachment

    public DataTable get_RFQ_MPD_Attachment(DbProviderFactory factory, string ConStr, string transType, string Keyword,string ControlNo)
    {
        SqlParameter[] parms = new SqlParameter[]
            {
                new SqlParameter("@TransType", transType),
                new SqlParameter("@Keyword", Keyword),
                new SqlParameter("@ControlNo", ControlNo)
            };
        try
        {
            return dbLayer.getDataTable(factory, ConStr, "dbo.sp_Select_RFQ_MPD_Attachment", CommandType.StoredProcedure, parms);
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    public string Insert_RFQ_MPD_Attachment(DbProviderFactory factory, string ConStr, cl_DataTransferObject dto, string transType)
    {
        try
        {
            return dbLayer.Scalar(factory, ConStr, "dbo.sp_Insert_RFQ_MPD_Attachment", CommandType.StoredProcedure, fillMPDAttachment(dto, transType)).ToString();
        }
        catch (Exception ex)
        {
            return "Request Timeout";
        }
    }


    SqlParameter[] fillMPDAttachment(cl_DataTransferObject dto, string transType)
    {
        return new SqlParameter[]
        {
            new SqlParameter("@TransType",  transType),
            new SqlParameter("@ID",  dto.ID),
            new SqlParameter("@RefID",  dto.RefID),
            new SqlParameter("@ControlNo",  dto.ControlNo),
            new SqlParameter("@FileName_Orig",  dto.FileName_Orig),
            new SqlParameter("@FileName_New",  dto.FileName_New),
            new SqlParameter("@FilePath",  dto.FilePath),
            new SqlParameter("@ActionRemarks",  dto.ActionRemarks),
            new SqlParameter("@CreatedBy_EmpNo",  dto.CreatedBy_EmpNo),
            new SqlParameter("@CreatedBy_EmpName",  dto.CreatedBy_EmpName),
            new SqlParameter("@Attribute1",  dto.Attribute1),
            new SqlParameter("@Attribute2",  dto.Attribute2)
        };
    }
    #endregion







}