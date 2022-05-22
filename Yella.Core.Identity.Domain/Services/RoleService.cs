using Yella.Core.EntityFrameworkCore;
using Yella.Core.Identity.Entities;
using Yella.Core.Identity.Interfaces;

namespace Yella.Core.Identity.Services;

public class RoleService<TUser, TRole> : IRoleService<TUser, TRole>
    where TUser : IdentityUser<TUser, TRole>
    where TRole : IdentityRole<TUser, TRole>
{
    private readonly IRepository<TRole, Guid> _roleRepository;
    private readonly IRepository<UserRole<TUser, TRole>, Guid> _userRoleRepository;

    public RoleService(IRepository<TRole, Guid> roleRepository, IRepository<UserRole<TUser, TRole>, Guid> userRoleRepository)
    {
        _roleRepository = roleRepository;
        _userRoleRepository = userRoleRepository;
    }

    public async Task<List<TRole>> GetListByUserIdAsync(Guid userId)
    {

        var result = (await _roleRepository.WithDetailsAsync(x => x.UserRoles,
                x => ((UserRole<TUser, TRole>)x.UserRoles).Role!,
                x => ((UserRole<TUser, TRole>)x.UserRoles).Role!.UserRoles))
            .Where(x => x.UserRoles.Any(pRole => pRole.Role!.UserRoles.Any(uRole => uRole.UserId == userId))).ToList();

        return result;
    }

    public async Task<List<TRole>> GetListWithoutRoleByUserIdAsync(Guid id)
    {

        var userRoleRoleIds = (await _userRoleRepository.GetListAsync(x => x.UserId == id)).Select(x => x.RoleId).ToList();

        var roles = (await _roleRepository.GetListAsync()).Where(x => !userRoleRoleIds.Contains(x.Id)).ToList();

        return roles;
    }

    /// <summary>
    /// This method fetches the records by Id
    /// </summary>
    /// <returns></returns>
    public async Task<List<TRole>> GetListAsync()
    {
        var query = await _roleRepository.GetListAsync();
        return query.ToList();
    }

    /// <summary>
    /// This method fetches the record by Id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<TRole> GetByIdAsync(Guid id)
    {
        var query = await _roleRepository.GetAsync(id);
        return query;
    }
}