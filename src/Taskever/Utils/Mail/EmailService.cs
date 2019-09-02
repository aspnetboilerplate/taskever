using System;
using System.Net.Mail;
using Abp.Configuration;
using Castle.Core.Logging;

namespace Taskever.Utils.Mail
{
    //TODO: Get setting from configuration
    /// <summary>
    /// Implements <see cref="IEmailService"/> to send emails using current settings.
    /// </summary>
    public class EmailService : IEmailService
    {
        private readonly ISettingManager _settingManager;
        public ILogger Logger { get; set; }

        /// <summary>
        /// Creates a new instance of <see cref="EmailService"/>.
        /// </summary>
        public EmailService(ISettingManager settingManager)
        {
            _settingManager = settingManager;
            //throw new NotImplementedException();
           // Logger = NullLogger.Instance;
        }

        public void SendEmail(MailMessage mail)
        {
            throw new NotImplementedException();
            //try
            //{
            //    mail.From = new MailAddress(_settingManager.GetSettingValue("Abp.Net.Mail.SenderAddress"), _settingManager.GetSettingValue("Abp.Net.Mail.DisplayName"));
            //    using (var client = new SmtpClient(_settingManager.GetSettingValue("Abp.Net.Mail.ServerAddress"), _settingManager.GetSettingValue<int>("Abp.Net.Mail.ServerPort")))
            //    {
            //        client.UseDefaultCredentials = false;
            //        client.Credentials = new System.Net.NetworkCredential(_settingManager.GetSettingValue("Abp.Net.Mail.Username"), _settingManager.GetSettingValue("Abp.Net.Mail.Password"));
            //        client.Send(mail);
            //    }
            //}
            //catch (Exception ex)
            //{
            //    Logger.wa("Could not send email!", ex);
            //}
        }
    }
}