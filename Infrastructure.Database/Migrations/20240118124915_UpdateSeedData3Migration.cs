using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Identity.Server.Migrations
{
    public partial class UpdateSeedData3Migration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d1acda05-a686-4e45-8ed4-db20c9df97d1");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "eb00e157-ed14-46d2-8faa-c49f09c60116");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "f467dd6f-a46c-4b3f-85de-fb421441c78c", "29deea11-a969-4514-8b48-f5502338932b" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "d6f71d15-ef1c-4411-8dcc-72f7802da16b", "ace94091-9804-46e4-9d7f-030e5eb309bc" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f467dd6f-a46c-4b3f-85de-fb421441c78c");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "29deea11-a969-4514-8b48-f5502338932b");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "ace94091-9804-46e4-9d7f-030e5eb309bc");

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

        protected override void Down(MigrationBuilder migrationBuilder)
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
                    { "d1acda05-a686-4e45-8ed4-db20c9df97d1", "4941da6c-1f52-4a70-afc7-3ca3ffc3bf95", "cook", "COOK" },
                    { "eb00e157-ed14-46d2-8faa-c49f09c60116", "0cf17e92-e6cb-47ef-9b5f-b2d4c8eedc38", "client", "CLIENT" },
                    { "f467dd6f-a46c-4b3f-85de-fb421441c78c", "1b91f4e7-4251-4f65-bcd3-7c30ad9f59b1", "admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "Name", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "29deea11-a969-4514-8b48-f5502338932b", 0, "8370ff07-40a6-4277-b9de-388eccf3d8c6", "admin@example.com", true, false, null, "admin", "admin@example.com", "admin", "AQAAAAEAACcQAAAAEGqpzLSv6zsXo2EPSLFQycIPq/vFd9K53L1QvznSq4LbEJph3nksaiHWErxPrPOhVw==", null, false, "", false, "admin" },
                    { "ace94091-9804-46e4-9d7f-030e5eb309bc", 0, "b902496f-5651-47fa-872d-300735068ea9", "client@example.com", true, false, null, "client", "client@example.com", "client", "AQAAAAEAACcQAAAAEPevlJ0SGnJUFwDAFcr4ZpMF9iB6p1i04odJoEJYi1QXIPwv4pOpCPG+KjJ4oK+PAw==", null, false, "", false, "client" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "f467dd6f-a46c-4b3f-85de-fb421441c78c", "29deea11-a969-4514-8b48-f5502338932b" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "d6f71d15-ef1c-4411-8dcc-72f7802da16b", "ace94091-9804-46e4-9d7f-030e5eb309bc" });
        }
    }
}
