using System;
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
                    Id = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Position = table.Column<string>(type: "nvarchar(max)", nullable: false)
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
                    Id = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Segment", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VideoSegmentationLogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PatientId = table.Column<int>(type: "int", nullable: false),
                    HandId = table.Column<int>(type: "int", nullable: false),
                    TaskId = table.Column<int>(type: "int", nullable: false),
                    SegmentId = table.Column<int>(type: "int", nullable: false),
                    CameraId = table.Column<int>(type: "int", nullable: false),
                    In = table.Column<int>(type: "int", nullable: false),
                    Out = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsSubmitted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VideoSegmentationLogs", x => x.Id);
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
                name: "TaskSegmentHandCameraMapping",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TaskId = table.Column<int>(type: "int", nullable: false),
                    SegmentId = table.Column<int>(type: "int", nullable: false),
                    HandId = table.Column<int>(type: "int", nullable: false),
                    CameraId = table.Column<int>(type: "int", nullable: false),
                    ViewType = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskSegmentHandCameraMapping", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TaskSegmentHandCameraMapping_Camera_CameraId",
                        column: x => x.CameraId,
                        principalTable: "Camera",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TaskSegmentHandCameraMapping_Segment_SegmentId",
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
                    End = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
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

            migrationBuilder.InsertData(
                table: "Camera",
                columns: new[] { "Id", "Position", "Title" },
                values: new object[,]
                {
                    { 1, "right", "cam 1" },
                    { 2, "back", "cam 2" },
                    { 3, "top", "cam 3" },
                    { 4, "left", "cam 4" }
                });

            migrationBuilder.InsertData(
                table: "Segment",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "IPT" },
                    { 2, "ET" },
                    { 3, "MTR" },
                    { 4, "PR" }
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
                name: "IX_TaskSegmentHandCameraMapping_CameraId",
                table: "TaskSegmentHandCameraMapping",
                column: "CameraId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskSegmentHandCameraMapping_SegmentId",
                table: "TaskSegmentHandCameraMapping",
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
                name: "TaskSegmentHandCameraMapping");

            migrationBuilder.DropTable(
                name: "VideoSegment");

            migrationBuilder.DropTable(
                name: "VideoSegmentationLogs");

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
