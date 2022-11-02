﻿using System.ComponentModel.DataAnnotations.Schema;

namespace INR.DAL.Models
{
    [Table("Camera")]
    public class Camera
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Position { get; set; }
    }
}
