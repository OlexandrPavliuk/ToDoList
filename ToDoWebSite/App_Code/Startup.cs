using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ToDoWebSite.Startup))]
namespace ToDoWebSite
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
