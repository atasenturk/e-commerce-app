using E_CommerceApp.Api.Domain.Models;
using E_CommerceApp.Common;
using E_CommerceApp.Common.Events;
using E_CommerceApp.Common.Infrastructure;

namespace ECommerce.Projections.UserService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IConfiguration _configuration;
        public Worker(ILogger<Worker> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var connStr = _configuration.GetConnectionString("SqlServer");
            var userService = new Services.UserService(connStr);
            RabbitMQFactory.CreateBasicConsumer()
                .EnsureExchange(AppConstants.UserExchangeName)
                .EnsureQueue(AppConstants.UserCreatedQueueName, AppConstants.UserExchangeName)
                .Receive<User>(user =>
                {
                    userService.CreateUser(user).GetAwaiter().GetResult();
                    _logger.LogInformation("User Created!");
                })
                .StartConsuming(AppConstants.UserCreatedQueueName);

            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}
