using OrganizeYou.BLL.DTO;
using OrganizeYou.DAL.Entities;
using System.ComponentModel.DataAnnotations;

namespace OrganizeYou.Web.Models
{
    public class TaskViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public DateTime Created { get; set; }

        [Required]
        public DateTime Completion { get; set; }
        public string? Status { get; set; }

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
