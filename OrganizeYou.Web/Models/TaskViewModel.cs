using OrganizeYou.BLL.DTO;
using OrganizeYou.DAL.Entities;

namespace OrganizeYou.Web.Models
{
    public class TaskViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Created { get; set; }
        public DateTime Completion { get; set; }
        public string Status { get; set; }

        public TaskObjectDTO Convert(StatusDTO status)
        {
            TaskObjectDTO dto = new TaskObjectDTO();
            dto.Status = new Status();

            dto.Id = Id;
            dto.Title = Title;
            dto.Description = Description;
            dto.Created = Created;
            dto.Completion = Completion;
            dto.Status.Id = status.Id;
            dto.Status.Name = status.Name;

            return dto;
        }
    }
}
