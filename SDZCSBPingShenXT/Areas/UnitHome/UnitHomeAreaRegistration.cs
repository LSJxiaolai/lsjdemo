using System.Web.Mvc;

namespace SDZCSBPingShenXT.Areas.UnitHome
{
    public class UnitHomeAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "UnitHome";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "UnitHome_default",
                "UnitHome/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}