using System.Diagnostics.Metrics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication3.Applications;
using WebApplication3.DTOs;
using WebApplication3.Models;

namespace WebApplication3.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public CustomerController(ApplicationDbContext context)
        {
            _context = context;
        }

        //[HttpGet("test")]
        //public IActionResult Test()
        //{
        //return Ok("API is working");
        //}

        [HttpGet("GetAllCustomers")]
        public async Task<ActionResult<IEnumerable<Customers>>> GetCustomer()
        {
            var result = await _context.Customer.Where(c => c.IsActive).ToListAsync();
            return Ok(result);
        }

        [HttpGet("GetCustomerById/{id}")]
        public async Task<ActionResult<Customers>> GetCustomer(Guid id)
        {
            var result = await _context.Customer.FirstOrDefaultAsync(c => c.CustomerId == id && c.IsActive);
            if (result == null)
            {
                return NotFound("That customer id is not have");
            }
            return Ok(result);
        }

        [HttpPost("CreateCustomer")]
        public async Task<ActionResult<Customers>> CreateCustomer([FromBody] CustomerDto customerDto)
        {
            var customer = new Customers
            {
                FirstName = customerDto.FirstName,
                LastName = customerDto.LastName,
                PhoneNumber = customerDto.PhoneNumber,
                Age = customerDto.Age,
                Country = customerDto.Country,
                State = customerDto.State,
                City = customerDto.City,
                CreatedOn = DateTimeOffset.Now.DateTime,
                ModifiedOn = null
            };

            await _context.Customer.AddAsync(customer);
            await _context.SaveChangesAsync();
            return customer;
        }

        [HttpPut("UpdateCustomer")]
        public async  Task<IActionResult> UpdateCustomer(Guid id,[FromBody] CustomerDto customerDto)
        {
            var existingCustomer = await _context.Customer.FindAsync(id);
            if (existingCustomer == null || !existingCustomer.IsActive)
            {
                return NotFound("Customers not found or inactive.");
            }
            if (!string.IsNullOrWhiteSpace(customerDto.FirstName) && customerDto.FirstName != "string")
                existingCustomer.FirstName = customerDto.FirstName;

            if (!string.IsNullOrWhiteSpace(customerDto.LastName) && customerDto.LastName != "string")
                existingCustomer.LastName = customerDto.LastName;

            if (!string.IsNullOrWhiteSpace(customerDto.PhoneNumber) && customerDto.PhoneNumber != "string")
                existingCustomer.PhoneNumber = customerDto.PhoneNumber;

            if (customerDto.Age > 0)
                existingCustomer.Age = customerDto.Age;

            if (!string.IsNullOrWhiteSpace(customerDto.Country) && customerDto.Country != "string")
                existingCustomer.Country = customerDto.Country;

            if (!string.IsNullOrWhiteSpace(customerDto.State) && customerDto.State != "string")
                existingCustomer.State = customerDto.State;

            if (!string.IsNullOrWhiteSpace(customerDto.City) && customerDto.City != "string")
                existingCustomer.City = customerDto.City;
            existingCustomer.ModifiedOn = DateTime.Now;

            await _context.SaveChangesAsync();
            return Ok(existingCustomer);
        }

        [HttpDelete("Deletecustomer/{id}")]
        public async Task<IActionResult> DeleteCustomer(Guid id)
        {
            var customer = await _context.Customer.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }
            else
            {
                customer.IsActive = false;
            }
            //_context.Customer.Remove(customer);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Deleted Successfully" });
        }


    }
}
