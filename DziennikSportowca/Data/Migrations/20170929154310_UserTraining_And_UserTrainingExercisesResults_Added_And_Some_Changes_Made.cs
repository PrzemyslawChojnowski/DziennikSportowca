using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DziennikSportowca.Data.Migrations
{
    public partial class UserTraining_And_UserTrainingExercisesResults_Added_And_Some_Changes_Made : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserTraining",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TrainingDate = table.Column<DateTime>(nullable: false),
                    TrainingId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTraining", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserTraining_TrainingPlan_TrainingId",
                        column: x => x.TrainingId,
                        principalTable: "TrainingPlan",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserTrainingExerciseResult",
                columns: table => new
                {
                    UserTrainingId = table.Column<int>(nullable: false),
                    TrainingPlanExerciseId = table.Column<int>(nullable: false),
                    RepsNo = table.Column<int>(nullable: false),
                    SeriesNo = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTrainingExerciseResult", x => new { x.UserTrainingId, x.TrainingPlanExerciseId });
                    table.ForeignKey(
                        name: "FK_UserTrainingExerciseResult_TrainingPlanExercise_TrainingPlanExerciseId",
                        column: x => x.TrainingPlanExerciseId,
                        principalTable: "TrainingPlanExercise",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserTrainingExerciseResult_UserTraining_UserTrainingId",
                        column: x => x.UserTrainingId,
                        principalTable: "UserTraining",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserTraining_TrainingId",
                table: "UserTraining",
                column: "TrainingId");

            migrationBuilder.CreateIndex(
                name: "IX_UserTrainingExerciseResult_TrainingPlanExerciseId",
                table: "UserTrainingExerciseResult",
                column: "TrainingPlanExerciseId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserTrainingExerciseResult");

            migrationBuilder.DropTable(
                name: "UserTraining");
        }
    }
}
