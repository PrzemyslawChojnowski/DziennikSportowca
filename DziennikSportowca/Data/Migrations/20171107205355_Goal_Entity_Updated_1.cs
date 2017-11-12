using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace DziennikSportowca.Data.Migrations
{
    public partial class Goal_Entity_Updated_1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Scopes",
                table: "Goal");

            migrationBuilder.AddColumn<int>(
                name: "Scope",
                table: "Goal",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ScopePosition",
                table: "Goal",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Scope",
                table: "Goal");

            migrationBuilder.DropColumn(
                name: "ScopePosition",
                table: "Goal");

            migrationBuilder.AddColumn<string>(
                name: "Scopes",
                table: "Goal",
                nullable: true);
        }
    }
}
