using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Identity.Server.Migrations
{
    public partial class UpdateSeedData5Migration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d349a58a-a4df-4569-97e5-7550dedbb659");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "0a3af66a-2137-4c60-9d2d-663c0023733c", "c9f11152-4dd7-4473-94c7-2653824d25c8" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "bfb448ff-6598-4a31-a969-f3516c3ef5bb", "f7194159-b143-47d0-bfdf-efa10830c160" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0a3af66a-2137-4c60-9d2d-663c0023733c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "bfb448ff-6598-4a31-a969-f3516c3ef5bb");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "c9f11152-4dd7-4473-94c7-2653824d25c8");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "f7194159-b143-47d0-bfdf-efa10830c160");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "340fc3d0-5df4-49c3-9a11-b73ca56a5f17", "562499d7-e958-4f37-bb35-ae87d5ea6d43", "cook", "COOK" },
                    { "d7f83458-796b-4038-9534-470f54f564b0", "c56a09a9-2b9d-4ab5-a82f-35d0aa63f425", "client", "CLIENT" },
                    { "f5e7508d-e9fa-4cf8-8dbd-e6127c35bfa5", "adcda3a8-d54e-4012-a29b-76b8a09c28c6", "admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "Name", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "32131582-161e-4a1a-a696-5df7af15bda0", 0, "d697c428-0ab1-49cd-bb33-8cc410a486af", "admin@example.com", true, false, null, "admin", "admin@example.com", "admin", "AQAAAAEAACcQAAAAECHZODtNq9zUpmO3YJeKSkiJwMkh19mZ913Zk6j8K3a2jlfbrb4Pb09FGQYIDpNyUw==", null, false, "", false, "admin" },
                    { "d33c3f10-ba31-4086-ab7b-048f2888e7a3", 0, "3579e543-9914-4b56-afd5-c991d20c6a99", "client@example.com", true, false, null, "client", "client@example.com", "client", "AQAAAAEAACcQAAAAENqj6Y8ivJJMk1RcKeXiGnYIGh1CWXNwqonHP/p2vJfd6ILxR1Z1oSHj1GDjzlCO0w==", null, false, "", false, "client" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserClaims",
                columns: new[] { "Id", "ClaimType", "ClaimValue", "UserId" },
                values: new object[,]
                {
                    { 1, "http://schemas.microsoft.com/ws/2008/06/identity/claims/role", "admin", "32131582-161e-4a1a-a696-5df7af15bda0" },
                    { 2, "http://schemas.xmlsoap.org/ws/2009/09/identity/claims/actor", "32131582-161e-4a1a-a696-5df7af15bda0", "32131582-161e-4a1a-a696-5df7af15bda0" },
                    { 3, "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress", "admin@example.com", "32131582-161e-4a1a-a696-5df7af15bda0" },
                    { 4, "http://schemas.microsoft.com/ws/2008/06/identity/claims/role", "client", "d33c3f10-ba31-4086-ab7b-048f2888e7a3" },
                    { 5, "http://schemas.xmlsoap.org/ws/2009/09/identity/claims/actor", "d33c3f10-ba31-4086-ab7b-048f2888e7a3", "d33c3f10-ba31-4086-ab7b-048f2888e7a3" },
                    { 6, "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress", "client@example.com", "d33c3f10-ba31-4086-ab7b-048f2888e7a3" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "f5e7508d-e9fa-4cf8-8dbd-e6127c35bfa5", "32131582-161e-4a1a-a696-5df7af15bda0" },
                    { "d7f83458-796b-4038-9534-470f54f564b0", "d33c3f10-ba31-4086-ab7b-048f2888e7a3" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "340fc3d0-5df4-49c3-9a11-b73ca56a5f17");

            migrationBuilder.DeleteData(
                table: "AspNetUserClaims",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "AspNetUserClaims",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "AspNetUserClaims",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "AspNetUserClaims",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "AspNetUserClaims",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "AspNetUserClaims",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "f5e7508d-e9fa-4cf8-8dbd-e6127c35bfa5", "32131582-161e-4a1a-a696-5df7af15bda0" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "d7f83458-796b-4038-9534-470f54f564b0", "d33c3f10-ba31-4086-ab7b-048f2888e7a3" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d7f83458-796b-4038-9534-470f54f564b0");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f5e7508d-e9fa-4cf8-8dbd-e6127c35bfa5");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "32131582-161e-4a1a-a696-5df7af15bda0");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d33c3f10-ba31-4086-ab7b-048f2888e7a3");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0a3af66a-2137-4c60-9d2d-663c0023733c", "80b029a1-1ac8-41c3-99ef-a5c8904111a7", "client", "CLIENT" },
                    { "bfb448ff-6598-4a31-a969-f3516c3ef5bb", "aeb352e1-2cfd-433e-8e38-563367c22288", "admin", "ADMIN" },
                    { "d349a58a-a4df-4569-97e5-7550dedbb659", "3a9c4e3f-c7b1-49d3-ba58-b02e4f1ce524", "cook", "COOK" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "Name", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "c9f11152-4dd7-4473-94c7-2653824d25c8", 0, "eb9f06df-e47b-4039-ad2f-50f061031de7", "client@example.com", true, false, null, "client", "client@example.com", "client", "AQAAAAEAACcQAAAAEMpRuLjzQRBCs1VIJaC9cmescZdLVe+VThjU0qczzRo40QxlvpypBRBJDmKag1p6SA==", null, false, "", false, "client" },
                    { "f7194159-b143-47d0-bfdf-efa10830c160", 0, "5e3f5b7e-62a9-4733-b3b8-086f8d64d155", "admin@example.com", true, false, null, "admin", "admin@example.com", "admin", "AQAAAAEAACcQAAAAEP2upSoFkC5ERk6K020nZqXnyDawhMQiIzsVuhLeQYKqM1hTGwFX7xg4etuIoPATNw==", null, false, "", false, "admin" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "0a3af66a-2137-4c60-9d2d-663c0023733c", "c9f11152-4dd7-4473-94c7-2653824d25c8" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "bfb448ff-6598-4a31-a969-f3516c3ef5bb", "f7194159-b143-47d0-bfdf-efa10830c160" });
        }
    }
}
