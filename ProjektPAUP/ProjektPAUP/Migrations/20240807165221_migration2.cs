using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjektPAUP.Migrations
{
    /// <inheritdoc />
    public partial class migration2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Skladista_Proizvodi_ProizvodId",
                table: "Skladista");

            migrationBuilder.DropIndex(
                name: "IX_Skladista_ProizvodId",
                table: "Skladista");

            migrationBuilder.DropColumn(
                name: "ProizvodId",
                table: "Skladista");

            migrationBuilder.AddColumn<string>(
                name: "Ime",
                table: "Skladista",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "SkladistaProizvodi",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    SkladisteId = table.Column<int>(type: "int", nullable: false),
                    ProizvodId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SkladistaProizvodi", x => x.ID);
                    table.ForeignKey(
                        name: "FK_SkladistaProizvodi_Proizvodi_ProizvodId",
                        column: x => x.ProizvodId,
                        principalTable: "Proizvodi",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SkladistaProizvodi_Skladista_SkladisteId",
                        column: x => x.SkladisteId,
                        principalTable: "Skladista",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_SkladistaProizvodi_ProizvodId",
                table: "SkladistaProizvodi",
                column: "ProizvodId");

            migrationBuilder.CreateIndex(
                name: "IX_SkladistaProizvodi_SkladisteId",
                table: "SkladistaProizvodi",
                column: "SkladisteId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SkladistaProizvodi");

            migrationBuilder.DropColumn(
                name: "Ime",
                table: "Skladista");

            migrationBuilder.AddColumn<int>(
                name: "ProizvodId",
                table: "Skladista",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Skladista_ProizvodId",
                table: "Skladista",
                column: "ProizvodId");

            migrationBuilder.AddForeignKey(
                name: "FK_Skladista_Proizvodi_ProizvodId",
                table: "Skladista",
                column: "ProizvodId",
                principalTable: "Proizvodi",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
