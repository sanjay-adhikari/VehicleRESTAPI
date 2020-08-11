using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace APIVehicle.VehicleServices
{
    public class VehicleFilterDto
    {
        public int Id { get; set; }
        public int FromDate { get; set; }
        public int ToDate { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
    }
}