using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DziennikSportowca.Data.Migrations
{
    public partial class TrainingPlanExercise_Updated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RepsNo",
                table: "TrainingPlanExercise",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SeriesNo",
                table: "TrainingPlanExercise",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RepsNo",
                table: "TrainingPlanExercise");

            migrationBuilder.DropColumn(
                name: "SeriesNo",
                table: "TrainingPlanExercise");
        }
    }
}
