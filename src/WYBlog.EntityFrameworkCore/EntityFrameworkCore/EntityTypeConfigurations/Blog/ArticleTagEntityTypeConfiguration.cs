using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WYBlog.Entities;

namespace WYBlog.EntityFrameworkCore
{
    public class ArticleTagEntityTypeConfiguration : IEntityTypeConfiguration<ArticleTag>
    {
        public void Configure(EntityTypeBuilder<ArticleTag> builder)
        {
            builder.ToTable(BlogDbTableConsts.ArticleTags);
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.ArticleId).HasDefaultValue(0).IsRequired();
            builder.Property(x => x.TagId).HasDefaultValue(0).IsRequired();
        }
    }
}