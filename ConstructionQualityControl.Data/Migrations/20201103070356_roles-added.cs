using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ConstructionQualityControl.Data.Migrations
{
    public partial class rolesadded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Builders_BuilderId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Customers_CustomerId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Builders_BuilderId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Customers_CustomerId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Reports_Builders_BuilderId",
                table: "Reports");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Builders_BuilderId",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Customers_CustomerId",
                table: "Users");

            migrationBuilder.DropTable(
                name: "Builders");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropIndex(
                name: "IX_Users_BuilderId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_CustomerId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Reports_BuilderId",
                table: "Reports");

            migrationBuilder.DropIndex(
                name: "IX_Orders_BuilderId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_CustomerId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Comments_BuilderId",
                table: "Comments");

            migrationBuilder.DropIndex(
                name: "IX_Comments_CustomerId",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "BuilderId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "isCustomer",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "BuilderId",
                table: "Reports");

            migrationBuilder.DropColumn(
                name: "BuilderId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "BuilderId",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "Comments");

            migrationBuilder.AddColumn<DateTime>(
                name: "BirthDate",
                table: "Users",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "CityId",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CompanyDescription",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CompanyName",
                table: "Users",
                maxLength: 35,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Users",
                maxLength: 25,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "Users",
                maxLength: 25,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Patronymic",
                table: "Users",
                maxLength: 25,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RegistrationDate",
                table: "Users",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Reports",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Orders",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsCompleted",
                table: "Orders",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsRoot",
                table: "Orders",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Orders",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Comments",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    UserId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Roles_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_CityId",
                table: "Users",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_Reports_UserId",
                table: "Reports",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_UserId",
                table: "Orders",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_UserId",
                table: "Comments",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Roles_UserId",
                table: "Roles",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Users_UserId",
                table: "Comments",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Users_UserId",
                table: "Orders",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Reports_Users_UserId",
                table: "Reports",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Cities_CityId",
                table: "Users",
                column: "CityId",
                principalTable: "Cities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Users_UserId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Users_UserId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Reports_Users_UserId",
                table: "Reports");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Cities_CityId",
                table: "Users");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropIndex(
                name: "IX_Users_CityId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Reports_UserId",
                table: "Reports");

            migrationBuilder.DropIndex(
                name: "IX_Orders_UserId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Comments_UserId",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "BirthDate",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "CityId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "CompanyDescription",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "CompanyName",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Patronymic",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "RegistrationDate",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Reports");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "IsCompleted",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "IsRoot",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Comments");

            migrationBuilder.AddColumn<int>(
                name: "BuilderId",
                table: "Users",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CustomerId",
                table: "Users",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "isCustomer",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "BuilderId",
                table: "Reports",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BuilderId",
                table: "Orders",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CustomerId",
                table: "Orders",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BuilderId",
                table: "Comments",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CustomerId",
                table: "Comments",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Builders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CityId = table.Column<int>(type: "int", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Builders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Builders_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CityId = table.Column<int>(type: "int", nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    Patronymic = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Customers_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_BuilderId",
                table: "Users",
                column: "BuilderId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_CustomerId",
                table: "Users",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Reports_BuilderId",
                table: "Reports",
                column: "BuilderId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_BuilderId",
                table: "Orders",
                column: "BuilderId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CustomerId",
                table: "Orders",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_BuilderId",
                table: "Comments",
                column: "BuilderId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_CustomerId",
                table: "Comments",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Builders_CityId",
                table: "Builders",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_CityId",
                table: "Customers",
                column: "CityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Builders_BuilderId",
                table: "Comments",
                column: "BuilderId",
                principalTable: "Builders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Customers_CustomerId",
                table: "Comments",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Builders_BuilderId",
                table: "Orders",
                column: "BuilderId",
                principalTable: "Builders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Customers_CustomerId",
                table: "Orders",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Reports_Builders_BuilderId",
                table: "Reports",
                column: "BuilderId",
                principalTable: "Builders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Builders_BuilderId",
                table: "Users",
                column: "BuilderId",
                principalTable: "Builders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Customers_CustomerId",
                table: "Users",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
