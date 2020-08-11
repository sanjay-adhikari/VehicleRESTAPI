using APIVehicle.Models;
using System.Web.Http;
using System.Web.Http.Description;
using VehicleAPI.VehicleServices;
using VehicleAPI.Models;
using APIVehicle.VehicleServices;

namespace VehicleAPI.Controllers
{
    public class VehicleController : ApiController
    {
        #region Dependencies and Constructor
        private readonly IVehicleService _iVehicleService;

        public VehicleController(IVehicleService ServiceBuilder)
        {
            _iVehicleService = ServiceBuilder;
        }
        #endregion

        // GET: api/VehicleDto
        [ResponseType(typeof(VehicleDto))]
        [HttpGet]
        public IHttpActionResult GetAllVehicles()
        {
            var vehicles = _iVehicleService.GetAllVehicles();
            if (vehicles == null)
            {
                return Ok("Vehicle details are not available");
            }            
            return Ok(vehicles);
        }

        // GET: api/VehicleDto/5
        [ResponseType(typeof(VehicleDto))]
        [HttpGet]
        public IHttpActionResult GetVehicleById(int id)
        {
            var vehicle = _iVehicleService.GetVehicleById(id);
            if (vehicle == null)
            {
                return BadRequest("Vehicle is not available");
            }
            return Ok(vehicle);
        }

        // POST: api/VehicleDto
        [HttpPost]
        public IHttpActionResult CreateVehicle(VehicleCreateDto vehicle)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Year, Make and Model are required. Year must be in between 1950 and 2050.");
            }
            var result = _iVehicleService.CreateVehicle(vehicle);
            switch (result)
            {
                case -1:
                    return Ok("System temporarily disconnected. Please try again");
                case 0:
                    return Ok("Vehicle couldn't be added. Please try again");
                case 1:
                    return Ok("Vehicle details added successfully");
                default:
                    break;
            }
            return Ok(result);
        }

        // PUT: api/VehicleDto/5
        [HttpPut]
        public IHttpActionResult UpdateVehicle(VehicleDto vehicle)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Year, Make and Model are required. Year must be in between 1950 and 2050.");
            }
            var result = _iVehicleService.UpdateVehicle(vehicle);
            switch (result)
            {
                case -1:
                    return Ok("System temporarily disconnected. Please try again");
                case 0:
                    return Ok("Vehicle details couldn't be updated. Please try again");
                case 1:
                    return Ok("Vehicle details updated successfully");
                default:
                    break;
            }
            return Ok(result);
        }

        // DELETE: api/VehicleDto/5
        [HttpDelete]
        public IHttpActionResult DeleteVehicle(int id)
        {
            var result = _iVehicleService.DeleteVehicle(id);
            switch (result)
            {
                case -1:
                    return Ok("System temporarily disconnected. Please try again");
                case 0:
                    return Ok("Vehicle couldn't be deleted. Please try again");
                case 1:
                    return Ok("Vehicle details deleted successfully");
                default:
                    break;
            }
            return Ok(result);
        }

        [HttpPost]
        [Route("api/vehicle/filterVehicles")]
        public IHttpActionResult GetVehicleFilterReport([FromBody] VehicleFilterDto vehicleFilter)
        {
            var result = _iVehicleService.GetFilteredVehicles(vehicleFilter);
            return Ok(result);
            
        }
    }
}
