<<<<<<< HEAD:src/WyBlog.Repository.MySql/Database/BlogDbContext.cs
﻿using Microsoft.EntityFrameworkCore;
using WyBlog.Entities;
<<<<<<< HEAD
<<<<<<< HEAD
using WyBlog.Repository.MySql.Database.EntityConfigurations;
=======
>>>>>>> e7f3251... 继续
=======
using WyBlog.Repository.MySql.Database.EntityConfigurations;
>>>>>>> 41fd800... EF Core 迁移数据库
=======
﻿using Blog.Core.Entities;
using Blog.Infrastructure.Database.EntityConfigurations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
>>>>>>> 12b277e... asp.net core 3.0:src/Blog.Infrastructure/Database/MyContext.cs


namespace Blog.Infrastructure.Database
{
    public class MyContext : DbContext
    {
<<<<<<< HEAD:src/WyBlog.Repository.MySql/Database/BlogDbContext.cs
<<<<<<< HEAD
<<<<<<< HEAD
        public BlogDbContext(DbContextOptions<BlogDbContext> options)
=======
        public BlogDbContext(DbContextOptions options)
>>>>>>> e7f3251... 继续
=======
        public BlogDbContext(DbContextOptions<BlogDbContext> options)
>>>>>>> 41fd800... EF Core 迁移数据库
=======
        public MyContext(DbContextOptions<MyContext> options)
>>>>>>> 12b277e... asp.net core 3.0:src/Blog.Infrastructure/Database/MyContext.cs
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
<<<<<<< HEAD
<<<<<<< HEAD
            //modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);

            modelBuilder.ApplyConfiguration(new ArticleConfiguration());
            modelBuilder.ApplyConfiguration(new ArticleTagConfiguration());
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
            modelBuilder.ApplyConfiguration(new TagConfiguration());
            modelBuilder.ApplyConfiguration(new FriendLinkConfiguration());
=======
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);

            //modelBuilder.ApplyConfiguration(new ArticleConfiguration());
>>>>>>> e7f3251... 继续
=======
            //modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);

            modelBuilder.ApplyConfiguration(new ArticleConfiguration());
            modelBuilder.ApplyConfiguration(new ArticleTagConfiguration());
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
            modelBuilder.ApplyConfiguration(new TagConfiguration());
<<<<<<< HEAD
>>>>>>> 41fd800... EF Core 迁移数据库
=======
            modelBuilder.ApplyConfiguration(new FriendLinkConfiguration());
>>>>>>> fb48ab8... 完善前端资源压缩
        }

        public DbSet<Article> Articles { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ArticleTag> ArticleTags { get; set; }
<<<<<<< HEAD
<<<<<<< HEAD
        public DbSet<FriendLink> FriendLinks { get; set; }
=======
>>>>>>> e7f3251... 继续
=======
        public DbSet<FriendLink> FriendLinks { get; set; }
>>>>>>> fb48ab8... 完善前端资源压缩
    }
}
