using Microsoft.AspNetCore.Mvc;
using Shop.Database;
using Shop.Models;

namespace Shop.Controllers
{
    public class HomeController : ControllerBase
    {
        [HttpGet]
        [Route("")]
        public async Task<ActionResult<dynamic>> Get(
            [FromServices]DataContext context,
            [FromServices]User user
        )
        {
        var employee = new User { Id = 1, UserName = "ronaldo", Password = "ronaldo2024", Role = "employee" };
        var manager = new User { Id = 2, UserName = "maria", Password = "maria2024", Role = "manager" };
        var category = new Category { Id = 1, Title = "Informática" };
        var product = new Product { Id = 1, Category = category, Title = "Mouse", Price = 299, Description = "Mouse Gamer" };
        context.Users.Add(employee);
        context.Users.Add(manager);
        context.Categories.Add(category);
        context.Products.Add(product);
        await context.SaveChangesAsync();
        return Ok(new { message = "Dados configurados" });
        }
    }
} 
