using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace DziennikSportowca.Data.Migrations
{
    public partial class Exercise_Entity_Updated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Exercise_ExerciseInstruction_ExerciseInstructionId",
                table: "Exercise");

            migrationBuilder.AlterColumn<int>(
                name: "ExerciseInstructionId",
                table: "Exercise",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Exercise_ExerciseInstruction_ExerciseInstructionId",
                table: "Exercise",
                column: "ExerciseInstructionId",
                principalTable: "ExerciseInstruction",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Exercise_ExerciseInstruction_ExerciseInstructionId",
                table: "Exercise");

            migrationBuilder.AlterColumn<int>(
                name: "ExerciseInstructionId",
                table: "Exercise",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Exercise_ExerciseInstruction_ExerciseInstructionId",
                table: "Exercise",
                column: "ExerciseInstructionId",
                principalTable: "ExerciseInstruction",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
