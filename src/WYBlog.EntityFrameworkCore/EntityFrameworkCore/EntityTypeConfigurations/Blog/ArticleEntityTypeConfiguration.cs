﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using WYBlog.Entities;

namespace WYBlog.EntityFrameworkCore
{
    public class ArticleEntityTypeConfiguration : IEntityTypeConfiguration<Article>
    {
        public void Configure(EntityTypeBuilder<Article> builder)
        {
            builder.ToTable(BlogDbTableConsts.Articles);
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.CategoryId).IsRequired().HasDefaultValue(0);
            builder.Property(x => x.ArticleType).IsRequired().HasDefaultValue(0);
            builder.Property(x => x.ArticleKey).HasMaxLength(32).IsRequired().HasDefaultValue("");
            builder.Property(x => x.Author).HasMaxLength(64).IsRequired().HasDefaultValue("");
            builder.Property(x => x.Title).HasMaxLength(256).IsRequired().HasDefaultValue("");
            builder.Property(x => x.Html).HasColumnType("text").IsRequired();
            builder.Property(x => x.Markdown).HasColumnType("text").IsRequired();
            builder.Property(x => x.Remark).HasMaxLength(1024).IsRequired().HasDefaultValue("");
            builder.Property(x => x.ClickCount).IsRequired().HasDefaultValue(0);
            builder.Property(x => x.CommentCount).IsRequired().HasDefaultValue(0);
            builder.Property(x => x.IsDeleted).IsRequired().HasDefaultValue(0);
            builder.Property(x => x.IsTop).IsRequired().HasDefaultValue(0);
            builder.Property(x => x.CreateTime).HasColumnType("datetime(6)").IsRequired().HasDefaultValue(DateTime.Now);
        }
    }
}