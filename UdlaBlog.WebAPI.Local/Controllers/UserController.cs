using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using UdlaBlog.Application.DTOs;
using UdlaBlog.Domain.Entities;
using UdlaBlog.Domain.Interfaces;

[Route("api/users")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserRepository _userRepository;

    public UserController(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<User>>> GetUsers()
    {
        var users = await _userRepository.GetAllAsync();
        return Ok(users);
    }

    [HttpGet("{username}")]
    public async Task<ActionResult<User>> GetUser(string username)
    {
        var user = await _userRepository.GetByUsernameAsync(username);
        if (user == null)
        {
            return NotFound();
        }
        return Ok(user);
    }

    [HttpPost]
    public async Task<ActionResult> PostUser(UserDto userDto)
    {
        var user = new User
        {
            Username = userDto.Username,
            Password = userDto.Password,
            Nombres = userDto.Nombres,
            Apellidos = userDto.Apellidos,
            CorreoElectronico = userDto.CorreoElectronico
        };

        await _userRepository.AddAsync(user);
        return CreatedAtAction(nameof(GetUser), new { username = user.Username }, user);
    }

    [HttpPut("{username}")]
    public async Task<IActionResult> PutUser(string username, UserDto userDto)
    {
        if (username != userDto.Username)
        {
            return BadRequest();
        }

        var user = new User
        {
            Username = userDto.Username,
            Password = userDto.Password,
            Nombres = userDto.Nombres,
            Apellidos = userDto.Apellidos,
            CorreoElectronico = userDto.CorreoElectronico
        };

        await _userRepository.UpdateAsync(user);
        return NoContent();
    }

    [HttpDelete("{username}")]
    public async Task<IActionResult> DeleteUser(string username)
    {
        await _userRepository.DeleteAsync(username);
        return NoContent();
    }
}
