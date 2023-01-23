using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartBusinessApplication.Persistence.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OutSideCurrentTemperature = table.Column<double>(type: "float", nullable: false),
                    InSideCurrentTemperature = table.Column<double>(type: "float", nullable: false),
                    State = table.Column<bool>(type: "bit", nullable: false),
                    TemperatureLimit = table.Column<double>(type: "float", nullable: false),
                    Role1 = table.Column<bool>(type: "bit", nullable: false),
                    Role2 = table.Column<bool>(type: "bit", nullable: false),
                    ClientUserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ClientPasswordEncrypt = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AutoSystemEnabled = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Clients",
                columns: new[] { "Id", "AutoSystemEnabled", "ClientPasswordEncrypt", "ClientUserName", "CreatedDate", "InSideCurrentTemperature", "Name", "OutSideCurrentTemperature", "Role1", "Role2", "State", "TemperatureLimit", "UpdatedDate" },
                values: new object[] { 1, true, "Deneme", "Deneme", new DateTime(2023, 1, 9, 17, 8, 1, 476, DateTimeKind.Local).AddTicks(6632), 20.5, "Deneme", 45.100000000000001, false, false, false, 30.100000000000001, new DateTime(2023, 1, 9, 17, 8, 1, 476, DateTimeKind.Local).AddTicks(6646) });

            migrationBuilder.InsertData(
                table: "Clients",
                columns: new[] { "Id", "AutoSystemEnabled", "ClientPasswordEncrypt", "ClientUserName", "CreatedDate", "InSideCurrentTemperature", "Name", "OutSideCurrentTemperature", "Role1", "Role2", "State", "TemperatureLimit", "UpdatedDate" },
                values: new object[] { 2, true, "Deneme2", "Deneme2", new DateTime(2023, 1, 9, 17, 8, 1, 476, DateTimeKind.Local).AddTicks(6686), 37.5, "Deneme2", 10.4, false, false, false, 30.100000000000001, new DateTime(2023, 1, 9, 17, 8, 1, 476, DateTimeKind.Local).AddTicks(6687) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Clients");
        }
    }
}
