using Application.DaoInterfaces;
using Application.LogicInterfaces;
using Models.DTO;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Logic
{
    public class UserLogic : IUserLogic
    {
        private readonly IUserDao userDao;

        public UserLogic(IUserDao userDao)
        {
            this.userDao = userDao;
        }

        public async Task<User> CreateAsync(UserCreationDto dto)
        {
            User? existing = await userDao.GetByUsernameAsync(dto.UserName);
            if (existing != null)
                throw new Exception("Username already taken!");

            ValidateData(dto);
            User toCreate = new User
            {
                UserName = dto.UserName,
                Password = dto.Password
            };

            User created = await userDao.CreateAsync(toCreate);

            return created;
        }

        private static void ValidateData(UserCreationDto userToCreate)
        {
            string userName = userToCreate.UserName;

            if (userName.Length < 3)
                throw new Exception("Username must be at least 3 characters!");

            if (userName.Length > 15)
                throw new Exception("Username must be less than 16 characters!");
            if (userToCreate.Password.Length < 3)
                throw new Exception("Passwprd must be at least 3 characters!");
        }
        public Task<IEnumerable<User>> GetAsync(SearchUserParametersDto searchParameters)
        {
            return userDao.GetAsync(searchParameters);
        }

        public async Task<User> ValidateUser(string userName, string password)
        {
            User? user = await userDao.GetByUsernameAsync(userName);
            if (user == null)
                throw new Exception("Validation error.");

            if (!user.Password.Equals(password))
            {
                throw new Exception("Validation error.");
            }

            return user;

        }
    }
}
