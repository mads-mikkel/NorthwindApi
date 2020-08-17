using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NorthwindApi.DataAccess;
using NorthwindApi.Entities;

namespace NorthwindApi.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        [HttpGet]
        public List<Customer> GetAll()
        {
            return new Repository().GetAllCustomers();
        }
    }
}
