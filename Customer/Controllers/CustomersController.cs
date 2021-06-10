
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Customer.Commands;
using Customer.Events;
using Customer.Infra.Data;
using Customer.Mappers;
using System;

namespace Customer.Controllers 
{
    public class CustomersController : ControllerBase 
    {
        private CustomerDBContext _dbContext;

        public CustomersController(CustomerDBContext dbContext) 
        {
            _dbContext = dbContext;
        }

        public async Task<IActionResult> GetAllAsync()
        {
            return Ok(await _dbContext.Customers.ToListAsync());
        }

        [HttpGet]
        [Route("{customerId}", Name = "GetByCustomerId")]
        public async Task<IActionResult> GetByCustomerId(string customerNumber)
        {
            var customer = await _dbContext.Customers.FirstOrDefaultAsync(c => c.CustomerNumber == customerNumber);
            if (customer == null)
            {
                return NotFound();
            }
            return Ok(customer);
        }

        [HttpPost]
        public async Task<IActionResult> RegisterAsync([FromBody] CreateCustomer command)
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
                    //await _messagePublisher.PublishMessageAsync(e.MessageType, e , "");

                    // return result
                    return CreatedAtRoute("GetByCustomerId", new { customerNumber = customer.CustomerNumber }, customer);
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
            try
            {
                if (ModelState.IsValid)
                {
                    // insert customer
                    Model.Customer customer = command.MapToCustomer();
                    _dbContext.Customers.Add(customer);
                    await _dbContext.SaveChangesAsync();

                    // send event
                    CustomerUpdated e = command.MapToCustomerUpdated();
                    //await _messagePublisher.PublishMessageAsync(e.MessageType, e , "");

                    // return result
                    return CreatedAtRoute("GetByCustomerId", new { customerNumber = customer.CustomerNumber }, customer);
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