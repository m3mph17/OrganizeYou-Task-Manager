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
    public class TaskRepository : IRepository<TaskObject>
    {
        private readonly AppDbContext _db;

        public TaskRepository(AppDbContext db)
        {
            _db = db;
        }

        public void Create(TaskObject item)
        {
            _db.Tasks.Add(item);
        }

        public void Delete(int id)
        {
            TaskObject item = _db.Tasks.Find(id);

            if (item != null) 
            {
                _db.Tasks.Remove(item);
            }
        }

        public IEnumerable<TaskObject> Find(Func<TaskObject, bool> predicate)
        {
            return _db.Tasks.Where(predicate).ToList();
        }

        public TaskObject Get(int id)
        {
            TaskObject item = _db.Tasks.Find(id);

            if (item != null)
            {
                var query = _db.Tasks.Include(t => t.Status);
                return query.Where(t => t.Id == id).FirstOrDefault();
            }
            else
                return null;
        }

        public IEnumerable<TaskObject> GetAll()
        {
            return _db.Tasks.ToList();
        }

        public void Update(TaskObject item)
        {
            _db.Entry(item).State = EntityState.Modified;
        }
    }
}
