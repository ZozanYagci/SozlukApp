using SozlukApp.Common.Events.Entry;
using SozlukApp.Common.Infrastructure;
using SozlukApp.Common;
using SozlukApp.Common.Events.User;
using SozlukApp.Projections.UserService.Services;

namespace SozlukApp.Projections.UserService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly Services.UserService userService;
        private readonly EmailService emailService;

        public Worker(ILogger<Worker> logger, Services.UserService userService, EmailService emailService)
        {
            _logger = logger;
            this.userService = userService;
            this.emailService = emailService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            QueueFactory.CreateBasicConsumer()
                .EnsureExchange(SozlukConstants.UserExchangeName)
                .EnsureQueue(SozlukConstants.UserEmailChangeQueueName, SozlukConstants.UserExchangeName)
                .Receive<UserEmailChangedEvent>(user =>
                {
                    //DB Insert
                    var confirmationId=userService.CreateEmailConfirmation(user).GetAwaiter().GetResult();

                    //Generate Link
                    var link = emailService.GenerateConfirmationLink(confirmationId);

                    //Send Email
                    emailService.SendEmail(user.NewEmailAddress, link).GetAwaiter().GetResult();
                })
                .StartConsuming(SozlukConstants.UserEmailChangeQueueName);
        }
        }
    }
