using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AlatorX.Server.Management.Service.Helpers;

namespace AlatorX.Server.Management.Extensions
{
    public static class WebApplicationExtensions
    {
        /// <summary>
        /// Initialize path of environment
        /// </summary>
        /// <param name="app"></param>
        public static void InitEnvironment(this WebApplication app)
        {
            var environment = app.Services.GetService<IWebHostEnvironment>();
            if (environment != null)
            {
                EnvironmentHelper.WebRootPath = environment.WebRootPath;
            }
        }

        /// <summary>
        /// Initialize HttpContext
        /// </summary>
        /// <param name="app"></param>
        public static void InitAccessor(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();

            HttpContextHelper.Accessor = scope.ServiceProvider.GetRequiredService<IHttpContextAccessor>();
        }
    }
}