using System.Web.Http;

namespace APIVehicle
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            UnityConfig.RegisterComponents(); //Dependency Injection
        }
    }
}
