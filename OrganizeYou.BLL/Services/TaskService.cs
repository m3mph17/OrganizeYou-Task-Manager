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
                Title = taskDto.Title,
                Description = taskDto.Description,
                Created = DateTime.Now,
                Completion = taskDto.Completion,
                Status = Database.Statuses.Get(1)
            };
            Database.Tasks.Create(task);
            Database.Save();
        }

        public void UpdateTask(TaskObjectDTO taskDto)
        {
            TaskObject task = Database.Tasks.Get(taskDto.Id);
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<TaskObjectDTO, TaskObject>()).CreateMapper();
            TaskObject taskUpdated = mapper.Map<TaskObjectDTO, TaskObject>(taskDto);

            if (task == null || task == taskUpdated)
            {
                return;
            }
            else
            {
                Database.Tasks.Update(taskUpdated);
                Database.Save();
            }
        }

        public void DeleteTask(int? id)
        {
            if(id != null)
            {
                Database.Tasks.Delete(id.Value);
                Database.Save();
            }
        }
    }
}
