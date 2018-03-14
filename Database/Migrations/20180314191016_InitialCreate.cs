using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    EmployeeID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    EmpType = table.Column<string>(nullable: true),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    Username = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.EmployeeID);
                });

            migrationBuilder.CreateTable(
                name: "Rooms",
                columns: table => new
                {
                    RoomNo = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    BedType = table.Column<string>(nullable: true),
                    Checkin = table.Column<DateTime>(nullable: false),
                    Checkout = table.Column<DateTime>(nullable: false),
                    Left = table.Column<double>(nullable: false),
                    Legend = table.Column<string>(nullable: true),
                    NoOfBeds = table.Column<int>(nullable: false),
                    Price = table.Column<double>(nullable: false),
                    Smoking = table.Column<string>(nullable: true),
                    Top = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rooms", x => x.RoomNo);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    ID = table.Column<string>(nullable: false),
                    City = table.Column<string>(nullable: true),
                    FirstName = table.Column<string>(nullable: true),
                    LastEdit = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    PaymentInfo = table.Column<string>(nullable: true),
                    PhoneNo = table.Column<string>(nullable: true),
                    RoomNo = table.Column<int>(nullable: false),
                    State = table.Column<string>(nullable: true),
                    Street = table.Column<string>(nullable: true),
                    Zip = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Customers_Rooms_RoomNo",
                        column: x => x.RoomNo,
                        principalTable: "Rooms",
                        principalColumn: "RoomNo",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    TrNumber = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Checkin = table.Column<DateTime>(nullable: false),
                    Checkout = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: false),
                    ID = table.Column<string>(nullable: true),
                    RoomNo = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.TrNumber);
                    table.ForeignKey(
                        name: "FK_Transactions_Customers_ID",
                        column: x => x.ID,
                        principalTable: "Customers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Transactions_Rooms_RoomNo",
                        column: x => x.RoomNo,
                        principalTable: "Rooms",
                        principalColumn: "RoomNo",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Customers_RoomNo",
                table: "Customers",
                column: "RoomNo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_ID",
                table: "Transactions",
                column: "ID");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_RoomNo",
                table: "Transactions",
                column: "RoomNo");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Transactions");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Rooms");
        }
    }
}
