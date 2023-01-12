﻿// <auto-generated />
using System;
using INR.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace INR.Migrations
{
    [DbContext(typeof(InrDbContext))]
    [Migration("20230112045706_Update_Table_VideoSegmentationHistory")]
    partial class Update_Table_VideoSegmentationHistory
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("INR.DAL.Models.Camera", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<string>("Position")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Camera");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Position = "right",
                            Title = "cam 1"
                        },
                        new
                        {
                            Id = 2,
                            Position = "back",
                            Title = "cam 2"
                        },
                        new
                        {
                            Id = 3,
                            Position = "top",
                            Title = "cam 3"
                        },
                        new
                        {
                            Id = 4,
                            Position = "left",
                            Title = "cam 4"
                        });
                });

            modelBuilder.Entity("INR.DAL.Models.FileInformation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("CameraId")
                        .HasColumnType("int");

                    b.Property<string>("FileName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PatientTaskHandmappingId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CameraId");

                    b.HasIndex("PatientTaskHandmappingId");

                    b.ToTable("FileInformation");
                });

            modelBuilder.Entity("INR.DAL.Models.Patient", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("PatientCode")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Patient");
                });

            modelBuilder.Entity("INR.DAL.Models.PatientTaskHandMapping", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("HandId")
                        .HasColumnType("int");

                    b.Property<bool>("IsImpaired")
                        .HasColumnType("bit");

                    b.Property<bool>("IsSubmitted")
                        .HasColumnType("bit");

                    b.Property<int>("PatientId")
                        .HasColumnType("int");

                    b.Property<int>("TaskId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PatientId");

                    b.ToTable("PatientTaskHandMapping");
                });

            modelBuilder.Entity("INR.DAL.Models.Segment", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Segment");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "IPT"
                        },
                        new
                        {
                            Id = 2,
                            Name = "ET"
                        },
                        new
                        {
                            Id = 3,
                            Name = "MTR1"
                        },
                        new
                        {
                            Id = 4,
                            Name = "PR"
                        },
                        new
                        {
                            Id = 5,
                            Name = "MTR2"
                        },
                        new
                        {
                            Id = 6,
                            Name = "TG"
                        },
                        new
                        {
                            Id = 7,
                            Name = "GI"
                        },
                        new
                        {
                            Id = 8,
                            Name = "GP"
                        });
                });

            modelBuilder.Entity("INR.DAL.Models.TaskSegmentHandCameraMapping", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("CameraId")
                        .HasColumnType("int");

                    b.Property<string>("Definition")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("HandId")
                        .HasColumnType("int");

                    b.Property<int>("SegmentId")
                        .HasColumnType("int");

                    b.Property<int>("TaskId")
                        .HasColumnType("int");

                    b.Property<string>("ViewType")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CameraId");

                    b.HasIndex("SegmentId");

                    b.ToTable("TaskSegmentHandCameraMapping");
                });

            modelBuilder.Entity("INR.DAL.Models.VideoSegment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("End")
                        .HasColumnType("int");

                    b.Property<int>("PatientTaskHandMappingId")
                        .HasColumnType("int");

                    b.Property<int>("SegmentId")
                        .HasColumnType("int");

                    b.Property<int>("Start")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("PatientTaskHandMappingId");

                    b.HasIndex("SegmentId");

                    b.ToTable("VideoSegment");
                });

            modelBuilder.Entity("INR.DAL.Models.VideoSegmentationHistory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("CameraId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("HandId")
                        .HasColumnType("int");

                    b.Property<string>("HandType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("In")
                        .HasColumnType("int");

                    b.Property<bool>("IsSubmitted")
                        .HasColumnType("bit");

                    b.Property<int>("Out")
                        .HasColumnType("int");

                    b.Property<int>("PatientId")
                        .HasColumnType("int");

                    b.Property<string>("Reason")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SegmentId")
                        .HasColumnType("int");

                    b.Property<string>("SegmentName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TaskId")
                        .HasColumnType("int");

                    b.Property<string>("ViewType")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("VideoSegmentationHistory");
                });

            modelBuilder.Entity("INR.DAL.Models.FileInformation", b =>
                {
                    b.HasOne("INR.DAL.Models.Camera", "Camera")
                        .WithMany()
                        .HasForeignKey("CameraId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("INR.DAL.Models.PatientTaskHandMapping", "PatientTaskHandMapping")
                        .WithMany()
                        .HasForeignKey("PatientTaskHandmappingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Camera");

                    b.Navigation("PatientTaskHandMapping");
                });

            modelBuilder.Entity("INR.DAL.Models.PatientTaskHandMapping", b =>
                {
                    b.HasOne("INR.DAL.Models.Patient", "Patient")
                        .WithMany()
                        .HasForeignKey("PatientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Patient");
                });

            modelBuilder.Entity("INR.DAL.Models.TaskSegmentHandCameraMapping", b =>
                {
                    b.HasOne("INR.DAL.Models.Camera", "Camera")
                        .WithMany()
                        .HasForeignKey("CameraId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("INR.DAL.Models.Segment", "Segment")
                        .WithMany()
                        .HasForeignKey("SegmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Camera");

                    b.Navigation("Segment");
                });

            modelBuilder.Entity("INR.DAL.Models.VideoSegment", b =>
                {
                    b.HasOne("INR.DAL.Models.PatientTaskHandMapping", "PatientTaskHandMapping")
                        .WithMany()
                        .HasForeignKey("PatientTaskHandMappingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("INR.DAL.Models.Segment", "Segment")
                        .WithMany()
                        .HasForeignKey("SegmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PatientTaskHandMapping");

                    b.Navigation("Segment");
                });
#pragma warning restore 612, 618
        }
    }
}
