using OrganizeYou.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace OrganizeYou.DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<TaskObject> Tasks { get; }
        IRepository<Status> Statuses { get; }
        void Save();
    }
}
