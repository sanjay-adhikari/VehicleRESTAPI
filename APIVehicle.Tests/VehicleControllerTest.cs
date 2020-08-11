using Microsoft.VisualStudio.TestTools.UnitTesting;
using VehicleAPI.Controllers;
using VehicleAPI.Models;
using VehicleAPI.VehicleServices;
using Moq;
using System.Web.Http;
using System.Web.Http.Results;
using System.Collections.Generic;
using APIVehicle.Models;

namespace VehicleAPI.Tests.Controllers
{
    [TestClass]
    public class VehicleControllerTest
    {
        #region Dependecies and Constructor for Moq

        private readonly VehicleController _vehicleController;
        private readonly Mock<IVehicleService> mockRepository = new Mock<IVehicleService>();

        public VehicleControllerTest()
        {
            _vehicleController = new VehicleController(mockRepository.Object);
        }
        #endregion

        [TestMethod]
        public void GetVehicleById_WhenValidVehicle()
        {
            // Arrange
            var vehicle = new VehicleDto
            {
                Id = 1,
                Year = 2015,
                Make = "Honda",
                Model = "Accord"
            };

            mockRepository.Setup(x => x.GetVehicleById(vehicle.Id))
                .Returns(vehicle);

            // Act
            IHttpActionResult result = _vehicleController.GetVehicleById(vehicle.Id);
            var contentResult = (result as OkNegotiatedContentResult<VehicleDto>)?.Content;

            // Assert
            Assert.IsNotNull(contentResult);

        }

        [TestMethod]
        public void GetAllVehicles()
        {
            //Arrange
            var vehicles = new List<VehicleDto> {
                new VehicleDto {Id = 1, Year = 2017, Make = "Toyota", Model = "Camry"},
                new VehicleDto {Id = 2, Year = 2019, Make = "Toyota", Model = "Corolla"},
                new VehicleDto {Id = 3, Year = 2016, Make = "Honda", Model = "Accord"},
                new VehicleDto {Id = 3, Year = 2008, Make = "Honda", Model = "Civic"},
                new VehicleDto {Id = 4, Year = 2013, Make = "Ford", Model = "Focus"},
                new VehicleDto {Id = 3, Year = 1999, Make = "Ford", Model = "Fiesta"}
            };

            mockRepository.Setup(x => x.GetAllVehicles())
                .Returns(vehicles);

            // Act
            IHttpActionResult result = _vehicleController.GetAllVehicles();
            var contentResult = (result as OkNegotiatedContentResult<List<VehicleDto>>)?.Content;

            // Assert
            Assert.IsNotNull(contentResult);

        }

        [TestMethod]
        public void CreateVehicle_WhenValidVehicle()
        {
            // Arrange
            var vehicle = new VehicleCreateDto
            {
                Year = 2015,
                Make = "Honda",
                Model = "Accord"
            };

            mockRepository.Setup(x => x.CreateVehicle(It.IsAny<VehicleCreateDto>()))
                .Returns(1);

            //Act
            var result = _vehicleController.CreateVehicle(vehicle);
            var contentResult = (result as OkNegotiatedContentResult<string>)?.Content;

            //Assert
            Assert.AreEqual("Vehicle details added successfully", contentResult);
        }

        [TestMethod]
        public void UpdateVehicle_WhenValidVehicle() 
        {
            // Arrange
            var vehicle = new VehicleDto
            {
                Year = 2015,
                Make = "Honda",
                Model = "Accord"
            };
            mockRepository.Setup(x => x.UpdateVehicle(It.IsAny<VehicleDto>()))
                .Returns(1);

            //Act
            var result = _vehicleController.UpdateVehicle(vehicle);
            var contentResult = (result as OkNegotiatedContentResult<string>)?.Content;

            //Assert
            Assert.AreEqual("Vehicle details updated successfully", contentResult);
        }

        [TestMethod]
        public void DeleteVehicle_WhenExistingVehicleSend()
        {
            //Arrange
            var vehicles = new List<VehicleDto> {
                new VehicleDto {Id = 1, Year = 2017, Make = "Toyota", Model = "Camry"},
                new VehicleDto {Id = 2, Year = 2019, Make = "Toyota", Model = "Corolla"},
                new VehicleDto {Id = 3, Year = 2016, Make = "Honda", Model = "Accord"},
                new VehicleDto {Id = 3, Year = 2008, Make = "Honda", Model = "Civic"},
                new VehicleDto {Id = 4, Year = 2013, Make = "Ford", Model = "Focus"},
                new VehicleDto {Id = 3, Year = 1999, Make = "Ford", Model = "Fiesta"}
            };

            var vehicleToDelete = new VehicleDto { Id = 1, Year = 2017, Make = "Toyota", Model = "Camry" };

            mockRepository.Setup(x => x.DeleteVehicle(It.IsAny<int>()))
                .Returns(1);

            //Act
            var result = _vehicleController.DeleteVehicle(vehicleToDelete.Id);
            var contentResult = (result as OkNegotiatedContentResult<string>)?.Content;

            //Assert
            Assert.AreEqual("Vehicle details deleted successfully", contentResult);
        }
    }
}
