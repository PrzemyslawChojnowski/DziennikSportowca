using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace DziennikSportowca.Data.Migrations
{
    public partial class ActvityType_Entity_Added : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ActivityTypeId",
                table: "Exercise",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ActivityType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActivityType", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Exercise_ActivityTypeId",
                table: "Exercise",
                column: "ActivityTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Exercise_ActivityType_ActivityTypeId",
                table: "Exercise",
                column: "ActivityTypeId",
                principalTable: "ActivityType",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Exercise_ActivityType_ActivityTypeId",
                table: "Exercise");

            migrationBuilder.DropTable(
                name: "ActivityType");

            migrationBuilder.DropIndex(
                name: "IX_Exercise_ActivityTypeId",
                table: "Exercise");

            migrationBuilder.DropColumn(
                name: "ActivityTypeId",
                table: "Exercise");
        }
    }
}
