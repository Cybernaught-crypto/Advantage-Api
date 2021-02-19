using System.Linq;
using Advantage.Api.Models;
using Advantage.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace Advantage.API.Controllers
{
    [Route("api/[controller]")]
    public class CustomerController : Controller
    {
        private readonly ApiContext _ctx;
        public CustomerController(ApiContext ctx)
        {
            _ctx = ctx;
        }

        //Get api/customer
        [HttpGet]
        public IActionResult Get()
        {
            // retrieving the data via the api.
            var data = _ctx.Customers.OrderBy(c => c.Id);

            return Ok(data);

        }

        //Get api/customer/5
        [HttpGet("{id}", Name = "GetCustomer")]
        public IActionResult Get(int id)
        {
            // retrieving the data via the api.
            var customer = _ctx.Customers.Find(id);

            return Ok(customer);

        }

        [HttpPost]
        public IActionResult Post([FromBody] Customer customer)
        {
            if (customer == null)
            {
                return BadRequest();
            }

            _ctx.Customers.Add(customer);
            _ctx.SaveChanges();

            return CreatedAtRoute("GetCustomer", new { id = customer.Id }, customer);
        }


    }
}