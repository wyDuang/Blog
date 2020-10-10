using Hangfire;
using Microsoft.Extensions.DependencyInjection;
using System;
using WYBlog.Jobs;

namespace WYBlog
{
    public static class BlogBackgroundJobsExtensions
    {
        public static void UseHangfireTest(this IServiceProvider service)
        {
            var job = service.GetService<HangfireTestJob>();

            RecurringJob.AddOrUpdate("定时任务测试", () => job.ExecuteAsync(), CronType.Minute(10));
        }
    }
}