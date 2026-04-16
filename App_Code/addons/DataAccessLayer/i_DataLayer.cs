using System.Data;
using System.Data.Common;

/// <summary>
/// Summary description for i_DataLayer
/// </summary>
public interface i_DataLayer
{
    #region Project Information
    bool Check_Currency(DbProviderFactory factory, string ConStr, string transType, string Currency);
    string Insert_RFQ_Request(DbProviderFactory factory, string ConStr, cl_RFQ_TransactionObject trans, DataTable dtRFQ_List, string transType);
    string Approve_Deny(DbProviderFactory factory, string ConStr, cl_RFQ_TransactionObject trans, string transType, string Remarks);
    string Resubmit(DbProviderFactory factory, string ConStr, cl_RFQ_TransactionObject trans, string transType, string Remarks);
    string Status_perItem(DbProviderFactory factory, string ConStr, cl_RFQ_StatusperItemObject stat, string transType, string Remarks);
    string Automatic_Closed(DbProviderFactory factory, string ConStr, cl_RFQ_TransactionObject trans);
    
    DataTable get_RFQ_Transaction_Approvers(DbProviderFactory factory, string ConStr, string TransType, string Keyword);
    DataTable get_RFQ_HistoryLogs(DbProviderFactory factory, string ConStr, string Keyword);
    DataTable get_RFQ_MPD_HistoryLogs(DbProviderFactory factory, string ConStr, string Keyword, string ControlNo);
    DataTable get_RFQ_Approvers(DbProviderFactory factory, string ConStr, string TransType, string Keyword);
    DataTable get_perItem(DbProviderFactory factory, string ConStr, string TransType, string TempItemCode);
    DataSet Select_Details_Of_Request(DbProviderFactory factory, string ConStr, string transType, string RefID, string Keyword, string DateFrom, string DateTo);
    DataTable get_RFQ_Transaction(DbProviderFactory factory, string ConStr, string TransType, string Status, string Keyword, string fromDate, string toDate, string userEmpNo);
    DataTable get_RFQ_Per_Item(DbProviderFactory factory, string ConStr, string TransType, string RefID);

    DataTable Automatic_Closed(DbProviderFactory factory, string ConStr, string TransType, string RefID);
    #endregion

    #region Maintenance
    string Insert_LOVMasterList(DbProviderFactory factory, string ConStr, cl_MaintenanceObject mo, string transType);
    DataTable getLOVMasterlist(DbProviderFactory factory, string ConStr, string transType, string category, string EmpNo);
    bool Check_UserAccess(DbProviderFactory factory, string ConStr, string transType, string Category, string EmpNo);


    #endregion


    #region Attachment
    DataTable get_RFQ_Attachment(DbProviderFactory factory, string ConStr, string transType, string Keyword);
    string Insert_RFQ_Attachment(DbProviderFactory factory, string ConStr, cl_DataTransferObject dto, string transType);
    #endregion

    #region MPD Attachment
    DataTable get_RFQ_MPD_Attachment(DbProviderFactory factory, string ConStr, string transType, string Keyword,string ControlNo);
    string Insert_RFQ_MPD_Attachment(DbProviderFactory factory, string ConStr, cl_DataTransferObject dto, string transType);
    #endregion

}