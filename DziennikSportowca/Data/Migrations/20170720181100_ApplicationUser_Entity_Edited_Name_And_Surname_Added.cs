using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DziennikSportowca.Data.Migrations
{
    public partial class ApplicationUser_Entity_Edited_Name_And_Surname_Added : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_TrainingPlanExercise_Id",
                table: "TrainingPlanExercise");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TrainingPlanExercise",
                table: "TrainingPlanExercise");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Surname",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_TrainingPlanExercise",
                table: "TrainingPlanExercise",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_TrainingPlanExercise_ExerciseId",
                table: "TrainingPlanExercise",
                column: "ExerciseId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_TrainingPlanExercise",
                table: "TrainingPlanExercise");

            migrationBuilder.DropIndex(
                name: "IX_TrainingPlanExercise_ExerciseId",
                table: "TrainingPlanExercise");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Surname",
                table: "AspNetUsers");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_TrainingPlanExercise_Id",
                table: "TrainingPlanExercise",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TrainingPlanExercise",
                table: "TrainingPlanExercise",
                columns: new[] { "ExerciseId", "TrainingPlanId" });
        }
    }
}
