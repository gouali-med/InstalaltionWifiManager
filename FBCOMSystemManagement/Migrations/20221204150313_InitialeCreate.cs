using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FBCOMSystemManagement.Migrations
{
    /// <inheritdoc />
    public partial class InitialeCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CPES",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SNAntenne = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SNRouteur = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IMEI = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateAjout = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Installed = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CPES", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Telephones",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SN = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Installed = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateAjout = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Telephones", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Installations",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClientAdress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContactName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContactPhone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ville = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TypeCient = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ICCID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NGSM = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompteSIP = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdConnexion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Offre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Distributeur = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GPSLatitude = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GPSLongitude = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DATEIntervention = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Etage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Emplacement = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Immeublefibre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TelephoneBluetooth = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IMSI = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Speed1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Speed2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Speed3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    eNodeBID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CELLband = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RSRP = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SINR = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BandAuto = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PRBBH = table.Column<string>(name: "PRB_BH", type: "nvarchar(max)", nullable: true),
                    PRBNBH = table.Column<string>(name: "PRB_NBH", type: "nvarchar(max)", nullable: true),
                    StatueScreen = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    SNRScreen = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    SpeedScreen = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    LTEScreen = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    VoipScreen = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    PVPicture = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    CarteCINRecto = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    CarteCINVerso = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    PictureInside = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    PictureOutside = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    PictureOutside2 = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    SNRNegative = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Installed = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CPEID = table.Column<int>(type: "int", nullable: true),
                    TelephoneID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Installations", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Installations_CPES_CPEID",
                        column: x => x.CPEID,
                        principalTable: "CPES",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Installations_Telephones_TelephoneID",
                        column: x => x.TelephoneID,
                        principalTable: "Telephones",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Installations_CPEID",
                table: "Installations",
                column: "CPEID");

            migrationBuilder.CreateIndex(
                name: "IX_Installations_TelephoneID",
                table: "Installations",
                column: "TelephoneID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Installations");

            migrationBuilder.DropTable(
                name: "CPES");

            migrationBuilder.DropTable(
                name: "Telephones");
        }
    }
}
