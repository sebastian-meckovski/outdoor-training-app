using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SportAdvisorAPI.Migrations
{
    /// <inheritdoc />
    public partial class RoleConfigurationsSeeded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "31a31832-e429-465f-9370-8d442cdfbd42", null, "Administrator", "ADMINISTRATOR" },
                    { "a6d5038c-390c-4228-8cb1-1793c322ba3b", null, "User", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "31a31832-e429-465f-9370-8d442cdfbd42");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a6d5038c-390c-4228-8cb1-1793c322ba3b");
        }
    }
}
