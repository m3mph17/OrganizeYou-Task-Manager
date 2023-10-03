using AutoMapper;
using OrganizeYou.BLL.DTO;
using OrganizeYou.BLL.Interfaces;
using OrganizeYou.DAL.EF;
using OrganizeYou.DAL.Entities;
using OrganizeYou.DAL.Interfaces;
using OrganizeYou.DAL.Repositories;

namespace OrganizeYou.BLL.Services
{
    public class UserService : IUserService
    {
        IUnitOfWork Database { get; set; }
        public UserService(AppDbContext db)
        {
            Database = new EFUnitOfWork(db);
        }

        public void CreateUser(UserDTO userDto)
        {
            User user = new User
            {
                Email = userDto.Email,
                Password = userDto.Password,
            };

            Database.Users.Create(user);
            Database.Save();
        }

        public void DeleteUser(int? id)
        {
            if (id != null)
            {
                Database.Users.Delete(id.Value);
                Database.Save();
            }
        }

        public void Dispose()
        {
            Database.Dispose();
        }

        public UserDTO GetUser(int? id)
        {
            User user = Database.Users.Get(id.Value);

            if (user == null)
                return null;

            return new UserDTO()
            {
                Id = user.Id,
                Email = user.Email,
                Password = user.Password,
                Role = user.Role
            };
        }

        public Role GetRole()
        {
            return Database.Roles.Get(1);
        }

        public IEnumerable<UserDTO> GetUsers()
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<User, UserDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<User>, List<UserDTO>>(Database.Users.GetAll());
        }

        public void UpdateUser(UserDTO userDto)
        {
            User user = Database.Users.Get(userDto.Id);

            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<UserDTO, User>()).CreateMapper();
            User userUpdated = mapper.Map<UserDTO, User>(userDto);

            if (user == null || user == userUpdated)
            {
                return;
            }
            else
            {
                Database.Users.Update(userUpdated);
                Database.Save();
            }
        }
    }
}
