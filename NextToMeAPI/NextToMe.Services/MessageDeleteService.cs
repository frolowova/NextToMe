using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NextToMe.Database;
using NextToMe.Database.Entities;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NextToMe.Services
{
    public class MessageDeleteService : BackgroundService
    {
        private static readonly TimeSpan _delayTime = TimeSpan.FromMinutes(1);
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public MessageDeleteService(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {

            while (!stoppingToken.IsCancellationRequested)
            {
                await DeleteOldMessagesFromDb();
                await Task.Delay(_delayTime, stoppingToken);
            }
        }

        private async Task DeleteOldMessagesFromDb()
        {
            using (IServiceScope scope = _serviceScopeFactory.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                var currentTime = DateTime.UtcNow;

                IQueryable<Message> messages = context.Messages.Where(x => x.DeleteAt != null && x.DeleteAt < currentTime);

                context.RemoveRange(messages);
                await context.SaveChangesAsync();
            }
        }
    }
}
