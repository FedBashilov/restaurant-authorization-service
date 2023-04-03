// Copyright (c) Fedor Bashilov. All rights reserved.

#nullable disable

namespace Identity.Server.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class AddNameOfUserMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FirstName",
                table: "AspNetUsers",
                newName: "Name");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "AspNetUsers",
                newName: "FirstName");
        }
    }
}
