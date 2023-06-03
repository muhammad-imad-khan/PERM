using Perm.Model.Abstraction;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Perm.Model.Management
{
    [Table(name: "Tasks", Schema = "Management")]

    public class TaskModel : ModelBase
    {
        [Key]
        public long TaskID { get; set; }

        public long? ParamTaskTypeID { get; set; }

        public string? TaskName { get; set; }

        public string TaskDescription { get; set; }

        public bool IsActive { get; set; }

        public DateTime Deadline { get; set; }

        public DateTime TargetCompletionDate { get; set; }

        public long? AssignedByID { get; set; }

        public long? AssignedToID { get; set; }
    }
}