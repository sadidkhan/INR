using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace INR.Migrations
{
    public partial class CreatedAt_ModifiedAt_Columns_Added_In_Ratings_Table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "TaskRatings",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedAt",
                table: "TaskRatings",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "SegmentRatings",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "FPO",
                table: "SegmentRatings",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedAt",
                table: "SegmentRatings",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "SA",
                table: "SegmentRatings",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "PthTherapistMappings",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedAt",
                table: "PthTherapistMappings",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "TaskRatings");

            migrationBuilder.DropColumn(
                name: "ModifiedAt",
                table: "TaskRatings");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "SegmentRatings");

            migrationBuilder.DropColumn(
                name: "FPO",
                table: "SegmentRatings");

            migrationBuilder.DropColumn(
                name: "ModifiedAt",
                table: "SegmentRatings");

            migrationBuilder.DropColumn(
                name: "SA",
                table: "SegmentRatings");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "PthTherapistMappings");

            migrationBuilder.DropColumn(
                name: "ModifiedAt",
                table: "PthTherapistMappings");
        }
    }
}
