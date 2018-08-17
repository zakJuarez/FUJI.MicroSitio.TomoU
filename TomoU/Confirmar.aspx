<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Confirmar.aspx.cs" Inherits="TomoU.Confirmar" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css" integrity="sha384-BVYiiSIFeK1dGmJRAkycuHAHRg32OmUcww7on3RYdg4Va+PmSTsz/K68vbdEjh4u" crossorigin="anonymous">
</head>
<body>

    <form id="form1" runat="server">
        <div class="col-lg-12 col-md-12 col-sm-12">
            <div class="text-center">
                <div class="row">
                    <img src="app/assets/Logo TOMO-U.png" style="height:60px; width:auto; margin: 10px 10px 0 10px;">
                </div>
                <div class="row">
                    <h2><asp:Label runat="server" Text="" ID="lblMensaje" Visible="false"></asp:Label></h2>
                </div>
            </div> 
            <div id="divConfirmar" runat="server" class="row">
                <div class="text-center">
                    <h2>Por favor confirma tu registro</h2>
                    <table width="100%">
                        <tr>
                            <td width="30%"></td>
                            <td align='right' style='font-family: Calibri,Trebuchet,Arial,sans-serif; font-weight: 300; font-stretch: normal; text-align: center; color: #fff; font-size: 15px; background: #e91e63; border-radius: 7px!important; line-height: 1.45em; padding: 7px 15px 8px; font-size: 1em; padding-bottom: 7px; margin: 0 auto 16px' valign='middle'>
                                <asp:Button ID="btnConfirmar" runat="server" Text="Confirmar" OnClick="btnConfirmar_Click" Width="100%" CssClass="butTrans" />
                            </td>
                            <td width="30%"></td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
    </form>
    <style type="text/css">
        .butTrans {
            background-color: transparent;
            border:none;
        }
    </style>
</body>
</html>
 