using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DziennikSportowca.Data.Migrations
{
    public partial class UserFigure_Entity_Added : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserFigure",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BicepsCircumference = table.Column<double>(nullable: false),
                    BodyFat = table.Column<double>(nullable: false),
                    ChestCircumference = table.Column<double>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    HipCircumference = table.Column<double>(nullable: false),
                    ShouldersCircumference = table.Column<double>(nullable: false),
                    ThighCircumference = table.Column<double>(nullable: false),
                    TricepsCircumference = table.Column<double>(nullable: false),
                    UserId = table.Column<string>(nullable: true),
                    WaistCircumference = table.Column<double>(nullable: false),
                    Weight = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserFigure", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserFigure_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserFigure_UserId",
                table: "UserFigure",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserFigure");
        }
    }
}
