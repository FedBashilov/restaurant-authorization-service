using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Identity.Server.Migrations
{
    public partial class UpdateSeedData2Migration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

        protected override void Down(MigrationBuilder migrationBuilder)
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
    }
}
