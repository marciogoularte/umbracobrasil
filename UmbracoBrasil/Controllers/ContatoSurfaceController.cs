using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Umbraco.Core.Logging;
using Umbraco.Web.Mvc;
using UmbracoBrasil.Models;

namespace UmbracoBrasil.Controllers
{
    public class ContatoSurfaceController : SurfaceController
    {

        [ChildActionOnly]
        public ActionResult ContatoForm()
        {
            // In case you need it...
            var currentNode = Umbraco.TypedContent(UmbracoContext.PageId.GetValueOrDefault());

            var model = new ContatoModel();
            return PartialView("ContatoForm", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Enviar(ContatoModel model)
        {
            if (ModelState.IsValid)
            {
                var sb = new StringBuilder();
                sb.AppendFormat("<p>Mensagem: {0}</p>", model.Mensagem);
                sb.AppendFormat("<p>Email do Site: {0}</p>", model.Assunto);
                sb.AppendFormat("<p>Nome: {0}</p>", model.Nome);
                sb.AppendFormat("<p>Email: {0}</p>", model.Email);
               
                SmtpClient smtp = new SmtpClient();
                MailMessage message = new MailMessage();

                //Pegando da AppSettings
                //message.To.Add(new MailAddress(ConfigurationManager.AppSettings["EmailContato"]));

                message.To.Add(new MailAddress("email@email.com"));                
                message.Sender = new MailAddress(model.Email);
                message.Body = sb.ToString();
                message.IsBodyHtml = true;

                try
                {
                    smtp.Send(message);

                }
                catch (SmtpException smtpEx)
                {
                   
                    LogHelper.Error<ContatoSurfaceController>("Erro ao enviar email", smtpEx);
                    return RedirectToUmbracoPage(1063); // Id da página de erro
                }

                //Retorna apra a página atual
                return RedirectToUmbracoPage(UmbracoContext.PageId.GetValueOrDefault());
            }
            return CurrentUmbracoPage();
        }

    }
}