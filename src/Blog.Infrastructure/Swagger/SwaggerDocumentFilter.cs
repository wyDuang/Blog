using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Collections.Generic;
using System.Linq;

namespace Blog.Infrastructure.Swagger
{
    public class SwaggerDocumentFilter: IDocumentFilter
    {
        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            var tags = new List<OpenApiTag>
            {
                new OpenApiTag {
                    Name = "Auth",
                    Description = "1、JWT模式认证授权",
                    ExternalDocs = new OpenApiExternalDocs { Description = "JSON Web Token" }
                },
                new OpenApiTag {
                    Name = "Common",
                    Description = "2、通用数据接口",
                    ExternalDocs = new OpenApiExternalDocs { Description = "bing接口" }
                },
                new OpenApiTag {
                    Name = "Article",
                    Description = "文章数据接口",
                    ExternalDocs = new OpenApiExternalDocs { Description = "分类/文章CORD/标签/友链" }
                },
                new OpenApiTag {
                    Name = "Category",
                    Description = "分类数据接口",
                    ExternalDocs = new OpenApiExternalDocs { Description = "分类CORD、分页、条件查询" }
                },
                new OpenApiTag {
                    Name = "Tag",
                    Description = "标签数据接口",
                    ExternalDocs = new OpenApiExternalDocs { Description = "标签CORD、分页、条件查询" }
                },
                new OpenApiTag {
                    Name = "FriendLink",
                    Description = "友链数据接口",
                    ExternalDocs = new OpenApiExternalDocs { Description = "友情链接CORD、分页、条件查询" }
                },
                new OpenApiTag {
                    Name = "Signature",
                    Description = "个性艺术签名设计接口",
                    ExternalDocs = new OpenApiExternalDocs { Description = "个性艺术签名设计" }
                },
                new OpenApiTag {
                    Name = "Upload",
                    Description = "上传文件接口",
                    ExternalDocs = new OpenApiExternalDocs { Description = "上传文件" }
                }
                //new OpenApiTag {
                //    Name = "MTA",
                //    Description = "腾讯移动分析",
                //    ExternalDocs = new OpenApiExternalDocs { Description = "MTA 未开发..." }
                //},
            };

            //swaggerDoc.v

            swaggerDoc.Tags = tags.OrderBy(x => x.Description).ToList();
        }
    }
}
