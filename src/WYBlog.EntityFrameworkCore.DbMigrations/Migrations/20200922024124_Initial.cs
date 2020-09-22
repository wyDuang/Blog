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
                name: "wy_article_tags",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ArticleId = table.Column<int>(nullable: false, defaultValue: 0),
                    TagId = table.Column<int>(nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_wy_article_tags", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "wy_articles",
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
                    LinkUrl = table.Column<string>(maxLength: 512, nullable: false, defaultValue: ""),
                    Remark = table.Column<string>(maxLength: 1024, nullable: false, defaultValue: ""),
                    ClickCount = table.Column<int>(nullable: false, defaultValue: 0),
                    CommentCount = table.Column<int>(nullable: false, defaultValue: 0),
                    IsDeleted = table.Column<bool>(nullable: false, defaultValue: false),
                    IsTop = table.Column<bool>(nullable: false, defaultValue: false),
                    CreateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false, defaultValue: new DateTime(2020, 9, 22, 10, 41, 24, 30, DateTimeKind.Local).AddTicks(6094))
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_wy_articles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "wy_categories",
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
                    table.PrimaryKey("PK_wy_categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "wy_friendlinks",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(maxLength: 128, nullable: false, defaultValue: ""),
                    LinkUrl = table.Column<string>(maxLength: 512, nullable: false, defaultValue: ""),
                    Avatar = table.Column<string>(maxLength: 128, nullable: false, defaultValue: ""),
                    Description = table.Column<string>(maxLength: 256, nullable: false, defaultValue: ""),
                    CreateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false, defaultValue: new DateTime(2020, 9, 22, 10, 41, 24, 38, DateTimeKind.Local).AddTicks(171)),
                    IsDeleted = table.Column<bool>(nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_wy_friendlinks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "wy_guestBooks",
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
                    CreateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false, defaultValue: new DateTime(2020, 9, 22, 10, 41, 24, 41, DateTimeKind.Local).AddTicks(404)),
                    IsDeleted = table.Column<bool>(nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_wy_guestBooks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "wy_tags",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    TagName = table.Column<string>(maxLength: 64, nullable: false, defaultValue: ""),
                    TagKey = table.Column<string>(maxLength: 32, nullable: false, defaultValue: "")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_wy_tags", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "wy_article_tags");

            migrationBuilder.DropTable(
                name: "wy_articles");

            migrationBuilder.DropTable(
                name: "wy_categories");

            migrationBuilder.DropTable(
                name: "wy_friendlinks");

            migrationBuilder.DropTable(
                name: "wy_guestBooks");

            migrationBuilder.DropTable(
                name: "wy_tags");
        }
    }
}
