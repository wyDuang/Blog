using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Blog.Core.Entities;
using System.Linq;

namespace Blog.Infrastructure.Database
{
    public class MyContextSeed
    {
        public static async Task SeedAsync(
            MyContext myContext,
            ILoggerFactory loggerFactory,
            int retry = 0)
        {
            int retryForAvailability = retry;
            try
            {
                // 只有在使用真正的数据库时才运行这个
                // myContext.Database.Migrate();

                if (!myContext.Set<Article>().Any())
                {
                    myContext.Set<Article>().AddRange(
                        new List<Article>{
                            new Article{
                                Title = "Post Title 1",
                                Html = "Post Body 1",
                                Author = "Dave",
                                CreateDate = DateTime.Now
                            },
                            new Article{
                                Title = "Post Title 2",
                                Html = "Post Body 2",
                                Author = "Dave",
                                CreateDate = DateTime.Now
                            },
                            new Article{
                                Title = "Post Title 3",
                                Html = "Post Body 3",
                                Author = "Dave",
                                CreateDate = DateTime.Now
                            },
                            new Article{
                                Title = "Post Title 4",
                                Html = "Post Body 4",
                                Author = "Dave",
                                CreateDate = DateTime.Now
                            },
                            new Article{
                                Title = "Post Title 5",
                                Html = "Post Body 5",
                                Author = "Dave",
                                CreateDate = DateTime.Now
                            },
                            new Article{
                                Title = "Post Title 6",
                                Html = "Post Body 6",
                                Author = "Dave",
                                CreateDate = DateTime.Now
                            }
                        }
                    );
                    await myContext.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                if (retryForAvailability < 10)
                {
                    retryForAvailability++;
                    var logger = loggerFactory.CreateLogger<MyContextSeed>();
                    logger.LogError(ex.Message);
                    await SeedAsync(myContext, loggerFactory, retryForAvailability);
                }
            }
        }
    }
}
