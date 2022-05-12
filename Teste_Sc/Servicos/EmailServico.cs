using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Threading.Tasks;
using Teste_Sc.ViewModels;

namespace Teste_Sc.Servicos
{
    public class EmailServico
    {
        public bool EnviaEmail(string email)
        {
            bool confirmaEnvio = false;
            try
            {
                string caminhoDaPastaEmail = @"C:\EmailTeste";

                if (!Directory.Exists(caminhoDaPastaEmail))
                {
                    Directory.CreateDirectory(caminhoDaPastaEmail);
                }
                MailMessage enviaEmail = new();
                var sender = new SmtpClient("smtp.gmail.com", 587)
                {
                    EnableSsl = true,
                    PickupDirectoryLocation = @"c:\EmailTeste",
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential("crisvideoaulas@gmail.com", "@Masha311291")
                };


                var planilha = File.ReadAllText("c:\\ExcelTeste\\TodasCidades.xlsx");
                Attachment data = new Attachment(planilha, MediaTypeNames.Application.Octet);
                enviaEmail.Attachments.Add(data);
                enviaEmail.From = new("Crisvideoaulas@gmail.com", "Planilha com estados e cidades do brasil");
                enviaEmail.Body = "Testando o envio de email smtp pelo gmail";
                enviaEmail.Subject = "Teste envio de planilha";
                enviaEmail.IsBodyHtml = true;
                enviaEmail.Priority = MailPriority.Normal;
                enviaEmail.To.Add(email);

                sender.Send(enviaEmail);
                return confirmaEnvio = true;

            }
            catch (Exception ex)
            {

                return confirmaEnvio;
            }
            
        }

    }
}
