using OrganizeYou.BLL.DTO;
using OrganizeYou.BLL.Interfaces;
using OrganizeYou.DAL.Entities;
using OrganizeYou.DAL.Repositories;
using OrganizeYou.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using System.Numerics;
using OrganizeYou.DAL.EF;

namespace OrganizeYou.BLL.Services
{
    public class TaskService : ITaskService
    {
        IUnitOfWork Database { get; set; }

        public TaskService(AppDbContext db)
        {
            Database = new EFUnitOfWork(db);
        }

        public void Dispose()
        {
            Database.Dispose();
        }

        public TaskObjectDTO GetTask(int? id)
        {
            TaskObject task = Database.Tasks.Get(id.Value);
            return new TaskObjectDTO
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description,
                Created = task.Created,
                Completion = task.Completion,
                Status = task.Status
            };
        }
        public IEnumerable<TaskObjectDTO> GetTasks()
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<TaskObject, TaskObjectDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<TaskObject>, List<TaskObjectDTO>>(Database.Tasks.GetAll());
        }

        public void CreateTask(TaskObjectDTO taskDto)
        {
            TaskObject task = new TaskObject
            {
                Id = taskDto.Id,
                Title = taskDto.Title,
                Description = taskDto.Description,
                Created = taskDto.Created,
                Completion = taskDto.Completion,
                Status = taskDto.Status
            };

            Database.Tasks.Create(task);
            Database.Save();
        }
    }
}
