using System.Linq.Expressions;
using Yella.Framework.EntityFrameworkCore;
using Yella.Framework.Identity.Entities;
using Yella.Framework.Identity.Interfaces;
using Yella.Framework.Utilities.Results;

namespace Yella.Framework.Identity.Services;

public class UserService<TUser, TRole> : IUserService<TUser, TRole>
    where TUser : IdentityUser<TUser, TRole>
    where TRole : IdentityRole<TUser, TRole>
{
    private readonly IRepository<TUser, Guid> _userRepository;

    public UserService(IRepository<TUser, Guid> userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<List<TUser>> GetListAsync()
    {
        var query = await _userRepository.GetListAsync();
        return query.ToList();
    }

    public async Task<List<TUser>> GetListWithRoleAsync()
    {
        var query = await _userRepository.WithIncludeAsync(x => x.UserRoles, x => ((UserRole<TUser, TRole>)x.UserRoles).Role);
        return query.ToList();
    }

    public async Task<TUser> GetWithRoleByIdAsync(Guid id)
    {
        var query = await _userRepository.GetAsync(id,x => x.UserRoles, x => ((UserRole<TUser, TRole>)x.UserRoles).Role);
        return query;
    }

    public async Task<TUser> GetByIdAsync(Guid id, params Expression<Func<TUser, object>>[] includes)
    {

        var query = includes != null 
            ? await _userRepository.GetAsync(id, includes) 
            : await _userRepository.GetAsync(id);
            
        return query;
    }

    public async Task<TUser> GetByUsernameAsync(string username, params Expression<Func<TUser, object>>[] includes)
    {
        var query = includes is { Length: > 0 }
            ? (await _userRepository.WithIncludeAsync(includes)).First(x => x.UserName == username)
            : await _userRepository.FirstOrDefaultAsync(x => x.UserName == username);
        return query;
    }

    public async Task<IDataResult<TUser>> UpdateAsync(TUser input)
    {
        var result = await _userRepository.UpdateAsync(input);
        if (!result.Success) return new ErrorDataResult<TUser>(input, result.Message);
        return new SuccessDataResult<TUser>(input, result.Message);
    }

    public async Task<IDataResult<TUser>> AddAsync(TUser input)
    {
        var result = await _userRepository.AddAsync(input);
        if (!result.Success) return new ErrorDataResult<TUser>(input, result.Message);
        return new SuccessDataResult<TUser>(input, result.Message);
    }

    public async Task<string> GetFullNameAsync(Guid id)
    {
        var query = await _userRepository.FirstOrDefaultAsync(id);
        return query == null ? null : $"{query.Name} {query.Surname}";
    }
}