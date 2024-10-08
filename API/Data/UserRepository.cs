using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
  public class UserRepository : IUserRepository
  {
    private readonly DataContext _context;
    private readonly IMapper _mapper;

    // private readonly IMapper _mapper;

    public UserRepository(DataContext context, IMapper mapper)
    {
      _context = context;
      _mapper = mapper;
      // _mapper = mapper;
    }

    public async Task<MemberDto> GetMemberAsync(string username)
    {
      return await _context.Users
          .Where(x => x.UserName == username)
          // Don't use .Select like this, you'll have to write repeatedly:
          // .Select(user => new MemberDto
          // {
          //   Id = user.Id,
          //   UserName = user.UserName,
          //   KnownAs = user.KnownAs
          // })

          // Use: ProjectTo from AutoMapper to avoid writing repeatedly
          .ProjectTo<MemberDto>(_mapper.ConfigurationProvider)
          .SingleOrDefaultAsync();
    }

    public async Task<IEnumerable<MemberDto>> GetMembersAsync()
    {
      return await _context.Users.ProjectTo<MemberDto>(_mapper.ConfigurationProvider).ToListAsync();
    }

    public async Task<AppUser> GetUserByIdAsync(int id)
    {
      return await _context.Users.FindAsync(id);
    }

    public async Task<AppUser> GetUserByUsernameAsync(string username)
    {
      return await _context.Users
          .Include(p => p.Photos) // LEFT JOIN
          .SingleOrDefaultAsync(x => x.UserName == username);
    }

    public async Task<IEnumerable<AppUser>> GetUsersAsync()
    {
      // return await _context.Users.ToListAsync();
      return await _context.Users
      .Include(p => p.Photos) // LEFT JOIN
      .ToListAsync();
    }

    public async Task<bool> SaveAllAsync()
    {
      // return boolean: 
      // if > 0 --> return true ()there is smt saved to database)
      // if 0 --> return false (nothing saved to db)
      return await _context.SaveChangesAsync() > 0;
    }

    public void Update(AppUser user)
    {
      _context.Entry(user).State = EntityState.Modified; // tell EntityFramework tracker that there's smt changed
    }
  }
}