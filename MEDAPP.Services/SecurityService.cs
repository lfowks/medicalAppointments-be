using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BCrypt.Net;
using MEDAPP.Models.Security;

namespace MEDAPP.Services
{
    public class SecurityService : ISecurityService
    {
        private readonly Repository.IRepository _myRepo;


        public SecurityService(Repository.IRepository repo)
        {
            _myRepo = repo;
        }

        public string EncryptPassword(string password,string salt)
        {
            string hashedPassword = "";

            try
            {

                hashedPassword = BCrypt.Net.BCrypt.HashPassword(password, salt);
            }
            catch (Exception e)
            {

                throw;
            }

            return hashedPassword;
        }

        public async Task<User> CheckUserAndPassword(User entity)
        {
            var userAuth =_myRepo.FindSingleByCondition<User>(user => entity.UserName.Equals(user.UserName));

            // check a password
            bool validPassword = BCrypt.Net.BCrypt.Verify(entity.Password, userAuth.Password);

            if (!validPassword) return null;

            entity.Password = ":)";
            entity.PasswordHash = ":)";

            var userRoles = _myRepo.FindByCondition<UserRole>(ur => ur.UserId == userAuth.Id);

            entity.Roles = new List<Role>();

            foreach (var item in userRoles)
            {
                item.Role = await FindRoleById<Role>(item.RoleId);
                item.Role.UserRole = null;
                entity.Roles.Add(item.Role);
            }

            entity.UserRole = null;
            entity.Id = userAuth.Id;
            entity.UserRole = userRoles;

            return entity;

        }

        public Task CreateRoleAsync<T>(Role entity)
        {
            throw new NotImplementedException();
        }

        public Task CreateUserAsync<T>(User entity)
        {
            // hash and save a password
            var hashedPassword = EncryptPassword(entity.Password, entity.PasswordHash);
            entity.Password = hashedPassword;

            return _myRepo.CreateAsync(entity);
        }

        public Task CreateUserRoleAsync<T>(UserRole entity)
        {
            return _myRepo.CreateAsync(entity);
        }



        public Task<Role> FindRoleById<T>(int id)
        {
            return _myRepo.FindById<Role>(id);
        }

        public Task<User> FindUserById<T>(int id)
        {
            throw new NotImplementedException();
        }


        public Task<List<Role>> FindAllRoles<T>()
        {
            throw new NotImplementedException();
        }

        public Task<List<User>> FindAllUsers<T>()
        {
            throw new NotImplementedException();
        }




        //LESS USED

        public Task DeleteRoleAsync<T>(Role entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteUserAsync<T>(User entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteUserRoleAsync<T>(UserRole entity)
        {
            throw new NotImplementedException();
        }
        
        public Task UpdateRoleAsync<T>(Role entity)
        {
            throw new NotImplementedException();
        }

        public Task UpdateUserAsync<T>(User entity)
        {
            throw new NotImplementedException();
        }

        public Task UpdateUserRoleAsync<T>(UserRole entity)
        {
            throw new NotImplementedException();
        }
    }
}
