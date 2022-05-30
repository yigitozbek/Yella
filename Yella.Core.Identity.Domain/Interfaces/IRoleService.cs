using Yella.Core.Helper.Results;
using Yella.Core.Identity.Dtos;
using Yella.Core.Identity.Entities;

namespace Yella.Core.Identity.Interfaces;

public interface IRoleService<TUser, TRole>
    where TUser : IdentityUser<TUser, TRole>
    where TRole : IdentityRole<TUser, TRole>
{
    /// <summary>
    /// This method is used to add Role to User.
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    Task<IDataResult<UserRole<TUser, TRole>>> AddUserRoleAsync(UserRoleAddDto input);

    /// <summary>
    /// This method is used to remove Role to User.
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    Task<IResult> RemoveUserRoleAsync(UserRoleRemoveDto input);

    /// <summary>
    /// This method fetches roles by user Id.
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    Task<List<TRole>> GetListByUserIdAsync(Guid userId);

    /// <summary>
    /// This method returns roles that the user does not have.
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    Task<List<TRole>> GetListWithoutRoleByUserIdAsync(Guid userId);

    /// <summary>
    /// This method fetches the roles
    /// </summary>
    /// <returns></returns>
    Task<List<TRole>> GetListAsync();

    /// <summary>
    /// This method fetches the role by Id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<TRole> GetByIdAsync(Guid id);

    /// <summary>
    /// This method is used to add Role
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    Task<IDataResult<TRole>> AddAsync(TRole input);

    /// <summary>
    /// This method is used to update Role
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    Task<IDataResult<TRole>> UpdateAsync(TRole input);

}