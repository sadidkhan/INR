using INR.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace INR.DAL.Configurations
{
    public class CameraConfiguration : IEntityTypeConfiguration<Camera>
    {
        public void Configure(EntityTypeBuilder<Camera> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id).ValueGeneratedNever();

            builder.HasData(new Camera { Id = 1, Title = "cam 1", Position = "right" },
                new Camera { Id = 2, Title = "cam 2", Position = "back" },
                new Camera { Id = 3, Title = "cam 3", Position = "top" },
                new Camera { Id = 4, Title = "cam 4", Position = "left" });
        }
    }
}
