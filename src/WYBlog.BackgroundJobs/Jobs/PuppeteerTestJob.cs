using PuppeteerSharp;
using System.Threading.Tasks;

namespace WYBlog.Jobs
{
    public class PuppeteerTestJob : IBackgroundJob
    {
        public async Task ExecuteAsync()
        {
            //1、第一次检测到没有浏览器文件会默认下载 chromium 浏览器执行程序（不会重复下载）
            // 默认下载地址是在启动目录下面
            // DownloadAsync 可以指定 Chromium 版本
            // BrowserFetcher.DefaultRevision 下载当前默认最稳定的版本  
            // 网络原因可能会下载的很慢，推荐手动下载，淘宝的源：https://npm.taobao.org/mirrors/chromium-browser-snapshots
            // Windows：..\.local-chromium\Win64-[版本号]\chrome-win
            // Linux：../.local-chromium/Linux-[版本号]/chrome-linux
            await new BrowserFetcher().DownloadAsync(800071);

            //2、创建一个浏览器实例
            using var browser = await Puppeteer.LaunchAsync(new LaunchOptions
            {
                Headless = true,//true表示无头模式运行浏览器
                Devtools = false, //是否为每个选项卡自动打开DevTools面板。如果这个选项是true，无头选项将自动设置为false
                Args = new string[] { "--no-sandbox" } //Linux环境运行在 root 权限下时，必须添加 "--no-sandbox" 参数，否则会启动失败
            });

            //3、打开一个页面
            using var page = await browser.NewPageAsync();

            //4、打开一个网站
            // WaitUntilNavigation.Networkidle0：等待网页加载完毕
            var response = await page.GoToAsync("https://www.baidu.com", WaitUntilNavigation.Networkidle0);
            // 设置网页预览大小
            await page.SetViewportAsync(new ViewPortOptions
            {
                Width = 1344,
                Height = 768
            });

            var content = await page.GetContentAsync();//获取到HTML
        }
    }
}