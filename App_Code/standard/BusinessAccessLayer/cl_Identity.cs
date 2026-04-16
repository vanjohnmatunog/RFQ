using System;
using System.Net;
using System.Web;

/// <summary>
/// Summary description for cl_Identity
/// </summary>
public class cl_Identity
{
    //Standard  - Do Not Alter the Keywords or method name
    public static string Get_UserID()
    {
        string UserName = string.Empty;

        // ================================
        UserName = TIPIS3WebAdmin.TIPUtilities.Identity.Website.Get_UserID();
        //Administrator
        //UserName = "z8316tip"; // Sir Van
        //UserName = "z7245tip"; // Maam Chi
        //UserName = "z7857tip"; //Warren
        //UserName = "z8226tip"; // Sir Neil

        //MPD
        //UserName = "z6144tip"; // Maam Cecilio
        //UserName = "z2289tip"; // Maam Graces
        //UserName = "z7433tip"; // Maam Ruth
        //UserName = "z7836tip"; // Sir Jaymar

        //Approver
        //UserName = "z1872tip"; // Sir Shed
        //UserName = "z2151tip"; // Sir Myko
        //UserName = "z1694tip"; // Maam Duran
        //UserName = "z0011tip"; // Maam Beverly
        //UserName = "z0474tip"; // Maam Riza

        //Requestor
        //UserName = "z7832tip"; //Sir Jims
        //UserName = "z8257tip"; //Sir LLyod

        return UserName;
    }

    public static string Get_ComputerName()
    {
        string PCName = string.Empty;
        try
        {
            PCName = (Dns.GetHostEntry(HttpContext.Current.Request.ServerVariables["remote_addr"]).HostName);
            //if (PCName.Contains(".")) { PCName = PCName.Remove(10, ".tip1.ap.toshiba.dpg.local".Length); }
            if (PCName.Length > 10)
            {
                PCName = PCName.Substring(0, 10);
            }
        }
        catch (Exception ex)
        {
            string err = ex.Message;
            PCName = Get_IPAddress();
        }
        return PCName;
    }

    public static string Get_IPAddress()
    {
        string nowIP = "";
        try { if (nowIP == "") { nowIP = HttpContext.Current.Request.ServerVariables["remote_addr"].ToString(); } }
        catch (Exception ex) { string err = ex.Message; }
        return nowIP;
    }

    public static bool ISWebAdministrator()
    {
        bool PCName = true;
        return PCName;
    }
}