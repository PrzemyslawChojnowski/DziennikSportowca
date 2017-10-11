using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DziennikSportowca.Data.Migrations
{
    public partial class ExerciseWeight_Entity_Added : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_UserTrainingExerciseResult",
                table: "UserTrainingExerciseResult");

            migrationBuilder.RenameColumn(
                name: "TrainingDate",
                table: "UserTraining",
                newName: "StartDateTime");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "UserTrainingExerciseResult",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDateTime",
                table: "UserTraining",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserTrainingExerciseResult",
                table: "UserTrainingExerciseResult",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "ExerciseWeight",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserTrainingExerciseResultId = table.Column<int>(nullable: false),
                    Weight = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExerciseWeight", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExerciseWeight_UserTrainingExerciseResult_UserTrainingExerciseResultId",
                        column: x => x.UserTrainingExerciseResultId,
                        principalTable: "UserTrainingExerciseResult",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserTrainingExerciseResult_UserTrainingId",
                table: "UserTrainingExerciseResult",
                column: "UserTrainingId");

            migrationBuilder.CreateIndex(
                name: "IX_ExerciseWeight_UserTrainingExerciseResultId",
                table: "ExerciseWeight",
                column: "UserTrainingExerciseResultId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExerciseWeight");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserTrainingExerciseResult",
                table: "UserTrainingExerciseResult");

            migrationBuilder.DropIndex(
                name: "IX_UserTrainingExerciseResult_UserTrainingId",
                table: "UserTrainingExerciseResult");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "UserTrainingExerciseResult");

            migrationBuilder.DropColumn(
                name: "EndDateTime",
                table: "UserTraining");

            migrationBuilder.RenameColumn(
                name: "StartDateTime",
                table: "UserTraining",
                newName: "TrainingDate");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserTrainingExerciseResult",
                table: "UserTrainingExerciseResult",
                columns: new[] { "UserTrainingId", "TrainingPlanExerciseId" });
        }
    }
}
