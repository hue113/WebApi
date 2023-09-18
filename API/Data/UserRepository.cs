using API.Entities;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
  public class UserRepository : IUserRepository
  {
    private readonly DataContext _context;
    // private readonly IMapper _mapper;

    public UserRepository(DataContext context)
    {
      _context = context;
      // _mapper = mapper;
    }

    // public async Task<MemberDto> GetMemberAsync(string username)
    // {
    //   return await _context.Users
    //       .Where(x => x.UserName == username)
    //       .ProjectTo<MemberDto>(_mapper.ConfigurationProvider)
    //       .SingleOrDefaultAsync();
    // }

    // public async Task<PagedList<MemberDto>> GetMembersAsync(UserParams userParams)
    // {
    //   var query = _context.Users.AsQueryable();

    //   query = query.Where(u => u.UserName != userParams.CurrentUsername);
    //   query = query.Where(u => u.Gender == userParams.Gender);

    //   var minDob = DateOnly.FromDateTime(DateTime.Today.AddYears(-userParams.MaxAge - 1));
    //   var maxDob = DateOnly.FromDateTime(DateTime.Today.AddYears(-userParams.MinAge));

    //   query = query.Where(u => u.DateOfBirth >= minDob && u.DateOfBirth <= maxDob);

    //   query = userParams.OrderBy switch
    //   {
    //     "created" => query.OrderByDescending(u => u.Created),
    //     _ => query.OrderByDescending(u => u.LastActive)
    //   };

    //   return await PagedList<MemberDto>.CreateAsync(query.AsNoTracking()
    //       .ProjectTo<MemberDto>(_mapper.ConfigurationProvider),
    //           userParams.PageNumber, userParams.PageSize);
    // }

    public async Task<AppUser> GetUserByIdAsync(int id)
    {
      return await _context.Users.FindAsync(id);
    }

    public async Task<AppUser> GetUserByUsernameAsync(string username)
    {
      return await _context.Users
          .Include(p => p.Photos)
          .SingleOrDefaultAsync(x => x.UserName == username);
    }

    public async Task<IEnumerable<AppUser>> GetUsersAsync()
    {
      // return await _context.Users.ToListAsync();
      return await _context.Users
      .Include(p => p.Photos)
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