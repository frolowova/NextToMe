using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using NextToMe.API;
using NextToMe.API.Controllers;
using NUnit.Framework;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using NextToMe.Common;
using NextToMe.Database;
using NextToMe.Database.Entities;
using NextToMe.Services;

namespace NextToMe.Tests.Helpers
{
    [TestFixture]
    public class TestBase
    {
        public const string TestUserName = "test";

        public const int DeleteMessageDelayInSeconds = 1;

        private IHost _host;

        protected MessagesController GetMessagesController()
            => _host.Services.GetRequiredService<MessagesController>();

        [OneTimeSetUp]
        public async Task Init()
        {
            _host = Program.CreateHostBuilder(null).ConfigureTestHost().Build();
            using (IServiceScope scope = _host.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                await db.Database.EnsureCreatedAsync();

                var settings = scope.ServiceProvider.GetRequiredService<IOptions<AppSettings>>();
                settings.Value.DeleteMessageServiceDelayInSeconds = DeleteMessageDelayInSeconds;


                var messageDeleteService = scope.ServiceProvider.GetRequiredService<MessageDeleteService>();
                await messageDeleteService.StartAsync(new CancellationToken());

                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
                await userManager.CreateAsync(new User { UserName = TestUserName, Email = TestUserName });
            }
        }

        [SetUp]
        public async Task InitTest()
        {
            using (IServiceScope scope = _host.Services.CreateScope())
            {
                var dbContext = _host.Services.GetRequiredService<ApplicationDbContext>();
                dbContext.Messages.RemoveRange(dbContext.Messages);
                await dbContext.SaveChangesAsync();
            }
        }
    }
}
