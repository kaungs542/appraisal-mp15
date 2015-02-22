using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;
using System.Net;

namespace Appraisal.Class
{
    public class email
    {
        public static bool SendMail(string message, string mailTo, string subject)
        {
            MailMessage ourMessage = new MailMessage();
            ourMessage.To.Add(mailTo);
            ourMessage.Subject = subject;
            ourMessage.From = new System.Net.Mail.MailAddress("asc-360_LeadershipSystem@tp.edu.sg");
            ourMessage.Body = message;
            System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient("smtp-application.tp.edu.sg");
            try
            {
                smtp.Send(ourMessage);      // Send your mail.
                return true;                // IF Mail sended Successfully
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;               // Send error
            }
        }
    }
}