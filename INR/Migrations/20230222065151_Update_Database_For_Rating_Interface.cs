using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace INR.Migrations
{
    public partial class Update_Database_For_Rating_Interface : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PatientTaskHandMappingId",
                table: "VideoSegmentationHistory",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.Sql(@"update VideoSegmentationHistory
                set PatientTaskHandMappingId = pth.Id
                from VideoSegmentationHistory vsh
                join PatientTaskHandMapping pth on pth.PatientId = vsh.PatientId and pth.TaskId = vsh.TaskId and pth.HandId = vsh.HandId");

            migrationBuilder.CreateTable(
                name: "PthTherapistMappings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PatientTaskHandMappingId = table.Column<int>(type: "int", nullable: false),
                    TherapistId = table.Column<int>(type: "int", nullable: false),
                    IsSubmitted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PthTherapistMappings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PthTherapistMappings_PatientTaskHandMapping_PatientTaskHandMappingId",
                        column: x => x.PatientTaskHandMappingId,
                        principalTable: "PatientTaskHandMapping",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SegmentFileInformation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PatientTaskHandMappingId = table.Column<int>(type: "int", nullable: false),
                    CameraId = table.Column<int>(type: "int", nullable: false),
                    SegmentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SegmentFileInformation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SegmentFileInformation_PatientTaskHandMapping_PatientTaskHandMappingId",
                        column: x => x.PatientTaskHandMappingId,
                        principalTable: "PatientTaskHandMapping",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SegmentFileInformation_Segment_SegmentId",
                        column: x => x.SegmentId,
                        principalTable: "Segment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SegmentRatings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PatientTaskHandMappingId = table.Column<int>(type: "int", nullable: false),
                    SegmentId = table.Column<int>(type: "int", nullable: false),
                    Completed = table.Column<bool>(type: "bit", nullable: false),
                    Initialized = table.Column<bool>(type: "bit", nullable: false),
                    Time = table.Column<bool>(type: "bit", nullable: false),
                    Impaired = table.Column<bool>(type: "bit", nullable: false),
                    SEAFR = table.Column<bool>(type: "bit", nullable: false),
                    TS = table.Column<bool>(type: "bit", nullable: false),
                    ROME = table.Column<bool>(type: "bit", nullable: false),
                    FPS = table.Column<bool>(type: "bit", nullable: false),
                    WPAT = table.Column<bool>(type: "bit", nullable: false),
                    HA = table.Column<bool>(type: "bit", nullable: false),
                    DP = table.Column<bool>(type: "bit", nullable: false),
                    DPO = table.Column<bool>(type: "bit", nullable: false),
                    SAT = table.Column<bool>(type: "bit", nullable: false),
                    DMR = table.Column<bool>(type: "bit", nullable: false),
                    THS = table.Column<bool>(type: "bit", nullable: false),
                    PP = table.Column<bool>(type: "bit", nullable: false),
                    Rating = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TherapistId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SegmentRatings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SegmentRatings_PatientTaskHandMapping_PatientTaskHandMappingId",
                        column: x => x.PatientTaskHandMappingId,
                        principalTable: "PatientTaskHandMapping",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TaskRatings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PatientTaskHandMappingId = table.Column<int>(type: "int", nullable: false),
                    Completed = table.Column<bool>(type: "bit", nullable: false),
                    Initialized = table.Column<bool>(type: "bit", nullable: false),
                    Time = table.Column<bool>(type: "bit", nullable: false),
                    Impaired = table.Column<bool>(type: "bit", nullable: false),
                    Rating = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TherapistId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskRatings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TaskRatings_PatientTaskHandMapping_PatientTaskHandMappingId",
                        column: x => x.PatientTaskHandMappingId,
                        principalTable: "PatientTaskHandMapping",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PthTherapistMappings_PatientTaskHandMappingId",
                table: "PthTherapistMappings",
                column: "PatientTaskHandMappingId");

            migrationBuilder.CreateIndex(
                name: "IX_SegmentFileInformation_PatientTaskHandMappingId",
                table: "SegmentFileInformation",
                column: "PatientTaskHandMappingId");

            migrationBuilder.CreateIndex(
                name: "IX_SegmentFileInformation_SegmentId",
                table: "SegmentFileInformation",
                column: "SegmentId");

            migrationBuilder.CreateIndex(
                name: "IX_SegmentRatings_PatientTaskHandMappingId",
                table: "SegmentRatings",
                column: "PatientTaskHandMappingId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskRatings_PatientTaskHandMappingId",
                table: "TaskRatings",
                column: "PatientTaskHandMappingId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PthTherapistMappings");

            migrationBuilder.DropTable(
                name: "SegmentFileInformation");

            migrationBuilder.DropTable(
                name: "SegmentRatings");

            migrationBuilder.DropTable(
                name: "TaskRatings");

            migrationBuilder.DropColumn(
                name: "PatientTaskHandMappingId",
                table: "VideoSegmentationHistory");
        }
    }
}
