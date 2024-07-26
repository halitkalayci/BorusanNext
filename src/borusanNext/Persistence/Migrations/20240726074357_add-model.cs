using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class addmodel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "UserOperationClaims",
                keyColumn: "Id",
                keyValue: new Guid("28c68993-d2c4-4bce-ab0e-a48567a3bcb9"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("72ae1df4-6988-4d13-a9f1-d8b0724c96a0"));

            migrationBuilder.CreateTable(
                name: "Models",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BrandId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Models", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Models_Brands_BrandId",
                        column: x => x.BrandId,
                        principalTable: "Brands",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "OperationClaims",
                columns: new[] { "Id", "CreatedDate", "DeletedDate", "Name", "UpdatedDate" },
                values: new object[,]
                {
                    { 30, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Brands.GetDynamic", null },
                    { 31, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Models.Admin", null },
                    { 32, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Models.Read", null },
                    { 33, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Models.Write", null },
                    { 34, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Models.Create", null },
                    { 35, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Models.Update", null },
                    { 36, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Models.Delete", null }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AuthenticatorType", "CreatedDate", "DeletedDate", "Email", "PasswordHash", "PasswordSalt", "UpdatedDate" },
                values: new object[] { new Guid("878c8463-9b99-4b2e-96bc-ccc20ce7cffd"), 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "narch@kodlama.io", new byte[] { 227, 183, 135, 0, 113, 79, 147, 185, 26, 168, 176, 97, 152, 128, 200, 154, 56, 146, 50, 181, 15, 70, 41, 45, 39, 72, 29, 189, 174, 18, 185, 179, 24, 225, 43, 29, 57, 180, 213, 155, 207, 168, 12, 239, 175, 185, 72, 208, 43, 16, 142, 183, 227, 10, 147, 26, 76, 30, 52, 39, 124, 25, 16, 175 }, new byte[] { 169, 19, 111, 149, 39, 110, 16, 145, 140, 10, 251, 247, 132, 183, 210, 21, 202, 175, 223, 53, 228, 192, 135, 201, 39, 104, 33, 53, 239, 16, 158, 50, 190, 63, 19, 223, 72, 55, 38, 72, 52, 187, 117, 151, 5, 233, 94, 248, 55, 58, 23, 81, 114, 89, 63, 64, 7, 18, 193, 138, 148, 216, 160, 142, 174, 162, 72, 187, 162, 77, 199, 160, 140, 12, 22, 154, 246, 207, 76, 175, 226, 144, 27, 176, 134, 58, 87, 68, 74, 163, 67, 41, 30, 118, 217, 174, 92, 110, 161, 63, 75, 9, 252, 182, 246, 249, 219, 62, 140, 251, 241, 102, 206, 4, 94, 46, 131, 167, 93, 165, 27, 213, 72, 103, 245, 179, 118, 65 }, null });

            migrationBuilder.InsertData(
                table: "UserOperationClaims",
                columns: new[] { "Id", "CreatedDate", "DeletedDate", "OperationClaimId", "UpdatedDate", "UserId" },
                values: new object[] { new Guid("b9ac0078-cd83-4c68-93a2-3d204e5462c5"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 1, null, new Guid("878c8463-9b99-4b2e-96bc-ccc20ce7cffd") });

            migrationBuilder.CreateIndex(
                name: "IX_Models_BrandId",
                table: "Models",
                column: "BrandId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Models");

            migrationBuilder.DeleteData(
                table: "OperationClaims",
                keyColumn: "Id",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "OperationClaims",
                keyColumn: "Id",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "OperationClaims",
                keyColumn: "Id",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "OperationClaims",
                keyColumn: "Id",
                keyValue: 33);

            migrationBuilder.DeleteData(
                table: "OperationClaims",
                keyColumn: "Id",
                keyValue: 34);

            migrationBuilder.DeleteData(
                table: "OperationClaims",
                keyColumn: "Id",
                keyValue: 35);

            migrationBuilder.DeleteData(
                table: "OperationClaims",
                keyColumn: "Id",
                keyValue: 36);

            migrationBuilder.DeleteData(
                table: "UserOperationClaims",
                keyColumn: "Id",
                keyValue: new Guid("b9ac0078-cd83-4c68-93a2-3d204e5462c5"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("878c8463-9b99-4b2e-96bc-ccc20ce7cffd"));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AuthenticatorType", "CreatedDate", "DeletedDate", "Email", "PasswordHash", "PasswordSalt", "UpdatedDate" },
                values: new object[] { new Guid("72ae1df4-6988-4d13-a9f1-d8b0724c96a0"), 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "narch@kodlama.io", new byte[] { 46, 48, 146, 35, 16, 186, 20, 204, 159, 153, 103, 244, 38, 141, 48, 18, 0, 95, 76, 112, 218, 46, 149, 102, 27, 123, 104, 130, 133, 103, 206, 184, 187, 129, 207, 165, 230, 128, 75, 173, 184, 50, 235, 184, 201, 100, 174, 3, 130, 120, 66, 131, 157, 5, 47, 158, 72, 15, 25, 42, 237, 233, 225, 191 }, new byte[] { 187, 255, 67, 117, 77, 42, 191, 42, 194, 166, 86, 100, 58, 235, 206, 14, 162, 117, 115, 198, 28, 254, 118, 7, 137, 16, 163, 177, 164, 2, 65, 71, 55, 18, 191, 15, 69, 108, 135, 114, 72, 1, 143, 154, 189, 248, 28, 106, 78, 195, 207, 220, 204, 52, 142, 174, 77, 111, 233, 40, 107, 6, 129, 134, 220, 50, 243, 163, 7, 153, 244, 114, 47, 239, 12, 175, 110, 148, 56, 26, 56, 35, 28, 215, 238, 249, 35, 91, 232, 169, 230, 148, 83, 214, 17, 101, 45, 31, 218, 215, 82, 255, 217, 239, 145, 173, 40, 160, 204, 249, 61, 237, 47, 84, 150, 233, 16, 172, 118, 58, 205, 140, 81, 155, 246, 116, 139, 222 }, null });

            migrationBuilder.InsertData(
                table: "UserOperationClaims",
                columns: new[] { "Id", "CreatedDate", "DeletedDate", "OperationClaimId", "UpdatedDate", "UserId" },
                values: new object[] { new Guid("28c68993-d2c4-4bce-ab0e-a48567a3bcb9"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 1, null, new Guid("72ae1df4-6988-4d13-a9f1-d8b0724c96a0") });
        }
    }
}
