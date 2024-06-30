using Microsoft.AspNetCore.Mvc;
using UdlaBlog.WebAPI.Local.Domain.Interfaces;
using UdlaBlog.WebAPI.Local.Domain.Models;
using UdlaBlog.WebAPI.Local.DTOs;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace UdlaBlog.WebAPI.Local.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _repository;

        public UsersController(IUserRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetUsers()
        {
            var users = await _repository.GetAllAsync();
            var usersDto = users.Select(u => ConvertToDto(u));
            return Ok(usersDto);
        }

        [HttpGet("{username}")]
        public async Task<ActionResult<UserDto>> GetUser(string username)
        {
            var user = await _repository.GetByUsernameAsync(username);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(ConvertToDto(user));
        }

        [HttpPost]
        public async Task<ActionResult<UserDto>> PostUser(UserDto userDto)
        {
            var user = ConvertToEntity(userDto);
            await _repository.AddAsync(user);
            return CreatedAtAction(nameof(GetUser), new { username = user.Username }, ConvertToDto(user));
        }

        [HttpPut("{username}")]
        public async Task<IActionResult> PutUser(string username, UserDto userDto)
        {
            if (username != userDto.Username)
            {
                return BadRequest();
            }

            var user = ConvertToEntity(userDto);
            try
            {
                await _repository.UpdateAsync(user);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await UserExists(username))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return NoContent();
        }

        [HttpDelete("{username}")]
        public async Task<IActionResult> DeleteUser(string username)
        {
            var user = await _repository.GetByUsernameAsync(username);
            if (user == null)
            {
                return NotFound();
            }

            await _repository.DeleteAsync(username);
            return NoContent();
        }

        private async Task<bool> UserExists(string username)
        {
            var user = await _repository.GetByUsernameAsync(username);
            return user != null;
        }

        private static UserDto ConvertToDto(User user)
        {
            return new UserDto
            {
                Username = user.Username,
                Password = user.Password,
                Nombres = user.Nombres,
                Apellidos = user.Apellidos,
                NumeroTelefono = user.NumeroTelefono
            };
        }

        private static User ConvertToEntity(UserDto userDto)
        {
            return new User
            {
                Username = userDto.Username,
                Password = userDto.Password,
                Nombres = userDto.Nombres,
                Apellidos = userDto.Apellidos,
                NumeroTelefono = userDto.NumeroTelefono
            };
        }
    }
}
