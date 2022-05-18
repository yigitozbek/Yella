using System.Linq.Expressions;
using AutoMapper;
using Yella.Core.EntityFrameworkCore;
using Yella.Core.Helper.Results;
using Yella.Core.Identity.Domain.Entities;
using Yella.Core.IdentityService.Interfaces;

namespace Yella.Core.IdentityService.Services;

public class UserService<TUser, TRole> : IUserService<TUser, TRole>
    where TUser : IdentityUser<TUser, TRole>
    where TRole : IdentityRole<TUser, TRole>
{
    private readonly IRepository<TUser, Guid> _userRepository;
    private readonly IMapper _mapper;

    public UserService(IRepository<TUser, Guid> userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<List<TUser>> GetListAsync()
    {
        var query = await _userRepository.GetListAsync();
        return query.ToList();
    }

    public async Task<List<TUser>> GetListWithRoleAsync()
    {
        var query = await _userRepository.WithDetailsAsync(x => x.UserRoles, x => ((UserRole<TUser, TRole>)x.UserRoles).Role);
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
            ? (await _userRepository.WithDetailsAsync(includes)).First(x => x.UserName == username)
            : await _userRepository.FirstOrDefaultAsync(x => x.UserName == username);
        return query;
    }

    public async Task<DataResult<TUser>> UpdateAsync(TUser input)
    {
        var result = await _userRepository.UpdateAsync(input);
        if (!result.Success) return new ErrorDataResult<TUser>(input, result.Message);
        return new SuccessDataResult<TUser>(input, result.Message);
    }

    public async Task<DataResult<TUser>> AddAsync(TUser input)
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