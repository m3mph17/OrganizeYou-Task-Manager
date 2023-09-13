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
    public class StatusRepository : IRepository<Status>
    {
        private readonly AppDbContext _db;

        public StatusRepository(AppDbContext db)
        {
            _db = db;
        }
        public void Create(Status item)
        {
            _db.Add<Status>(item);
        }

        public void Delete(int id)
        {
            Status item = _db.Statuses.Find(id);

            if (item != null)
            {
                _db.Statuses.Remove(item);
            }
        }

        public IEnumerable<Status> Find(Func<Status, bool> predicate)
        {
            return _db.Statuses.Where(predicate).ToList();
        }

        public Status Get(int id)
        {
            Status item = _db.Statuses.Find(id);

            if (item != null)
                return item;
            else
                return null;
        }

        public IEnumerable<Status> GetAll()
        {
            return _db.Statuses.ToList();
        }

        public void Update(Status item)
        {
            _db.Entry(item).State = EntityState.Modified;
        }
    }
}
