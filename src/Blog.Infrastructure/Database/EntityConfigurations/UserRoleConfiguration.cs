using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Blog.Core.Entities;
using System;

namespace Blog.Infrastructure.Database.EntityConfigurations
{
	public class UserRoleConfiguration : IEntityTypeConfiguration<UserRole>
	{
		public void Configure(EntityTypeBuilder<UserRole> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.Property(x => x.UserId).HasDefaultValue(0).IsRequired();
            builder.Property(x => x.RoleId).HasDefaultValue(0).IsRequired();
            builder.Property(x => x.CreateDate).HasColumnType("datetime(6)").HasDefaultValue(DateTime.Now);

            builder.ToTable(BlogDbTableNames.UserRoles);
        }
	}
}