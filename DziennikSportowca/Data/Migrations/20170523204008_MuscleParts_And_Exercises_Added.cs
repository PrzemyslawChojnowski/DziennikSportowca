using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DziennikSportowca.Data.Migrations
{
    public partial class MuscleParts_And_Exercises_Added : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TrainingPlans_AspNetUsers_UserId",
                table: "TrainingPlans");

            migrationBuilder.DropForeignKey(
                name: "FK_TrainingPlanExercises_Exercises_ExerciseId",
                table: "TrainingPlanExercises");

            migrationBuilder.DropForeignKey(
                name: "FK_TrainingPlanExercises_TrainingPlans_TrainingPlanId",
                table: "TrainingPlanExercises");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TrainingPlanExercises",
                table: "TrainingPlanExercises");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TrainingPlans",
                table: "TrainingPlans");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Exercises",
                table: "Exercises");

            migrationBuilder.RenameTable(
                name: "TrainingPlanExercises",
                newName: "TrainingPlanExercise");

            migrationBuilder.RenameTable(
                name: "TrainingPlans",
                newName: "TrainingPlan");

            migrationBuilder.RenameTable(
                name: "Exercises",
                newName: "Exercise");

            migrationBuilder.RenameIndex(
                name: "IX_TrainingPlanExercises_TrainingPlanId",
                table: "TrainingPlanExercise",
                newName: "IX_TrainingPlanExercise_TrainingPlanId");

            migrationBuilder.RenameIndex(
                name: "IX_TrainingPlans_UserId",
                table: "TrainingPlan",
                newName: "IX_TrainingPlan_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TrainingPlanExercise",
                table: "TrainingPlanExercise",
                columns: new[] { "ExerciseId", "TrainingPlanId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_TrainingPlan",
                table: "TrainingPlan",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Exercise",
                table: "Exercise",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "MusclePart",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MusclePart", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MusclePartExercise",
                columns: table => new
                {
                    ExerciseId = table.Column<int>(nullable: false),
                    MuscePartId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MusclePartExercise", x => new { x.ExerciseId, x.MuscePartId });
                    table.ForeignKey(
                        name: "FK_MusclePartExercise_Exercise_ExerciseId",
                        column: x => x.ExerciseId,
                        principalTable: "Exercise",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MusclePartExercise_MusclePart_MuscePartId",
                        column: x => x.MuscePartId,
                        principalTable: "MusclePart",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MusclePartExercise_MuscePartId",
                table: "MusclePartExercise",
                column: "MuscePartId");

            migrationBuilder.AddForeignKey(
                name: "FK_TrainingPlan_AspNetUsers_UserId",
                table: "TrainingPlan",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TrainingPlanExercise_Exercise_ExerciseId",
                table: "TrainingPlanExercise",
                column: "ExerciseId",
                principalTable: "Exercise",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TrainingPlanExercise_TrainingPlan_TrainingPlanId",
                table: "TrainingPlanExercise",
                column: "TrainingPlanId",
                principalTable: "TrainingPlan",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TrainingPlan_AspNetUsers_UserId",
                table: "TrainingPlan");

            migrationBuilder.DropForeignKey(
                name: "FK_TrainingPlanExercise_Exercise_ExerciseId",
                table: "TrainingPlanExercise");

            migrationBuilder.DropForeignKey(
                name: "FK_TrainingPlanExercise_TrainingPlan_TrainingPlanId",
                table: "TrainingPlanExercise");

            migrationBuilder.DropTable(
                name: "MusclePartExercise");

            migrationBuilder.DropTable(
                name: "MusclePart");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TrainingPlanExercise",
                table: "TrainingPlanExercise");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TrainingPlan",
                table: "TrainingPlan");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Exercise",
                table: "Exercise");

            migrationBuilder.RenameTable(
                name: "TrainingPlanExercise",
                newName: "TrainingPlanExercises");

            migrationBuilder.RenameTable(
                name: "TrainingPlan",
                newName: "TrainingPlans");

            migrationBuilder.RenameTable(
                name: "Exercise",
                newName: "Exercises");

            migrationBuilder.RenameIndex(
                name: "IX_TrainingPlanExercise_TrainingPlanId",
                table: "TrainingPlanExercises",
                newName: "IX_TrainingPlanExercises_TrainingPlanId");

            migrationBuilder.RenameIndex(
                name: "IX_TrainingPlan_UserId",
                table: "TrainingPlans",
                newName: "IX_TrainingPlans_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TrainingPlanExercises",
                table: "TrainingPlanExercises",
                columns: new[] { "ExerciseId", "TrainingPlanId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_TrainingPlans",
                table: "TrainingPlans",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Exercises",
                table: "Exercises",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TrainingPlans_AspNetUsers_UserId",
                table: "TrainingPlans",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TrainingPlanExercises_Exercises_ExerciseId",
                table: "TrainingPlanExercises",
                column: "ExerciseId",
                principalTable: "Exercises",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TrainingPlanExercises_TrainingPlans_TrainingPlanId",
                table: "TrainingPlanExercises",
                column: "TrainingPlanId",
                principalTable: "TrainingPlans",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
