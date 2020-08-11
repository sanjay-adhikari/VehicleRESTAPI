using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VehicleAPI.Models
{
    public class VehicleDto
    {
        [Key]
        public int Id { get; set; }

        [Range(1950, 2050)]
        public int Year { get; set; }

        [Required]
        public string Make { get; set; }

        [Required]
        public string Model { get; set; }
    }
}