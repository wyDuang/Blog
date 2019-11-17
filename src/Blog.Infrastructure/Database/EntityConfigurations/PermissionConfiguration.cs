using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Blog.Core.Entities;

namespace Blog.Infrastructure.Database.EntityConfigurations
{
	public class PermissionConfiguration : IEntityTypeConfiguration<Permission>
	{
		public void Configure(EntityTypeBuilder<Permission> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.Property(x => x.PermissionCode).HasMaxLength(50).IsRequired();
            builder.Property(x => x.PermissionName).HasMaxLength(50).IsRequired();
            
            builder.Property(x => x.IsDeleted).HasDefaultValue(0).IsRequired();
            builder.Property(x => x.IsEnabled).HasDefaultValue(0).IsRequired();
            builder.Property(x => x.IsButton).HasDefaultValue(0).IsRequired();
            builder.Property(x => x.IsHide).HasDefaultValue(0).IsRequired();
            builder.Property(x => x.Sort).HasDefaultValue(0).IsRequired();
            builder.Property(x => x.Icon).HasMaxLength(100).IsRequired();
            builder.Property(x => x.Func).HasMaxLength(2000).IsRequired();
            builder.Property(x => x.CreateDate).HasColumnType("datetime(6)");

            builder.ToTable(BlogDbTableNames.Permissions);
        }
	}
}