
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Contract.Commands;
using Contract.Events;
using Contract.Infra.Data;
using System;
using Contract.Mappers;

namespace Contract.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ContractController : ControllerBase
    {
        private RabbitMQService _rabbitMQService;

        public ContractController(RabbitMQService rabbitMQService)
        {
            _rabbitMQService = rabbitMQService;
        }

        [HttpGet("CreateContract")]
        public string CreateContract()
        {
            _rabbitMQService.PublishObject(new ContractCreated(new Guid(), "12345", "Contract", "NL00IBAN01234567", 100.0, "1"));

            return "OK"; 
        }
    }
}