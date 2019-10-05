using Blog.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blog.Infrastructure.Database.EntityConfigurations
{
    public class ArticleConfiguration : IEntityTypeConfiguration<Article>
    {
        public void Configure(EntityTypeBuilder<Article> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.Property(x => x.Title).HasMaxLength(100).IsRequired();
            builder.Property(x => x.Html).HasColumnType("text");
            builder.Property(x => x.Markdown).HasColumnType("text");

            builder.Property(x => x.Author).HasMaxLength(20).IsRequired();
            builder.Property(x => x.Source).HasMaxLength(20).IsRequired();
            builder.Property(x => x.ClickCount).HasDefaultValue(0).IsRequired();
            builder.Property(x => x.CommentCount).HasDefaultValue(0).IsRequired();

            builder.Property(x => x.Remark).HasMaxLength(2000).IsRequired();
            builder.Property(x => x.CreateDate).HasColumnType("datetime(6)");

            builder.ToTable(BlogDbTableNames.Articles);
        }
    }
}
