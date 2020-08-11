using APIVehicle.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using VehicleAPI.Models;
using VehicleAPI.InMemoryService;
using VehicleAPI.VehicleServices;
using APIVehicle.VehicleServices;

namespace VehicleAPI.VehicleServices
{
    public class VehicleService : IVehicleService
    {
        #region Dependencies and Constructor

        private IVehicleInMemoryService _iVehicleInMemoryService; //implementing in-memory now. In future we can easily use DB and no code changes are required here

        public VehicleService(IVehicleInMemoryService iVehicleInMemoryService)
        {
            _iVehicleInMemoryService = iVehicleInMemoryService;
        }

        #endregion

        public int CreateVehicle(VehicleCreateDto vehicle)
        {
            int result = 0;
            try
            {
                result = _iVehicleInMemoryService.CreateVehicle(vehicle);
            }
            catch (Exception)
            {
                result = -1;
            }
            return result;
        }

        public int DeleteVehicle(int id)
        {
            int result = 0;
            try
            {
                result = _iVehicleInMemoryService.DeleteVehicle(id);
            }
            catch (Exception)
            {
                result = -1;
            }
            return result;            
        }

        public List<VehicleDto> GetAllVehicles()
        {
            List<VehicleDto> vehicles;
            try
            {
                vehicles = _iVehicleInMemoryService.GetVehicles();
            }
            catch (Exception)
            {
                vehicles = null;
            }
            return vehicles;
            
        }
        
        public VehicleDto GetVehicleById(int id)
        {
            var lstVehicles = _iVehicleInMemoryService.GetVehicles();
            if (lstVehicles != null)
            {
                return lstVehicles.Where(x => x.Id == id).FirstOrDefault();
            }
            return null;
        }


        public int UpdateVehicle(VehicleDto vehicle)
        {
            int result = 0;
            try
            {
                result = _iVehicleInMemoryService.UpdateVehicle(vehicle);
            }
            catch (Exception)
            {
                result = -1;
            }
            return result;
            
        }

        public List<VehicleDto> GetFilteredVehicles(VehicleFilterDto vehicleFilterDto)
        {
            return _iVehicleInMemoryService.GetFilteredVehicles(vehicleFilterDto);
            
        }
    }
}