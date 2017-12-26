using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Security.Cryptography;
using System.Text;
using System.Net;
using System.IO;
using IYCFreameWork;
using System.Collections.Specialized;

public partial class ProgramFiles_PG_RegisFee_RegisFeePay : System.Web.UI.Page
{
    public string action1 = string.Empty;
    public string hash1 = string.Empty;
    public string txnid1 = string.Empty;
    string strHTTP_HOST = "";
    static string UniqueId = "";
    NameValueCollection collection = new NameValueCollection();

    protected void Page_Load(object sender, EventArgs e)
    {
        IYCDataUtility objIYC = new IYCDataUtility();

        strHTTP_HOST = HttpContext.Current.Request.Url.Host.ToUpper();
        try
        {
            if (Request.QueryString.HasKeys())
            {
                UniqueId = Request.QueryString["ID"].ToString();
            }



            DataTable dtData = new DataTable();
            dtData = objIYC.GetDataTable("select amount,firstname,email,phone,productinfo from FM_PMGRegistrationFee where uniquetxnno='" + UniqueId + "'");
            if (dtData.Rows.Count > 0)
            {
                amount.Text = dtData.Rows[0]["amount"].ToString();


                collection.Add("amount", dtData.Rows[0]["amount"].ToString());
                /*collection.Add("firstname", dtData.Rows[0]["firstname"].ToString());
                collection.Add("email", dtData.Rows[0]["email"].ToString());
                collection.Add("phone", dtData.Rows[0]["phone"].ToString());
                collection.Add("productinfo", dtData.Rows[0]["productinfo"].ToString());*/
                firstname.Value = dtData.Rows[0]["firstname"].ToString();
                email.Value = dtData.Rows[0]["email"].ToString();
                phone.Value = dtData.Rows[0]["phone"].ToString();
                productinfo.Value = dtData.Rows[0]["productinfo"].ToString();
            }

            /*amount.Text = Request.Form["amount"];          
            firstname.Value = Request.Form["firstname"];
            email.Value = Request.Form["email"];
            phone.Value = Request.Form["phone"];
            productinfo.Value = Request.Form["productinfo"];*/

            if ((strHTTP_HOST == "LOCALHOST") || (strHTTP_HOST == "DOTNET"))
            {
                surl.Value = "http://dotnet/DPSBopal/ProgramFiles/PG/ResponseHandling.aspx";
                furl.Text = "http://dotnet/DPSBopal/ProgramFiles/PG/ResponseHandling.aspx";
                curl.Text = "http://dotnet/DPSBopal/ProgramFiles/PG/ResponseHandling.aspx";
            }
            else
            {
                surl.Value = "http://dpsbopal.iycworld.com/ProgramFiles/PG/ResponseHandling.aspx";
                furl.Text = "http://dpsbopal.iycworld.com/ProgramFiles/PG/ResponseHandling.aspx";
                curl.Text = "http://dpsbopal.iycworld.com/ProgramFiles/OPG/ResponseHandling.aspx";
            }




            key.Value = ConfigurationManager.AppSettings["MERCHANT_KEY"];
            form1.Action = "SubmitRedirect.aspx";
            /*if (!IsPostBack)
            {
                //frmError.Visible = false; // error form
            }
            else
            {
                //frmError.Visible = true;
            }
            if (string.IsNullOrEmpty(Request.Form["hash"]))
            {
                submit.Visible = true;
            }
            else
            {
                submit.Visible = false;
            }*/



        }
        catch (Exception ex)
        {
            Response.Write("<span style='color:red'>" + ex.Message + "</span>");

        }

    }



}
