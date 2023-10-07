using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace UsersCrud.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddUsersData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Age", "Email", "Name" },
                values: new object[,]
                {
                    { new Guid("3d52299a-3ccc-4e11-95a3-c5fd845977ae"), 35, "super.admin@email.com", "SuperAdmin" },
                    { new Guid("4cdbb269-bc5a-4d68-85d9-381d8b667df6"), 20, "user1@email.com", "User 1" },
                    { new Guid("86c1e29a-b899-449d-ac08-7cd798850cd2"), 30, "admin@email.com", "Admin" },
                    { new Guid("d9ebeb68-e7d5-4430-8866-a23660c1a4e2"), 36, "user2@email.com", "User 2" },
                    { new Guid("ebe53741-14e6-4cf3-9797-6e3dae227d49"), 33, "support@email.com", "Suport" }
                });

            migrationBuilder.InsertData(
                table: "RoleUser",
                columns: new[] { "RolesId", "UsersId" },
                values: new object[,]
                {
                    { new Guid("2dc2c800-6e33-470d-87e6-f7dfcfdebc09"), new Guid("4cdbb269-bc5a-4d68-85d9-381d8b667df6") },
                    { new Guid("2dc2c800-6e33-470d-87e6-f7dfcfdebc09"), new Guid("d9ebeb68-e7d5-4430-8866-a23660c1a4e2") },
                    { new Guid("513c041c-bfaa-498d-9433-d66411f24370"), new Guid("3d52299a-3ccc-4e11-95a3-c5fd845977ae") },
                    { new Guid("76695450-ea70-4398-891d-bf1cdfa0d7e4"), new Guid("86c1e29a-b899-449d-ac08-7cd798850cd2") },
                    { new Guid("aef1452f-f830-4c51-91d1-372209eb83d8"), new Guid("3d52299a-3ccc-4e11-95a3-c5fd845977ae") },
                    { new Guid("aef1452f-f830-4c51-91d1-372209eb83d8"), new Guid("ebe53741-14e6-4cf3-9797-6e3dae227d49") }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "RoleUser",
                keyColumns: new[] { "RolesId", "UsersId" },
                keyValues: new object[] { new Guid("2dc2c800-6e33-470d-87e6-f7dfcfdebc09"), new Guid("4cdbb269-bc5a-4d68-85d9-381d8b667df6") });

            migrationBuilder.DeleteData(
                table: "RoleUser",
                keyColumns: new[] { "RolesId", "UsersId" },
                keyValues: new object[] { new Guid("2dc2c800-6e33-470d-87e6-f7dfcfdebc09"), new Guid("d9ebeb68-e7d5-4430-8866-a23660c1a4e2") });

            migrationBuilder.DeleteData(
                table: "RoleUser",
                keyColumns: new[] { "RolesId", "UsersId" },
                keyValues: new object[] { new Guid("513c041c-bfaa-498d-9433-d66411f24370"), new Guid("3d52299a-3ccc-4e11-95a3-c5fd845977ae") });

            migrationBuilder.DeleteData(
                table: "RoleUser",
                keyColumns: new[] { "RolesId", "UsersId" },
                keyValues: new object[] { new Guid("76695450-ea70-4398-891d-bf1cdfa0d7e4"), new Guid("86c1e29a-b899-449d-ac08-7cd798850cd2") });

            migrationBuilder.DeleteData(
                table: "RoleUser",
                keyColumns: new[] { "RolesId", "UsersId" },
                keyValues: new object[] { new Guid("aef1452f-f830-4c51-91d1-372209eb83d8"), new Guid("3d52299a-3ccc-4e11-95a3-c5fd845977ae") });

            migrationBuilder.DeleteData(
                table: "RoleUser",
                keyColumns: new[] { "RolesId", "UsersId" },
                keyValues: new object[] { new Guid("aef1452f-f830-4c51-91d1-372209eb83d8"), new Guid("ebe53741-14e6-4cf3-9797-6e3dae227d49") });

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("3d52299a-3ccc-4e11-95a3-c5fd845977ae"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("4cdbb269-bc5a-4d68-85d9-381d8b667df6"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("86c1e29a-b899-449d-ac08-7cd798850cd2"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("d9ebeb68-e7d5-4430-8866-a23660c1a4e2"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("ebe53741-14e6-4cf3-9797-6e3dae227d49"));
        }
    }
}
