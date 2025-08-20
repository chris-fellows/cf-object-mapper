using CFObjectMapper.Console.Interfaces;
using CFObjectMapper.Console.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFObjectMapper.Console.Services
{
    public class UserService : IUserService
    {
        public UserModel? GetUserModel(int id)
        {
            return new UserModel()
            {
                Id = id,
                Name = $"User {id}"
            };            
        }
    }
}
