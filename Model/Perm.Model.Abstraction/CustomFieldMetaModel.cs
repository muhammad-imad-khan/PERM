using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Perm.Model.Abstraction
{
    /// <summary>
    /// If any table holds the record for custom fields, then this table hold its meta information here
    /// </summary>
    [Table(name: "CustomFieldMeta", Schema = "Config")]
    public class CustomFieldMetaModel : ModelBase
    {
        /// <summary>
        /// 
        /// </summary>
        [Key] public long CustomMetaID { get; set; }

        /// <summary>
        /// Set the table for custom filed
        /// ApplicationParamMasterID => 7
        /// </summary>
        public long ParamEntityTypeID { get; set; }

        /// <summary>
        /// Column display name on UI
        /// </summary>
        [Required]
        [StringLength(50)]
        public string DisplayName { get; set; }

        /// <summary>
        /// Column name that need to be add. This column name should be exactly same as defined in Config.CustomField table.
        /// </summary>
        [Required]
        [StringLength(50)]
        public string ColumnName { get; set; }

        /// <summary>
        /// To store list values as csv when list data type is selected.
        /// </summary>
        public string ListValuesCSV { get; set; }

        /// <summary>
        /// This field extract the number from custom column.
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public int ColumnNumber { get; set; }

        /// <summary>
        /// Specify the data type of column, data type will map the columns defined in custom filed table accordingly.
        /// Currently this field accept following values
        /// 1) STRING
        /// 2) DATE_TIME
        /// 3) NUMBER
        /// 4) AMOUNT
        /// 5) BOOLEAN
        /// 6) LIST
        /// Reference Master ID => 30
        /// </summary>
        [Required]
        [StringLength(10)]
        public long ParamDataTypeID { get; set; }

        /// <summary>
        /// Custom Field Description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Is custom field mandatory to be filled.
        /// </summary>
        public bool IsRequired { get; set; }

        [StringLength(200)]
        public string SectionName { get; set; }

        /// <summary>
        /// ---IGNORE_IN_MIGRATIONS---
        /// </summary>
        public ApplicationParamDetailModel ParamEntityType { get; set; }

        /// <summary>
        /// ---IGNORE_IN_MIGRATIONS---
        /// </summary>
        public ApplicationParamDetailModel ParamDataType { get; set; }
    }
}