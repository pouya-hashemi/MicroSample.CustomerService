using MicroSample.CustomerService.Dtos;
using MicroSample.CustomerService.Entities;
using MicroSample.CustomerService.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MicroSample.CustomerService.Controllers
{
    [Route("CustomerService/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly IAppDbContext _appDbContext;

        public CustomerController(IAppDbContext appDbContext)
        {
            this._appDbContext = appDbContext;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _appDbContext.Customers.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var customer = await _appDbContext.Customers.FirstOrDefaultAsync(f => f.Id == id);

            if (customer is null)
                return NotFound();

            return Ok(customer);
        }

        [HttpPost]
        public async Task<IActionResult> Post(CustomerCreateDto customerCreateDto)
        {
            if (customerCreateDto == null ||
                String.IsNullOrWhiteSpace(customerCreateDto.Name) ||
               String.IsNullOrWhiteSpace(customerCreateDto.Email))
            {
                return BadRequest("Fill the required fields");
            }
            var customer = new Customer()
            {
                Name = customerCreateDto.Name,
                Email = customerCreateDto.Email,
            };
            await _appDbContext.Customers.AddAsync(customer);
            await _appDbContext.SaveChangesAsync();

            return CreatedAtAction("GetById", new { Id = customer.Id }, customer);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, CustomerEditDto customerEditDto)
        {
            if (customerEditDto == null ||
               String.IsNullOrWhiteSpace(customerEditDto.Name)||
               String.IsNullOrWhiteSpace(customerEditDto.Email))
            {
                return BadRequest("Fill the required fields");
            }

            var customer = await _appDbContext.Customers.FirstOrDefaultAsync(f => f.Id == id);

            if (customer is null)
                return NotFound();

            customer.Name = customerEditDto.Name;
            customer.Email= customerEditDto.Email;

            await _appDbContext.SaveChangesAsync();

            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var customer = await _appDbContext.Customers.FirstOrDefaultAsync(f => f.Id == id);

            if (customer is null)
                return NotFound();

            _appDbContext.Customers.Remove(customer);
            await _appDbContext.SaveChangesAsync();

            return NoContent();
        }
    }
}
