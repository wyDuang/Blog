using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using WyBlog.Entities;
using WyBlog.Repository.MySql.Database;

namespace WyBlog.Repository.MySql.Configurations
{
    public class ArticleTagConfiguration : IEntityTypeConfiguration<ArticleTag>
    {
        public void Configure(EntityTypeBuilder<ArticleTag> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.Property(x => x.ArticleId).HasDefaultValue(0).IsRequired();
            builder.Property(x => x.TagId).HasDefaultValue(0).IsRequired();

            builder.ToTable(BlogDbTableNames.Article_Tags);
        }
    }
}
