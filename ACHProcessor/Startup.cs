using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ACHProcessor.Startup))]
namespace ACHProcessor
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
