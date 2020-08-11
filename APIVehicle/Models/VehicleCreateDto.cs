using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace APIVehicle.Models
{
    public class VehicleCreateDto
    {
        //id- we want it to be a primary key and don't want that from the client.
        [Range(1950, 2050)]
        public int Year { get; set; }

        [Required]
        public string Make { get; set; }

        [Required]
        public string Model { get; set; }
    }
}