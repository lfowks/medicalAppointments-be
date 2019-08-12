using MEDAPP.Models.Security;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MEDAPP.Services
{
    public interface ISecurityService
    {


        string EncryptPassword(string password, string salt);
        Task<User> CheckUserAndPassword(User entity);


        User FindSingleByCondition<T>(System.Linq.Expressions.Expression<Func<User, bool>> expression);


        Task<List<User>> FindAllUsers<T>();
        Task<List<Role>> FindAllRoles<T>();

        Task<User> FindUserById<T>(int id);
        Task<Role> FindRoleById<T>(int id);

        Task CreateUserAsync<T>(User entity);
        Task UpdateUserAsync<T>(User entity);
        Task DeleteUserAsync<T>(User entity);

        Task CreateRoleAsync<T>(Role entity);
        Task UpdateRoleAsync<T>(Role entity);
        Task DeleteRoleAsync<T>(Role entity);

        Task CreateUserRoleAsync<T>(UserRole entity);
        Task UpdateUserRoleAsync<T>(UserRole entity);
        Task DeleteUserRoleAsync<T>(UserRole entity);
    }
}
