using Perm.Model.Abstraction;
using Perm.Model.EmployeeMasterData;
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
        public long? ParamTaskPriorityID { get; set; }

        public long? ParamTaskStatusID { get; set; }

        public string? TaskName { get; set; }

        public string TaskDescription { get; set; }

        public bool IsActive { get; set; }

        public DateTime Deadline { get; set; }

        public DateTime TargetCompletionDate { get; set; }

        public long? AssignedByID { get; set; }

        public long? AssignedToID { get; set; }

        public ApplicationParamDetailModel ParamTaskType { get; set; }
        public ApplicationParamDetailModel ParamTaskPriority { get; set; }
        public ApplicationParamDetailModel ParamTaskStatus { get; set; }
        public BusinessPartnerModel AssignedBy { get; set; }
        public BusinessPartnerModel AssignedTo { get; set; }
    }
}