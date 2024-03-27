using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication3.Data.Migrations
{
    public partial class dkm : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ReviewViewModelID",
                table: "ReviewModel",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ReviewViewModel",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NewReviewId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReviewViewModel", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ReviewViewModel_ReviewModel_NewReviewId",
                        column: x => x.NewReviewId,
                        principalTable: "ReviewModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ReviewModel_ReviewViewModelID",
                table: "ReviewModel",
                column: "ReviewViewModelID");

            migrationBuilder.CreateIndex(
                name: "IX_ReviewViewModel_NewReviewId",
                table: "ReviewViewModel",
                column: "NewReviewId");

            migrationBuilder.AddForeignKey(
                name: "FK_ReviewModel_ReviewViewModel_ReviewViewModelID",
                table: "ReviewModel",
                column: "ReviewViewModelID",
                principalTable: "ReviewViewModel",
                principalColumn: "ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReviewModel_ReviewViewModel_ReviewViewModelID",
                table: "ReviewModel");

            migrationBuilder.DropTable(
                name: "ReviewViewModel");

            migrationBuilder.DropIndex(
                name: "IX_ReviewModel_ReviewViewModelID",
                table: "ReviewModel");

            migrationBuilder.DropColumn(
                name: "ReviewViewModelID",
                table: "ReviewModel");
        }
    }
}
