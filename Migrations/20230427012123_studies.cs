using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace flashcards.Migrations
{
    public partial class studies : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "OppositeExpression",
                table: "FLASHCARDS",
                type: "TEXT",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 50);

            migrationBuilder.CreateTable(
                name: "STUDIES",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    StackId = table.Column<int>(type: "INTEGER", nullable: false),
                    FlashCardId = table.Column<int>(type: "INTEGER", nullable: false),
                    date = table.Column<DateTime>(type: "TEXT", nullable: false),
                    HaveSucess = table.Column<bool>(type: "INTEGER", nullable: false),
                    Response = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_STUDIES", x => x.Id);
                    table.ForeignKey(
                        name: "FK_STUDIES_FLASHCARDS_FlashCardId",
                        column: x => x.FlashCardId,
                        principalTable: "FLASHCARDS",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_STUDIES_STACKS_StackId",
                        column: x => x.StackId,
                        principalTable: "STACKS",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_STUDIES_FlashCardId",
                table: "STUDIES",
                column: "FlashCardId");

            migrationBuilder.CreateIndex(
                name: "IX_STUDIES_StackId",
                table: "STUDIES",
                column: "StackId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "STUDIES");

            migrationBuilder.AlterColumn<string>(
                name: "OppositeExpression",
                table: "FLASHCARDS",
                type: "TEXT",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 50,
                oldNullable: true);
        }
    }
}
