using OrganizeYou.DAL.EF;
using OrganizeYou.DAL.Entities;
using OrganizeYou.DAL.Interfaces;

namespace OrganizeYou.DAL.Repositories
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _db;
        private TaskRepository taskRepository;
        private StatusRepository statusRepository;

        public EFUnitOfWork(AppDbContext db)
        {
            _db = db;
        }

        public IRepository<TaskObject> Tasks
        {
            get
            {
                if (taskRepository == null)
                    taskRepository = new TaskRepository(_db);

                return taskRepository;
            }
        }

        public IRepository<Status> Statuses
        {
            get
            {
                if (statusRepository == null)
                    statusRepository = new StatusRepository(_db);

                return statusRepository;
            }
        }

        public void Save()
        {
            _db.SaveChanges();
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _db.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
