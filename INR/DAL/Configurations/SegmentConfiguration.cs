using INR.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace INR.DAL.Configurations
{
    public class SegmentConfiguration : IEntityTypeConfiguration<Segment>
    {
        public void Configure(EntityTypeBuilder<Segment> builder)
        {
            builder.HasKey(s => s.Id);
            builder.Property(s => s.Id).ValueGeneratedNever();

            builder.HasData(new Segment { Id = 1, Name = "IPT" },
                new Segment { Id = 2, Name = "ET" },
                new Segment { Id = 3, Name = "MTR1" },
                new Segment { Id = 4, Name = "PR" },
                new Segment { Id = 5, Name = "MTR2" },
                new Segment { Id = 6, Name = "TG" },
                new Segment { Id = 7, Name = "GI" },
                new Segment { Id = 8, Name = "GP" });
        }
    }
}
