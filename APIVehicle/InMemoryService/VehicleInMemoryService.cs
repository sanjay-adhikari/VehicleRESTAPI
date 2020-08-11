using APIVehicle.Models;
using APIVehicle.VehicleServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using VehicleAPI.Models;

namespace VehicleAPI.InMemoryService
{
    public class VehicleInMemoryService : IVehicleInMemoryService
    {
        #region Cache Implementation
        private readonly string cacheKey = "Vehicles";
        private static readonly MemoryCache _cache = MemoryCache.Default;

        private void AddCacheData(List<VehicleDto> lstVehicles)
        {
            var cacheItemPolicy = new CacheItemPolicy()
            {
                //Set your Cache expiration.
                AbsoluteExpiration = DateTime.Now.AddDays(1)
            };
            _cache.Add(cacheKey, lstVehicles, cacheItemPolicy);
        }

        private List<VehicleDto> GetCacheData()
        {
            var cacheItemPolicy = new CacheItemPolicy()
            {
                //Set your Cache expiration.
                AbsoluteExpiration = DateTime.Now.AddDays(1)
            };
            var lstVehicles = _cache.Get(cacheKey) as List<VehicleDto>;
            _cache.Remove(cacheKey);

            return lstVehicles;
        }

        #endregion

        public int CreateVehicle(VehicleCreateDto vehicle)
        {
            int result = 0;
            
            try
            {
                var lstVehicles = GetCacheData();
                if (lstVehicles == null)
                {
                    lstVehicles = new List<VehicleDto>();
                }

                int id = lstVehicles.Count() + 1;

                VehicleDto vehicleDto = null;
                if (vehicle != null)
                {
                    vehicleDto = new VehicleDto
                    {
                        Id = id,
                        Make = vehicle.Make,
                        Model = vehicle.Model,
                        Year = vehicle.Year
                    };
                }

                lstVehicles.Add(vehicleDto ?? new VehicleDto());
                AddCacheData(lstVehicles);
                result = 1;
            }
            catch (Exception)
            {
                result = -1;
                throw;
            }

            return result;
        }

        public int DeleteVehicle(int id)
        {
            int result = 0;
            try
            {
                var lstVehicles = GetCacheData();
                var delVehicle = lstVehicles.Where(x => x.Id == id).FirstOrDefault();
                if (delVehicle != null)
                {
                    lstVehicles.Remove(delVehicle);
                    AddCacheData(lstVehicles);
                    result = 1;
                }                
            }
            catch (Exception)
            {
                result = -1;
                throw;
            }

            return result;
        }

        public List<VehicleDto> GetVehicles()
        {
            var cacheItemPolicy = new CacheItemPolicy()
            {
                //Set your Cache expiration.
                AbsoluteExpiration = DateTime.Now.AddDays(1)
            };
            return _cache.Get(cacheKey) as List<VehicleDto>;
        }

        public int UpdateVehicle(VehicleDto vehicle)
        {
            int result = 0;
            try
            {
                var lstVehicles = GetCacheData();
                if (lstVehicles != null)
                {
                    var updateVehicle = lstVehicles.Where(x => x.Id == vehicle.Id).FirstOrDefault();
                    if (updateVehicle != null)
                    {
                        lstVehicles.Remove(updateVehicle);
                        lstVehicles.Add(vehicle);
                        AddCacheData(lstVehicles);
                        result = 1;
                    }
                }                
            }
            catch (Exception)
            {
                result = -1;
                throw;
            }
            return result;
        }
        
        public List<VehicleDto> GetFilteredVehicles(VehicleFilterDto vehicleFilterDto)
        {
            var vehicles = GetVehicles();

            var hasFromAndToDate = vehicleFilterDto.FromDate > 0 && vehicleFilterDto.ToDate > 0;
            
            if (vehicleFilterDto.Id > 0)
                vehicles = vehicles.Where(v => v.Id == vehicleFilterDto.Id).ToList();

            if (!string.IsNullOrEmpty(vehicleFilterDto.Make))
                vehicles = vehicles.Where(v => v.Make == vehicleFilterDto.Make).ToList();

            if (!string.IsNullOrEmpty(vehicleFilterDto.Model))
                vehicles = vehicles.Where(v => v.Make == vehicleFilterDto.Model).ToList();

            if (vehicleFilterDto.FromDate > 0)
                vehicles = vehicles.Where(v => v.Year >= vehicleFilterDto.FromDate).ToList();

            if (vehicleFilterDto.ToDate > 0)
                vehicles = vehicles.Where(v => v.Year <= vehicleFilterDto.FromDate).ToList();


            return vehicles;

        }
    }
}