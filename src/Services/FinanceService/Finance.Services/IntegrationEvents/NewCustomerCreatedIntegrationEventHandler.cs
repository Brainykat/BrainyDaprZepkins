using EventBus.Dtos;
using EventBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Finance.Services.Interfaces;
using Microsoft.Extensions.Logging;
using System.Reflection;
using WebHosts;
using FinanceDtos;

namespace Finance.Services.IntegrationEvents
{
    public class NewCustomerCreatedIntegrationEventHandler
     : IIntegrationEventHandler<CustomerEBDto>
    {
        private readonly IAccountService _accountService;
        private readonly ILogger<NewCustomerCreatedIntegrationEventHandler> _logger;
        public NewCustomerCreatedIntegrationEventHandler(
            IAccountService accountService, ILogger<NewCustomerCreatedIntegrationEventHandler> logger)
        {
            _accountService = accountService;
            _logger = logger;
        }

        public async Task Handle(CustomerEBDto @event)
        {
            try
            {
                Random rnd = new();
                var accDto = new AccountDto
                {
                    AccountNumber = rnd.Next().ToString(),
                    CustomerId = @event.customerId,
                    Name = @event.Name,
                    TransactionLimit = 33000,
                    AccountBearerType = SharedBase.Enums.AccountBearerType.Customer
                };
                var res = await _accountService.Add(accDto,Guid.NewGuid());
                return; // Task.CompletedTask;
            }
            catch (Exception ex)
            {
                LogHelper.LogError(_logger, ex, MethodBase.GetCurrentMethod());
                return; // Task.FromException(ex);
            }
            
        }
        public static string Generate()
        {
            string[] randomChars = new[]
            {
                "ABCDEFGHJKLMNOPQRSTUVWXYZ",    // uppercase 
				"abcdefghijkmnopqrstuvwxyz",    // lowercase
				"0123456789",                   // digits
				"!@$?_-"                        // non-alphanumeric
			};
            Random rand = new Random(Environment.TickCount);
            List<char> chars = new List<char>();

            chars.Insert(rand.Next(0, chars.Count),
                randomChars[0][rand.Next(0, randomChars[0].Length)]);

            chars.Insert(rand.Next(0, chars.Count),
                randomChars[1][rand.Next(0, randomChars[1].Length)]);

            chars.Insert(rand.Next(0, chars.Count),
                randomChars[2][rand.Next(0, randomChars[2].Length)]);

            chars.Insert(rand.Next(0, chars.Count),
                randomChars[3][rand.Next(0, randomChars[3].Length)]);

            for (int i = chars.Count; i < 8
                || chars.Distinct().Count() < 1; i++)
            {
                string rcs = randomChars[rand.Next(0, randomChars.Length)];
                chars.Insert(rand.Next(0, chars.Count),
                    rcs[rand.Next(0, rcs.Length)]);
            }
            return new string(chars.ToArray());
        }
    }
}
