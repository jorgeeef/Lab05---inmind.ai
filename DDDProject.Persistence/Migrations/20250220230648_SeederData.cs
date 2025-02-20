using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DDD.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class SeederData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("083789b3-47fc-49ac-bfd8-c5f2a918f734"));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Discriminator", "Email", "FirstName", "LastName", "Role" },
                values: new object[] { new Guid("c9c9e8b7-daa9-4209-8835-16ed2b01a47a"), "User", "admin@ums.com", "Admin", "User", 0 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("c9c9e8b7-daa9-4209-8835-16ed2b01a47a"));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Discriminator", "Email", "FirstName", "LastName", "Role" },
                values: new object[] { new Guid("083789b3-47fc-49ac-bfd8-c5f2a918f734"), "User", "admin@ums.com", "Admin", "User", 0 });
        }
    }
}
