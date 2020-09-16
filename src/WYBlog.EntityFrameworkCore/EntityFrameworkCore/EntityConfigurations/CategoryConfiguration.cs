using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WYBlog.Entities;

namespace WYBlog.EntityFrameworkCore.EntityConfigurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable(BlogConsts.DbTablePrefix + BlogDbTableConsts.Categories);
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.CategoryKey).HasMaxLength(200).HasDefaultValue("").IsRequired();
            builder.Property(x => x.CategoryName).HasMaxLength(100).HasDefaultValue("").IsRequired();
            builder.Property(x => x.Sort).HasDefaultValue(0).IsRequired();
        }
    }
}