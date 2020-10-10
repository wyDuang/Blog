using Volo.Abp.Settings;

namespace WYBlog
{
    /// <summary>
    /// 设置授权码为明文
    /// </summary>
    public class BlogSettingDefinitionProvider : SettingDefinitionProvider
    {
        public override void Define(ISettingDefinitionContext context)
        {
            var smtpPassword = context.GetOrNull("Abp.Mailing.Smtp.Password");
            if (smtpPassword != null)
            {
                smtpPassword.IsEncrypted = false;
            }
        }
    }
}