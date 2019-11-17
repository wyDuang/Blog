using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Blog.Core.Entities;

namespace Blog.Infrastructure.Database.EntityConfigurations
{
	public class RoleConfiguration : IEntityTypeConfiguration<Role>
	{
		public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.Property(x => x.RoleName).HasMaxLength(50).IsRequired();
            builder.Property(x => x.Description).HasMaxLength(100).IsRequired();
            builder.Property(x => x.Sort).HasDefaultValue(0).IsRequired();
            builder.Property(x => x.IsDeleted).HasDefaultValue(0).IsRequired();
            builder.Property(x => x.CreateDate).HasColumnType("datetime(6)");

            builder.ToTable(BlogDbTableNames.Roles);
        }
	}
}