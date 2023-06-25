using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Apartament",
                columns: table => new
                {
                    IDApartament = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NumarApartament = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Apartame__E10A290A1020E9EA", x => x.IDApartament);
                });

            migrationBuilder.CreateTable(
                name: "Bloc",
                columns: table => new
                {
                    IDBloc = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NumarBloc = table.Column<string>(unicode: false, maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Bloc__3CFB8BB234155D77", x => x.IDBloc);
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
                    cnp = table.Column<string>(unicode: false, maxLength: 13, nullable: true),
                    Token = table.Column<string>(unicode: false, maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Utilizator", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Apometre",
                columns: table => new
                {
                    IdApometre = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Iduser = table.Column<int>(nullable: false),
                    ap1 = table.Column<string>(unicode: false, maxLength: 10, nullable: true),
                    ap2 = table.Column<string>(unicode: false, maxLength: 10, nullable: true),
                    ap3 = table.Column<string>(unicode: false, maxLength: 10, nullable: true),
                    ap4 = table.Column<string>(unicode: false, maxLength: 10, nullable: true),
                    TransmitereData = table.Column<DateTime>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Apometre__12F610C948212894", x => x.IdApometre);
                    table.ForeignKey(
                        name: "FK__Apometre__Iduser__01142BA1",
                        column: x => x.Iduser,
                        principalTable: "Utilizator",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Factura",
                columns: table => new
                {
                    IdFactura = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Id_utilizator = table.Column<int>(nullable: false),
                    TotalApa = table.Column<decimal>(type: "decimal(20, 0)", nullable: true),
                    TotalRetim = table.Column<decimal>(type: "decimal(20, 0)", nullable: true),
                    TotalAdmin = table.Column<decimal>(type: "decimal(20, 0)", nullable: true),
                    TotalCuratenie = table.Column<decimal>(type: "decimal(20, 0)", nullable: true),
                    ApaUsage = table.Column<decimal>(type: "decimal(20, 0)", nullable: true),
                    RetimUsage = table.Column<decimal>(type: "decimal(20, 0)", nullable: true),
                    AdminUsage = table.Column<decimal>(type: "decimal(20, 0)", nullable: true),
                    CuratenieUsage = table.Column<decimal>(type: "decimal(20, 0)", nullable: true),
                    TransmitereData = table.Column<DateTime>(type: "date", nullable: true),
                    IsPaid = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Factura__50E7BAF157512CD6", x => x.IdFactura);
                    table.ForeignKey(
                        name: "FK__Factura__Id_util__1EA48E88",
                        column: x => x.Id_utilizator,
                        principalTable: "Utilizator",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Locatii",
                columns: table => new
                {
                    IdLocatii = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdUser = table.Column<int>(nullable: false),
                    judet = table.Column<string>(unicode: false, maxLength: 30, nullable: true),
                    oras = table.Column<string>(unicode: false, maxLength: 30, nullable: true),
                    strada = table.Column<string>(unicode: false, maxLength: 60, nullable: true),
                    bloc = table.Column<string>(unicode: false, maxLength: 10, nullable: true),
                    apartament = table.Column<int>(nullable: true),
                    nrlocatari = table.Column<int>(nullable: true),
                    nrapometre = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Locatii__9A1EDBC7C8E60F77", x => x.IdLocatii);
                    table.ForeignKey(
                        name: "FK__Locatii__IdUser__787EE5A0",
                        column: x => x.IdUser,
                        principalTable: "Utilizator",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Tarife",
                columns: table => new
                {
                    IdTarife = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Id = table.Column<int>(nullable: false),
                    pret_retim = table.Column<string>(unicode: false, maxLength: 10, nullable: true),
                    pret_curatenie = table.Column<string>(unicode: false, maxLength: 10, nullable: true),
                    pret_admin = table.Column<string>(unicode: false, maxLength: 10, nullable: true),
                    pret_apa = table.Column<string>(unicode: false, maxLength: 10, nullable: true),
                    TransmitereData = table.Column<DateTime>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Tarife__78F1A91178F6604D", x => x.IdTarife);
                    table.ForeignKey(
                        name: "FK__Tarife__Id__160F4887",
                        column: x => x.Id,
                        principalTable: "Utilizator",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Apometre_Iduser",
                table: "Apometre",
                column: "Iduser");

            migrationBuilder.CreateIndex(
                name: "IX_Factura_Id_utilizator",
                table: "Factura",
                column: "Id_utilizator");

            migrationBuilder.CreateIndex(
                name: "IX_Locatii_IdUser",
                table: "Locatii",
                column: "IdUser");

            migrationBuilder.CreateIndex(
                name: "IX_Tarife_Id",
                table: "Tarife",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Apartament");

            migrationBuilder.DropTable(
                name: "Apometre");

            migrationBuilder.DropTable(
                name: "Bloc");

            migrationBuilder.DropTable(
                name: "Factura");

            migrationBuilder.DropTable(
                name: "Judet");

            migrationBuilder.DropTable(
                name: "Locatii");

            migrationBuilder.DropTable(
                name: "Oras");

            migrationBuilder.DropTable(
                name: "Strada");

            migrationBuilder.DropTable(
                name: "Tarife");

            migrationBuilder.DropTable(
                name: "Utilizator");
        }
    }
}
