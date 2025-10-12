using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HOVCustomers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    LicensePlate = table.Column<string>(type: "TEXT", nullable: false),
                    FirstName = table.Column<string>(type: "TEXT", nullable: false),
                    LastName = table.Column<string>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    Phone = table.Column<string>(type: "TEXT", nullable: false),
                    Address = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HOVCustomers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HOVMasters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Make = table.Column<string>(type: "TEXT", nullable: false),
                    Model = table.Column<string>(type: "TEXT", nullable: false),
                    Weight = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HOVMasters", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HOVRules",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Value = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HOVRules", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HOVTransactions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Date = table.Column<DateTime>(type: "TEXT", nullable: false),
                    HOVMasterId = table.Column<int>(type: "INTEGER", nullable: false),
                    HOVCustomerId = table.Column<int>(type: "INTEGER", nullable: true),
                    ViolationStatus = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HOVTransactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HOVTransactions_HOVCustomers_HOVCustomerId",
                        column: x => x.HOVCustomerId,
                        principalTable: "HOVCustomers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_HOVTransactions_HOVMasters_HOVMasterId",
                        column: x => x.HOVMasterId,
                        principalTable: "HOVMasters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HOVTransactions_HOVCustomerId",
                table: "HOVTransactions",
                column: "HOVCustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_HOVTransactions_HOVMasterId",
                table: "HOVTransactions",
                column: "HOVMasterId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HOVRules");

            migrationBuilder.DropTable(
                name: "HOVTransactions");

            migrationBuilder.DropTable(
                name: "HOVCustomers");

            migrationBuilder.DropTable(
                name: "HOVMasters");
        }
    }
}
