using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace DziennikSportowca.Data.Migrations
{
    public partial class ExerciseInstructionPhoto_Entity_Updated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExerciseInstructionPhoto_ExerciseInstruction_ExerciseInstructionId",
                table: "ExerciseInstructionPhoto");

            migrationBuilder.AlterColumn<int>(
                name: "ExerciseInstructionId",
                table: "ExerciseInstructionPhoto",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_ExerciseInstructionPhoto_ExerciseInstruction_ExerciseInstructionId",
                table: "ExerciseInstructionPhoto",
                column: "ExerciseInstructionId",
                principalTable: "ExerciseInstruction",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExerciseInstructionPhoto_ExerciseInstruction_ExerciseInstructionId",
                table: "ExerciseInstructionPhoto");

            migrationBuilder.AlterColumn<int>(
                name: "ExerciseInstructionId",
                table: "ExerciseInstructionPhoto",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ExerciseInstructionPhoto_ExerciseInstruction_ExerciseInstructionId",
                table: "ExerciseInstructionPhoto",
                column: "ExerciseInstructionId",
                principalTable: "ExerciseInstruction",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
