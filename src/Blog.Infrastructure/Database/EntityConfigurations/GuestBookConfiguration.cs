using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Blog.Core.Entities;

namespace Blog.Infrastructure.Database.EntityConfigurations
{
	public class GuestbookConfiguration : IEntityTypeConfiguration<GuestBook>
	{
		public void Configure(EntityTypeBuilder<GuestBook> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.Property(x => x.ArticleId).HasDefaultValue(0).IsRequired();
            builder.Property(x => x.UserName).HasMaxLength(50).IsRequired();
            builder.Property(x => x.Email).HasMaxLength(100).IsRequired();
            builder.Property(x => x.MobilePhone).HasMaxLength(200).IsRequired();
            builder.Property(x => x.CreateDate).HasColumnType("datetime(6)");
            builder.Property(x => x.IsDeleted).HasDefaultValue(0).IsRequired();

            builder.ToTable(BlogDbTableNames.GuestBooks);
        }
	}
}