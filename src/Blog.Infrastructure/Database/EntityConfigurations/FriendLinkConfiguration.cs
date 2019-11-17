using Blog.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Infrastructure.Database.EntityConfigurations
{
    public class FriendLinkConfiguration : IEntityTypeConfiguration<FriendLink>
    {
        public void Configure(EntityTypeBuilder<FriendLink> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.Property(x => x.Title).HasMaxLength(100).IsRequired();
            builder.Property(x => x.LinkUrl).HasMaxLength(512).IsRequired();
            builder.Property(x => x.Avatar).HasMaxLength(100).IsRequired();
            builder.Property(x => x.Description).HasMaxLength(200).IsRequired();
            builder.Property(x => x.CreateDate).HasColumnType("datetime(6)");
            builder.Property(x => x.IsDeleted).HasDefaultValue(0).IsRequired();

            builder.ToTable(BlogDbTableNames.FriendLinks);
        }
    }
}