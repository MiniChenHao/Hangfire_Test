using Hangfire;
using ServiceStack;
using System;
using System.Web.Mvc;

namespace Hangfire_Test.Controllers
{
    public class EmailController : Controller
    {
        // GET: Email
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 每天发送邮件
        /// </summary>
        [HttpPost]
        public string DailyForSendEmail()
        {
            RecurringJob.AddOrUpdate("每天[3:00]执行[SendEmail]", () => SendEmail(), Cron.Daily(3), TimeZoneInfo.Local);
            return new { IsOK = true }.ToJson();
        }

        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <returns></returns>
        public string SendEmail()
        {
            return "成功";
        }
    }
}