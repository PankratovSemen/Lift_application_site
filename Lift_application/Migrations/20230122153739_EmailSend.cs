using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Liftapplication.Migrations
{
    /// <inheritdoc />
    public partial class EmailSend : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EmailForSend",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailForSend", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ArticlesSender",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    emailId = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Subject = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TextMessage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StatusSend = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArticlesSender", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ArticlesSender_EmailForSend_emailId",
                        column: x => x.emailId,
                        principalTable: "EmailForSend",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ArticlesSender_emailId",
                table: "ArticlesSender",
                column: "emailId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ArticlesSender");

            migrationBuilder.DropTable(
                name: "EmailForSend");
        }
    }
}
