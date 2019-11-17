using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Blog.Core.Entities;

namespace Blog.Infrastructure.Database.EntityConfigurations
{
	public class UserConfiguration : IEntityTypeConfiguration<User>
	{
		public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.Property(x => x.RoleId).HasDefaultValue(0).IsRequired();
            builder.Property(x => x.UserName).HasMaxLength(50).IsRequired();
            builder.Property(x => x.Password).HasMaxLength(50).IsRequired();
            builder.Property(x => x.MobilePhone).HasMaxLength(50).IsRequired(); 
            builder.Property(x => x.Email).HasMaxLength(100).IsRequired();

            builder.Property(x => x.Status).HasDefaultValue(0).IsRequired();
            builder.Property(x => x.IsDeleted).HasDefaultValue(0).IsRequired();

            builder.Property(x => x.LastLoginTime).HasColumnType("datetime(6)");
            builder.Property(x => x.CreateDate).HasColumnType("datetime(6)");

            builder.Property(x => x.LastLoginIp).HasMaxLength(100).IsRequired();
            builder.Property(x => x.ErrorCount).HasDefaultValue(0).IsRequired();

            builder.ToTable(BlogDbTableNames.Users);
        }
	}
}