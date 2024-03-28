using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SportAdvisorAPI.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedUserForEmailConfirmLogic : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "31a31832-e429-465f-9370-8d442cdfbd42");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a6d5038c-390c-4228-8cb1-1793c322ba3b");

            migrationBuilder.AddColumn<Guid>(
                name: "ResetToken",
                table: "AspNetUsers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ResetTokenExpiry",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "VerifyToken",
                table: "AspNetUsers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "5f9868d5-425e-4449-b4cf-6f977156bcdd", null, "User", "USER" },
                    { "996d213a-e265-4445-a5f8-98bf5d4f64c0", null, "Administrator", "ADMINISTRATOR" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5f9868d5-425e-4449-b4cf-6f977156bcdd");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "996d213a-e265-4445-a5f8-98bf5d4f64c0");

            migrationBuilder.DropColumn(
                name: "ResetToken",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ResetTokenExpiry",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "VerifyToken",
                table: "AspNetUsers");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "31a31832-e429-465f-9370-8d442cdfbd42", null, "Administrator", "ADMINISTRATOR" },
                    { "a6d5038c-390c-4228-8cb1-1793c322ba3b", null, "User", "USER" }
                });
        }
    }
}
