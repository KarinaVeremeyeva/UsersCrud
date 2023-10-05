using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace UsersCrud.DAL.Migrations
{
    /// <inheritdoc />
    public partial class SeedDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("2dc2c800-6e33-470d-87e6-f7dfcfdebc09"), 0 },
                    { new Guid("513c041c-bfaa-498d-9433-d66411f24370"), 3 },
                    { new Guid("76695450-ea70-4398-891d-bf1cdfa0d7e4"), 1 },
                    { new Guid("aef1452f-f830-4c51-91d1-372209eb83d8"), 2 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("2dc2c800-6e33-470d-87e6-f7dfcfdebc09"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("513c041c-bfaa-498d-9433-d66411f24370"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("76695450-ea70-4398-891d-bf1cdfa0d7e4"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("aef1452f-f830-4c51-91d1-372209eb83d8"));
        }
    }
}
