using OrganizeYou.BLL.DTO;
using OrganizeYou.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganizeYou.BLL.Interfaces
{
    public interface IUserService
    {
        void CreateUser(UserDTO userDto);
        UserDTO GetUser(int? id);
        void DeleteUser(int? id);
        void UpdateUser(UserDTO userDto);
        Role GetRole();
        IEnumerable<UserDTO> GetUsers();
        void Dispose();

    }
}
