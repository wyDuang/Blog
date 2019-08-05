using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WyBlog.Entities;
using WyBlog.Repository.MySql.Database;

namespace WyBlog.Repository.MySql.Configurations
{
    public class TagConfiguration : IEntityTypeConfiguration<Tag>
    {
        public void Configure(EntityTypeBuilder<Tag> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.Property(x => x.TagName).HasMaxLength(50).IsRequired();
            builder.Property(x => x.DisplayName).HasMaxLength(50).IsRequired();

            builder.ToTable(BlogDbTableNames.Tags);
        }
    }
}
