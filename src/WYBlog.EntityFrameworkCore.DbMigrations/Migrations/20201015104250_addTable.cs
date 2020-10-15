using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WYBlog.Migrations
{
    public partial class addTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateTime",
                table: "guestBooks",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2020, 10, 15, 18, 42, 50, 116, DateTimeKind.Local).AddTicks(995),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2020, 10, 2, 22, 3, 48, 924, DateTimeKind.Local).AddTicks(83));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateTime",
                table: "friendlinks",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2020, 10, 15, 18, 42, 50, 113, DateTimeKind.Local).AddTicks(1507),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2020, 10, 2, 22, 3, 48, 916, DateTimeKind.Local).AddTicks(3498));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateTime",
                table: "articles",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2020, 10, 15, 18, 42, 50, 106, DateTimeKind.Local).AddTicks(9988),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2020, 10, 2, 22, 3, 48, 900, DateTimeKind.Local).AddTicks(5270));

            migrationBuilder.CreateTable(
                name: "advertisement",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ImgUrl = table.Column<string>(maxLength: 512, nullable: false, defaultValue: "", comment: "广告图片"),
                    Title = table.Column<string>(maxLength: 64, nullable: false, defaultValue: "", comment: "广告标题"),
                    Url = table.Column<string>(maxLength: 256, nullable: false, defaultValue: "", comment: "广告链接"),
                    Remark = table.Column<string>(maxLength: 1024, nullable: false, defaultValue: "", comment: "备注"),
                    CreateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false, defaultValue: new DateTime(2020, 10, 15, 18, 42, 50, 94, DateTimeKind.Local).AddTicks(1061), comment: "创建时间")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_advertisement", x => x.Id);
                },
                comment: "广告表");

            migrationBuilder.CreateTable(
                name: "sys_users",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserName = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    RealName = table.Column<string>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    Remark = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    Sex = table.Column<sbyte>(nullable: false),
                    Age = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sys_users", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "advertisement");

            migrationBuilder.DropTable(
                name: "sys_users");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateTime",
                table: "guestBooks",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2020, 10, 2, 22, 3, 48, 924, DateTimeKind.Local).AddTicks(83),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2020, 10, 15, 18, 42, 50, 116, DateTimeKind.Local).AddTicks(995));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateTime",
                table: "friendlinks",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2020, 10, 2, 22, 3, 48, 916, DateTimeKind.Local).AddTicks(3498),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2020, 10, 15, 18, 42, 50, 113, DateTimeKind.Local).AddTicks(1507));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateTime",
                table: "articles",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2020, 10, 2, 22, 3, 48, 900, DateTimeKind.Local).AddTicks(5270),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2020, 10, 15, 18, 42, 50, 106, DateTimeKind.Local).AddTicks(9988));
        }
    }
}
