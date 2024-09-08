using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChatApp.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddJsonToMessageTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SeenBy",
                table: "Message");

            migrationBuilder.AddColumn<string>(
                name: "SeenByJson",
                table: "Message",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "CreatedDate",
                table: "ConvenstionParticipant",
                type: "datetimeoffset",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(2024, 9, 8, 0, 28, 13, 155, DateTimeKind.Unspecified).AddTicks(402), new TimeSpan(0, 7, 0, 0, 0)),
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset",
                oldDefaultValue: new DateTimeOffset(new DateTime(2024, 9, 7, 23, 43, 37, 867, DateTimeKind.Unspecified).AddTicks(172), new TimeSpan(0, 7, 0, 0, 0)));

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "LastOnline",
                table: "AppUsers",
                type: "datetimeoffset",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(2024, 9, 8, 0, 28, 13, 151, DateTimeKind.Unspecified).AddTicks(2304), new TimeSpan(0, 7, 0, 0, 0)),
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset",
                oldDefaultValue: new DateTimeOffset(new DateTime(2024, 9, 7, 23, 43, 37, 860, DateTimeKind.Unspecified).AddTicks(3707), new TimeSpan(0, 7, 0, 0, 0)));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SeenByJson",
                table: "Message");

            migrationBuilder.AddColumn<string>(
                name: "SeenBy",
                table: "Message",
                type: "nvarchar(max)",
                nullable: true,
                defaultValue: "[]");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "CreatedDate",
                table: "ConvenstionParticipant",
                type: "datetimeoffset",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(2024, 9, 7, 23, 43, 37, 867, DateTimeKind.Unspecified).AddTicks(172), new TimeSpan(0, 7, 0, 0, 0)),
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset",
                oldDefaultValue: new DateTimeOffset(new DateTime(2024, 9, 8, 0, 28, 13, 155, DateTimeKind.Unspecified).AddTicks(402), new TimeSpan(0, 7, 0, 0, 0)));

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "LastOnline",
                table: "AppUsers",
                type: "datetimeoffset",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(2024, 9, 7, 23, 43, 37, 860, DateTimeKind.Unspecified).AddTicks(3707), new TimeSpan(0, 7, 0, 0, 0)),
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset",
                oldDefaultValue: new DateTimeOffset(new DateTime(2024, 9, 8, 0, 28, 13, 151, DateTimeKind.Unspecified).AddTicks(2304), new TimeSpan(0, 7, 0, 0, 0)));
        }
    }
    public partial class AddSeenByToMessages : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SeenByJson",
                table: "Messages",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SeenByJson",
                table: "Messages");
        }
    }
}
