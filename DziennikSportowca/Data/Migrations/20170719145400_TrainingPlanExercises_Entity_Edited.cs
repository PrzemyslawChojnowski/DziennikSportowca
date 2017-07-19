using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DziennikSportowca.Data.Migrations
{
    public partial class TrainingPlanExercises_Entity_Edited : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "TrainingPlanExercise",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddUniqueConstraint(
                name: "AK_TrainingPlanExercise_Id",
                table: "TrainingPlanExercise",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_TrainingPlanExercise_Id",
                table: "TrainingPlanExercise");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "TrainingPlanExercise");
        }
    }
}
