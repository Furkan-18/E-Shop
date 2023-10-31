using E_Shop.Business.Dtos;
using E_Shop.Business.Services;
using E_Shop.Business.Types;
using E_Shop.Data.Entities;
using E_Shop.Data.Repository;
using Microsoft.AspNetCore.DataProtection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Shop.Business.Managers
{
    public class UserManager : IUserService
    {
        private readonly IRepository<UserEntity> _userRepository;
        private readonly IDataProtector _dataProtector;
        public UserManager(IRepository<UserEntity> userRepository, IDataProtectionProvider dataProtectionProvider)
        {
            _userRepository = userRepository;
            _dataProtector = dataProtectionProvider.CreateProtector("security");
        }
        public ServiceMessage AddUser(AddUserDto addUserDto)
        {
            var hasMail = _userRepository.Get(x => x.Email.ToLower() == addUserDto.Email.ToLower());
            if (hasMail != null)
            {
                return new ServiceMessage
                {
                    IsSucceed = false,
                    Message = "Böyle bir Eposta zaten var"
                };
            }
            var userEntity = new UserEntity()
            {
                Email = addUserDto.Email,
                Password =_dataProtector.Protect( addUserDto.Password),
                FirstName = addUserDto.FirstName,
                LastName = addUserDto.LastName,
                UserType = Data.Enums.UserTypeEnum.User
            };
            _userRepository.Add(userEntity);
            return new ServiceMessage
            {
                IsSucceed = true
            };
        }
        public UserInfoDto LoginUser(LoginUserDto loginUserDto)
        {
            var hasMail = _userRepository.Get(x => x.Email == loginUserDto.Email);

            if (hasMail == null)
            {
                return null;
            }
            var rawPassword = _dataProtector.Unprotect(hasMail.Password);
            if (rawPassword == loginUserDto.Password)
            {
                var userInfoDto = new UserInfoDto()
                {
                    Id=hasMail.Id,
                    Email = hasMail.Email,
                    FirstName = hasMail.FirstName,
                    LastName = hasMail.LastName,
                    UserType = hasMail.UserType
                };
                return userInfoDto;
            }
            else
            {
                return null;
            }


        }
    }
}
