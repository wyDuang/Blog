using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WYBlog.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "article_tags",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ArticleId = table.Column<int>(nullable: false, defaultValue: 0),
                    TagId = table.Column<int>(nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_article_tags", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "articles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CategoryId = table.Column<int>(nullable: false, defaultValue: 0),
                    ArticleType = table.Column<sbyte>(nullable: false, defaultValue: (sbyte)0),
                    ArticleKey = table.Column<string>(maxLength: 32, nullable: false, defaultValue: ""),
                    Title = table.Column<string>(maxLength: 256, nullable: false, defaultValue: ""),
                    Html = table.Column<string>(type: "text", nullable: false),
                    Markdown = table.Column<string>(type: "text", nullable: false),
                    Author = table.Column<string>(maxLength: 64, nullable: false, defaultValue: ""),
                    Remark = table.Column<string>(maxLength: 1024, nullable: false, defaultValue: ""),
                    ClickCount = table.Column<int>(nullable: false, defaultValue: 0),
                    CommentCount = table.Column<int>(nullable: false, defaultValue: 0),
                    IsDeleted = table.Column<bool>(nullable: false, defaultValue: false),
                    IsTop = table.Column<bool>(nullable: false, defaultValue: false),
                    CreateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false, defaultValue: new DateTime(2020, 10, 2, 22, 3, 48, 900, DateTimeKind.Local).AddTicks(5270))
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_articles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "categories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CategoryName = table.Column<string>(maxLength: 64, nullable: false, defaultValue: ""),
                    CategoryKey = table.Column<string>(maxLength: 32, nullable: false, defaultValue: ""),
                    Sort = table.Column<int>(nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "friendlinks",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(maxLength: 128, nullable: false, defaultValue: ""),
                    LinkUrl = table.Column<string>(maxLength: 512, nullable: false, defaultValue: ""),
                    Avatar = table.Column<string>(maxLength: 128, nullable: false, defaultValue: ""),
                    Description = table.Column<string>(maxLength: 256, nullable: false, defaultValue: ""),
                    CreateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false, defaultValue: new DateTime(2020, 10, 2, 22, 3, 48, 916, DateTimeKind.Local).AddTicks(3498)),
                    IsDeleted = table.Column<bool>(nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_friendlinks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "guestBooks",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ArticleId = table.Column<int>(nullable: false, defaultValue: 0),
                    ParentId = table.Column<int>(nullable: false, defaultValue: 0),
                    UserName = table.Column<string>(maxLength: 64, nullable: false, defaultValue: ""),
                    MobilePhone = table.Column<string>(maxLength: 32, nullable: false, defaultValue: ""),
                    Email = table.Column<string>(maxLength: 64, nullable: false, defaultValue: ""),
                    Content = table.Column<string>(type: "text", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false, defaultValue: new DateTime(2020, 10, 2, 22, 3, 48, 924, DateTimeKind.Local).AddTicks(83)),
                    IsDeleted = table.Column<bool>(nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_guestBooks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tags",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    TagName = table.Column<string>(maxLength: 64, nullable: false, defaultValue: ""),
                    TagKey = table.Column<string>(maxLength: 32, nullable: false, defaultValue: "")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tags", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "article_tags");

            migrationBuilder.DropTable(
                name: "articles");

            migrationBuilder.DropTable(
                name: "categories");

            migrationBuilder.DropTable(
                name: "friendlinks");

            migrationBuilder.DropTable(
                name: "guestBooks");

            migrationBuilder.DropTable(
                name: "tags");
        }
    }
}
