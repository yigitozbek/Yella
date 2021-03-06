using Yella.Identity.Entities;
using Yella.Identity.Models;
using Yella.Utilities.Results;

namespace Yella.Identity.Interfaces;

public interface IIdentityRoleService<TUser, TRole>
    where TUser : IdentityUser<TUser, TRole>
    where TRole : IdentityRole<TUser, TRole>
{
    /// <summary>
    /// This method is used to add Role to User.
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    Task<IDataResult<UserRole<TUser, TRole>>> AddUserRoleAsync(CreateUserRoleModel input);

    /// <summary>
    /// This method is used to remove Role to User.
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    Task<IResult> RemoveUserRoleAsync(UserRoleRemoveModel input);

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


    /// <summary>
    /// After this method adds the role, it also adds the permissions that depend on the role. 
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    Task<IDataResult<TRole>> AddWithPermissionAsync(RolePermissionModel input);


    /// <summary>
    /// After this method update the role, it also updates the permissions that depend on the role. 
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    Task<IDataResult<TRole>> UpdateWithPermissionAsync(RolePermissionModel input);



}