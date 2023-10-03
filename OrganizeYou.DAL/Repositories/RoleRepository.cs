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
    public class RoleRepository : IRepository<Role>
    {
        private readonly AppDbContext _db;
        public RoleRepository(AppDbContext db)
        {
            _db = db;
        }
        public void Create(Role item)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Role> Find(Func<Role, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public Role Get(int id)
        {
            Role userRole = _db.Roles.First(r => r.Name == "user");
            return userRole;
        }

        public Role Get(string name)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Role> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Update(Role item)
        {
            throw new NotImplementedException();
        }
    }
}
