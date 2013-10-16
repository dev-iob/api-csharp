using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace API_IOB
{
    // Observação: Para obter instruções sobre a habilitação do modo clássico IIS6 ou IIS7, 
    // visite http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default", // Nome da rota
                "{controller}/{action}/{id}", // URL com parâmetros
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Padrões de parâmetro
            );

        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            // Usar LocalDB para Entity Framework por padrão
            Database.DefaultConnectionFactory = new SqlConnectionFactory(@"Data Source=(localdb)\v11.0; Integrated Security=True; MultipleActiveResultSets=True");

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
        }
    }
}