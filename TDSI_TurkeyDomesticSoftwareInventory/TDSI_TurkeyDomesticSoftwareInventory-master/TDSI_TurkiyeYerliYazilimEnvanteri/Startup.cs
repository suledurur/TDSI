using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TDSI_TurkiyeYerliYazilimEnvanteri.Startup))]
namespace TDSI_TurkiyeYerliYazilimEnvanteri
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //ConfigureAuth(app);
        }
    }
}
