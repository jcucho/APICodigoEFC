using APICodigoEFC.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APICodigoEFC.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly CodigoContext _context;

        public ProductsController(CodigoContext context)
        {
                _context = context;
        }
        
        [HttpGet]

        public List<Product> Get(string? name)
        {
            //version antigua
            //var query = _context.Products.Where(
            //                                        x => x.Name.Contains(name.Trim())                                                    
            //                                        );
            IQueryable<Product> query = _context.Products;

            if (!string.IsNullOrEmpty(name))
                query = query.Where(x => x.Name.Contains(name));

            return query.ToList();
        }

        [HttpPost]
        public void Insert([FromBody] Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();
        }

        [HttpPut]
        public void Update([FromBody] Product product)
        {
            _context.Entry(product).State = EntityState.Modified;
            _context.SaveChanges();
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var product = _context.Products.Find(id);
            //product.IsActive = false;
            _context.Entry(product).State = EntityState.Modified;
            //_context.Customers.Remove(customer);
            _context.SaveChanges();

        }
    }
}
