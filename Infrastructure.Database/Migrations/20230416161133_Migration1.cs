using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Identity.Server.Migrations
{
    public partial class Migration1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        protected override void Down(MigrationBuilder migrationBuilder)
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
        }
    }
}
