using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WYBlog.Entities;

namespace WYBlog.EntityFrameworkCore
{
    public class CategoryEntityTypeConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable(BlogDbTableConsts.Categories);
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.CategoryKey).HasMaxLength(32).HasDefaultValue("").IsRequired();
            builder.Property(x => x.CategoryName).HasMaxLength(64).HasDefaultValue("").IsRequired();
            builder.Property(x => x.Sort).HasDefaultValue(0).IsRequired();
        }
    }
}