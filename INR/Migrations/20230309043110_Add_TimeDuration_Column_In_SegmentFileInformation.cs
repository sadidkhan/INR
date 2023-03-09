using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace INR.Migrations
{
    public partial class Add_TimeDuration_Column_In_SegmentFileInformation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "TimeDuration",
                table: "SegmentFileInformation",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TimeDuration",
                table: "SegmentFileInformation");
        }
    }
}
