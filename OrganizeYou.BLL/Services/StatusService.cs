using AutoMapper;
using Microsoft.EntityFrameworkCore.Storage;
using OrganizeYou.BLL.DTO;
using OrganizeYou.BLL.Interfaces;
using OrganizeYou.DAL.EF;
using OrganizeYou.DAL.Entities;
using OrganizeYou.DAL.Interfaces;
using OrganizeYou.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganizeYou.BLL.Services
{
    public class StatusService : IStatusService
    {
        IUnitOfWork Database { get; set; }

        public StatusService(AppDbContext db)
        {
            Database = new EFUnitOfWork(db);
        }

        public StatusDTO GetStatus(int? id)
        {
            if (id == null)
                return null;

            Status status = Database.Statuses.Get(id.Value);

            return new StatusDTO
            {
                Id = status.Id,
                Name = status.Name
            };
        }

        public StatusDTO GetStatus(string name)
        {
            if (name == null)
                return null;

            Status status = Database.Statuses.Get(name);

            return new StatusDTO
            {
                Id = status.Id,
                Name = status.Name
            };
        }

        public IEnumerable<StatusDTO> GetStatuses()
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Status, StatusDTO>()).CreateMapper();
            var statuses = Database.Statuses.GetAll();
            return mapper.Map<IEnumerable<Status>, List<StatusDTO>>(statuses);
        }

        public void Dispose()
        {
            Database.Dispose();
        }

    }
}
