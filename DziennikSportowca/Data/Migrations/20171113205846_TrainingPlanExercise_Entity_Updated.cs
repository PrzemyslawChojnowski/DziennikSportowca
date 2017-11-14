using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace DziennikSportowca.Data.Migrations
{
    public partial class TrainingPlanExercise_Entity_Updated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "SeriesNo",
                table: "TrainingPlanExercise",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "RepsNo",
                table: "TrainingPlanExercise",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "ExerciseLength",
                table: "TrainingPlanExercise",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TrainingPlanExerciseInfo",
                table: "TrainingPlanExercise",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExerciseLength",
                table: "TrainingPlanExercise");

            migrationBuilder.DropColumn(
                name: "TrainingPlanExerciseInfo",
                table: "TrainingPlanExercise");

            migrationBuilder.AlterColumn<int>(
                name: "SeriesNo",
                table: "TrainingPlanExercise",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "RepsNo",
                table: "TrainingPlanExercise",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }
    }
}
