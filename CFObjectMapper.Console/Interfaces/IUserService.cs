using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CFObjectMapper.Console.Models;

namespace CFObjectMapper.Console.Interfaces
{
    public interface IUserService
    {
        UserModel? GetUserModel(int id);
    }
}
