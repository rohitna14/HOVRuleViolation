using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class MakeHOVCustomerIdNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HOVTransactions_HOVCustomers_HOVCustomerId",
                table: "HOVTransactions");

            migrationBuilder.AlterColumn<int>(
                name: "HOVCustomerId",
                table: "HOVTransactions",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddForeignKey(
                name: "FK_HOVTransactions_HOVCustomers_HOVCustomerId",
                table: "HOVTransactions",
                column: "HOVCustomerId",
                principalTable: "HOVCustomers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HOVTransactions_HOVCustomers_HOVCustomerId",
                table: "HOVTransactions");

            migrationBuilder.AlterColumn<int>(
                name: "HOVCustomerId",
                table: "HOVTransactions",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_HOVTransactions_HOVCustomers_HOVCustomerId",
                table: "HOVTransactions",
                column: "HOVCustomerId",
                principalTable: "HOVCustomers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
