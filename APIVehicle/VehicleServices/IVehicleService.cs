using APIVehicle.Models;
using APIVehicle.VehicleServices;
using System.Collections.Generic;
using VehicleAPI.Models;

namespace VehicleAPI.VehicleServices
{
    public interface IVehicleService
    {
        List<VehicleDto> GetAllVehicles();
        VehicleDto GetVehicleById(int id);
        int CreateVehicle(VehicleCreateDto vehicle);
        int UpdateVehicle(VehicleDto vehicle);

        int DeleteVehicle(int id);

        //provide filter feature
        List<VehicleDto> GetFilteredVehicles(VehicleFilterDto vehicleFilterDto);
    }
}
