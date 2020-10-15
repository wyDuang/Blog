using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;
using WYBlog.Entities;

namespace WYBlog.EntityFrameworkCore
{
    [ConnectionStringName("Default")]
    public class BlogDbContext : AbpDbContext<BlogDbContext>
    {
        public BlogDbContext(DbContextOptions<BlogDbContext> options)
            : base(options)
        {
        }

        public DbSet<Article> Articles { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ArticleTag> ArticleTags { get; set; }
        public DbSet<FriendLink> FriendLinks { get; set; }
        public DbSet<GuestBook> GuestBooks { get; set; }
        public DbSet<Signature> Signatures { get; set; }
        public DbSet<Advertisement> Advertisements { get; set; }
        public DbSet<UserInfo> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ConfigureBlog();
        }
    }
}