using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(CatProjService.Startup))]

namespace CatProjService
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureMobileApp(app);
        }
    }
}