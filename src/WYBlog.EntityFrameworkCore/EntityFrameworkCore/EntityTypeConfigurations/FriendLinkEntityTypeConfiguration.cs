using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using WYBlog.Entities;

namespace WYBlog.EntityFrameworkCore.EntityTypeConfigurations
{
    public class FriendLinkEntityTypeConfiguration : IEntityTypeConfiguration<FriendLink>
    {
        public void Configure(EntityTypeBuilder<FriendLink> builder)
        {
            builder.ToTable(BlogConsts.DbTablePrefix + BlogDbTableConsts.FriendLinks);
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.Property(x => x.Title).HasMaxLength(128).HasDefaultValue("").IsRequired();
            builder.Property(x => x.LinkUrl).HasMaxLength(512).HasDefaultValue("").IsRequired();
            builder.Property(x => x.Avatar).HasMaxLength(128).HasDefaultValue("").IsRequired();
            builder.Property(x => x.Description).HasMaxLength(256).HasDefaultValue("").IsRequired();
            builder.Property(x => x.CreateTime).HasColumnType("datetime(6)").IsRequired().HasDefaultValue(DateTime.Now);
            builder.Property(x => x.IsDeleted).HasDefaultValue(0).IsRequired();
        }
    }
}