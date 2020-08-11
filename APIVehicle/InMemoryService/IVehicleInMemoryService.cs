using APIVehicle.Models;
using APIVehicle.VehicleServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleAPI.Models;

namespace VehicleAPI.InMemoryService
{
    public interface IVehicleInMemoryService
    {
        List<VehicleDto> GetVehicles();
        int CreateVehicle(VehicleCreateDto vehicle);
        int UpdateVehicle(VehicleDto vehicle);
        int DeleteVehicle(int id);

        List<VehicleDto> GetFilteredVehicles(VehicleFilterDto vehicleFilterDto);

    }
}
