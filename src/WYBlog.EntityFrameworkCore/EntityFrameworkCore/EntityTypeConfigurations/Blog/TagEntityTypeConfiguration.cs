using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WYBlog.Entities;

namespace WYBlog.EntityFrameworkCore
{
    public class TagEntityTypeConfiguration : IEntityTypeConfiguration<Tag>
    {
        public void Configure(EntityTypeBuilder<Tag> builder)
        {
            builder.ToTable(BlogDbTableConsts.Tags);
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.TagName).HasMaxLength(64).HasDefaultValue("").IsRequired();
            builder.Property(x => x.TagKey).HasMaxLength(32).HasDefaultValue("").IsRequired();
        }
    }
}