using System.ComponentModel.DataAnnotations.Schema;

namespace INR.DAL.Models
{
    [Table("Segment")]
    public class Segment
    {
        public int Id { get; set; }
        public string Label { get; set; }
    }
}
