using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace DziennikSportowca.Data.Migrations
{
    public partial class FoodProductType_Entity_Added : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TypeId",
                table: "FoodProduct",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "FoodProductType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FoodProductType", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FoodProduct_TypeId",
                table: "FoodProduct",
                column: "TypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_FoodProduct_FoodProductType_TypeId",
                table: "FoodProduct",
                column: "TypeId",
                principalTable: "FoodProductType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FoodProduct_FoodProductType_TypeId",
                table: "FoodProduct");

            migrationBuilder.DropTable(
                name: "FoodProductType");

            migrationBuilder.DropIndex(
                name: "IX_FoodProduct_TypeId",
                table: "FoodProduct");

            migrationBuilder.DropColumn(
                name: "TypeId",
                table: "FoodProduct");
        }
    }
}
