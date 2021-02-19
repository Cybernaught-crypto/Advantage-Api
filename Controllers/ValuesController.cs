using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace Advantage.Api.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        //Get api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2", "Value3" };
        }

        //Get api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "values from request";
        }

        //Post api/values
        public void Post([FromBody] string value)
        {

        }

        //put api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {

        }

        // delete api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {

        }

    }

}