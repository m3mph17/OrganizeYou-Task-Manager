using Microsoft.EntityFrameworkCore;
using OrganizeYou.DAL.EF;
using OrganizeYou.DAL.Entities;
using OrganizeYou.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganizeYou.DAL.Repositories
{
    public class UserRepository : IRepository<User>
    {
        private readonly AppDbContext _db;
        public UserRepository(AppDbContext db)
        {
            _db = db;
        }
        public void Create(User item)
        {
            item.Role = _db.Roles.First(r => r.Name == "user");
            _db.Users.Add(item);   
        }

        public void Delete(int id)
        {
            User user = _db.Users.Find(id);

            if (user != null)
            {
                _db.Users.Remove(user);
            }
        }

        public IEnumerable<User> Find(Func<User, bool> predicate)
        {
            return _db.Users.Where(predicate).ToList();
        }

        public User Get(int id)
        {
            User user = _db.Users.Find(id);

            if (user != null)
            {
                var query = _db.Users.Include(u => u.Role);
                return query.Where(u => u.Id == id).First();
            }
            else
                return null;
        }

        public User Get(string name)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> GetAll()
        {
            var query = _db.Users.Include(u => u.Role);
            var users = query.ToList();

            if (users.Count == 0)
                return null;

            return users;
        }

        public void Update(User item)
        {
            _db.ChangeTracker.Clear();
            _db.Users.Update(item);
        }
    }
}
