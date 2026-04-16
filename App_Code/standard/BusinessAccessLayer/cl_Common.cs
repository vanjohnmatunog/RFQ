using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

/// <summary>
/// Summary description for cl_Common
/// </summary>
public class cl_Common
{
    public string appEmail { get; set; }

    public static Boolean isValidDate(string dateTime)
    {
        bool result = false;
        DateTime d;
        try
        {
            d = Convert.ToDateTime(dateTime);
            result = true;
        }
        catch (Exception ex) { }
        return result;
    }

    public static Boolean isInteger(string myString)
    {
        bool result = false;
        int number = 0;
        try
        {
            number = Convert.ToInt32(myString);
            result = true;
        }
        catch (Exception ex) { }
        return result;
    }
    
    public static DataTable get_TIP_Site()
    {
        TIPIS3WebAdmin.TIPEmployeeMaster.TIPSite tipSite = new TIPIS3WebAdmin.TIPEmployeeMaster.TIPSite(cl_DBConn.MSSQLEmp());
        DataTable dt = new DataTable();
        tipSite.Fill(dt);

        return dt;
    }

    public static DataTable get_TIP_Department()
    {
        TIPIS3WebAdmin.TIPEmployeeMaster.Department tipDept = new TIPIS3WebAdmin.TIPEmployeeMaster.Department(cl_DBConn.MSSQLEmp());
        DataTable dt = new DataTable();
        tipDept.All(dt);

        return dt;
    }
    public static DataTable get_TIP_Section(string depCode)
    {
        TIPIS3WebAdmin.TIPEmployeeMaster.Section tipSection = new TIPIS3WebAdmin.TIPEmployeeMaster.Section(cl_DBConn.MSSQLEmp());
        DataTable dt = new DataTable();
        tipSection.DepCode(dt, depCode);

        return dt;
    }

    public static void fill_RadioButtonList(RadioButtonList rbl, DataTable dtValue, string valueField, string textField)
    {
        try
        {
            if (dtValue.Rows.Count > 0)
            {
                rbl.DataSource = dtValue;
                rbl.DataTextField = textField;
                rbl.DataValueField = valueField;
                rbl.DataBind();
            }
        }
        catch (Exception ex)
        {
            //
        }
    }

    public static void fill_DropDownList(DropDownList ddl, DataTable dtValue, string valueField, string textField, string firstItem)
    {
        try
        {
            ddl.DataSource = dtValue;
            ddl.DataTextField = textField;
            ddl.DataValueField = valueField;
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem(firstItem, "0"));
        }
        catch (Exception ex)
        {
            //
        }
    }

    public static int fill_DropDownList1(DropDownList ddl, DataTable dtValue, string valueField, string textField, string firstItem)
    {
        try
        {
            if (dtValue.Rows.Count > 0)
            {
                ddl.DataSource = dtValue;
                ddl.DataTextField = textField;
                ddl.DataValueField = valueField;
                ddl.DataBind();
                ddl.Items.Insert(0, new ListItem(firstItem, "0"));
            }

            return dtValue.Rows.Count;
        }
        catch (Exception ex)
        {
            return -1;
        }
    }
    public static void getApprovers(string position, string DepCode, DropDownList ddl, string ddlfield, string ddlvalue)
    {
        DataTable dt = new DataTable();
        switch (position)
        {
            case "Approver1":


                //string sql = "SELECT [EmpNo],[LastEmpName] FROM [dbo].[VW_AP_SUPERVISOR] WHERE DepCode='" + DepCode + "'";

                //TIPIS3WebAdmin.TIPEmployeeMaster.CustomQuery cq = new TIPIS3WebAdmin.TIPEmployeeMaster.CustomQuery(cl_DBConn.MSSQLTrans1());
                //string sql = "SELECT [Type],[Description] FROM [dbo].[tbl_Maintenance] WHERE Active = 'True' AND Category = 'TDEG_Admin'";

                TIPIS3WebAdmin.TIPEmployeeMaster.CustomQuery cq = new TIPIS3WebAdmin.TIPEmployeeMaster.CustomQuery(cl_DBConn.MSSQLEmp());

                string sql = "SELECT [EmpNo],[LastEmpName] FROM [dbo].[VW_AP_SUPERVISOR] WHERE DepCode='" + DepCode + "'" +
                    "UNION SELECT [EmpNo],[LastEmpName] FROM [dbo].[VW_AP_MANAGER] WHERE depcode='" + DepCode + "' and  BU = 1 ORDER BY LastEmpName";
                cq.Fill(dt, sql);

                ddl.DataSource = dt;
                ddl.DataTextField = ddlfield;
                ddl.DataValueField = ddlvalue;
                ddl.DataBind();

                ddl.Items.Insert(0, "-- Please Select --");

                break;

            case "Approver2":


                TIPIS3WebAdmin.TIPEmployeeMaster.CustomQuery cq2 = new TIPIS3WebAdmin.TIPEmployeeMaster.CustomQuery(cl_DBConn.MSSQLEmp());
                string sql2 = "SELECT [EmpNo],[LastEmpName] FROM [dbo].[VW_AP_MANAGER] WHERE BU = '1' AND DepCode='" + DepCode + "'" +
                    "and  BU = 1 UNION SELECT [EmpNo],[LastEmpName] FROM [dbo].[VW_AP_DEPARTMENTHEAD] WHERE depcode='" + DepCode + "' and  BU = 1 ORDER BY LastEmpName";

                //TIPIS3WebAdmin.TIPEmployeeMaster.CustomQuery cq2 = new TIPIS3WebAdmin.TIPEmployeeMaster.CustomQuery(cl_DBConn.MSSQLTrans1());
                //string sql2 = "SELECT [Type],[Description] FROM [dbo].[tbl_Maintenance] WHERE Category = 'TDEG_Approver'";

                cq2.Fill(dt, sql2);


                ddl.DataSource = dt;
                ddl.DataTextField = ddlfield;
                ddl.DataValueField = ddlvalue;
                ddl.DataBind();

                ddl.Items.Insert(0, "-- Please Select --");

                break;
            //case "Approver2":

            //    TIPIS3WebAdmin.TIPEmployeeMaster.CustomQuery cq3 = new TIPIS3WebAdmin.TIPEmployeeMaster.CustomQuery(cl_DBConn.MSSQLEmp());
            //    string sql3 = "SELECT [FirstEmpName],[EmpEmail] +'@'+ [EmailDomain] AS [EmpEmail] FROM [dbo].[VW_AP_DEPARTMENTHEAD_ALL] WHERE BU = '1' AND DepCode= '" + DepCode + "'";

            //    //TIPIS3WebAdmin.TIPEmployeeMaster.CustomQuery cq3 = new TIPIS3WebAdmin.TIPEmployeeMaster.CustomQuery(cl_DBConn.MSSQLTrans1());
            //    //string sql3 = "SELECT [Type],[Description] FROM [dbo].[tbl_Maintenance] WHERE Category = 'TDEG_Approver'";

            //    cq3.Fill(dt, sql3);

            //    ddl.DataSource = dt;
            //    //ddl.DataTextField = ddlfield;
            //    //ddl.DataValueField = ddlvalue;
            //    ddl.DataTextField = "FirstEmpName";
            //    ddl.DataValueField = "EmpEmail"; ;
            //    ddl.DataBind();

            //    ddl.Items.Insert(0, "Please Select");

            //    break;
        }

    }
    //public static void getApprovers(string DepCode, DropDownList ddl, string ddlfield, string ddlvalue)
    //{
    //    DataTable dt = new DataTable();

    //    //TIPIS3WebAdmin.TIPEmployeeMaster.Approver.Manager sv;
    //    //sv = new TIPIS3WebAdmin.TIPEmployeeMaster.Approver.Manager(cl_DBConn.MSSQLEmp());
    //    //sv.Fill(dt, DepCode);
    //    TIPIS3WebAdmin.TIPEmployeeMaster.CustomQuery cq = new TIPIS3WebAdmin.TIPEmployeeMaster.CustomQuery(cl_DBConn.MSSQLEmp());
    //    string sql = "SELECT [EmpNo],[LastEmpName] FROM [dbo].[VW_AP_SUPERVISOR] WHERE depcode='" + DepCode +
    //        "' and  BU = 1 UNION SELECT [EmpNo],[LastEmpName] FROM [dbo].[VW_AP_MANAGER] WHERE depcode='" + DepCode + 
    //        "' and  BU = 1 ORDER BY LastEmpName";
    //    cq.Fill(dt, sql);

    //    ddl.DataSource = dt;
    //    ddl.DataTextField = ddlfield;
    //    ddl.DataValueField = ddlvalue;
    //    ddl.DataBind();
    //    ddl.Items.Insert(0, "-- Please Select --");

    //}
    //public static void CheckApproverAcces(string DepCode, string empNo, GridView approver)
    //{
    //    DataTable dtApp = new DataTable();
    //    TIPIS3WebAdmin.TIPEmployeeMaster.CustomQuery cq = new TIPIS3WebAdmin.TIPEmployeeMaster.CustomQuery(cl_DBConn.MSSQLEmp());
    //    string sql = "SELECT TOP 1 [EmpNo],[LastEmpName] FROM [dbo].[VW_AP_SUPERVISOR] WHERE depcode='" + DepCode +
    //        "' and  BU = 1 UNION SELECT [EmpNo],[LastEmpName] FROM [dbo].[VW_AP_MANAGER] WHERE depcode='" + DepCode +
    //        "' and  BU = 1 and EmpNo= '" + empNo + "' ORDER BY LastEmpName";

    //    cq.Fill(dtApp, sql);
    //    approver.DataSource = dtApp;

    //    if (dtLink != null && dtLink.Rows.Count > 0)
    //    {
    //        hdnLink.Value = !string.IsNullOrEmpty(dtLink.Rows[0]["Link"].ToString()) ? dtLink.Rows[0]["Link"].ToString() : "";
    //    }
    //}
    public static string getDepCode(string EmpNo)
    {
        string IssuerDepCode = string.Empty;
        SqlConnection sqlConnection = new SqlConnection(cl_DBConn.MSSQLEmp1());
        string sqlQuery = "SELECT TOP 1 [DepCode] FROM [dbo].[VW_WEB_ALL_EMP_ALL] WHERE EmpNo = '" + EmpNo + "'";
        sqlConnection.Open();
        SqlDataAdapter da = new SqlDataAdapter(sqlQuery, sqlConnection);

        DataSet ds = new DataSet();

        try
        {
            da.Fill(ds);
            IssuerDepCode = ds.Tables[0].Rows[0]["DepCode"].ToString();
        }

        catch (Exception e)
        {
            string a = e.Message;
        }
        sqlConnection.Close();
        return IssuerDepCode;
    }

}