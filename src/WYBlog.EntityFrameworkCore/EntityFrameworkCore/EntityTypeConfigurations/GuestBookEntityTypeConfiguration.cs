using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using WYBlog.Entities;

namespace WYBlog.EntityFrameworkCore.EntityTypeConfigurations
{
    public class GuestbookEntityTypeConfiguration : IEntityTypeConfiguration<GuestBook>
    {
        public void Configure(EntityTypeBuilder<GuestBook> builder)
        {
            builder.ToTable(BlogConsts.DbTablePrefix + BlogDbTableConsts.GuestBooks);
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.ArticleId).HasDefaultValue(0).IsRequired();
            builder.Property(x => x.UserName).HasMaxLength(64).HasDefaultValue("").IsRequired();
            builder.Property(x => x.Email).HasMaxLength(64).HasDefaultValue("").IsRequired();
            builder.Property(x => x.MobilePhone).HasMaxLength(32).HasDefaultValue("").IsRequired();
            builder.Property(x => x.CreateTime).IsRequired().HasColumnType("datetime(6)").HasDefaultValue(DateTime.Now);
            builder.Property(x => x.IsDeleted).HasDefaultValue(0).IsRequired();
        }
    }
}