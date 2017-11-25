using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace DziennikSportowca.Data.Migrations
{
    public partial class DishFoodProduct_Entity_Update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_DishFoodProduct",
                table: "DishFoodProduct");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "DishFoodProduct",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_DishFoodProduct",
                table: "DishFoodProduct",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_DishFoodProduct_DishId",
                table: "DishFoodProduct",
                column: "DishId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_DishFoodProduct",
                table: "DishFoodProduct");

            migrationBuilder.DropIndex(
                name: "IX_DishFoodProduct_DishId",
                table: "DishFoodProduct");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "DishFoodProduct");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DishFoodProduct",
                table: "DishFoodProduct",
                columns: new[] { "DishId", "FoodProductId" });
        }
    }
}
