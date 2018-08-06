

namespace Publinter.App_Start
{
    using System;
    using Microsoft.Owin;
    using Microsoft.Owin.Security.Cookies;
    using Owin;


    public partial class Startup
    {
        public void ConfigureAuth(IAppBuilder app)
        {
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                ExpireTimeSpan = TimeSpan.FromHours(12),
                AuthenticationType = "PublinterCookie",
                LoginPath = new PathString("/Account/Login")
            });
        }

        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
