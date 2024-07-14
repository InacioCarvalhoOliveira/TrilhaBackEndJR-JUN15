using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shop.Database;
using Shop.Models;
namespace Shop.Controllers
{
    [Route("Products")]
    public class ProductController : ControllerBase
    {
        [HttpGet]
        [Route("")]
        public async Task<ActionResult<List<Product>>> Get(
            [FromServices] DataContext context)
        {
            var products = await context
                .Products
                .Include(x => x.Category)
                .AsNoTracking()
                .ToListAsync();
            return Ok(products);
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult<Product>> GetbyId(int id,
            [FromServices] DataContext context)
        {
            var products = await context.Products
                .AsNoTracking()
                .Include(x => x.Category)
                .FirstOrDefaultAsync(x => x.Id == id);
            return Ok(products);
        }

        [HttpGet]
        [Route("products/{id:int}")]
        public async Task<ActionResult<List<Product>>> GetByCategory(int id,
            [FromServices] DataContext context)
        {
            var products = await context.Products
                .Include(x => x.Category)
                .AsNoTracking()
                .Where(x => x.CategoryId == id).ToListAsync();
            return Ok(products);
        }

        [HttpPost]
        [Route("")]
        public async Task<ActionResult<Product>> Post(
            [FromBody] Product model,
            [FromServices] DataContext context)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                context.Products.Add(model);
                await context.SaveChangesAsync();
                return Ok(model);
            }catch{
                return BadRequest(new { message = "Não foi possível criar o produto" });
            }
        }
    }
}