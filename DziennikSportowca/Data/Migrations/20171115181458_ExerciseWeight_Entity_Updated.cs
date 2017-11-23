using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace DziennikSportowca.Data.Migrations
{
    public partial class ExerciseWeight_Entity_Updated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TrainingPlanExerciseInfo",
                table: "TrainingPlanExercise");

            migrationBuilder.DropColumn(
                name: "Weight",
                table: "ExerciseWeight");

            migrationBuilder.AddColumn<double>(
                name: "Result",
                table: "ExerciseWeight",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Result",
                table: "ExerciseWeight");

            migrationBuilder.AddColumn<string>(
                name: "TrainingPlanExerciseInfo",
                table: "TrainingPlanExercise",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Weight",
                table: "ExerciseWeight",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
