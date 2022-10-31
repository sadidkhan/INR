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

        public DbSet<Camera> Cameras { get; set; } = null!;
        public DbSet<Patient> Patients { get; set; } = null!;
        public DbSet<Segment> Segments { get; set; } = null!;
        public DbSet<FileInformation> FileInformations { get; set; } = null!;
        public DbSet<PatientTaskHandMapping> PatientTaskHandMappings { get; set; } = null!;
        public DbSet<TaskSegmentHandCameraMapping> TaskSegmentCameraMappings { get; set; } = null!;
        public DbSet<VideoSegment> VideoSegments { get; set; } = null!;

    }
}
