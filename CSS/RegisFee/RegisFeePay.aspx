<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RegisFeePay.aspx.cs" Inherits="ProgramFiles_PG_RegisFee_RegisFeePay" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Untitled Page</title>
    <script type="text/javascript" src="../../../Resources/JSFiles/jquery-1.7.2.min.js"></script>
           <script type="text/javascript">
$(document).ready(function(){
     $('#form1').submit();
     alert('12');
});
</script>
</head>
<body>
    <form id="form1" runat="server" name="form1" method="post" >
        <div id="frmError" runat="server">
            <%--<span style="color: red">Please fill all mandatory fields.</span>--%>
            <br />
            <br />
            <input type="hidden" runat="server" id="key" name="key" />
            <input type="hidden" runat="server" id="hash" name="hash" />
            <input type="hidden" runat="server" id="txnid" name="txnid" />
            <%--<center>--%>
            <table>
                <tr style="display: none;">
                    <td>
                        <b>Mandatory Parameters</b></td>
                </tr>
                <tr>
                    <td>
                        Amount:
                    </td>
                    <td>
                        <asp:TextBox ID="amount" runat="server" Enabled="false" /></td>
                    <td>
                        First Name:
                    </td>
                    <td>
                        <%--<asp:TextBox ID="firstname" runat="server" />--%>
                        <input type="hidden" runat="server" id="firstname" name="firstname" />
                        </td>
                </tr>
                <tr>
                    <td>
                        Email:
                    </td>
                    <td>
                        <%--<asp:TextBox ID="email" runat="server" />--%>
                        <input type="hidden" runat="server" id="email" name="email" />
                        </td>
                    <td>
                        Phone:
                    </td>
                    <td>
                        <%--<asp:TextBox ID="phone" runat="server" />--%>
                        <input type="hidden" runat="server" id="phone" name="phone" />
                        </td>
                </tr>
                <tr>
                    <td>
                        Product Info:
                    </td>
                    <td colspan="3">
                    <input type="hidden" runat="server" id="productinfo" name="productinfo" />
                        <%--<asp:TextBox ID="productinfo" runat="server" Enabled="false" />--%>
                        </td>
                </tr>
                <tr style="display: none;">
                    <td>
                        Success URI:
                    </td>
                    <td colspan="3">
                    <input type="hidden" runat="server" id="surl" name="surl" />
                        <%--<asp:TextBox ID="surl" runat="server" />--%></td>
                </tr>
                <tr style="display: none;">
                    <td>
                        Failure URI:
                    </td>
                    <td colspan="3">
                        <asp:TextBox ID="furl" runat="server" /></td>
                </tr>
                <tr style="display: none;">
                    <td>
                        <b>Optional Parameters</b></td>
                </tr>
                <tr style="display: none;">
                    <td>
                        Last Name:
                    </td>
                    <td>
                        <asp:TextBox ID="lastname" runat="server" /></td>
                    <td>
                        Cancel URI:
                    </td>
                    <td>
                        <asp:TextBox ID="curl" runat="server" /></td>
                </tr>
                <tr style="display: none;">
                    <td>
                        Address1:
                    </td>
                    <td>
                        <asp:TextBox ID="address1" runat="server" /></td>
                    <td>
                        Address2:
                    </td>
                    <td>
                        <asp:TextBox ID="address2" runat="server" /></td>
                </tr>
                <tr style="display: none;">
                    <td>
                        City:
                    </td>
                    <td>
                        <asp:TextBox ID="city" runat="server" /></td>
                    <td>
                        State:
                    </td>
                    <td>
                        <asp:TextBox ID="state" runat="server" /></td>
                </tr>
                <tr style="display: none;">
                    <td>
                        Country:
                    </td>
                    <td>
                        <asp:TextBox ID="country" runat="server" /></td>
                    <td>
                        Zipcode:
                    </td>
                    <td>
                        <asp:TextBox ID="zipcode" runat="server" /></td>
                </tr>
                <tr style="display: none;">
                    <td>
                        UDF1:
                    </td>
                    <td>
                        <asp:TextBox ID="udf1" runat="server" /></td>
                    <td>
                        UDF2:
                    </td>
                    <td>
                        <asp:TextBox ID="udf2" runat="server" /></td>
                </tr>
                <tr style="display: none;">
                    <td>
                        UDF3:
                    </td>
                    <td>
                        <asp:TextBox ID="udf3" runat="server" /></td>
                    <td>
                        UDF4:
                    </td>
                    <td>
                        <asp:TextBox ID="udf4" runat="server" /></td>
                </tr>
                <tr style="display: none;">
                    <td>
                        UDF5:
                    </td>
                    <td>
                        <asp:TextBox ID="udf5" runat="server" /></td>
                    <td>
                        PG:
                    </td>
                    <td>
                        <asp:TextBox ID="pg" runat="server" Text="CC" /></td>
                </tr>
                <tr>
                    <td colspan="4">
                    </td>
                </tr>
            </table>
            <br />
            <%--<asp:Button ID="submit" Text="submit" Width="100px" runat="server" OnClick="Button1_Click"
                    Style="display: none;" />--%>
            <%--</center>--%>
        </div>
    </form>
</body>
</html>
