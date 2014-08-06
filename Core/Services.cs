using System;
using System.Net.Mail;
using System.Net;
using Lib;
using SystemBase;
using System.Text;
using System.Web;
using Newtonsoft.Json;
using Core.Utilities;
using Core.Properties;
using System.Threading.Tasks;

namespace Core.Services
{
    public class API
    {
        #region Properties
        HttpContext context { set; get; }

        public string lang { set; get; }
        #endregion Properties

        #region Constructors
        public API(HttpContext context)
        {
            this.context = context;
        }
        #endregion Constructors

        #region Methods
        public string GetResult()
        {
            switch (context.Request.Form["iud"])
            {
                case "0":
                    {
                        return Authenticate();
                    }
                case "1":
                    {
                        return ResetPasswordRequest();
                    }
                default:
                    {
                        return string.Empty;
                    }
            }
        }

        public string Authenticate()
        {
            User U = new User();
            if (U.Authenticate(context.Request.Form["username"].Trim(), context.Request.Form["password"].Trim().MD5()))
            {
                return JsonConvert.SerializeObject(new { res = "success" }, Formatting.None);
            }
            else
            {
                return JsonConvert.SerializeObject(new { res = "error", message = Resources.InvalidUsernameOrPassword }, Formatting.None);
            }
        }

        public string ResetPasswordRequest()
        {
            var Result = new object();
            var U = new User();
            var Email = context.Request.Form["email"];

            U.GetSingleUser(Slug: Email);
            if (U.IsError)
            {
                Result = new
                {
                    res = "error",
                    message = Resources.InformationEmailNotInDB
                };
            }
            else
            {
                Result = new
                {
                    res = "success",
                };

                var Key = string.Format("<data><user_id>{0}</user_id><email><![CDATA[{1}]]></email><date><![CDATA[{2}]]></date></data>", U.ID, Email, DateTime.Now.AddHours(2)).EncryptWeb();
                var Url = string.Format("{0}passwordreset/{1}/", AppSettings.WebsiteHttpFullPath, Key);

                var Subject = "პაროლის განახლება";
                var Keys = new string[] { "[subject]", "[body]", "[link]", "[button_text]" };
                var Values = new string[] { Subject, Resources.InformationPasswordReset, Url, Subject };
                var Body = Utility.GetTplContent(AppDomain.CurrentDomain.BaseDirectory +"TPL\\EmailTemplateWithButton.htm", Keys, Values);
                                
                Task.Factory.StartNew(() =>
                {
                    var M = new Mail();
                    M.Send(Email, Subject, Body);
                });
            }

            return JsonConvert.SerializeObject(Result, Formatting.None);
        }
        #endregion Methods
    }

    public class Mail : ObjectBase
    {
        #region Properties
        string _SMTP = "smtp.gmail.com";
        string _Username = "emma@63bits.com";
        string _Password = "1qaz!QAZ";
        int _Port = 587;
        string _From;
        bool _EnableSSL = true;

        public string From
        {
            set { _From = value; }
            get { return _From; }
        }
        #endregion Properties

        #region Constructors
        public Mail()
        {
            
        }

        public Mail(string Username, string Password)
        {
            _Username = Username;
            _Password = Password;
        }

        public Mail(string SMTP, int Port, string Username, string Password, string From, bool EnableSSL)
        {
            _SMTP = SMTP;
            _Username = Username;
            _Password = Password;
            _Port = Port;
            _From = From;
            _EnableSSL = EnableSSL;
        }
        #endregion Constructors

        #region Methods
        public void Send(string To, string Subject, string Body, string ReplyTo = null)
        {
            TryExecute(string.Format("Core.Services.Mail.Send(To = {0}, Subject = {1}, Body = {2})", To, Subject, Body), () =>
            {
                MailMessage message = new MailMessage();
                message.From = new MailAddress(_Username);
                message.To.Add(To);
                message.Subject = Subject;
                message.Body = Body;
                message.IsBodyHtml = true;
                message.BodyEncoding = Encoding.UTF8;
                message.SubjectEncoding = Encoding.UTF8;

                if (!string.IsNullOrEmpty(ReplyTo))
                {
                    message.ReplyToList.Add(ReplyTo);
                }

                var Attachment = new Attachment(AppDomain.CurrentDomain.BaseDirectory + "images\\logo.png");
                Attachment.ContentId = "logo";
                message.Attachments.Add(Attachment);


                var client = new SmtpClient(_SMTP, _Port)
                {
                    Credentials = new NetworkCredential(_Username, _Password),
                    EnableSsl = _EnableSSL
                };

                client.Send(message);
            });
        }
        #endregion Methods
    }
}
