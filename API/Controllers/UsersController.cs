using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
  [Authorize] //
  public class UsersController : BaseApiController
  {
    private readonly DataContext _context;

    public UsersController(DataContext context)
    {
      // convention, but many dev don't like the overuse of 'this'
      // this.context refer to the context it was originally created above in the constructor
      // this.context = context;  
      _context = context;
    }

    [AllowAnonymous]
    [HttpGet] // api/users
    public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers()
    // public ActionResult<IEnumerable<AppUser>> GetUsers()
    {
      Console.WriteLine("💥 Clicked: api/users");
      var users = await _context.Users.ToListAsync();
      return users;
    }

    [HttpGet("{id}")]  // /api/users/3
    public async Task<ActionResult<AppUser>> GetUser(int id)
    {
      Console.WriteLine($"💥 Clicked: api/users/{id}");
      return await _context.Users.FindAsync(id);
    }
  }
}