using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SistemaContabilidad.Startup))]
namespace SistemaContabilidad
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
