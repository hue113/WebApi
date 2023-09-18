using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Entities;
using API.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
  [Authorize] //
  public class UsersController : BaseApiController
  {
    // private readonly DataContext _context;
    private readonly IUserRepository _userRepository;

    public UsersController(IUserRepository userRepository)
    {
      // convention, but many dev don't like the overuse of 'this'
      // this.context refer to the context it was originally created above in the constructor
      // this.context = context;  
      // _context = context;
      _userRepository = userRepository;
    }

    [HttpGet] // api/users
    public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers()
    // public ActionResult<IEnumerable<AppUser>> GetUsers()
    {
      Console.WriteLine("💥 Clicked: api/users");
      // var users = await _context.Users.ToListAsync();
      var users = await _userRepository.GetUsersAsync();
      return Ok(users); // to work around  
    }

    [HttpGet("{username}")]  // /api/users/3
    public async Task<ActionResult<AppUser>> GetUser(string username)
    {
      Console.WriteLine($"💥 Clicked: api/users/{username}");
      return await _userRepository.GetUserByUsernameAsync(username);
    }
  }
}