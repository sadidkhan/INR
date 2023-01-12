using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace INR.Migrations
{
    public partial class Update_Table_VideoSegmentationHistory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "HandType",
                table: "VideoSegmentationHistory",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SegmentName",
                table: "VideoSegmentationHistory",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ViewType",
                table: "VideoSegmentationHistory",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HandType",
                table: "VideoSegmentationHistory");

            migrationBuilder.DropColumn(
                name: "SegmentName",
                table: "VideoSegmentationHistory");

            migrationBuilder.DropColumn(
                name: "ViewType",
                table: "VideoSegmentationHistory");
        }
    }
}
