using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace DziennikSportowca.Data.Migrations
{
    public partial class ExerciseInstruction_And_ExerciseInstructionPhoto_Entities_Added : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ExerciseInstructionId",
                table: "Exercise",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ExerciseInstruction",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ExerciseId = table.Column<int>(type: "int", nullable: false),
                    Instructions = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExerciseInstruction", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExerciseInstruction_Exercise_ExerciseId",
                        column: x => x.ExerciseId,
                        principalTable: "Exercise",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "ExerciseInstructionPhoto",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Content = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    ExerciseInstructionId = table.Column<int>(type: "int", nullable: false),
                    PhotoTitle = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExerciseInstructionPhoto", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExerciseInstructionPhoto_ExerciseInstruction_ExerciseInstructionId",
                        column: x => x.ExerciseInstructionId,
                        principalTable: "ExerciseInstruction",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Exercise_ExerciseInstructionId",
                table: "Exercise",
                column: "ExerciseInstructionId");

            migrationBuilder.CreateIndex(
                name: "IX_ExerciseInstruction_ExerciseId",
                table: "ExerciseInstruction",
                column: "ExerciseId");

            migrationBuilder.CreateIndex(
                name: "IX_ExerciseInstructionPhoto_ExerciseInstructionId",
                table: "ExerciseInstructionPhoto",
                column: "ExerciseInstructionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Exercise_ExerciseInstruction_ExerciseInstructionId",
                table: "Exercise",
                column: "ExerciseInstructionId",
                principalTable: "ExerciseInstruction",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Exercise_ExerciseInstruction_ExerciseInstructionId",
                table: "Exercise");

            migrationBuilder.DropTable(
                name: "ExerciseInstructionPhoto");

            migrationBuilder.DropTable(
                name: "ExerciseInstruction");

            migrationBuilder.DropIndex(
                name: "IX_Exercise_ExerciseInstructionId",
                table: "Exercise");

            migrationBuilder.DropColumn(
                name: "ExerciseInstructionId",
                table: "Exercise");
        }
    }
}
