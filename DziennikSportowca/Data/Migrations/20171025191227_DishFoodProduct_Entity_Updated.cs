using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace DziennikSportowca.Data.Migrations
{
    public partial class DishFoodProduct_Entity_Updated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "FoodProductWeight",
                table: "DishFoodProduct",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FoodProductWeight",
                table: "DishFoodProduct");
        }
    }
}
