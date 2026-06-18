using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Love4AnimalsApi.Migrations
{
    /// <inheritdoc />
    public partial class AgregarUserIdACampaign : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "UserId",
                table: "Campaigns",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_Campaigns_UserId",
                table: "Campaigns",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Campaigns_Users_UserId",
                table: "Campaigns",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Campaigns_Users_UserId",
                table: "Campaigns");

            migrationBuilder.DropIndex(
                name: "IX_Campaigns_UserId",
                table: "Campaigns");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Campaigns");
        }
    }
}
