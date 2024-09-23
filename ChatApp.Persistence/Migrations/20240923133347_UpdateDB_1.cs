using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChatApp.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDB_1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "CreatedDate",
                table: "ConvenstionParticipant",
                type: "datetimeoffset",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(2024, 9, 23, 20, 33, 45, 98, DateTimeKind.Unspecified).AddTicks(3754), new TimeSpan(0, 7, 0, 0, 0)),
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset",
                oldDefaultValue: new DateTimeOffset(new DateTime(2024, 9, 8, 0, 28, 13, 155, DateTimeKind.Unspecified).AddTicks(402), new TimeSpan(0, 7, 0, 0, 0)));

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "LastOnline",
                table: "AppUsers",
                type: "datetimeoffset",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(2024, 9, 23, 20, 33, 45, 95, DateTimeKind.Unspecified).AddTicks(444), new TimeSpan(0, 7, 0, 0, 0)),
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset",
                oldDefaultValue: new DateTimeOffset(new DateTime(2024, 9, 8, 0, 28, 13, 151, DateTimeKind.Unspecified).AddTicks(2304), new TimeSpan(0, 7, 0, 0, 0)));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "CreatedDate",
                table: "ConvenstionParticipant",
                type: "datetimeoffset",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(2024, 9, 8, 0, 28, 13, 155, DateTimeKind.Unspecified).AddTicks(402), new TimeSpan(0, 7, 0, 0, 0)),
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset",
                oldDefaultValue: new DateTimeOffset(new DateTime(2024, 9, 23, 20, 33, 45, 98, DateTimeKind.Unspecified).AddTicks(3754), new TimeSpan(0, 7, 0, 0, 0)));

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "LastOnline",
                table: "AppUsers",
                type: "datetimeoffset",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(2024, 9, 8, 0, 28, 13, 151, DateTimeKind.Unspecified).AddTicks(2304), new TimeSpan(0, 7, 0, 0, 0)),
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset",
                oldDefaultValue: new DateTimeOffset(new DateTime(2024, 9, 23, 20, 33, 45, 95, DateTimeKind.Unspecified).AddTicks(444), new TimeSpan(0, 7, 0, 0, 0)));
        }
    }
}
