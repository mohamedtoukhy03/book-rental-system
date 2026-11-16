using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookRentalSystem.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddAvailableCopiesConcurrencyToken : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_RentalRequests_UserId",
                table: "RentalRequests");

            migrationBuilder.CreateIndex(
                name: "IX_RentalRequests_UserId_BookId_Status",
                table: "RentalRequests",
                columns: new[] { "UserId", "BookId", "Status" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_RentalRequests_UserId_BookId_Status",
                table: "RentalRequests");

            migrationBuilder.CreateIndex(
                name: "IX_RentalRequests_UserId",
                table: "RentalRequests",
                column: "UserId");
        }
    }
}
