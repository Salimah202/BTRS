using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BTRS.Migrations
{
    /// <inheritdoc />
    public partial class ABC : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "administrators",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    userName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    full_name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_administrators", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "passengers",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    username = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    email_address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    phone_number = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    gender = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_passengers", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "trip",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    destination = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    bus_number = table.Column<int>(type: "int", nullable: false),
                    start_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    end_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    administorID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_trip", x => x.ID);
                    table.ForeignKey(
                        name: "FK_trip_administrators_administorID",
                        column: x => x.administorID,
                        principalTable: "administrators",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Bus",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    captain_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    numOfSeats = table.Column<int>(type: "int", nullable: false),
                    tripID = table.Column<int>(type: "int", nullable: false),
                    administratorID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bus", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Bus_administrators_administratorID",
                        column: x => x.administratorID,
                        principalTable: "administrators",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Bus_trip_tripID",
                        column: x => x.tripID,
                        principalTable: "trip",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "passenger_Trip",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    passengerID = table.Column<int>(type: "int", nullable: false),
                    tripID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_passenger_Trip", x => x.ID);
                    table.ForeignKey(
                        name: "FK_passenger_Trip_passengers_passengerID",
                        column: x => x.passengerID,
                        principalTable: "passengers",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_passenger_Trip_trip_tripID",
                        column: x => x.tripID,
                        principalTable: "trip",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_administrators_userName",
                table: "administrators",
                column: "userName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Bus_administratorID",
                table: "Bus",
                column: "administratorID");

            migrationBuilder.CreateIndex(
                name: "IX_Bus_tripID",
                table: "Bus",
                column: "tripID");

            migrationBuilder.CreateIndex(
                name: "IX_passenger_Trip_passengerID",
                table: "passenger_Trip",
                column: "passengerID");

            migrationBuilder.CreateIndex(
                name: "IX_passenger_Trip_tripID",
                table: "passenger_Trip",
                column: "tripID");

            migrationBuilder.CreateIndex(
                name: "IX_passengers_username",
                table: "passengers",
                column: "username",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_trip_administorID",
                table: "trip",
                column: "administorID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bus");

            migrationBuilder.DropTable(
                name: "passenger_Trip");

            migrationBuilder.DropTable(
                name: "passengers");

            migrationBuilder.DropTable(
                name: "trip");

            migrationBuilder.DropTable(
                name: "administrators");
        }
    }
}
