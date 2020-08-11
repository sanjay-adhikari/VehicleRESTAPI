using System.Web.Http;
using Unity;
using Unity.WebApi;
using VehicleAPI.VehicleServices;
using VehicleAPI.InMemoryService;

namespace APIVehicle
{
	public static class UnityConfig
	{
		public static void RegisterComponents()
		{
			var container = new UnityContainer();
			
			GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);

			container.RegisterType<IVehicleService, VehicleService>();

			container.RegisterType<IVehicleInMemoryService, VehicleInMemoryService>();

		}
	}
}