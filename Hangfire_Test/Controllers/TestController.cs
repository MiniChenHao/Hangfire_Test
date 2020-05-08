using Hangfire;
using Hangfire.Console;
using Hangfire.Server;
using ServiceStack;
using System;
using System.Linq;
using System.Web.Mvc;

namespace Hangfire_Test.Controllers
{
    public class TestController : Controller
    {
        // GET: Test
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 每分钟执行控制台作业
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public string MinutelyForConsoleJob()
        {
            RecurringJob.AddOrUpdate("每分钟执行ConsoleJob", () => ConsoleJob(null), Cron.Minutely(), TimeZoneInfo.Local);
            return new { IsOK = true }.ToJson();
        }

        /// <summary>
        /// 控制台作业
        /// </summary>
        /// <param name="context"></param>
        public string ConsoleJob(PerformContext context)
        {
            context.WriteLine($"{DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")} SimpleJob Running ...");
            var progressBar = context.WriteProgressBar();
            foreach (var i in Enumerable.Range(1, 50).ToList().WithProgress(progressBar))
            {
                System.Threading.Thread.Sleep(1000);
            }
            return "成功";
        }
    }
}