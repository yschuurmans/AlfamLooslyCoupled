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

namespace Customer.Controllers 
{
    [ApiController]
    [Route("[controller]")]
    public class CustomersController : ControllerBase 
    {
        private CustomerDBContext _dbContext;
        private RabbitMQService _publisher;

        public CustomersController(CustomerDBContext dbContext, RabbitMQService publisher) 
        {
            _dbContext = dbContext;
            _publisher = publisher;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            return Ok(await _dbContext.Customers.ToListAsync());
        }

        [HttpGet]
        [Route("{customerNumber}", Name = "GetByCustomerNumber")]
        public async Task<IActionResult> GetByCustomerNumber(string customerNumber)
        {
            var customer = await _dbContext.Customers.FirstOrDefaultAsync(c => c.CustomerNumber == customerNumber);
            if (customer == null)
            {
                return NotFound();
            }
            return Ok(customer);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCustomer([FromBody] CreateCustomer command)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // insert customer
                    Model.Customer customer = command.MapToCustomer();
                    _dbContext.Customers.Add(customer);
                    await _dbContext.SaveChangesAsync();

                    // send event
                    CustomerCreated e = command.MapToCustomerCreated();
                    _publisher.PublishObject(e);

                    // return result
                    return CreatedAtRoute("GetByCustomerNumber", new { customerNumber = customer.CustomerNumber }, customer);
                }
                return BadRequest();
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError("", "Unable to save changes. " +
                    "Try again, and if the problem persists " +
                    "see your system administrator.");
                return StatusCode(StatusCodes.Status500InternalServerError);
                throw;
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromBody] UpdateCustomer command)
        {
            Model.Customer customer = await _dbContext.Customers.FirstOrDefaultAsync(c => c.CustomerNumber == command.CustomerNumber);
            if (customer == null)
            {
                return NotFound();
            }
            try
            {
                if (ModelState.IsValid)
                {
                    // update customer
                    customer.Firstname = command.Firstname;
                    customer.Lastname = command.Lastname;
                    customer.PostalCode = command.PostalCode;
                    customer.City = command.City;
                    customer.Email = command.Email;
                    await _dbContext.SaveChangesAsync();

                    // send event
                    CustomerUpdated e = command.MapToCustomerUpdated();
                    _publisher.PublishObject(e);

                    // return result
                    return CreatedAtRoute("GetByCustomerNumber", new { customerNumber = customer.CustomerNumber }, customer);
                }
                return BadRequest();
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError("", "Unable to save changes. " +
                    "Try again, and if the problem persists " +
                    "see your system administrator.");
                return StatusCode(StatusCodes.Status500InternalServerError);
                throw;
            }
        }
    }
}