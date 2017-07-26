using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DziennikSportowca.Data.Migrations
{
    public partial class ApplicationUser_Entity_Updated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Dish",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Dish_UserId",
                table: "Dish",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Dish_AspNetUsers_UserId",
                table: "Dish",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Dish_AspNetUsers_UserId",
                table: "Dish");

            migrationBuilder.DropIndex(
                name: "IX_Dish_UserId",
                table: "Dish");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Dish");
        }
    }
}
