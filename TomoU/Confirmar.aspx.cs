using System;
using System.Linq;
using TomoU.Extensions;

namespace TomoU
{
    public partial class Confirmar : System.Web.UI.Page
    {
        public static int intRegistroID = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    if (Request.QueryString.Count > 0)
                    {
                        if (Request.QueryString["var"] != null)
                        {
                            String ID = Security.Decrypt(Request.QueryString["var"].ToString());
                            intRegistroID = Convert.ToInt32(ID);
                            if (intRegistroID > 0)
                            {
                                using (var db = new App_Data.TomoUDBEntities())
                                {
                                    if (db.tbl_CAT_Registro.Any(i => i.intRegistroID == intRegistroID && (bool)i.bitConfirmado))
                                    {
                                        divConfirmar.Visible = false;
                                        lblMensaje.Text = "Ya se realizó la confirmación del registro.";
                                        lblMensaje.Visible = true;
                                    }
                                    else
                                    {
                                        divConfirmar.Visible = true;
                                        lblMensaje.Text = "";
                                        lblMensaje.Visible = false;
                                    }
                                }
                            }
                            else
                            {
                                divConfirmar.Visible = false;
                                lblMensaje.Text = "No es posible obtener el numero de registro";
                                lblMensaje.Visible = true;
                            }
                        }
                        else
                        {
                            divConfirmar.Visible = false;
                            lblMensaje.Text = "No es posible obtener el numero de registro";
                            lblMensaje.Visible = true;
                        }
                    }
                }
            }
            catch (Exception er)
            {
                Log.EscribeLog("Existe un error al confirmar: " + er.Message);
            }
        }

        protected void btnConfirmar_Click(object sender, EventArgs e)
        {
            bool valido = false;
            string site = "";
            try
            {
                if (intRegistroID > 0)
                {
                    using (var db = new App_Data.TomoUDBEntities())
                    {
                        var sitio = db.tbl_SYS_Configuraciones.FirstOrDefault(i => i.vchConfiguracion == "ubicacion_sitio");
                        var registro = db.tbl_CAT_Registro.FirstOrDefault(i => i.intRegistroID == intRegistroID);
                        registro.bitConfirmado = true;
                        db.SaveChanges();
                        valido = true;
                        site = sitio.vchValor;
                    }
                    if (valido)
                    {
                        divConfirmar.Visible = false;
                        lblMensaje.Text = "Ya se realizó la confirmación del registro.";
                        lblMensaje.Visible = true;
                        Response.Redirect(site);
                    }
                }
                else
                {
                    divConfirmar.Visible = false;
                    lblMensaje.Text = "No es posible obtener el numero de registro";
                    lblMensaje.Visible = true;
                }
            }
            catch (Exception eR)
            {
                Log.EscribeLog("Existe un error al confirmar: " + eR.Message);
            }
        }
    }
}