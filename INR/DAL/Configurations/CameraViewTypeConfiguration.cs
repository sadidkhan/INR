using INR.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace INR.DAL.Configurations
{
    public class CameraViewTypeConfiguration : IEntityTypeConfiguration<CameraViewType>
    {
        public void Configure(EntityTypeBuilder<CameraViewType> builder)
        {
            builder.HasData(new CameraViewType { ViewType = "Ipsilateral" },
                new CameraViewType { ViewType = "Contralteral" },
                new CameraViewType { ViewType = "Transverse" });
        }
    }
}
