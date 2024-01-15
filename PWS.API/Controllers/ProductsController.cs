using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PWS.API.Data;
using PWS.API.Models;
using System.Runtime.InteropServices;

namespace PWS.API.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class ProductsController : Controller
    {
        private PMSDbContext _pmsDbContext;
        public ProductsController(PMSDbContext pmsDbContext)
        {
            this._pmsDbContext = pmsDbContext;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            var products = await _pmsDbContext.Products.ToListAsync();
            return Ok(products);
            
        }
        [HttpPost]
        public async Task<IActionResult> AddProduct([FromBody] Product product)
        {
            product.Id= Guid.NewGuid();
            await _pmsDbContext.Products.AddAsync(product);
            await _pmsDbContext.SaveChangesAsync();
            return Ok(product);

        }
        
        [HttpGet]
        [Route("/api/Products/{id:Guid}")]
        public async Task<IActionResult> GetProduct(Guid id)
        {
            var products = await _pmsDbContext.Products.FirstOrDefaultAsync( x => x.Id ==id );
            if (products == null)
                return NotFound();
            return Ok(products);

        }
        [HttpPut]
        [Route("/api/Products/{id:Guid}")]
        public async Task<IActionResult> UpdateProduct( Guid id , Product updatedproduct)
        {

            var product = await _pmsDbContext.Products.FirstAsync(x => x.Id == id);
            if (product == null)
                return NotFound();
            product.Name = updatedproduct.Name;
            product.Type = updatedproduct.Type;
            product.Color = updatedproduct.Color;
            product.Id = id;
            product.Price = updatedproduct.Price;
            await _pmsDbContext.SaveChangesAsync();
            return Ok(product);

        }
        [HttpDelete]
        [Route("/api/Products/{id:Guid}")]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            var deleteproduct = _pmsDbContext.Products.FirstAsync(x => x.Id == id);
            if (deleteproduct == null)
                return NotFound();
            _pmsDbContext.Products.Remove(await deleteproduct);
            await _pmsDbContext.SaveChangesAsync();
            return Ok(deleteproduct);
                       

        }
           
    }
}
