using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Blog.Core.Entities;

namespace Blog.Infrastructure.Database.EntityConfigurations
{
	public class OperatelogConfiguration : IEntityTypeConfiguration<OperateLog>
	{
		public void Configure(EntityTypeBuilder<OperateLog> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.Property(x => x.Area).HasMaxLength(500).IsRequired();
            builder.Property(x => x.Controller).HasMaxLength(500).IsRequired();
            builder.Property(x => x.Action).HasMaxLength(500).IsRequired();
            builder.Property(x => x.IpAddress).HasMaxLength(255).IsRequired();
            builder.Property(x => x.Description).HasColumnType("text").IsRequired();
            builder.Property(x => x.IsDeleted).HasDefaultValue(0).IsRequired();
            builder.Property(x => x.LoginUserId).HasDefaultValue(0).IsRequired();
            builder.Property(x => x.IpAddress).HasMaxLength(255).IsRequired();
            builder.Property(x => x.CreateDate).HasColumnType("datetime(6)");

            builder.ToTable(BlogDbTableNames.OperateLogs);
        }
	}
}