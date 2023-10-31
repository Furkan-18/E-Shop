using E_Shop.Business.Dtos;
using E_Shop.Business.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Shop.Business.Services
{
    public interface IUserService
    {

        ServiceMessage AddUser(AddUserDto addUserDto);

        UserInfoDto LoginUser(LoginUserDto loginUserDto);





    }
}
