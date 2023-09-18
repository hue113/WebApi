using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
  [Authorize] //
  public class UsersController : BaseApiController
  {
    // private readonly DataContext _context;
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public UsersController(IUserRepository userRepository, IMapper mapper)
    {
      // convention, but many dev don't like the overuse of 'this'
      // this.context refer to the context it was originally created above in the constructor
      // this.context = context;  
      // _context = context;
      _userRepository = userRepository;
      _mapper = mapper;
    }

    [HttpGet] // api/users
    public async Task<ActionResult<IEnumerable<MemberDto>>> GetUsers()
    // public ActionResult<IEnumerable<AppUser>> GetUsers()
    {
      Console.WriteLine("💥 Clicked: api/users");
      // var users = await _context.Users.ToListAsync();
      var users = await _userRepository.GetUsersAsync();
      // return Ok(users); // to work around  

      var usersToReturn = _mapper.Map<IEnumerable<MemberDto>>(users);
      return Ok(usersToReturn);
    }

    [HttpGet("{username}")]  // /api/users/3
    public async Task<ActionResult<MemberDto>> GetUser(string username)
    {
      Console.WriteLine($"💥 Clicked: api/users/{username}");
      var user = await _userRepository.GetUserByUsernameAsync(username);
      return _mapper.Map<MemberDto>(user);
    }
  }
}