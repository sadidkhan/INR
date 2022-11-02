using INR.DAL.Configurations;
using INR.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace INR.DAL
{
    public class InrDbContext : DbContext
    {
        public InrDbContext(DbContextOptions<InrDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CameraConfiguration());
            //modelBuilder.ApplyConfiguration(new CameraViewTypeConfiguration());
            modelBuilder.ApplyConfiguration(new SegmentConfiguration());
        }

        public DbSet<Camera> Cameras { get; set; } = null!;
        //public DbSet<CameraViewTypeConfiguration> CameraViewTypeConfigurations { get; set; } = null!;
        public DbSet<Patient> Patients { get; set; } = null!;
        public DbSet<Segment> Segments { get; set; } = null!;
        public DbSet<FileInformation> FileInformations { get; set; } = null!;
        public DbSet<PatientTaskHandMapping> PatientTaskHandMappings { get; set; } = null!;
        public DbSet<TaskSegmentHandCameraMapping> TaskSegmentHandCameraMappings { get; set; } = null!;
        public DbSet<VideoSegment> VideoSegments { get; set; } = null!;
        public DbSet<VideoSegmentationHistory> VideoSegmentationHistories { get; set; } = null!;

    }
}
