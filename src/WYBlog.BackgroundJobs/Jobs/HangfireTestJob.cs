using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using Volo.Abp.Emailing;
using Volo.Abp.MailKit;

namespace WYBlog.Jobs
{
    public class HangfireTestJob : IBackgroundJob
    {
        private readonly ILogger<HangfireTestJob> _logger;
        private readonly IMailKitSmtpEmailSender _emailSender;
        public HangfireTestJob(IMailKitSmtpEmailSender emailSender, ILogger<HangfireTestJob> logger)
        {
            _emailSender = emailSender;
            _logger = logger;
        }

        public async Task ExecuteAsync()
        {
            Console.WriteLine("定时任务测试");

            await _emailSender.SendAsync("wuyang@31huiyi.com", "这是标题啊", "这是内容");

            //await Task.CompletedTask;
        }
    }
}