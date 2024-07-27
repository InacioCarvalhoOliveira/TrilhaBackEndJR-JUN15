using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shop.Database;
using Shop.Models;
namespace Shop.Controllers
{
    [Route("Categories")] 
    public class CategoryController : ControllerBase
    {
        [HttpGet]
        [Route("")]
        public async Task<ActionResult<List<Category>>> Get(
            [FromServices]DataContext context
        )
        {
            var categories = await context.Categories.AsNoTracking().ToListAsync();
            
            try
            {
                return Ok(categories);
            }catch{
                return BadRequest(new {message = "Não foi possível listar as categorias"});
            }
        }
        [HttpGet]
        [Route("{id:int}")]
        
        public async Task<ActionResult<Category>> GetById(int id,
        [FromServices]DataContext context)
        {
            var category = await context.Categories.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            if (category == null)
                return NotFound(new {message = "Categoria não encontrada"});
            try
            {
                return Ok(category);
            }catch{
                return BadRequest(new {message = "Não foi possível listar a categoria"});
            }
        }           
        
        [HttpPost]
        [Route("")]
        
        public async Task<ActionResult<Category>> Post(
            [FromBody]Category model,
            [FromServices]DataContext context)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            
          try
          {
            context.Categories.Add(model);
            await context.SaveChangesAsync();
            return Ok(model);
          }catch{
                return BadRequest(new {message = "Não foi possível criar a categoria"});
          }
        }
        [HttpPut]
        [Route("{id:int}")]
        
        public async Task<ActionResult<Category>> Put(
            int id,
            [FromBody]Category model,
            [FromServices]DataContext context)
        {
            if (id != model.Id)
            {
                return NotFound(new{message = "Category not found"});
            }if (!ModelState.IsValid)
                return BadRequest(ModelState);
            
            try
            {
                context.Entry<Category>(model).State = EntityState.Modified;
                await context.SaveChangesAsync();
                return model;
            }catch(DbUpdateConcurrencyException)
            {
                return BadRequest(new {message = "Este registro já foi atualizado"});
            }catch(Exception)
            {
                return BadRequest(new {message = "Não foi possível atualizar a categoria"});
            }
        }
        
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<ActionResult<Category>> Delete(
            int id,
            [FromServices]DataContext context)
        
        {
            var category = await context.Categories.FirstOrDefaultAsync(x => x.Id == id);
            if (category == null)
                return NotFound(new {message = "Categoria não encontrada"});
            try
            {            
                context.Categories.Remove(category);
                await context.SaveChangesAsync();
                return Ok(new {message = "Categoria removida com sucesso"});            
            }catch (System.Exception)
            {
                return BadRequest(new {message = "Não foi possível remover a categoria"});
            }
        }
    }
}
