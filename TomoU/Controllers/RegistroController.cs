using Newtonsoft.Json.Linq;
using System;
using System.Linq;
using System.Web.Http;
using TomoU.App_Data;
using TomoU.Extensions;

namespace TomoU.Controllers
{
    [RoutePrefix("api/registro")]
    public class RegistroController : ApiController
    {
        [HttpPost]
        [Route("AgregarRegistro")]
        public object AgregarRegistroAsync([FromBody]JObject data)
        {
            dynamic response = null;
            try
            {

                var _registro = data["Registro"].ToObject<tbl_CAT_Registro>();
                _registro.bitActivo = true;
                _registro.datFecha = DateTime.Now;
                _registro.intEstadoID = _registro.intEstadoID == 0 ? null : _registro.intEstadoID;
                if (_registro.vchEmail == "")
                    throw new Exception("Se requiere un Correo para el registro.");
                if (_context.tbl_CAT_Registro.Any(i => i.vchEmail == _registro.vchEmail && (bool)i.bitConfirmado && (bool)i.bitActivo))
                    throw new Exception("Ya existe un registro con el correo: " + _registro.vchEmail);
                if (_context.tbl_CAT_Registro.Any(i => i.vchEmail == _registro.vchEmail && !(bool)i.bitConfirmado && (bool)i.bitActivo))
                    throw new Exception("Se requiere confirmar el registro, favor de verificar el correo: " + _registro.vchEmail);
                _context.tbl_CAT_Registro.Add(_registro);
                _context.SaveChanges();
                if (_registro.intRegistroID > 0)
                    enviarCorreoAsync(_registro.intRegistroID, _registro.vchEmail, _registro.vchNombre);
                response = new
                {
                    Message = "El registro " + _registro.vchEmail + " ha sido dado de alta exitosamente. Favor de revisar su correo para confirmar",
                    Success = true
                };
            }
            catch (Exception exc)
            {
                response = new
                {
                    Message = exc.Message,
                    Success = false
                };
            }
            return response;
        }

        private void enviarCorreoAsync(int intRegistroID, string vchEmail, string vchNombre)
        {
            dynamic response = null;
            try
            {
                string mensaje = "";
                string ubicacion_sitio = "";
                using (var db = new App_Data.TomoUDBEntities())
                {
                    var sitio = db.tbl_SYS_Configuraciones.FirstOrDefault(i => i.vchConfiguracion == "ubicacion_sitio");
                    ubicacion_sitio = sitio.vchValor;
                }
                string url = ubicacion_sitio + "/Confirmar.aspx?var=" + Security.Encrypt(intRegistroID.ToString());
                mensaje = "<table border='0' cellspacing='0' cellpadding='0' width='652' height='400'><tbody><tr><td><table border='0' cellspacing='0' cellpadding='0' width='650' height='400'><tbody><tr><td><table cellspacing='0' cellpadding='0' border='0'><tbody><tr height='24'><td></td></tr><tr><td width='25'></td><td style='font:ArialMT;font-family:Arial;font-size:16px;font-weight:bold;color:#e91e63' align='left' width='300' height='17'>Apreciable profesional de la salud:</td></tr><tr height='19'><td></td></tr></tbody></table></td><td><table cellspacing='0' cellpadding='0'><tbody><tr height='20'><td></td></tr><tr><td align='right' width='299.8'><img src='http://www.tomo-u.com/wp-content/uploads/2017/09/logo01-tomo-u-e1506445011780.png' class='CToWUd'></td><td width='20.2'></td></tr><tr height='15'><td></td></tr></tbody></table></td></tr><tr height='5'><td bgcolor='#e91e63' colspan='2'></td></tr><tr height='10'><td bgcolor='#fafafa' colspan='2'></td></tr><tr bgcolor='#fafafa'><td colspan='2'><table cellspacing='0' cellpadding='0' border='0'><tbody><tr><td width='25'></td><td><table bgcolor='#ffffff' cellspacing='0' cellpadding='0' border='0' width='600'><tbody><tr><td><table cellspacing='0' cellpadding='0' border='0'><tbody><tr><td width='20'></td><td style='font:ArialMT;font-family:Arial;font-size:10px;line-height:2;text-align:right;color:#8c8c8c' height='20' width='560'>FechadelCorreo</td><td width='20'></td></tr></tbody></table></td></tr><tr><td><table cellspacing='0' cellpadding='0' border='0'><tbody><tr height='10'><td></td></tr><tr height='4'><td></td></tr><tr height='22'><td colspan='2'><table cellspacing='0' cellpadding='0' border='0'><tbody><tr><td width='55'></td><td style='font:ArialMT;font-family:Arial;line-height:1.54;text-align:left;color:#666666;font-size:14px' width='300'><b>NombredelProfesional</b></td><td width='15'></td></tr></tbody></table></td></tr><tr height='16'><td></td></tr><tr height='18'><td><table cellspacing='0' cellpadding='0' border='0'><tbody><tr><td width='55'></td><td style='font:ArialMT;font-family:Arial;line-height:1.13;font-size:16px;text-align:left;color:#666666' width='510'><div style='text-align: justify; text-justify: inter-word;'>Se ha realizado el <b>registro</b> al evento de Tomo-U 2018; si usted no realizó el registro favor de hacer caso omiso al presente correo, de lo contrario, favor de confirmar a través del siguiente link:</div></td><td width='45'></td></tr></tbody></table></td></tr><tr height='8'><td></td></tr></tbody></table></td></tr><tr height='10'><td colspan='5'></td></tr><tr height='27'><td colspan='5'><table cellspacing='0' cellpadding='0' border='0'><tbody><tr><td width='50'></td><td style='font:ArialMT;font-family:Arial;font-size:14px;line-height:1.93;text-align:right;color:#666666' width='500'><table width=100%><tr><td width='250'></td><td align='right' class='m_6713843551829686631button_style' id='m_6713843551829686631button_text' style='font-family:Calibri,Trebuchet,Arial,sans-serif;font-weight:300;font-stretch:normal;text-align:center;color:#fff;font-size:15px;background:#0079c1;border-radius:7px!important;line-height:1.45em;padding:7px 15px 8px;font-size:1em;padding-bottom:7px;margin:0 auto 16px' valign='middle'><span class='m_6713843551829686631aloha-editable'><a type='Link' style='text-decoration:none;color:#ffffff' href='https://www.google.com' target='_blank'>Confirmar</a></span></td><td width='250'></td></tr></table></td><td width='50'></td></tr></tbody></table></td></tr><tr height='10'><td colspan='5'></td></tr></tbody></table></td><td width='25'></td></tr></tbody></table></td></tr><tr bgcolor='#fafafa'><td colspan='2'><table cellspacing='0' cellpadding='0' border='0'><tbody><tr><td colspan='3'></td></tr><tr><td width='30'></td><td style='font:ArialMT;font-family:Arial;font-size:14px;line-height:1.14;text-align:center;color:#e91e63' width='590'><b>Tomo-U 2018</b></td><td width='30'></td></tr><tr height='15'><td width='30'></td><td style='font:ArialMT;font-family:Arial;font-size:11px;line-height:1.14;text-align:center;color:#e91e63' width='590'><b>tomo-u@fujifilm.com.mx</b></td><td width='30'></td></tr><tr><td colspan='3'></td></tr></tbody></table></td></tr></tbody></table></td></tr></tbody></table>";
                mensaje = mensaje.Replace("NombredelProfesional", vchNombre);
                mensaje = mensaje.Replace("https://www.google.com", url);
                string fecha = DateTime.Now.ToString("U");
                mensaje = mensaje.Replace("FechadelCorreo", fecha);
                Notificador.EnviarCorreo(vchEmail, "Registro Tomo-u", mensaje);
            }
            catch (Exception eEC)
            {
                response = new
                {
                    Message = "Operación no permitida",
                    Success = false
                };
                Log.EscribeLog("Existe un error al enviar el correo: " + eEC.Message);
            }
        }


        [HttpPost]
        [Route("EnviarMensaje")]
        public object EnviarMensaje([FromBody]JObject data)
        {
            dynamic response = null;
            try
            {
                var _Nombre = data["Nombre"].ToObject<string>();
                var _Correo = data["Correo"].ToObject<string>();
                var _Mensaje = data["Mensaje"].ToObject<string>();
                if (_Correo != "")
                {
                    string mensaje = "";
                    mensaje = "<table border='0' cellspacing='0' cellpadding='0' width='652' height='400'><tbody><tr><td><table border='0' cellspacing='0' cellpadding='0' width='650' height='400'><tbody><tr><td><table cellspacing='0' cellpadding='0' border='0'><tbody><tr height='24'><td></td></tr><tr><td width='25'></td><td style='font:ArialMT;font-family:Arial;font-size:16px;font-weight:bold;color:#e91e63' align='left' width='300' height='17'>Mensaje desde el sistema:</td></tr><tr height='19'><td></td></tr></tbody></table></td><td><table cellspacing='0' cellpadding='0'><tbody><tr height='20'><td></td></tr><tr><td align='right' width='299.8'><img src='http://www.tomo-u.com/wp-content/uploads/2017/09/logo01-tomo-u-e1506445011780.png' class='CToWUd'></td><td width='20.2'></td></tr><tr height='15'><td></td></tr></tbody></table></td></tr><tr height='5'><td bgcolor='#e91e63' colspan='2'></td></tr><tr height='10'><td bgcolor='#fafafa' colspan='2'></td></tr><tr bgcolor='#fafafa'><td colspan='2'><table cellspacing='0' cellpadding='0' border='0'><tbody><tr><td width='25'></td><td><table bgcolor='#ffffff' cellspacing='0' cellpadding='0' border='0' width='600'><tbody><tr><td><table cellspacing='0' cellpadding='0' border='0'><tbody><tr><td width='20'></td><td style='font:ArialMT;font-family:Arial;font-size:10px;line-height:2;text-align:right;color:#8c8c8c' height='20' width='560'>FechadelCorreo</td><td width='20'></td></tr></tbody></table></td></tr><tr><td><table cellspacing='0' cellpadding='0' border='0'><tbody><tr height='10'><td></td></tr><tr height='4'><td></td></tr><tr height='22'><td colspan='2'><table cellspacing='0' cellpadding='0' border='0'><tbody><tr><td width='55'></td><td style='font:ArialMT;font-family:Arial;line-height:1.54;text-align:left;color:#666666;font-size:14px' width='300'><b><table><tr><td>NombreContacto</td></tr><tr><td><small>EmailContacto<small></td></tr></table></b></td><td width='15'></td></tr></tbody></table></td></tr><tr height='16'><td></td></tr><tr height='18'><td><table cellspacing='0' cellpadding='0' border='0'><tbody><tr><td width='55'></td><td style='font:ArialMT;font-family:Arial;line-height:1.13;font-size:16px;text-align:left;color:#666666' width='510'><div style='text-align: justify; text-justify: inter-word;'>MensajeContacto</div></td><td width='45'></td></tr></tbody></table></td></tr><tr height='8'><td></td></tr></tbody></table></td></tr><tr height='10'><td colspan='5'></td></tr><tr height='10'><td colspan='5'></td></tr></tbody></table></td><td width='25'></td></tr></tbody></table></td></tr></tbody></table></td></tr></tbody></table>";
                    mensaje = mensaje.Replace("NombreContacto", _Nombre);
                    mensaje = mensaje.Replace("EmailContacto", _Correo);
                    mensaje = mensaje.Replace("MensajeContacto", _Mensaje);
                    string fecha = DateTime.Now.ToString("U");
                    mensaje = mensaje.Replace("FechadelCorreo", fecha);
                    string correoDistribucion = "";
                    using (var db = new App_Data.TomoUDBEntities())
                    {
                        var sitio = db.tbl_SYS_Configuraciones.FirstOrDefault(i => i.vchConfiguracion == "correo_distribucion");
                        correoDistribucion = sitio.vchValor;
                    }
                    //Para pruebas
                    //correoDistribucion = "ijuarez@fujifilm.com.mx";
                    Notificador.EnviarCorreo(correoDistribucion, "Mensaje desde Sistema", mensaje);
                    response = new
                    {
                        Message = "Se ha enviado el mensaje.",
                        Success = true
                    };
                }
                else
                {
                    response = new
                    {
                        Message = "Se requiere un correo.",
                        Success = false
                    };
                }
            }
            catch (Exception exc)
            {
                response = new
                {
                    Message = exc.Message,
                    Success = false
                };
            }
            return response;
        }

        private App_Data.TomoUDBEntities _context = new App_Data.TomoUDBEntities();
        /// <summary>
        /// Libera los recursos de la base de datos
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}