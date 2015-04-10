using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Community_Medicine.Startup))]
namespace Community_Medicine
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
