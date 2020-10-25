using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using NextToMe.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.DependencyInjection;
using NextToMe.Database.Entities;

namespace NextToMe.Services
{
    public class MessageDeleteService : BackgroundService
    {
        private readonly int _secondsToDelete = 60;
        private readonly IServiceProvider _serviceProvider;
        public MessageDeleteService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {

            while (!stoppingToken.IsCancellationRequested)
            {
                DeleteOldMessagesFromDb();
                await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
            }
        }

        private void DeleteOldMessagesFromDb()
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                var currentTime = DateTime.UtcNow;

                var messages = context.Messages.Where(x =>
                    MySqlDbFunctionsExtensions.DateDiffSecond(null, x.CreatedAt, currentTime) > _secondsToDelete);

                context.RemoveRange(messages);
                context.SaveChanges();
            }
        }
    }
}
