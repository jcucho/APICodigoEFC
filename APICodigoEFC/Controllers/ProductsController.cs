using APICodigoEFC.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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

            var query = _context.Products.Where(
                                                    x => x.Name.Contains(name.Trim())                                                    
                                                    );
            return query.ToList();
            //if (!string.IsNullOrEmpty(name))
            //    query = query.Where(x => x.Name.Contains(name));



            //return _context.Customers.ToList();
        }

        [HttpPost]
        public void Insert([FromBody] Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();
        }
    }
}
