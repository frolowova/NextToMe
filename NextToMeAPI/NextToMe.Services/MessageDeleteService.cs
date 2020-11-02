using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NextToMe.Database;
using NextToMe.Database.Entities;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using NextToMe.Common;

namespace NextToMe.Services
{
    public class MessageDeleteService : BackgroundService
    {
        private readonly TimeSpan _delayTime;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public MessageDeleteService(IServiceScopeFactory serviceScopeFactory, IOptions<AppSettings> settings)
        {
            _serviceScopeFactory = serviceScopeFactory;
            _delayTime = TimeSpan.FromSeconds(settings.Value.DeleteMessageServiceDelayInSeconds);
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
