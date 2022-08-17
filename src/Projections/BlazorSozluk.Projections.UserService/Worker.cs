using BlazorSozluk.Common;
using BlazorSozluk.Common.Events.User;
using BlazorSozluk.Common.Infrastructure;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BlazorSozluk.Projections.UserService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly Services.UserService userService;
        private readonly Services.EmailService emailService;

        public Worker(ILogger<Worker> logger, Services.UserService userService, Services.EmailService emailService)
        {
            _logger = logger;
            this.userService = userService;
            this.emailService = emailService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            QueueFactory.CreateBasicConsumer()
                .EnsureExchange(SozlukConstants.UserExchangeName)
                .EnsureQueue(SozlukConstants.UserEmailExchangedQueueName, SozlukConstants.UserExchangeName)
                .Receive<UserEmailChangedEvent>(user =>
                {
                    //Db Insert
                    var confirmationId = userService.CreateEmailConfirmation(user).GetAwaiter().GetResult();

                    //Generate Link
                    var link = emailService.GenerateConfirmationLink(confirmationId);

                    //Send Email
                    emailService.SendEmail(user.NewEmailAddress, link).GetAwaiter().GetResult();
                })
                .StartConsuming(SozlukConstants.UserEmailExchangedQueueName);
        }
    }
}
