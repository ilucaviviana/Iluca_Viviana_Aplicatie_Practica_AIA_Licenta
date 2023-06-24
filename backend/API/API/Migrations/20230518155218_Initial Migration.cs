using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Bloc",
                columns: table => new
                {
                    IDBloc = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NumarBloc = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Bloc__3CFB8BB245FBCA1E", x => x.IDBloc);
                });

            migrationBuilder.CreateTable(
                name: "Judet",
                columns: table => new
                {
                    IDJudet = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NumeJudet = table.Column<string>(unicode: false, maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Judet__D2AAE0672BBB57F3", x => x.IDJudet);
                });

            migrationBuilder.CreateTable(
                name: "Oras",
                columns: table => new
                {
                    IDOras = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NumeOras = table.Column<string>(unicode: false, maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Oras__BA6D4AB46994EE18", x => x.IDOras);
                });

            migrationBuilder.CreateTable(
                name: "Strada",
                columns: table => new
                {
                    IDStrada = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NumeStrada = table.Column<string>(unicode: false, maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Strada__A66FDE6924C83BA8", x => x.IDStrada);
                });

            migrationBuilder.CreateTable(
                name: "Utilizator",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    tip = table.Column<int>(nullable: true),
                    email = table.Column<string>(unicode: false, maxLength: 30, nullable: true),
                    parola = table.Column<string>(unicode: false, maxLength: 14, nullable: true),
                    nume = table.Column<string>(unicode: false, maxLength: 30, nullable: true),
                    prenume = table.Column<string>(unicode: false, maxLength: 30, nullable: true),
                    telefon = table.Column<string>(unicode: false, maxLength: 10, nullable: true),
                    cnp = table.Column<string>(unicode: false, maxLength: 13, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Utilizator", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Locatii",
                columns: table => new
                {
                    IDLocatii = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IDUser = table.Column<int>(nullable: false),
                    Judet = table.Column<string>(unicode: false, maxLength: 30, nullable: true),
                    Oras = table.Column<string>(unicode: false, maxLength: 30, nullable: true),
                    Strada = table.Column<string>(unicode: false, maxLength: 60, nullable: true),
                    Bloc = table.Column<string>(unicode: false, maxLength: 10, nullable: true),
                    NrApometre = table.Column<int>(nullable: true),
                    NrLocatari = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Locatii__6EB72DA768FBB044", x => x.IDLocatii);
                    table.ForeignKey(
                        name: "FK__Locatii__IDUser__398D8EEE",
                        column: x => x.IDUser,
                        principalTable: "Utilizator",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Locatii_IDUser",
                table: "Locatii",
                column: "IDUser");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bloc");

            migrationBuilder.DropTable(
                name: "Judet");

            migrationBuilder.DropTable(
                name: "Locatii");

            migrationBuilder.DropTable(
                name: "Oras");

            migrationBuilder.DropTable(
                name: "Strada");

            migrationBuilder.DropTable(
                name: "Utilizator");
        }
    }
}
