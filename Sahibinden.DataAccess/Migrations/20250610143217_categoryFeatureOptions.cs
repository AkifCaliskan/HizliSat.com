using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sahibinden.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class categoryFeatureOptions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CategoryFeatureOption",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryFeatureId = table.Column<int>(type: "int", nullable: false),
                    OptionText = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DisplayOrder = table.Column<int>(type: "int", nullable: false),
                    RecordDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryFeatureOption", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CategoryFeatureOption_CategoryFeatures_CategoryFeatureId",
                        column: x => x.CategoryFeatureId,
                        principalSchema: "dbo",
                        principalTable: "CategoryFeatures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CategoryFeatureOption_CategoryFeatureId",
                table: "CategoryFeatureOption",
                column: "CategoryFeatureId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CategoryFeatureOption");
        }
    }
}
