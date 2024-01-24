// Copyright (c) Fedor Bashilov. All rights reserved.

#nullable disable

namespace Identity.Server.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class AddUserImageMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "340fc3d0-5df4-49c3-9a11-b73ca56a5f17");

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

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "AspNetUsers",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "58a94650-3ba0-441e-8999-7b0b85905ade", "89a43de4-b586-4708-bef2-eeca8b5a7176", "cook", "COOK" },
                    { "6f6d7207-e87a-4f6b-a7f2-f5a0b6509116", "f601de6e-4ec6-4818-8822-2535a05c63a2", "admin", "ADMIN" },
                    { "8f6414b2-0243-4f8e-abae-228b95d9afa9", "a9ee8441-12c6-4bb7-a98e-687422e4ad14", "client", "CLIENT" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "ImageUrl", "LockoutEnabled", "LockoutEnd", "Name", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "12ba564a-1d19-454c-ab47-3578648a3e59", 0, "42c31add-15ec-4ec6-92dc-c7bc19d7823c", "admin@example.com", true, null, false, null, "admin", "admin@example.com", "admin", "AQAAAAEAACcQAAAAEHqzDQFaf0HPRiclZuqc/0cCZuq6AOnaFzCU4PQ8T9iV4msdO6N9rsnT6VFIdZAtIw==", null, false, "", false, "admin" },
                    { "d41d974f-448a-4ae9-84d4-eb512fe91bf8", 0, "daab6709-60c5-4e8a-a648-97cb95cd3d23", "client@example.com", true, null, false, null, "client", "client@example.com", "client", "AQAAAAEAACcQAAAAEMi72NxDfhUbKW299e23gwQbRWHi0ODHvlC8Of3T9ZiU3TieGFe9b2CTFKWcAmbHcQ==", null, false, "", false, "client" }
                });

            migrationBuilder.UpdateData(
                table: "AspNetUserClaims",
                keyColumn: "Id",
                keyValue: 1,
                column: "UserId",
                value: "12ba564a-1d19-454c-ab47-3578648a3e59");

            migrationBuilder.UpdateData(
                table: "AspNetUserClaims",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ClaimValue", "UserId" },
                values: new object[] { "12ba564a-1d19-454c-ab47-3578648a3e59", "12ba564a-1d19-454c-ab47-3578648a3e59" });

            migrationBuilder.UpdateData(
                table: "AspNetUserClaims",
                keyColumn: "Id",
                keyValue: 3,
                column: "UserId",
                value: "12ba564a-1d19-454c-ab47-3578648a3e59");

            migrationBuilder.UpdateData(
                table: "AspNetUserClaims",
                keyColumn: "Id",
                keyValue: 4,
                column: "UserId",
                value: "d41d974f-448a-4ae9-84d4-eb512fe91bf8");

            migrationBuilder.UpdateData(
                table: "AspNetUserClaims",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "ClaimValue", "UserId" },
                values: new object[] { "d41d974f-448a-4ae9-84d4-eb512fe91bf8", "d41d974f-448a-4ae9-84d4-eb512fe91bf8" });

            migrationBuilder.UpdateData(
                table: "AspNetUserClaims",
                keyColumn: "Id",
                keyValue: 6,
                column: "UserId",
                value: "d41d974f-448a-4ae9-84d4-eb512fe91bf8");

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "6f6d7207-e87a-4f6b-a7f2-f5a0b6509116", "12ba564a-1d19-454c-ab47-3578648a3e59" },
                    { "8f6414b2-0243-4f8e-abae-228b95d9afa9", "d41d974f-448a-4ae9-84d4-eb512fe91bf8" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "58a94650-3ba0-441e-8999-7b0b85905ade");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "6f6d7207-e87a-4f6b-a7f2-f5a0b6509116", "12ba564a-1d19-454c-ab47-3578648a3e59" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "8f6414b2-0243-4f8e-abae-228b95d9afa9", "d41d974f-448a-4ae9-84d4-eb512fe91bf8" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6f6d7207-e87a-4f6b-a7f2-f5a0b6509116");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8f6414b2-0243-4f8e-abae-228b95d9afa9");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "12ba564a-1d19-454c-ab47-3578648a3e59");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d41d974f-448a-4ae9-84d4-eb512fe91bf8");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "AspNetUsers");

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

            migrationBuilder.UpdateData(
                table: "AspNetUserClaims",
                keyColumn: "Id",
                keyValue: 1,
                column: "UserId",
                value: "32131582-161e-4a1a-a696-5df7af15bda0");

            migrationBuilder.UpdateData(
                table: "AspNetUserClaims",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ClaimValue", "UserId" },
                values: new object[] { "32131582-161e-4a1a-a696-5df7af15bda0", "32131582-161e-4a1a-a696-5df7af15bda0" });

            migrationBuilder.UpdateData(
                table: "AspNetUserClaims",
                keyColumn: "Id",
                keyValue: 3,
                column: "UserId",
                value: "32131582-161e-4a1a-a696-5df7af15bda0");

            migrationBuilder.UpdateData(
                table: "AspNetUserClaims",
                keyColumn: "Id",
                keyValue: 4,
                column: "UserId",
                value: "d33c3f10-ba31-4086-ab7b-048f2888e7a3");

            migrationBuilder.UpdateData(
                table: "AspNetUserClaims",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "ClaimValue", "UserId" },
                values: new object[] { "d33c3f10-ba31-4086-ab7b-048f2888e7a3", "d33c3f10-ba31-4086-ab7b-048f2888e7a3" });

            migrationBuilder.UpdateData(
                table: "AspNetUserClaims",
                keyColumn: "Id",
                keyValue: 6,
                column: "UserId",
                value: "d33c3f10-ba31-4086-ab7b-048f2888e7a3");

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "f5e7508d-e9fa-4cf8-8dbd-e6127c35bfa5", "32131582-161e-4a1a-a696-5df7af15bda0" },
                    { "d7f83458-796b-4038-9534-470f54f564b0", "d33c3f10-ba31-4086-ab7b-048f2888e7a3" }
                });
        }
    }
}
