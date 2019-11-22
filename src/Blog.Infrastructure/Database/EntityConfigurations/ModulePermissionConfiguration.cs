using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Blog.Core.Entities;
using System;

namespace Blog.Infrastructure.Database.EntityConfigurations
{
	public class ModulePermissionConfiguration : IEntityTypeConfiguration<ModulePermission>
	{
		public void Configure(EntityTypeBuilder<ModulePermission> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.Property(x => x.ModuleId).HasDefaultValue(0).IsRequired();
            builder.Property(x => x.PermissionId).HasDefaultValue(0).IsRequired();
            builder.Property(x => x.CreateDate).HasColumnType("datetime(6)").HasDefaultValue(DateTime.Now);

            builder.ToTable(BlogDbTableNames.ModulePermissions);
        }
	}
}