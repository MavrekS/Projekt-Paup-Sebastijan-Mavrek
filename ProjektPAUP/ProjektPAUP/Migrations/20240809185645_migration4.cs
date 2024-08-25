using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjektPAUP.Migrations
{
    /// <inheritdoc />
    public partial class migration4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RacuniProizvodi");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "SkladistaProizvodi",
                newName: "Id");

            migrationBuilder.AddColumn<int>(
                name: "ProizvodId",
                table: "Racuni",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "StanjeNaRacunu",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProizvodId",
                table: "Racuni");

            migrationBuilder.DropColumn(
                name: "StanjeNaRacunu",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "SkladistaProizvodi",
                newName: "ID");

            migrationBuilder.CreateTable(
                name: "RacuniProizvodi",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ProizvodId = table.Column<int>(type: "int", nullable: false),
                    RacunId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RacuniProizvodi", x => x.ID);
                    table.ForeignKey(
                        name: "FK_RacuniProizvodi_Proizvodi_ProizvodId",
                        column: x => x.ProizvodId,
                        principalTable: "Proizvodi",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RacuniProizvodi_Racuni_RacunId",
                        column: x => x.RacunId,
                        principalTable: "Racuni",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_RacuniProizvodi_ProizvodId",
                table: "RacuniProizvodi",
                column: "ProizvodId");

            migrationBuilder.CreateIndex(
                name: "IX_RacuniProizvodi_RacunId",
                table: "RacuniProizvodi",
                column: "RacunId");
        }
    }
}
