using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sahibinden.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class InputTypeEnum : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AdvertDetails_CategoryFeatures_FeautureId",
                schema: "dbo",
                table: "AdvertDetails");

            migrationBuilder.DropColumn(
                name: "ImageId",
                schema: "dbo",
                table: "Adverts");

            migrationBuilder.DropColumn(
                name: "CategoryFeautureId",
                schema: "dbo",
                table: "AdvertDetails");

            migrationBuilder.RenameColumn(
                name: "FeautureId",
                schema: "dbo",
                table: "AdvertDetails",
                newName: "CategoryFeatureId");

            migrationBuilder.RenameIndex(
                name: "IX_AdvertDetails_FeautureId",
                schema: "dbo",
                table: "AdvertDetails",
                newName: "IX_AdvertDetails_CategoryFeatureId");

            migrationBuilder.AddColumn<int>(
                name: "InputType",
                schema: "dbo",
                table: "CategoryFeatures",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "CategoryFeatures",
                keyColumn: "Id",
                keyValue: 1,
                column: "InputType",
                value: 0);

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "CategoryFeatures",
                keyColumn: "Id",
                keyValue: 2,
                column: "InputType",
                value: 0);

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "CategoryFeatures",
                keyColumn: "Id",
                keyValue: 3,
                column: "InputType",
                value: 0);

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "CategoryFeatures",
                keyColumn: "Id",
                keyValue: 4,
                column: "InputType",
                value: 0);

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "CategoryFeatures",
                keyColumn: "Id",
                keyValue: 5,
                column: "InputType",
                value: 0);

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "CategoryFeatures",
                keyColumn: "Id",
                keyValue: 6,
                column: "InputType",
                value: 0);

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "CategoryFeatures",
                keyColumn: "Id",
                keyValue: 7,
                column: "InputType",
                value: 0);

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "CategoryFeatures",
                keyColumn: "Id",
                keyValue: 8,
                column: "InputType",
                value: 0);

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "CategoryFeatures",
                keyColumn: "Id",
                keyValue: 9,
                column: "InputType",
                value: 0);

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "CategoryFeatures",
                keyColumn: "Id",
                keyValue: 10,
                column: "InputType",
                value: 0);

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "CategoryFeatures",
                keyColumn: "Id",
                keyValue: 11,
                column: "InputType",
                value: 0);

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "CategoryFeatures",
                keyColumn: "Id",
                keyValue: 12,
                column: "InputType",
                value: 0);

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "CategoryFeatures",
                keyColumn: "Id",
                keyValue: 13,
                column: "InputType",
                value: 0);

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "CategoryFeatures",
                keyColumn: "Id",
                keyValue: 14,
                column: "InputType",
                value: 0);

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "CategoryFeatures",
                keyColumn: "Id",
                keyValue: 15,
                column: "InputType",
                value: 0);

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "CategoryFeatures",
                keyColumn: "Id",
                keyValue: 16,
                column: "InputType",
                value: 0);

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "CategoryFeatures",
                keyColumn: "Id",
                keyValue: 17,
                column: "InputType",
                value: 0);

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "CategoryFeatures",
                keyColumn: "Id",
                keyValue: 18,
                column: "InputType",
                value: 0);

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "CategoryFeatures",
                keyColumn: "Id",
                keyValue: 19,
                column: "InputType",
                value: 0);

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "CategoryFeatures",
                keyColumn: "Id",
                keyValue: 20,
                column: "InputType",
                value: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Images_AdvertId",
                schema: "dbo",
                table: "Images",
                column: "AdvertId");

            migrationBuilder.CreateIndex(
                name: "IX_Adverts_UserId",
                schema: "dbo",
                table: "Adverts",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AdvertDetails_CategoryFeatures_CategoryFeatureId",
                schema: "dbo",
                table: "AdvertDetails",
                column: "CategoryFeatureId",
                principalSchema: "dbo",
                principalTable: "CategoryFeatures",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Adverts_Users_UserId",
                schema: "dbo",
                table: "Adverts",
                column: "UserId",
                principalSchema: "dbo",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Adverts_AdvertId",
                schema: "dbo",
                table: "Images",
                column: "AdvertId",
                principalSchema: "dbo",
                principalTable: "Adverts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AdvertDetails_CategoryFeatures_CategoryFeatureId",
                schema: "dbo",
                table: "AdvertDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_Adverts_Users_UserId",
                schema: "dbo",
                table: "Adverts");

            migrationBuilder.DropForeignKey(
                name: "FK_Images_Adverts_AdvertId",
                schema: "dbo",
                table: "Images");

            migrationBuilder.DropIndex(
                name: "IX_Images_AdvertId",
                schema: "dbo",
                table: "Images");

            migrationBuilder.DropIndex(
                name: "IX_Adverts_UserId",
                schema: "dbo",
                table: "Adverts");

            migrationBuilder.DropColumn(
                name: "InputType",
                schema: "dbo",
                table: "CategoryFeatures");

            migrationBuilder.RenameColumn(
                name: "CategoryFeatureId",
                schema: "dbo",
                table: "AdvertDetails",
                newName: "FeautureId");

            migrationBuilder.RenameIndex(
                name: "IX_AdvertDetails_CategoryFeatureId",
                schema: "dbo",
                table: "AdvertDetails",
                newName: "IX_AdvertDetails_FeautureId");

            migrationBuilder.AddColumn<int>(
                name: "ImageId",
                schema: "dbo",
                table: "Adverts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CategoryFeautureId",
                schema: "dbo",
                table: "AdvertDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_AdvertDetails_CategoryFeatures_FeautureId",
                schema: "dbo",
                table: "AdvertDetails",
                column: "FeautureId",
                principalSchema: "dbo",
                principalTable: "CategoryFeatures",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
