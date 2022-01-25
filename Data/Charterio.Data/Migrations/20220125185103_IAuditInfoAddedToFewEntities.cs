﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Charterio.Data.Migrations
{
    public partial class IAuditInfoAddedToFewEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "UserQuestions",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedOn",
                table: "UserQuestions",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "Tickets",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedOn",
                table: "Tickets",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "Payments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedOn",
                table: "Payments",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedOn",
                table: "Offers",
                type: "datetime2",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "UserQuestions");

            migrationBuilder.DropColumn(
                name: "ModifiedOn",
                table: "UserQuestions");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "ModifiedOn",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "ModifiedOn",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "ModifiedOn",
                table: "Offers");
        }
    }
}
