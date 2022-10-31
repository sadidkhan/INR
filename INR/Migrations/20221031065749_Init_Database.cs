using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace INR.Migrations
{
    public partial class Init_Database : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Camera",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ViewType = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Camera", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Patient",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PatientCode = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patient", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Segment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Label = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Segment", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PatientTaskHandMapping",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PatientId = table.Column<int>(type: "int", nullable: false),
                    TaskId = table.Column<int>(type: "int", nullable: false),
                    HandId = table.Column<int>(type: "int", nullable: false),
                    IsImpaired = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatientTaskHandMapping", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PatientTaskHandMapping_Patient_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patient",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TaskSegmentCameraMapping",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TaskId = table.Column<int>(type: "int", nullable: false),
                    SegmentId = table.Column<int>(type: "int", nullable: false),
                    HandId = table.Column<int>(type: "int", nullable: false),
                    CameraId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskSegmentCameraMapping", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TaskSegmentCameraMapping_Camera_CameraId",
                        column: x => x.CameraId,
                        principalTable: "Camera",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TaskSegmentCameraMapping_Segment_SegmentId",
                        column: x => x.SegmentId,
                        principalTable: "Segment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FileInformation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PatientTaskHandmappingId = table.Column<int>(type: "int", nullable: false),
                    CameraId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileInformation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FileInformation_Camera_CameraId",
                        column: x => x.CameraId,
                        principalTable: "Camera",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FileInformation_PatientTaskHandMapping_PatientTaskHandmappingId",
                        column: x => x.PatientTaskHandmappingId,
                        principalTable: "PatientTaskHandMapping",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VideoSegment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PatientTaskHandMappingId = table.Column<int>(type: "int", nullable: false),
                    SegmentId = table.Column<int>(type: "int", nullable: false),
                    Start = table.Column<int>(type: "int", nullable: false),
                    End = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VideoSegment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VideoSegment_PatientTaskHandMapping_PatientTaskHandMappingId",
                        column: x => x.PatientTaskHandMappingId,
                        principalTable: "PatientTaskHandMapping",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VideoSegment_Segment_SegmentId",
                        column: x => x.SegmentId,
                        principalTable: "Segment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FileInformation_CameraId",
                table: "FileInformation",
                column: "CameraId");

            migrationBuilder.CreateIndex(
                name: "IX_FileInformation_PatientTaskHandmappingId",
                table: "FileInformation",
                column: "PatientTaskHandmappingId");

            migrationBuilder.CreateIndex(
                name: "IX_PatientTaskHandMapping_PatientId",
                table: "PatientTaskHandMapping",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskSegmentCameraMapping_CameraId",
                table: "TaskSegmentCameraMapping",
                column: "CameraId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskSegmentCameraMapping_SegmentId",
                table: "TaskSegmentCameraMapping",
                column: "SegmentId");

            migrationBuilder.CreateIndex(
                name: "IX_VideoSegment_PatientTaskHandMappingId",
                table: "VideoSegment",
                column: "PatientTaskHandMappingId");

            migrationBuilder.CreateIndex(
                name: "IX_VideoSegment_SegmentId",
                table: "VideoSegment",
                column: "SegmentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FileInformation");

            migrationBuilder.DropTable(
                name: "TaskSegmentCameraMapping");

            migrationBuilder.DropTable(
                name: "VideoSegment");

            migrationBuilder.DropTable(
                name: "Camera");

            migrationBuilder.DropTable(
                name: "PatientTaskHandMapping");

            migrationBuilder.DropTable(
                name: "Segment");

            migrationBuilder.DropTable(
                name: "Patient");
        }
    }
}
