using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Identity.Server.Migrations
{
    public partial class Migration3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7d141565-db75-47cf-9291-bcdabe35edf9");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "903469e1-65e2-44b4-b59d-c3d31d034ce9");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ccd7463f-8926-47a3-b618-545fd684a7e0");

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

        protected override void Down(MigrationBuilder migrationBuilder)
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
                values: new object[] { "7d141565-db75-47cf-9291-bcdabe35edf9", "244265d4-34eb-41ba-bd94-69dc46492968", "admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "903469e1-65e2-44b4-b59d-c3d31d034ce9", "7154235b-c70b-4c41-9342-3535938f9cce", "client", "CLIENT" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "ccd7463f-8926-47a3-b618-545fd684a7e0", "c360bd5c-0055-47c7-900d-29c9e0378c10", "cook", "COOK" });
        }
    }
}
