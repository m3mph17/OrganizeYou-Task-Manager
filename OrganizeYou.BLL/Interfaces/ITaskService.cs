using OrganizeYou.BLL.DTO;
using OrganizeYou.DAL.Entities;

namespace OrganizeYou.BLL.Interfaces
{
    public interface ITaskService
    {
        void CreateTask(TaskObjectDTO taskDto);
        TaskObjectDTO GetTask(int? id);
        void DeleteTask(int? id);
        void UpdateTask(TaskObjectDTO taskDto);
        IEnumerable<TaskObjectDTO> GetTasks();
        void Dispose();
    }
}
