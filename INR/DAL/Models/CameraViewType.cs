using System.ComponentModel.DataAnnotations.Schema;

namespace INR.DAL.Models
{
    [Table("CameraViewType")]
    public class CameraViewType
    {
        public int Id { get; set; }
        public string ViewType { get; set; }
    }
}
