using Hangfire;
using Hangfire.Console;
using Hangfire.Dashboard;
using Hangfire.SqlServer;
using Microsoft.Owin;
using Owin;
using System;

[assembly: OwinStartup(typeof(Hangfire_Test.Startup))]

namespace Hangfire_Test
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // 有关如何配置应用程序的详细信息，请访问 http://go.microsoft.com/fwlink/?LinkID=316888
            GlobalConfiguration.Configuration
                .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
                .UseSqlServerStorage("ShenOnlineJob", new SqlServerStorageOptions()
                {
                    CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                    SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                    QueuePollInterval = TimeSpan.Zero,
                    UseRecommendedIsolationLevel = true,
                    UsePageLocksOnDequeue = true,
                    DisableGlobalLocks = true
                })
                .UseConsole();

            GlobalConfiguration.Configuration
                .UseDashboardMetric(DashboardMetrics.AwaitingCount)
                .UseDashboardMetric(DashboardMetrics.DeletedCount)
                .UseDashboardMetric(DashboardMetrics.EnqueuedAndQueueCount)
                .UseDashboardMetric(DashboardMetrics.EnqueuedCountOrNull)
                .UseDashboardMetric(DashboardMetrics.FailedCount)
                .UseDashboardMetric(DashboardMetrics.FailedCountOrNull)
                .UseDashboardMetric(DashboardMetrics.ProcessingCount)
                .UseDashboardMetric(DashboardMetrics.RecurringJobCount)
                .UseDashboardMetric(DashboardMetrics.RetriesCount)
                .UseDashboardMetric(DashboardMetrics.ScheduledCount)
                .UseDashboardMetric(DashboardMetrics.ServerCount)
                .UseDashboardMetric(DashboardMetrics.SucceededCount)
                .UseDashboardMetric(SqlServerStorage.ActiveConnections)
                .UseDashboardMetric(SqlServerStorage.TotalConnections);

            BasicAuthAuthorizationFilter filter = new BasicAuthAuthorizationFilter(new BasicAuthAuthorizationFilterOptions
            {
                SslRedirect = false,
                RequireSsl = false,
                LoginCaseSensitive = true,
                Users = new[] { new BasicAuthAuthorizationUser { Login = "Admin", PasswordClear = "123456" } }
            });
            // SslRedirect          是否将所有非SSL请求重定向到SSL URL
            // RequireSsl           需要SSL连接才能访问HangFire Dahsboard。强烈建议在使用基本身份验证时使用SSL
            // LoginCaseSensitive   登录检查是否区分大小写
            // Password             密码 SHA1 hash 【Password = new byte[] { 0x7c,0x4a,0x8d,0x09,0xca,0x37,0x62,0xaf,0x61,0xe5,0x95,0x20,0x94,0x3d,0xc2,0x64,0x94,0xf8,0x94,0x1b }】

            DashboardOptions options = new DashboardOptions { Authorization = new[] { filter } };
            app.UseHangfireDashboard("/Hangfire", options);
            //app.UseHangfireServer();
        }
    }
}
