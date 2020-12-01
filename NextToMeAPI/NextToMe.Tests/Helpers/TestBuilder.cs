using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NextToMe.Database;
using Microsoft.Extensions.Hosting;
using Moq;
using NextToMe.Services;

namespace NextToMe.Tests.Helpers
{
    public static class TestBuilder
    {
        private const string _controllersAssembly = "NextToMe.API";

        public static IHostBuilder ConfigureTestHost(this IHostBuilder builder)
        {
            return builder.ConfigureServices((context, services) =>
            {
                Assembly assembly = Assembly.Load(_controllersAssembly);
                services.AddMvc().AddApplicationPart(assembly).AddControllersAsServices();

                var mock = new Mock<IHttpContextAccessor>();
                mock.Setup(m => m.HttpContext.User.Identity.Name)
                    .Returns(TestBase.TestUserName);
                services.AddTransient<IHttpContextAccessor>(x => mock.Object);

                services.AddSingleton<MessageDeleteService>();
            });
        }
    }
}
