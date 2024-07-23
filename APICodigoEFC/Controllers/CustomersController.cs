using APICodigoEFC.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace APICodigoEFC.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly CodigoContext _context;

        public CustomersController(CodigoContext context)
        {
            _context = context;
        }

        [HttpGet]
        public List<Customer> Get(string? name, string? documentNumber) {
            //version antigua
            //var query = _context.Customers.Where(
            //                                        x=>x.Name.Contains(name.Trim()) 
            //                                        || x.DocumentNumber.Contains(documentNumber.Trim())
            //                                        );
            //return query.ToList();
            //if (!string.IsNullOrEmpty(name))
            //    query = query.Where(x => x.Name.Contains(name));



            //return _context.Customers.ToList();
            IQueryable<Customer> query = _context.Customers.Where(x=>x.IsActive);
            if (!string.IsNullOrEmpty(name))
                query = query.Where(x => x.Name.Contains(name));

            if (!string.IsNullOrEmpty(documentNumber))
                query = query.Where(x => x.DocumentNumber.Contains(documentNumber));

            return query.ToList();

        }

        [HttpPost]
        public void Insert([FromBody] Customer customer) 
        { 
            _context.Customers.Add(customer);
            _context.SaveChanges();
        }

        [HttpPut]
        public void Update([FromBody] Customer customer)
        {
            _context.Entry(customer).State = EntityState.Modified;
            _context.SaveChanges();
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var customer = _context.Customers.Find(id);
            customer.IsActive = false;

            //_context.Customers.Remove(customer);
            _context.SaveChanges();

        }
    }
}
