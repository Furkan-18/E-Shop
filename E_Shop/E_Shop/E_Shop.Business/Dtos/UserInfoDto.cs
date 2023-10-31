using E_Shop.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Shop.Business.Dtos
{
    public class UserInfoDto
    {


        public int Id { get; set; }
        public string FirstName { get; set; }

        public string LastName { get; set; }
        public string Email { get; set; }

        public UserTypeEnum UserType
        { get; set; }

    }
}
