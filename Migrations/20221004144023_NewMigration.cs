using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace flashcards.Migrations
{
    public partial class NewMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "STACKS",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_STACKS", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FLASHCARDS",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Expression = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    OppositeExpression = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    StackId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FLASHCARDS", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FLASHCARDS_STACKS_StackId",
                        column: x => x.StackId,
                        principalTable: "STACKS",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FLASHCARDS_Expression",
                table: "FLASHCARDS",
                column: "Expression");

            migrationBuilder.CreateIndex(
                name: "IX_FLASHCARDS_StackId",
                table: "FLASHCARDS",
                column: "StackId");

            migrationBuilder.CreateIndex(
                name: "IX_STACKS_Name",
                table: "STACKS",
                column: "Name");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FLASHCARDS");

            migrationBuilder.DropTable(
                name: "STACKS");
        }
    }
}
