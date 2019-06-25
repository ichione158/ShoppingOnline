using System.Web.Mvc;

namespace ShoppingOnline.Areas.AdministratorCP
{
    public class AdministratorCPAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "AdministratorCP";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "AdministratorCP_default",
                "AdministratorCP/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}