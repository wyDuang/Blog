﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WyBlog.Repository.MySql.Database;

namespace WyBlog.Repository.MySql.Migrations
{
    [DbContext(typeof(BlogDbContext))]
    partial class BlogDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("WyBlog.Models.Article", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Author")
                        .IsRequired()
                        .IsFixedLength(true)
                        .HasMaxLength(20);

                    b.Property<int>("CategoryId");

                    b.Property<int>("ClickCount")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(0);

                    b.Property<int>("CommentCount")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(0);

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime");

                    b.Property<int>("CreateUserId");

                    b.Property<string>("Remark")
                        .IsRequired()
                        .HasMaxLength(2000);

                    b.Property<string>("Source")
                        .IsRequired()
                        .IsFixedLength(true)
                        .HasMaxLength(20);

                    b.Property<string>("Title")
                        .IsRequired()
                        .IsFixedLength(true)
                        .HasMaxLength(100);

                    b.Property<DateTime>("UpdateDate")
                        .HasColumnType("datetime");

                    b.Property<int>("UpdateUserId");

                    b.HasKey("Id");

                    b.ToTable("articles");
                });

            modelBuilder.Entity("WyBlog.Models.Tag", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("ArticleId");

                    b.Property<DateTime>("CreateDate");

                    b.Property<int>("CreateUserId");

                    b.Property<string>("DisplayName")
                        .IsRequired()
                        .IsFixedLength(true)
                        .HasMaxLength(50);

                    b.Property<string>("TagName")
                        .IsRequired()
                        .IsFixedLength(true)
                        .HasMaxLength(50);

                    b.Property<DateTime>("UpdateDate");

                    b.Property<int>("UpdateUserId");

                    b.HasKey("Id");

                    b.HasIndex("ArticleId");

                    b.ToTable("tags");
                });

            modelBuilder.Entity("WyBlog.Models.Tag", b =>
                {
                    b.HasOne("WyBlog.Models.Article")
                        .WithMany("ArticleTags")
                        .HasForeignKey("ArticleId");
                });
#pragma warning restore 612, 618
        }
    }
}
