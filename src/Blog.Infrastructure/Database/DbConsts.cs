using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Infrastructure.Database
{
    public class BlogDbTableNames
    {
        public const string Articles = "article";       
        public const string Tags = "tag";
        public const string Article_Tags = "article_tag";
        public const string Categories = "category";
        public const string FriendLinks = "friendlink";
        public const string Advertisements = "advertisement";
        public const string GuestBooks = "guestbook";
        public const string Modules = "module";
        public const string ModulePermissions = "module_permission";
        public const string OperateLogs = "operatelog";
        public const string Permissions = "permission";
        public const string Roles = "role";
        public const string RoleModulePermissions = "role_module_permission";
        public const string Users = "user";
        public const string UserRoles = "user_role";
    }
}
