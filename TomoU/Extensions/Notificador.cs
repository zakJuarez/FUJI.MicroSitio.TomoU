using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;

namespace TomoU.Extensions
{
    public class Notificador
    {
        /// <summary>
        /// Permite enviar correos electrónicos desde los parámetros del archivo web.config, si son varios destinatarios, se deben ingresar separados por coma (,)
        /// </summary>
        /// <param name="Destinatario"></param>
        /// <param name="Asunto"></param>
        /// <param name="Mensaje"></param>
        /// <returns></returns>
        public static bool EnviarCorreo(string Destinatario, string Asunto, string Mensaje)
        {
            bool Enviado = false;
            try
            {
                using (var db = new App_Data.TomoUDBEntities())
                {
                    var correo_notificador = db.tbl_SYS_Configuraciones.FirstOrDefault(i => i.vchConfiguracion == "correo_notificador");
                    var correo_notificador_password = db.tbl_SYS_Configuraciones.FirstOrDefault(i => i.vchConfiguracion == "correo_notificador_password");
                    var correo_notificador_host = db.tbl_SYS_Configuraciones.FirstOrDefault(i => i.vchConfiguracion == "correo_notificador_host");
                    var correo_notificador_port = db.tbl_SYS_Configuraciones.FirstOrDefault(i => i.vchConfiguracion == "correo_notificador_port");
                    var correo_notificador_noreply = db.tbl_SYS_Configuraciones.FirstOrDefault(i => i.vchConfiguracion == "correo_notificador_noreply");
                    if (correo_notificador != null && correo_notificador_password != null && correo_notificador_host != null && correo_notificador_port != null)
                    {
                        SmtpClient client = new SmtpClient();
                        client.Port = Convert.ToInt32(correo_notificador_port.vchValor);
                        client.Host = correo_notificador_host.vchValor;
                        client.EnableSsl = true;
                        client.DeliveryMethod = SmtpDeliveryMethod.Network;
                        client.UseDefaultCredentials = false;
                        client.Credentials = new System.Net.NetworkCredential(correo_notificador.vchValor, correo_notificador_password.vchValor);
                        MailAddress From = new MailAddress(correo_notificador_noreply != null ? correo_notificador_noreply.vchValor : correo_notificador.vchValor);
                        MailMessage message = new MailMessage();
                        message.From = From;
                        message.Sender = new MailAddress(correo_notificador_noreply != null ? correo_notificador_noreply.vchValor : correo_notificador.vchValor, "No Reply");
                        message.ReplyToList.Add(new MailAddress(correo_notificador_noreply != null ? correo_notificador_noreply.vchValor : correo_notificador.vchValor, "No Reply"));
                        string[] Destinatarios = Destinatario.Split(',');
                        foreach (string destinatario in Destinatarios)
                            message.To.Add(new MailAddress(destinatario.Trim()));
                        message.Subject = Asunto;
                        string FilePath = string.Empty;
                        List<string> ConjuntoCertificados = new List<string>();
                        message.Body = Mensaje;
                        message.IsBodyHtml = true;
                        client.Send(message);
                        client.Dispose();
                        message.Dispose();
                        Enviado = true;
                    }
                    else
                    {
                        Enviado = false;
                    }
                }
            }
            catch(Exception e)
            {
                Log.EscribeLog("Existe un erorr al enviar el correo: " + e.Message);
                Enviado = false;
            }
            return Enviado;
        }

        public static async System.Threading.Tasks.Task EnviarCorreoAsync(string Destinatario, string Asunto, string Mensaje)
        {
            try
            {
                using (var db = new App_Data.TomoUDBEntities())
                {
                    var correo_notificador = db.tbl_SYS_Configuraciones.FirstOrDefault(i => i.vchConfiguracion == "correo_notificador");
                    var correo_notificador_password = db.tbl_SYS_Configuraciones.FirstOrDefault(i => i.vchConfiguracion == "correo_notificador_password");
                    var correo_notificador_host = db.tbl_SYS_Configuraciones.FirstOrDefault(i => i.vchConfiguracion == "correo_notificador_host");
                    var correo_notificador_port = db.tbl_SYS_Configuraciones.FirstOrDefault(i => i.vchConfiguracion == "correo_notificador_port");
                    var correo_notificador_noreply = db.tbl_SYS_Configuraciones.FirstOrDefault(i => i.vchConfiguracion == "correo_notificador_noreply");
                    if (correo_notificador != null && correo_notificador_password != null && correo_notificador_host != null && correo_notificador_port != null)
                    {
                        SmtpClient client = new SmtpClient();
                        client.Port = Convert.ToInt32(correo_notificador_port.vchValor);
                        client.Host = correo_notificador_host.vchValor;
                        client.EnableSsl = true;
                        client.DeliveryMethod = SmtpDeliveryMethod.Network;
                        client.UseDefaultCredentials = false;
                        client.Credentials = new System.Net.NetworkCredential(correo_notificador.vchValor, correo_notificador_password.vchValor);
                        MailAddress From = new MailAddress(correo_notificador_noreply != null ? correo_notificador_noreply.vchValor : correo_notificador.vchValor);
                        MailMessage message = new MailMessage();
                        message.From = From;
                        message.Sender = new MailAddress(correo_notificador_noreply != null ? correo_notificador_noreply.vchValor : correo_notificador.vchValor, "No Reply");
                        message.ReplyToList.Add(new MailAddress(correo_notificador_noreply != null ? correo_notificador_noreply.vchValor : correo_notificador.vchValor, "No Reply"));
                        string[] Destinatarios = Destinatario.Split(',');
                        foreach (string destinatario in Destinatarios)
                            message.To.Add(new MailAddress(destinatario.Trim()));
                        message.Subject = Asunto;
                        string FilePath = string.Empty;
                        List<string> ConjuntoCertificados = new List<string>();
                        message.Body = Mensaje;
                        message.IsBodyHtml = true;
                        client.SendCompleted += (s, e) => {
                            client.Dispose();
                            message.Dispose();
                        };
                        await client.SendMailAsync(message);
                    }
                }
            }
            catch(Exception eEV) {
                Log.EscribeLog("Existe un error al enviar el correo: " + eEV.Message);
            }
        }
    }
}