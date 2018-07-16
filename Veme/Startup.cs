using Microsoft.Owin;
using Owin;
using System;

[assembly: OwinStartupAttribute(typeof(Veme.Startup))]
namespace Veme
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            try
            {
                ConfigureAuth(app);
            }
            catch(Exception ex)
            {
                throw;
            }
        }
    }
}
