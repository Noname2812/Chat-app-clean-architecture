using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChatApp.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTableMessageSeenBy : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                oldDefaultValue: new DateTimeOffset(new DateTime(2024, 9, 7, 21, 36, 48, 721, DateTimeKind.Unspecified).AddTicks(9577), new TimeSpan(0, 7, 0, 0, 0)));

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "LastOnline",
                table: "AppUsers",
                type: "datetimeoffset",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(2024, 9, 7, 23, 43, 37, 860, DateTimeKind.Unspecified).AddTicks(3707), new TimeSpan(0, 7, 0, 0, 0)),
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset",
                oldDefaultValue: new DateTimeOffset(new DateTime(2024, 9, 7, 21, 36, 48, 717, DateTimeKind.Unspecified).AddTicks(6516), new TimeSpan(0, 7, 0, 0, 0)));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SeenBy",
                table: "Message");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "CreatedDate",
                table: "ConvenstionParticipant",
                type: "datetimeoffset",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(2024, 9, 7, 21, 36, 48, 721, DateTimeKind.Unspecified).AddTicks(9577), new TimeSpan(0, 7, 0, 0, 0)),
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset",
                oldDefaultValue: new DateTimeOffset(new DateTime(2024, 9, 7, 23, 43, 37, 867, DateTimeKind.Unspecified).AddTicks(172), new TimeSpan(0, 7, 0, 0, 0)));

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "LastOnline",
                table: "AppUsers",
                type: "datetimeoffset",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(2024, 9, 7, 21, 36, 48, 717, DateTimeKind.Unspecified).AddTicks(6516), new TimeSpan(0, 7, 0, 0, 0)),
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset",
                oldDefaultValue: new DateTimeOffset(new DateTime(2024, 9, 7, 23, 43, 37, 860, DateTimeKind.Unspecified).AddTicks(3707), new TimeSpan(0, 7, 0, 0, 0)));
        }
    }
}
