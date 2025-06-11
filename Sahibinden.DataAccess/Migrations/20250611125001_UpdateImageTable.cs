using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sahibinden.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class UpdateImageTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Images",
                schema: "dbo",
                table: "Images",
                newName: "ImageUrl");

            migrationBuilder.AddColumn<bool>(
                name: "IsCover",
                schema: "dbo",
                table: "Images",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsCover",
                schema: "dbo",
                table: "Images");

            migrationBuilder.RenameColumn(
                name: "ImageUrl",
                schema: "dbo",
                table: "Images",
                newName: "Images");
        }
    }
}
