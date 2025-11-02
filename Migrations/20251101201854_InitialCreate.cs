using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Airbb.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Location",
                columns: table => new
                {
                    LocationId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Location", x => x.LocationId);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    PhoneNo = table.Column<string>(type: "TEXT", nullable: false),
                    EmailAddress = table.Column<string>(type: "TEXT", nullable: false),
                    DOB = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Residence",
                columns: table => new
                {
                    ResidenceId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    ResidencePicture = table.Column<string>(type: "TEXT", nullable: false),
                    GuestNumber = table.Column<int>(type: "INTEGER", nullable: false),
                    BedroomNumber = table.Column<int>(type: "INTEGER", nullable: false),
                    BathroomNumber = table.Column<int>(type: "INTEGER", nullable: false),
                    PricePerNight = table.Column<string>(type: "TEXT", nullable: false),
                    LocationId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Residence", x => x.ResidenceId);
                    table.ForeignKey(
                        name: "FK_Residence_Location_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Location",
                        principalColumn: "LocationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reservation",
                columns: table => new
                {
                    ReservationId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ReservationStartDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ReservationEndDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ResidenceId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservation", x => x.ReservationId);
                    table.ForeignKey(
                        name: "FK_Reservation_Residence_ResidenceId",
                        column: x => x.ResidenceId,
                        principalTable: "Residence",
                        principalColumn: "ResidenceId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Location",
                columns: new[] { "LocationId", "Name" },
                values: new object[,]
                {
                    { 1, "Seattle" },
                    { 2, "Denver" },
                    { 3, "Houston" },
                    { 4, "Orlando" }
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "UserId", "DOB", "EmailAddress", "Name", "PhoneNo" },
                values: new object[,]
                {
                    { 1, "02/19/1997", "lucas.bennett@gmail.com", "Lucas Bennett", "955-707-8080" },
                    { 2, "06/23/2000", "isabella.perez@gmail.com", "Isabella Perez", "201-909-1010" },
                    { 3, "10/14/1999", "ethan.clark@gmail.com", "Ethan Clark", "614-111-2121" },
                    { 4, "12/12/1999", "jim.greevy@gmail.com", "Jim Greevy", "216-090-6767" }
                });

            migrationBuilder.InsertData(
                table: "Residence",
                columns: new[] { "ResidenceId", "BathroomNumber", "BedroomNumber", "GuestNumber", "LocationId", "Name", "PricePerNight", "ResidencePicture" },
                values: new object[,]
                {
                    { 1, 1, 1, 2, 1, "Seattle Skyline Silhouette", "130", "SeattleSkylineSilhouette.png" },
                    { 2, 2, 3, 6, 2, "Denver Mountain Cabin", "150", "DenverMountainCabin.png" },
                    { 3, 2, 2, 4, 3, "Houston Downtown Loft", "95", "HoustonDowntownLoft.png" },
                    { 4, 3, 4, 8, 4, "Orlando Family Villa", "170", "OrlandoFamilyVilla.png" }
                });

            migrationBuilder.InsertData(
                table: "Reservation",
                columns: new[] { "ReservationId", "ReservationEndDate", "ReservationStartDate", "ResidenceId" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 12, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 12, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 2, new DateTime(2025, 12, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 12, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 2 },
                    { 3, new DateTime(2025, 12, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 12, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 3 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Reservation_ResidenceId",
                table: "Reservation",
                column: "ResidenceId");

            migrationBuilder.CreateIndex(
                name: "IX_Residence_LocationId",
                table: "Residence",
                column: "LocationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Reservation");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Residence");

            migrationBuilder.DropTable(
                name: "Location");
        }
    }
}
