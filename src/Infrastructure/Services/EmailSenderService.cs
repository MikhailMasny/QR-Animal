﻿using MailKit.Net.Smtp;
using Masny.QRAnimal.Application.Interfaces;
using Masny.QRAnimal.Application.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MimeKit;
using System;
using System.Threading.Tasks;

namespace Masny.QRAnimal.Infrastructure.Services
{
    /// <summary>
    /// Сервис для отправки сообщений по электронной почте.
    /// </summary>
    public class EmailSenderService : IMessageSender
    {
        private readonly ILogger _logger;
        private readonly MailSettings _emailConfiguration;

        /// <summary>
        /// Конструктор с параметрами.
        /// </summary>
        /// <param name="logger">Логгер.</param>
        public EmailSenderService(ILogger<EmailSenderService> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));

            _emailConfiguration = MailKitConfiguration();
        }

        /// <inheritdoc />
        public async Task SendMessageAsync(string recipient, string subject, string body)
        {
            var message = new MimeMessage();

            message.From.Add(new MailboxAddress("QRAnimalApp", _emailConfiguration.EmailAddress));
            message.To.Add(new MailboxAddress("", recipient));
            message.Subject = subject;
            message.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = body
            };

            try
            {
                using var client = new SmtpClient();
                await client.ConnectAsync(_emailConfiguration.Server, _emailConfiguration.Port, false);
                await client.AuthenticateAsync(_emailConfiguration.EmailAddress, _emailConfiguration.Password);
                await client.SendAsync(message);

                await client.DisconnectAsync(true);
            }
            catch (Exception ex)
            {

                _logger.LogError(ex, ex.ToString());
            }
        }

        // UNDONE: Перенести в сервисы DI.
        private MailSettings MailKitConfiguration()
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("mailsettings.json")
                .Build();

            return configuration.Get<MailSettings>();
        }
    }
}
