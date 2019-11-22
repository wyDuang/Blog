using Blog.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Infrastructure.Database.EntityConfigurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.Property(x => x.CategoryKey).HasMaxLength(200).HasDefaultValue("").IsRequired();
            builder.Property(x => x.CategoryName).HasMaxLength(100).HasDefaultValue("").IsRequired();
            builder.Property(x => x.Sort).HasDefaultValue(0).IsRequired();
            builder.Property(x => x.Remark).HasMaxLength(2000).IsRequired();
            builder.Property(x => x.CreateDate).HasColumnType("datetime(6)").HasDefaultValue(DateTime.Now);
            builder.Property(x => x.IsDeleted).HasDefaultValue(0).IsRequired();

            builder.ToTable(BlogDbTableNames.Categories);
        }
    }
}
