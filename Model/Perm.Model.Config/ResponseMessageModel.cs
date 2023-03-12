using Perm.Model.Abstraction;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Perm.Model.Config
{
    /// <summary>
    /// </summary>
    [Table(name: "ResponseMessage", Schema = "Config")]
    public class ResponseMessageModel : ModelBase
    {
        /// <summary>
        /// 
        /// </summary>
        [Key] public long ResponseMessageID { get; set; }

        /// <summary>
        /// Error code identifier
        /// </summary>
        [Required]
        [StringLength(4)]
        public string ResponseCode { get; set; }

        /// <summary>
        /// User friendly message to be shown on UI
        /// </summary>
        [Required]
        [StringLength(500)]
        public string ResponseMessage { get; set; }
    }
}