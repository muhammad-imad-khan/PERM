using System.ComponentModel.DataAnnotations.Schema;

namespace Perm.Model.Abstraction
{
    public class ModelBase
    {
        public bool IsDeleted { get; set; }

        [Column(TypeName = "datetime", Order = 998)]
        public DateTime CreatedOn { get; set; } = DateTime.Now;

        [Column(Order = 999)]
        public long CreatedBy { get; set; }
    }
}