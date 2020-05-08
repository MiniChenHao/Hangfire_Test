using Hangfire;
using Hangfire.Console;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Hangfire_Test.Startup))]

namespace Hangfire_Test
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // 有关如何配置应用程序的详细信息，请访问 http://go.microsoft.com/fwlink/?LinkID=316888
            GlobalConfiguration.Configuration.UseSqlServerStorage("ShenOnlineJob").UseConsole();
            app.UseHangfireDashboard();
            app.UseHangfireServer();
        }
    }
}
