using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shop.Database;
using Shop.Models;
using Shop.Services;

namespace Shop.Controllers
{

    public class TokenRequest
    {
        public string Token { get; set; }
    }


    [Route("Users")]
    public class UserController : ControllerBase
    {

        [HttpGet]
        [Route("")]
        [AllowAnonymous]
        //[Authorize(Roles = "manager")]
        public async Task<ActionResult<List<User>>> Get(
            [FromServices] DataContext context
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
        [AllowAnonymous]
        public async Task<ActionResult<dynamic>> Authenticate(
            [FromBody] User model,
            [FromServices] DataContext context
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
                // user = user,
                token = token
            };
        }

        [HttpPost]
        [Route("logout")]
        [AllowAnonymous]
        public IActionResult Logout([FromBody] TokenRequest request)
        {
            if (request == null || string.IsNullOrEmpty(request.Token))
                return BadRequest(new { message = "Token is required" });

            TokenBlacklist.AddToBlacklist(request.Token);

            return Ok(new { message = "Logged out successfully" });
        }

        [HttpPost]
        [Route("validateToken")]
        [AllowAnonymous]
        public IActionResult ValidateToken([FromBody] TokenRequest request)
        {
            if (request == null || string.IsNullOrEmpty(request.Token))
                return BadRequest(new { message = "Token is required" });
        
            try
            {
                var userId = TokenService.ValidateToken(request.Token);
                if (userId == null)
                    return Unauthorized(new { message = "Invalid token" });
        
                return Ok(new { message = "Token is valid", userId = userId });
            }
            catch (Exception ex)
            {
                // Log the exception (ex) here if needed
                return StatusCode(500, new { message = "An error occurred while validating the token." });
            }
        }

        /// <summary>
        /// Obtém um usuário com base no nome de usuário e senha fornecidos.
        /// </summary>
        /// <param name="username">O nome de usuário do usuário a ser obtido.</param>
        /// <param name="password">A senha do usuário a ser obtido.</param>
        /// <param name="context">O contexto de dados utilizado para consultar o usuário.</param>
        /// <returns>Um objeto <see cref="User"/> se o usuário for encontrado; caso contrário, um erro.</returns>
        /// <response code="200">Retorna o usuário encontrado.</response>
        /// <response code="400">Se não for possível encontrar o usuário.</response>
        /// <response code="500">Se ocorrer um erro inesperado durante a operação.</response>
        [HttpGet]
        [Route("{username}/{password}")]
        [AllowAnonymous]
        public async Task<ActionResult<User>> GetById(
            [FromRoute] string username,
            [FromRoute] string password,
            [FromServices] DataContext context
        )
        {
            try
            {
                var user = await context.Users.AsNoTracking()
                    .FirstOrDefaultAsync(x => x.UserName == username && x.Password == password);

                if (user == null)
                {
                    return NotFound(new { message = "Usuário não encontrado." });
                }

                return Ok(user);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Não foi possível processar a solicitação." });
            }
        }

        [HttpPost]
        [Route("")]
        [AllowAnonymous]
        public async Task<ActionResult<User>> Post(
            [FromBody] User model,
            [FromServices] DataContext context
        )
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                model.Role = "employee";
                context.Users.Add(model);
                await context.SaveChangesAsync();
                model.Password = "";
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
            [FromBody] User model,
            [FromServices] DataContext context
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
            [FromServices] DataContext context
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
