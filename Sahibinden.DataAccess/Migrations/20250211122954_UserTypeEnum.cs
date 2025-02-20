using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sahibinden.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class UserTypeEnum : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                schema: "dbo",
                table: "Users");

            migrationBuilder.AddColumn<int>(
                name: "UserType",
                schema: "dbo",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "UserType",
                value: 2);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserType",
                schema: "dbo",
                table: "Users");

            migrationBuilder.AddColumn<short>(
                name: "Type",
                schema: "dbo",
                table: "Users",
                type: "smallint",
                nullable: false,
                defaultValue: (short)0);

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "Type",
                value: (short)1);
        }
    }
}
