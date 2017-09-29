using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DziennikSportowca.Data.Migrations
{
    public partial class UserFriend_Entity_Updated_1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FriendshipStatus",
                table: "UserFriend",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FriendshipStatus",
                table: "UserFriend");
        }
    }
}
