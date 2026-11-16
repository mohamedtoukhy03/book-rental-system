using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookRentalSystem.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class RemoveIsAvaliableFromBook : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RentalRequest_AspNetUsers_ReviewedByUserId",
                table: "RentalRequest");

            migrationBuilder.DropForeignKey(
                name: "FK_RentalRequest_AspNetUsers_UserId",
                table: "RentalRequest");

            migrationBuilder.DropForeignKey(
                name: "FK_RentalRequest_Book_BookId",
                table: "RentalRequest");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RentalRequest",
                table: "RentalRequest");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Book",
                table: "Book");

            migrationBuilder.DropColumn(
                name: "IsAvailable",
                table: "Book");

            migrationBuilder.RenameTable(
                name: "RentalRequest",
                newName: "RentalRequests");

            migrationBuilder.RenameTable(
                name: "Book",
                newName: "Books");

            migrationBuilder.RenameIndex(
                name: "IX_RentalRequest_UserId",
                table: "RentalRequests",
                newName: "IX_RentalRequests_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_RentalRequest_ReviewedByUserId",
                table: "RentalRequests",
                newName: "IX_RentalRequests_ReviewedByUserId");

            migrationBuilder.RenameIndex(
                name: "IX_RentalRequest_BookId",
                table: "RentalRequests",
                newName: "IX_RentalRequests_BookId");

            migrationBuilder.RenameIndex(
                name: "IX_Book_ISBN",
                table: "Books",
                newName: "IX_Books_ISBN");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RentalRequests",
                table: "RentalRequests",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Books",
                table: "Books",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RentalRequests_AspNetUsers_ReviewedByUserId",
                table: "RentalRequests",
                column: "ReviewedByUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RentalRequests_AspNetUsers_UserId",
                table: "RentalRequests",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RentalRequests_Books_BookId",
                table: "RentalRequests",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RentalRequests_AspNetUsers_ReviewedByUserId",
                table: "RentalRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_RentalRequests_AspNetUsers_UserId",
                table: "RentalRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_RentalRequests_Books_BookId",
                table: "RentalRequests");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RentalRequests",
                table: "RentalRequests");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Books",
                table: "Books");

            migrationBuilder.RenameTable(
                name: "RentalRequests",
                newName: "RentalRequest");

            migrationBuilder.RenameTable(
                name: "Books",
                newName: "Book");

            migrationBuilder.RenameIndex(
                name: "IX_RentalRequests_UserId",
                table: "RentalRequest",
                newName: "IX_RentalRequest_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_RentalRequests_ReviewedByUserId",
                table: "RentalRequest",
                newName: "IX_RentalRequest_ReviewedByUserId");

            migrationBuilder.RenameIndex(
                name: "IX_RentalRequests_BookId",
                table: "RentalRequest",
                newName: "IX_RentalRequest_BookId");

            migrationBuilder.RenameIndex(
                name: "IX_Books_ISBN",
                table: "Book",
                newName: "IX_Book_ISBN");

            migrationBuilder.AddColumn<bool>(
                name: "IsAvailable",
                table: "Book",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_RentalRequest",
                table: "RentalRequest",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Book",
                table: "Book",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RentalRequest_AspNetUsers_ReviewedByUserId",
                table: "RentalRequest",
                column: "ReviewedByUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RentalRequest_AspNetUsers_UserId",
                table: "RentalRequest",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RentalRequest_Book_BookId",
                table: "RentalRequest",
                column: "BookId",
                principalTable: "Book",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
