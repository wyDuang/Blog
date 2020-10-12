using Volo.Abp.Authorization.Permissions;

namespace WYBlog.Permissions
{
    public class BlogPermissionDefinitionProvider : PermissionDefinitionProvider
    {
        public override void Define(IPermissionDefinitionContext context)
        {
            var blogGroup = context.AddGroup(BlogPermissions.GroupName);

            var articlesPermission = blogGroup.AddPermission(BlogPermissions.Articles.Default);
            articlesPermission.AddChild(BlogPermissions.Articles.Create);
            articlesPermission.AddChild(BlogPermissions.Articles.Edit);
            articlesPermission.AddChild(BlogPermissions.Articles.Delete);

            var categoriesPermission = blogGroup.AddPermission(BlogPermissions.Categories.Default);
            categoriesPermission.AddChild(BlogPermissions.Categories.Create);
            categoriesPermission.AddChild(BlogPermissions.Categories.Edit);
            categoriesPermission.AddChild(BlogPermissions.Categories.Delete);
        }
    }
}