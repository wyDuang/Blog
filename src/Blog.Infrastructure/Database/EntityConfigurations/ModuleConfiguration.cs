using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Blog.Core.Entities;
using System;

namespace Blog.Infrastructure.Database.EntityConfigurations
{
	public class ModuleConfiguration : IEntityTypeConfiguration<Module>
	{
		public void Configure(EntityTypeBuilder<Module> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.Property(x => x.ParentId).HasDefaultValue(0).IsRequired();
            builder.Property(x => x.ModuleCode).HasMaxLength(256).HasDefaultValue("").IsRequired();
            builder.Property(x => x.ModuleName).HasMaxLength(50).HasDefaultValue("").IsRequired();
            builder.Property(x => x.LinkUrl).HasMaxLength(256).HasDefaultValue("").IsRequired();
            builder.Property(x => x.Description).HasMaxLength(200).HasDefaultValue("").IsRequired();
            builder.Property(x => x.Area).HasMaxLength(500).HasDefaultValue("").IsRequired();
            builder.Property(x => x.Controller).HasMaxLength(500).HasDefaultValue("").IsRequired();
            builder.Property(x => x.Action).HasMaxLength(500).HasDefaultValue("").IsRequired();
            builder.Property(x => x.Icon).HasMaxLength(256).HasDefaultValue("").IsRequired();
            builder.Property(x => x.Sort).HasDefaultValue(0).IsRequired();
            builder.Property(x => x.IsMenu).HasDefaultValue(0).IsRequired(); 
            builder.Property(x => x.CreateDate).HasColumnType("datetime(6)").HasDefaultValue(DateTime.Now);
            builder.Property(x => x.IsDeleted).HasDefaultValue(0).IsRequired();

            builder.ToTable(BlogDbTableNames.Modules);
        }
	}
}