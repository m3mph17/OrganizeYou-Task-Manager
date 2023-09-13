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
    }
}
