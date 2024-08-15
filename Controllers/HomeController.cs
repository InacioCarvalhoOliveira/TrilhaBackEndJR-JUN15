using Microsoft.AspNetCore.Mvc;
using Shop.Database;
using Shop.Models;

namespace Shop.Controllers
{
    [Route("v1")]
    public class HomeController : ControllerBase
    {
        [HttpGet]
        [Route("")]
        public async Task<ActionResult<dynamic>> Get(
            [FromServices]DataContext context
        )
        {
        var employee = new User { Id = 4, UserName = "ronaldo12", Password = "ronaldo2024", Role = "employee" };
        var manager = new User { Id = 5, UserName = "maria12", Password = "maria2024", Role = "manager" };
        var category = new Category { Id = 7, Title = "Informática" };
        var product = new Product { Id = 7, Category = category, Title = "Mouse", Price = 299, Description = "Mouse Gamer" };
        context.Users.Add(employee);
        context.Users.Add(manager);
        context.Categories.Add(category);
        context.Products.Add(product);
        await context.SaveChangesAsync();
        return Ok(new { message = "Dados configurados" });
        }
    }
} 
