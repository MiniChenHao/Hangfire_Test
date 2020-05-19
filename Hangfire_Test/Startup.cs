using Hangfire;
using Hangfire.Console;
using Hangfire.Dashboard;
using Hangfire.SqlServer;
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
            GlobalConfiguration.Configuration.UseDashboardMetric(DashboardMetrics.ServerCount);
            GlobalConfiguration.Configuration.UseDashboardMetric(SqlServerStorage.ActiveConnections);
            GlobalConfiguration.Configuration.UseDashboardMetric(SqlServerStorage.TotalConnections);
            GlobalConfiguration.Configuration.UseDashboardMetric(DashboardMetrics.RecurringJobCount);
            GlobalConfiguration.Configuration.UseDashboardMetric(DashboardMetrics.RetriesCount);
            GlobalConfiguration.Configuration.UseDashboardMetric(DashboardMetrics.AwaitingCount);
            GlobalConfiguration.Configuration.UseDashboardMetric(DashboardMetrics.EnqueuedAndQueueCount);
            GlobalConfiguration.Configuration.UseDashboardMetric(DashboardMetrics.ScheduledCount);
            GlobalConfiguration.Configuration.UseDashboardMetric(DashboardMetrics.ProcessingCount);
            GlobalConfiguration.Configuration.UseDashboardMetric(DashboardMetrics.SucceededCount);
            GlobalConfiguration.Configuration.UseDashboardMetric(DashboardMetrics.FailedCount);
            GlobalConfiguration.Configuration.UseDashboardMetric(DashboardMetrics.DeletedCount);
            BasicAuthAuthorizationFilter filter = new BasicAuthAuthorizationFilter(new BasicAuthAuthorizationFilterOptions
            {
                SslRedirect = false,//是否将所有非SSL请求重定向到SSL URL
                RequireSsl = false,//需要SSL连接才能访问HangFire Dahsboard。强烈建议在使用基本身份验证时使用SSL
                LoginCaseSensitive = true,//登录检查是否区分大小写
                Users = new[] {
                    new BasicAuthAuthorizationUser {
                        Login = "Admin",
                        // Password as SHA1 hash
                        // Password = new byte[] { 0x7c,0x4a,0x8d,0x09,0xca,0x37,0x62,0xaf,0x61,0xe5,0x95,0x20,0x94,0x3d,0xc2,0x64,0x94,0xf8,0x94,0x1b }//密码
                        PasswordClear = "123456"
                    }
                }
            });
            DashboardOptions options = new DashboardOptions { Authorization = new[] { filter } };
            app.UseHangfireDashboard("/Hangfire", options);
            //app.UseHangfireServer();
        }
    }
}
