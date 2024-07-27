using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shop.Database;
using Shop.Models;
using Shop.Services;

namespace Shop.Controllers
{
    [Route("Users")]
    public class UserController : ControllerBase 
    {
        [HttpGet]
        [Route("")]
        //[Authorize(Roles = "manager")]
        public async Task<ActionResult<List<User>>> Get(
            [FromServices]DataContext context
        )
        {
            var users = await context
                .Users
                .AsNoTracking()
                .ToListAsync();
            return Ok(users);

        }
        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<dynamic>> Authenticate(
            [FromBody]User model,
            [FromServices]DataContext context
        )
        {
            var user = await context
                .Users
                .AsNoTracking()
                .Where(x => x.UserName == model.UserName && x.Password == model.Password)
                .FirstOrDefaultAsync();
            if (user == null)
                return NotFound(new { message = "Usuário ou senha inválidos" });
            var token = TokenService.GenerateToken(user);
            user.Password = "";
            return new
            {
                user = user,
                token = token
            };
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult<User>> GetById(
            int id,
            [FromServices]DataContext context
        )
        {
            var user = await context.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            return Ok(user);
        }

        [HttpPost]
        [Route("")]
        public async Task<ActionResult<User>> Post(
            [FromBody]User model,
            [FromServices]DataContext context
        )
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                context.Users.Add(model);
                await context.SaveChangesAsync();
                return Ok(model);
            }
            catch
            {
                return BadRequest(new { message = "Não foi possível criar o usuário" });
            }
        }

        [HttpPut]
        [Route("{id:int}")]
        [Authorize(Roles = "manager")]
        public async Task<ActionResult<User>> Put(
            int id,
            [FromBody]User model,
            [FromServices]DataContext context
        )
        {
            if (model.Id != id)
                return NotFound(new { message = "Usuário não encontrado" });
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                context.Entry(model).State = EntityState.Modified;
                await context.SaveChangesAsync();
                return Ok(model);
            }
            catch
            {
                return BadRequest(new { message = "Não foi possível atualizar o usuário" });
            }
        }

        [HttpDelete]
        [Route("{id:int}")]
        [Authorize(Roles = "manager")]

        public async Task<ActionResult<User>> Delete(
            int id,
            [FromServices]DataContext context
        )
        {
            var user = await context.Users.FirstOrDefaultAsync(x => x.Id == id);
            if (user == null)
                return NotFound(new { message = "Usuário não encontrado" });
            try
            {
                context.Users.Remove(user);
                await context.SaveChangesAsync();
                return Ok(new { message = "Usuário removido com sucesso" });
            }
            catch
            {
                return BadRequest(new { message = "Não foi possível remover o usuário" });
            }
        }
    }
}