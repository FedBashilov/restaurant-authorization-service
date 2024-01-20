using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Identity.Server.Migrations
{
    public partial class UpdateSeedDataMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "05ff1ce5-332f-4cc4-aef0-b11318697fee");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0c36e836-9adb-499e-b549-46dffeea1b48");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "46db5185-4768-48a6-b6a9-b9a4bfb25f64");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "6a0f6d18-0881-490e-b4eb-b6a0575a89c3", "4bd5e4ac-4b0f-448a-ad22-8854176d7b48", "cook", "COOK" },
                    { "95bd9f40-29af-493a-9bc3-1fb626c5a611", "386023eb-c3a8-47ab-9f8b-b20eefc90dc4", "admin", "ADMIN" },
                    { "b2e54531-402c-4807-84e1-3a6cbc515507", "7491fbeb-044f-4055-9c63-8741cd75a650", "client", "CLIENT" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "Name", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "554b8926-041e-47ec-bece-ba4b1698cbc0", 0, "49337883-3fd3-4bad-a793-0cb13b6c2c8c", "admin@example.com", true, false, null, "admin", "admin@example.com", "admin", "AQAAAAEAACcQAAAAEAJ8Qlf0sfME+pmzGAi8fyAIaB4zXWUGWPPXgQuSgD0KsTvquJV/PS+Oj+Ds2gfz/w==", null, false, "", false, "admin" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "95bd9f40-29af-493a-9bc3-1fb626c5a611", "554b8926-041e-47ec-bece-ba4b1698cbc0" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6a0f6d18-0881-490e-b4eb-b6a0575a89c3");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b2e54531-402c-4807-84e1-3a6cbc515507");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "95bd9f40-29af-493a-9bc3-1fb626c5a611", "554b8926-041e-47ec-bece-ba4b1698cbc0" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "95bd9f40-29af-493a-9bc3-1fb626c5a611");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "554b8926-041e-47ec-bece-ba4b1698cbc0");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "05ff1ce5-332f-4cc4-aef0-b11318697fee", "2afa708e-9a32-4023-94a9-65a3d7e33419", "client", "CLIENT" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "0c36e836-9adb-499e-b549-46dffeea1b48", "e9a409e5-d337-4314-9e44-7cbd414f19c2", "admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "46db5185-4768-48a6-b6a9-b9a4bfb25f64", "8e2063a7-93fa-4554-a91f-c6a62d3656e4", "cook", "COOK" });
        }
    }
}
