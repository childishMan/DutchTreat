using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace DutchTreat.Services
{
    public class TestMailService : IMailService
    {
        private readonly ILogger<TestMailService> _logger;

        public TestMailService(ILogger<TestMailService> logger)
        {
            _logger = logger;
        }

        public void SendMessage(string to, string from, string subject, string message)
        {
            _logger.LogInformation($"Sending message from {from} to {to} with subject {subject} and message {message}");
        }
    }
}
