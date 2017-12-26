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

public partial class ProgramFiles_PG_RegisFee_SubmitRedirect : System.Web.UI.Page
{
    public string action1 = string.Empty;
    public string hash1 = string.Empty;
    public string txnid1 = string.Empty;
    string strHTTP_HOST = "";
    static string UniqueId = "";
    NameValueCollection collection = new NameValueCollection();

    protected void Page_Load(object sender, EventArgs e)
    {


        /*foreach (string key in Request.Form.Keys)
        {
            Response.Write(key + "-->" + Request.Form[key].ToString() + "<br>");
        }
        Response.End();*/
        IYCDataUtility objIYC = new IYCDataUtility();


        strHTTP_HOST = HttpContext.Current.Request.Url.Host.ToUpper();
        try
        {
            if (Request.QueryString.HasKeys())
            {
                UniqueId = Request.QueryString["UID"].ToString();
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
                firstname.Text = dtData.Rows[0]["firstname"].ToString();
                email.Text = dtData.Rows[0]["email"].ToString();
                phone.Text = dtData.Rows[0]["phone"].ToString();
                productinfo.Text = dtData.Rows[0]["productinfo"].ToString();
            }
            else
            {
                amount.Text = "1";
                collection.Add("amount", "1");
            }
            /*amount.Text = Request.Form["amount"];          */
            firstname.Text = Request.Form["firstname"];
            email.Text = Request.Form["email"];
            phone.Text = Request.Form["phone"];
            productinfo.Text = Request.Form["productinfo"];

            if ((strHTTP_HOST == "LOCALHOST") || (strHTTP_HOST == "DOTNET"))
            {
                surl.Text = Request.Form["surl"];// "http://dotnet/DPSBopal/ProgramFiles/OnlineFeePayment/FeeBillReturnSave.asp";
                furl.Text = Request.Form["surl"];// "http://dotnet/DPSBopal/ProgramFiles/OnlineFeePayment/FeeBillReturnSave.asp";
                curl.Text = Request.Form["surl"];// "http://dotnet/DPSBopal/ProgramFiles/OnlineFeePayment/FeeBillReturnSave.asp";
            }
            else
            {
                surl.Text = "http://dpsbopal.iycworld.com/ProgramFiles/OnlineFeePayment/FeeBillReturnSave.asp";
                furl.Text = "http://dpsbopal.iycworld.com/ProgramFiles/OnlineFeePayment/FeeBillReturnSave.asp";
                curl.Text = "http://dpsbopal.iycworld.com/ProgramFiles/OnlineFeePayment/FeeBillReturnSave.asp";
            }




            key.Value = ConfigurationManager.AppSettings["MERCHANT_KEY"];
            PayNow();
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



    public string Generatehash512(string text)
    {

        byte[] message = Encoding.UTF8.GetBytes(text);

        UnicodeEncoding UE = new UnicodeEncoding();
        byte[] hashValue;
        SHA512Managed hashString = new SHA512Managed();
        string hex = "";
        hashValue = hashString.ComputeHash(message);
        foreach (byte x in hashValue)
        {
            hex += String.Format("{0:x2}", x);
        }
        return hex;
    }




    private string PreparePOSTForm(string url, System.Collections.Hashtable data)      // post form
    {
        //Set a name for the form
        string formID = "PostForm";
        //Build the form using the specified data to be posted.
        StringBuilder strForm = new StringBuilder();
        strForm.Append("<form id=\"" + formID + "\" name=\"" +
                       formID + "\" action=\"" + url +
                       "\" method=\"POST\">");

        foreach (System.Collections.DictionaryEntry key in data)
        {

            strForm.Append("<input type=\"hidden\" name=\"" + key.Key +
                           "\" value=\"" + key.Value + "\">");
        }


        strForm.Append("</form>");
        //Build the JavaScript which will do the Posting operation.
        StringBuilder strScript = new StringBuilder();
        strScript.Append("<script language='javascript'>");
        strScript.Append("var v" + formID + " = document." +
                         formID + ";");
        strScript.Append("v" + formID + ".submit();");
        strScript.Append("</script>");
        //Return the form and the script concatenated.
        //(The order is important, Form then JavaScript)

        return strForm.ToString() + strScript.ToString();
    }




    protected void PayNow()
    {
        try
        {

            string[] hashVarsSeq;
            string hash_string = string.Empty;


            if (string.IsNullOrEmpty(Request.Form["txnid"])) // generating txnid
            {
                Random rnd = new Random();
                string strHash = Generatehash512(rnd.ToString() + DateTime.Now);
                txnid1 = strHash.ToString().Substring(0, 20);

            }
            else
            {
                txnid1 = Request.Form["txnid"];
            }
            if (string.IsNullOrEmpty(Request.Form["hash"])) // generating hash value
            {
                if (
                    string.IsNullOrEmpty(ConfigurationManager.AppSettings["MERCHANT_KEY"]) ||
                    string.IsNullOrEmpty(txnid1) ||
                    string.IsNullOrEmpty(collection["amount"]) ||
                    string.IsNullOrEmpty(firstname.Text) ||
                    string.IsNullOrEmpty(email.Text) ||
                    string.IsNullOrEmpty(phone.Text) ||
                    string.IsNullOrEmpty(productinfo.Text)
                    )
                {
                    //error

                    frmError.Visible = true;
                    return;
                }

                else
                {
                    frmError.Visible = false;
                    hashVarsSeq = ConfigurationManager.AppSettings["hashSequence"].Split('|'); // spliting hash sequence from config
                    hash_string = "";
                    foreach (string hash_var in hashVarsSeq)
                    {
                        if (hash_var == "key")
                        {
                            hash_string = hash_string + ConfigurationManager.AppSettings["MERCHANT_KEY"];
                            hash_string = hash_string + '|';
                        }
                        else if (hash_var == "txnid")
                        {
                            hash_string = hash_string + txnid1;
                            hash_string = hash_string + '|';
                        }
                        else if (hash_var == "amount")
                        {
                            hash_string = hash_string + Convert.ToDecimal(collection[hash_var]).ToString("g29");
                            hash_string = hash_string + '|';
                        }


                        else
                        {

                            hash_string = hash_string + (Request.Form[hash_var] != null ? Request.Form[hash_var] : "");// isset if else
                            hash_string = hash_string + '|';
                        }
                    }

                    hash_string += ConfigurationManager.AppSettings["SALT"];// appending SALT

                    hash1 = Generatehash512(hash_string).ToLower();         //generating hash
                    action1 = ConfigurationManager.AppSettings["PAYU_BASE_URL"] + "/_payment";// setting URL

                    

                }


            }

            else if (!string.IsNullOrEmpty(Request.Form["hash"]))
            {
                hash1 = Request.Form["hash"];
                action1 = ConfigurationManager.AppSettings["PAYU_BASE_URL"] + "/_payment";

            }




            if (!string.IsNullOrEmpty(hash1))
            {
                hash.Value = hash1;
                txnid.Value = txnid1;

                System.Collections.Hashtable data = new System.Collections.Hashtable(); // adding values in gash table for data post
                data.Add("hash", hash.Value);
                data.Add("txnid", txnid.Value);
                data.Add("key", key.Value);
                string AmountForm = Convert.ToDecimal(amount.Text.Trim()).ToString("g29");// eliminating trailing zeros
                amount.Text = AmountForm;
                data.Add("amount", AmountForm);
                data.Add("firstname", firstname.Text.Trim());
                data.Add("email", email.Text.Trim());
                data.Add("phone", phone.Text.Trim());
                data.Add("productinfo", productinfo.Text.Trim());
                data.Add("surl", surl.Text.Trim());
                data.Add("furl", furl.Text.Trim());
                data.Add("lastname", lastname.Text.Trim());
                data.Add("curl", curl.Text.Trim());
                data.Add("address1", address1.Text.Trim());
                data.Add("address2", address2.Text.Trim());
                data.Add("city", city.Text.Trim());
                data.Add("state", state.Text.Trim());
                data.Add("country", country.Text.Trim());
                data.Add("zipcode", zipcode.Text.Trim());
                data.Add("udf1", udf1.Text.Trim());
                data.Add("udf2", udf2.Text.Trim());
                data.Add("udf3", udf3.Text.Trim());
                data.Add("udf4", udf4.Text.Trim());
                data.Add("udf5", udf5.Text.Trim());
                data.Add("pg", pg.Text.Trim());


                string strForm = PreparePOSTForm(action1, data);
                Page.Controls.Add(new LiteralControl(strForm));

            }

            else
            {
                //no hash

            }

        }

        catch (Exception ex)
        {
            Response.Write("<span style='color:red'>" + ex.Message + "</span>");

        }



    }
}
