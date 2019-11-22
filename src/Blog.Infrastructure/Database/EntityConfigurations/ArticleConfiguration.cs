using Blog.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Blog.Infrastructure.Database.EntityConfigurations
{
    public class ArticleConfiguration : IEntityTypeConfiguration<Article>
    {
        public void Configure(EntityTypeBuilder<Article> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.Property(x => x.CategoryId).HasDefaultValue(0).IsRequired();
            builder.Property(x => x.ArticleType).HasDefaultValue(0).IsRequired();

            builder.Property(x => x.ArticleKey).HasMaxLength(500).HasDefaultValue("").IsRequired();
            builder.Property(x => x.Title).HasMaxLength(200).HasDefaultValue("").IsRequired();
            builder.Property(x => x.Html).HasColumnType("text").HasDefaultValue("");
            builder.Property(x => x.Markdown).HasColumnType("text").HasDefaultValue("");

            builder.Property(x => x.Author).HasMaxLength(50).HasDefaultValue("").IsRequired();
            builder.Property(x =>x.LinkUrl).HasMaxLength(512).HasDefaultValue("").IsRequired();
            builder.Property(x => x.Remark).HasMaxLength(1000).HasDefaultValue("").IsRequired();

            builder.Property(x => x.ClickCount).HasDefaultValue(0).IsRequired();
            builder.Property(x => x.CommentCount).HasDefaultValue(0).IsRequired();
            builder.Property(x => x.IsDeleted).HasDefaultValue(0).IsRequired();
            builder.Property(x => x.IsTop).HasDefaultValue(0).IsRequired();

            builder.Property(x => x.CreateDate).HasColumnType("datetime(6)").HasDefaultValue(DateTime.Now);
            builder.Property(x => x.UpdateDate).HasColumnType("datetime(6)").HasDefaultValue(DateTime.Now);

            builder.ToTable(BlogDbTableNames.Articles);
        }
    }
}
