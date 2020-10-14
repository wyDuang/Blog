using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using WYBlog.Entities;

namespace WYBlog.EntityFrameworkCore
{
    public class AdvertisementEntityTypeConfiguration : IEntityTypeConfiguration<Advertisement>
    {
        public void Configure(EntityTypeBuilder<Advertisement> builder)
        {
            builder.ToTable(BlogDbTableConsts.Advertisements).HasComment("广告表");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.ImgUrl).HasMaxLength(512).IsRequired().HasDefaultValue("").HasComment("广告图片");
            builder.Property(x => x.Title).HasMaxLength(64).IsRequired().HasDefaultValue("").HasComment("广告标题");
            builder.Property(x => x.Url).HasMaxLength(256).IsRequired().HasDefaultValue("").HasComment("广告链接");
            builder.Property(x => x.Remark).HasMaxLength(1024).IsRequired().HasDefaultValue("").HasComment("备注");
            builder.Property(x => x.CreateTime).HasColumnType("datetime(6)").IsRequired().HasDefaultValue(DateTime.Now).HasComment("创建时间");
        }
    }
}