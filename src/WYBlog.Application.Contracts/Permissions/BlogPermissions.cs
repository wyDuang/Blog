using System;
using System.Collections.Generic;
using System.Text;

namespace WYBlog.Permissions
{
    public static class BlogPermissions
    {
        public const string GroupName = "Blog";

        public static class Articles
        {
            public const string Default = GroupName + ".Articles";
            public const string Create = Default + ".Create";
            public const string Edit = Default + ".Edit";
            public const string Delete = Default + ".Delete";
        }

        public static class Categories
        {
            public const string Default = GroupName + ".Categories";
            public const string Create = Default + ".Create";
            public const string Edit = Default + ".Edit";
            public const string Delete = Default + ".Delete";
        }
    }
}
