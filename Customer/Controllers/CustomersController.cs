using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Customer.Commands;
using Customer.Events;
using Customer.Infra.Data;
using Customer.Infra.RabbitMQ;
using Customer.Mappers;
using System;
using Contract.Events;

namespace Customer.Controllers 
{
    [ApiController]
    [Route("[controller]")]
    public class CustomersController : ControllerBase 
    {
        private CustomerDBContext _dbContext;
        private RabbitMQService _rabbitMQService;

        public CustomersController(CustomerDBContext dbContext, RabbitMQService publisher) 
        {
            _dbContext = dbContext;
            _rabbitMQService = publisher;
        }

        [HttpGet("StartListening")]
        public string StartListening()
        {
            Console.WriteLine($"Started listening for new Contracts!");
            _rabbitMQService.RegisterConsumer<ContractCreated>((contract) =>
            {
                Console.WriteLine($"New contract has been added! \n ContractNumber: {contract.ContractNumber}\n");
            });

            return "OK";
        }
    }
}