using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Owin;
using Owin;
using Microsoft.Owin.Cors;

[assembly: OwinStartup(typeof(CFenix_Dev.Startup))]

namespace CFenix_Dev
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // This must come first to intercept the /Token requests
            app.UseCors(CorsOptions.AllowAll);

            ConfigureAuth(app);
        }
    }
}
