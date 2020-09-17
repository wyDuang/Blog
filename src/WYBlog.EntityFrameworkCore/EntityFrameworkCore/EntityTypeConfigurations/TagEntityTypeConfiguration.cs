using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WYBlog.Entities;

namespace WYBlog.EntityFrameworkCore.EntityTypeConfigurations
{
    public class TagEntityTypeConfiguration : IEntityTypeConfiguration<Tag>
    {
        public void Configure(EntityTypeBuilder<Tag> builder)
        {
            builder.ToTable(BlogConsts.DbTablePrefix + BlogDbTableConsts.Tags);
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.TagName).HasMaxLength(64).HasDefaultValue("").IsRequired();
            builder.Property(x => x.TagKey).HasMaxLength(32).HasDefaultValue("").IsRequired();
        }
    }
}