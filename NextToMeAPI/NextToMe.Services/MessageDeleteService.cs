using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NextToMe.Database;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NextToMe.Services
{
    public class MessageDeleteService : BackgroundService
    {
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

                var messages = context.Messages.Where(x => x.DeleteAt != null && x.DeleteAt < currentTime);

                context.RemoveRange(messages);
                context.SaveChanges();
            }
        }
    }
}
