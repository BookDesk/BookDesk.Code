using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;
using System.IO;
using System.Net;
using System.Net.Mail;
using System;
using System.Configuration;

namespace BuySellOldBooks
{
    public class FilterConfig
    {
        //Here we register filters that we want to apply on whole application.
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleAndLogErrorAttribute());
        }
    }

    //Override HandleErrorAttribute to also log the exception. 
    public class HandleAndLogErrorAttribute : HandleErrorAttribute
    {
        public override void OnException(ExceptionContext filterContext)
        {
            try
            {
                //Get the path of Log file.
                string filePath = HttpContext.Current.Server.MapPath(ConfigurationSettings.AppSettings["ExceptionLogFileAddress"]);
                //log filter context.exception details here
                if (!(File.Exists(filePath)))
                {
                    XElement ExceptionDetails = new XElement("Exceptions", new XElement("Exception", new XAttribute("Date", System.DateTime.Now),
                            new XElement("Message", filterContext.Exception.Message),
                            new XElement("HelpLink", filterContext.Exception.HelpLink),
                            new XElement("InnerException", filterContext.Exception.InnerException),
                            new XElement("StackTrace", filterContext.Exception.StackTrace)
                                )
                            );
                    ExceptionDetails.Save(filePath);
                }
                else
                {
                    XDocument doc = XDocument.Load(filePath);
                    XElement root = new XElement("Exception");
                    root.Add(new XAttribute("Date", System.DateTime.Now));
                    root.Add(new XElement("Message", filterContext.Exception.Message));
                    root.Add(new XElement("HelpLink", filterContext.Exception.HelpLink));
                    root.Add(new XElement("InnerException", filterContext.Exception.InnerException));
                    root.Add(new XElement("StackTrace", filterContext.Exception.StackTrace));
                    doc.Element("Exceptions").Add(root);
                    doc.Save(filePath);
                }
                SendEmailMessage(filterContext);

                base.OnException(filterContext);
            }
            catch (Exception ex)
            {
                //TRACE
            }
        }

        
        public void SendEmailMessage(ExceptionContext exc)
        {
                //Send Mail Logic Here.
                string email                            = ConfigurationSettings.AppSettings["HosterEmailAddress"];        
                string password                         = ConfigurationSettings.AppSettings["HosterEmailPassword"];    
                string host                             = ConfigurationSettings.AppSettings["HosterEmailHost"];
                int port                                = Convert.ToInt16(ConfigurationSettings.AppSettings["HosterEmailPort"]);
                string emailSubject                     = ConfigurationSettings.AppSettings["HosterEmailSubject"];
                string adminEmailToSendExceptionDetail  = ConfigurationSettings.AppSettings["AdminEmailToSendExceptionDetail"];

                var loginInfo                           = new NetworkCredential(email, password);
                var msg                                 = new MailMessage();
                var smtpClient                          = new SmtpClient(host, port);        

                //Attachment attachment = new System.Net.Mail.Attachment(fileToAttach);
                //msg.Attachments.Add(attachment);

                msg.From        = new MailAddress(email);

            //TRACE send all email to a single-mail id (Decide after update Home/ContactUs send mail functionality)
                msg.To.Add(new MailAddress(adminEmailToSendExceptionDetail));
                msg.Subject = "Exception Notification! from BookDesk";
                msg.Body = "<b>Exception Date: </b>" + System.DateTime.Now + "<br/><b> Exception Message: </b>" + exc.Exception.Message + "<br/> <b>Exception InnerException: </b>"
                    + exc.Exception.InnerException + "<br/> <b>Stack Trace: </b>" + exc.Exception.StackTrace;       
                msg.IsBodyHtml  = true;

                //smtpClient.Timeout    = int.Parse(ConfigurationSettings.AppSettings["smtpClientTimeout"]); // 100000;
                smtpClient.EnableSsl    = true;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials  = loginInfo;
                smtpClient.Send(msg);
        }
    }   
}