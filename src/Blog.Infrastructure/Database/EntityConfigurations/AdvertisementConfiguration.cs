using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Blog.Core.Entities;
using System;

namespace Blog.Infrastructure.Database.EntityConfigurations
{
	public class AdvertisementConfiguration : IEntityTypeConfiguration<Advertisement>
	{
		public void Configure(EntityTypeBuilder<Advertisement> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.Property(x => x.ImgUrl).HasMaxLength(512).HasDefaultValue("").IsRequired();
            builder.Property(x => x.Title).HasMaxLength(100).HasDefaultValue("").IsRequired();
            builder.Property(x => x.LinkUrl).HasMaxLength(512).HasDefaultValue("").IsRequired();
            builder.Property(x => x.Remark).HasMaxLength(1000).HasDefaultValue("").IsRequired();

            builder.Property(x => x.IsDeleted).HasDefaultValue(0).IsRequired();
            builder.Property(x => x.CreateDate).HasColumnType("datetime(6)").HasDefaultValue(DateTime.Now);

            builder.ToTable(BlogDbTableNames.Advertisements);
        }
	}
}