using API.DTOs;
using API.Entities;

namespace API.Interfaces
{
  public interface IUserRepository
  {
    void Update(AppUser user);

    // can use List (List is more powerful, can Add/Get) 
    // IEnumerable is a type of List, but we just need to get users --> use IEnumerable instead of List
    Task<IEnumerable<AppUser>> GetUsersAsync();
    Task<AppUser> GetUserByIdAsync(int id);
    Task<AppUser> GetUserByUsernameAsync(string username);
    Task<IEnumerable<MemberDto>> GetMembersAsync();
    Task<MemberDto> GetMemberAsync(string username);
    // Task<PagedList<MemberDto>> GetMembersAsync(UserParams userParams);
  }
}