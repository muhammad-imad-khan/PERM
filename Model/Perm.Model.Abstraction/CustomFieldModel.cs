using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Perm.Model.Abstraction
{
    public class CustomFieldModel : ModelBase
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        public long CustomFieldID { get; set; }

        /// <summary>
        /// Acting as foreign key column with the combination of Entity Type.
        /// For Example: If we are storing custom fields of User table, then here we will insert the UserID.
        /// </summary>
        public long EntityID { get; set; }

        /// <summary>
        /// Set the table for custom filed.
        /// This field already exist in Config.CustomFieldMeta table, we created this field here too, to avoid the complex join quires.
        /// ApplicationParamMasterID => 7
        /// </summary>

        public long ParamEntityTypeID { get; set; }


        /// <summary>
        /// Placeholder to store string
        /// </summary>
        [StringLength(200)]
        public string StringField1 { get; set; }


        /// <summary>
        /// Placeholder to store string
        /// </summary>
        [StringLength(200)]
        public string StringField2 { get; set; }
        /// <summary>
        /// Placeholder to store string
        /// </summary>
        [StringLength(200)]
        public string StringField3 { get; set; }
        /// <summary>
        /// Placeholder to store string
        /// </summary>
        [StringLength(200)]
        public string StringField4 { get; set; }
        /// <summary>
        /// Placeholder to store string
        /// </summary>
        [StringLength(200)]
        public string StringField5 { get; set; }
        /// <summary>
        /// Placeholder to store string
        /// </summary>
        [StringLength(200)]
        public string StringField6 { get; set; }
        /// <summary>
        /// Placeholder to store string
        /// </summary>
        [StringLength(200)]
        public string StringField7 { get; set; }
        /// <summary>
        /// Placeholder to store string
        /// </summary>
        [StringLength(200)]
        public string StringField8 { get; set; }
        /// <summary>
        /// Placeholder to store string
        /// </summary>
        [StringLength(200)]
        public string StringField9 { get; set; }
        /// <summary>
        /// Placeholder to store string
        /// </summary>
        [StringLength(200)]
        public string StringField10 { get; set; }
        /// <summary>
        /// Placeholder to store string
        /// </summary>
        [StringLength(200)]
        public string StringField11 { get; set; }
        /// <summary>
        /// Placeholder to store string
        /// </summary>
        [StringLength(200)]
        public string StringField12 { get; set; }
        /// <summary>
        /// Placeholder to store string
        /// </summary>
        [StringLength(200)]
        public string StringField13 { get; set; }
        /// <summary>
        /// Placeholder to store string
        /// </summary>
        [StringLength(200)]
        public string StringField14 { get; set; }
        /// <summary>
        /// Placeholder to store string
        /// </summary>
        [StringLength(200)]
        public string StringField15 { get; set; }
        /// <summary>
        /// Placeholder to store string
        /// </summary>
        [StringLength(200)]
        public string StringField16 { get; set; }
        /// <summary>
        /// Placeholder to store string
        /// </summary>
        [StringLength(200)]
        public string StringField17 { get; set; }
        /// <summary>
        /// Placeholder to store string
        /// </summary>
        [StringLength(200)]
        public string StringField18 { get; set; }
        /// <summary>
        /// Placeholder to store string
        /// </summary>
        [StringLength(200)]
        public string StringField19 { get; set; }
        /// <summary>
        /// Placeholder to store string
        /// </summary>
        [StringLength(200)]
        public string StringField20 { get; set; }
        /// <summary>
        /// Placeholder to store number
        /// </summary>
        [Column(TypeName = "numeric(18, 6)")]
        public decimal? NumberField1 { get; set; }
        /// <summary>
        /// Placeholder to store number
        /// </summary>
        [Column(TypeName = "numeric(18, 6)")]
        public decimal? NumberField2 { get; set; }
        /// <summary>
        /// Placeholder to store number
        /// </summary>
        [Column(TypeName = "numeric(18, 6)")]
        public decimal? NumberField3 { get; set; }
        /// <summary>
        /// Placeholder to store number
        /// </summary>
        [Column(TypeName = "numeric(18, 6)")]
        public decimal? NumberField4 { get; set; }
        /// <summary>
        /// Placeholder to store number
        /// </summary>
        [Column(TypeName = "numeric(18, 6)")]
        public decimal? NumberField5 { get; set; }
        /// <summary>
        /// Placeholder to store number
        /// </summary>
        [Column(TypeName = "numeric(18, 6)")]
        public decimal? NumberField6 { get; set; }
        /// <summary>
        /// Placeholder to store number
        /// </summary>
        [Column(TypeName = "numeric(18, 6)")]
        public decimal? NumberField7 { get; set; }
        /// <summary>
        /// Placeholder to store number
        /// </summary>
        [Column(TypeName = "numeric(18, 6)")]
        public decimal? NumberField8 { get; set; }
        /// <summary>
        /// Placeholder to store number
        /// </summary>
        [Column(TypeName = "numeric(18, 6)")]
        public decimal? NumberField9 { get; set; }
        /// <summary>
        /// Placeholder to store number
        /// </summary>
        [Column(TypeName = "numeric(18, 6)")]
        public decimal? NumberField10 { get; set; }
        /// <summary>
        /// Placeholder to store number
        /// </summary>
        [Column(TypeName = "numeric(18, 6)")]
        public decimal? NumberField11 { get; set; }
        /// <summary>
        /// Placeholder to store number
        /// </summary>
        [Column(TypeName = "numeric(18, 6)")]
        public decimal? NumberField12 { get; set; }
        /// <summary>
        /// Placeholder to store number
        /// </summary>
        [Column(TypeName = "numeric(18, 6)")]
        public decimal? NumberField13 { get; set; }
        /// <summary>
        /// Placeholder to store number
        /// </summary>
        [Column(TypeName = "numeric(18, 6)")]
        public decimal? NumberField14 { get; set; }
        /// <summary>
        /// Placeholder to store number
        /// </summary>
        [Column(TypeName = "numeric(18, 6)")]
        public decimal? NumberField15 { get; set; }
        /// <summary>
        /// Placeholder to store number
        /// </summary>
        [Column(TypeName = "numeric(18, 6)")]
        public decimal? NumberField16 { get; set; }
        /// <summary>
        /// Placeholder to store number
        /// </summary>
        [Column(TypeName = "numeric(18, 6)")]
        public decimal? NumberField17 { get; set; }
        /// <summary>
        /// Placeholder to store number
        /// </summary>
        [Column(TypeName = "numeric(18, 6)")]
        public decimal? NumberField18 { get; set; }
        /// <summary>
        /// Placeholder to store number
        /// </summary>
        [Column(TypeName = "numeric(18, 6)")]
        public decimal? NumberField19 { get; set; }
        /// <summary>
        /// Placeholder to store number
        /// </summary>
        [Column(TypeName = "numeric(18, 6)")]
        public decimal? NumberField20 { get; set; }
        /// <summary>
        /// Placeholder to store datetime
        /// </summary>
        [Column(TypeName = "datetime")]
        public DateTime? DateField1 { get; set; }
        /// <summary>
        /// Placeholder to store datetime
        /// </summary>
        [Column(TypeName = "datetime")]
        public DateTime? DateField2 { get; set; }
        /// <summary>
        /// Placeholder to store datetime
        /// </summary>
        [Column(TypeName = "datetime")]
        public DateTime? DateField3 { get; set; }
        /// <summary>
        /// Placeholder to store datetime
        /// </summary>
        [Column(TypeName = "datetime")]
        public DateTime? DateField4 { get; set; }
        /// <summary>
        /// Placeholder to store datetime
        /// </summary>
        [Column(TypeName = "datetime")]
        public DateTime? DateField5 { get; set; }
        /// <summary>
        /// Placeholder to store datetime
        /// </summary>
        [Column(TypeName = "datetime")]
        public DateTime? DateField6 { get; set; }
        /// <summary>
        /// Placeholder to store datetime
        /// </summary>
        [Column(TypeName = "datetime")]
        public DateTime? DateField7 { get; set; }
        /// <summary>
        /// Placeholder to store datetime
        /// </summary>
        [Column(TypeName = "datetime")]
        public DateTime? DateField8 { get; set; }
        /// <summary>
        /// Placeholder to store datetime
        /// </summary>
        [Column(TypeName = "datetime")]
        public DateTime? DateField9 { get; set; }
        /// <summary>
        /// Placeholder to store datetime
        /// </summary>
        [Column(TypeName = "datetime")]
        public DateTime? DateField10 { get; set; }
        /// <summary>
        /// Placeholder to store datetime
        /// </summary>
        [Column(TypeName = "datetime")]
        public DateTime? DateField11 { get; set; }
        /// <summary>
        /// Placeholder to store datetime
        /// </summary>
        [Column(TypeName = "datetime")]
        public DateTime? DateField12 { get; set; }
        /// <summary>
        /// Placeholder to store datetime
        /// </summary>
        [Column(TypeName = "datetime")]
        public DateTime? DateField13 { get; set; }
        /// <summary>
        /// Placeholder to store datetime
        /// </summary>
        [Column(TypeName = "datetime")]
        public DateTime? DateField14 { get; set; }
        /// <summary>
        /// Placeholder to store datetime
        /// </summary>
        [Column(TypeName = "datetime")]
        public DateTime? DateField15 { get; set; }
        /// <summary>
        /// Placeholder to store datetime
        /// </summary>
        [Column(TypeName = "datetime")]
        public DateTime? DateField16 { get; set; }
        /// <summary>
        /// Placeholder to store datetime
        /// </summary>
        [Column(TypeName = "datetime")]
        public DateTime? DateField17 { get; set; }
        /// <summary>
        /// Placeholder to store datetime
        /// </summary>
        [Column(TypeName = "datetime")]
        public DateTime? DateField18 { get; set; }
        /// <summary>
        /// Placeholder to store datetime
        /// </summary>
        [Column(TypeName = "datetime")]
        public DateTime? DateField19 { get; set; }
        /// <summary>
        /// Placeholder to store datetime
        /// </summary>
        [Column(TypeName = "datetime")]
        public DateTime? DateField20 { get; set; }

        /// <summary>
        /// Placeholder to store amount
        /// </summary>
        [Column(TypeName = "money")]
        public decimal? AmountField1 { get; set; }

        /// <summary>
        /// Placeholder to store amount
        /// </summary>
        [Column(TypeName = "money")]
        public decimal? AmountField2 { get; set; }

        /// <summary>
        /// Placeholder to store amount
        /// </summary>
        [Column(TypeName = "money")]
        public decimal? AmountField3 { get; set; }

        /// <summary>
        /// Placeholder to store amount
        /// </summary>
        [Column(TypeName = "money")]
        public decimal? AmountField4 { get; set; }

        /// <summary>
        /// Placeholder to store amount
        /// </summary>
        [Column(TypeName = "money")]
        public decimal? AmountField5 { get; set; }


        /// <summary>
        /// Placeholder to store amount
        /// </summary>
        [Column(TypeName = "money")]
        public decimal? AmountField6 { get; set; }


        /// <summary>
        /// Placeholder to store amount
        /// </summary>
        [Column(TypeName = "money")]
        public decimal? AmountField7 { get; set; }


        /// <summary>
        /// Placeholder to store amount
        /// </summary>
        [Column(TypeName = "money")]
        public decimal? AmountField8 { get; set; }


        /// <summary>
        /// Placeholder to store amount
        /// </summary>
        [Column(TypeName = "money")]
        public decimal? AmountField9 { get; set; }

        /// <summary>
        /// Placeholder to store amount
        /// </summary>
        [Column(TypeName = "money")]
        public decimal? AmountField10 { get; set; }

        /// <summary>
        /// Placeholder to store amount
        /// </summary>
        [Column(TypeName = "money")]
        public decimal? AmountField11 { get; set; }

        /// <summary>
        /// Placeholder to store amount
        /// </summary>
        [Column(TypeName = "money")]
        public decimal? AmountField12 { get; set; }

        /// <summary>
        /// Placeholder to store amount
        /// </summary>
        [Column(TypeName = "money")]
        public decimal? AmountField13 { get; set; }

        /// <summary>
        /// Placeholder to store amount
        /// </summary>
        [Column(TypeName = "money")]
        public decimal? AmountField14 { get; set; }

        /// <summary>
        /// Placeholder to store amount
        /// </summary>
        [Column(TypeName = "money")]
        public decimal? AmountField15 { get; set; }

        /// <summary>
        /// Placeholder to store amount
        /// </summary>
        [Column(TypeName = "money")]
        public decimal? AmountField16 { get; set; }

        /// <summary>
        /// Placeholder to store amount
        /// </summary>
        [Column(TypeName = "money")]
        public decimal? AmountField17 { get; set; }

        /// <summary>
        /// Placeholder to store amount
        /// </summary>
        [Column(TypeName = "money")]
        public decimal? AmountField18 { get; set; }

        /// <summary>
        /// Placeholder to store amount
        /// </summary>
        [Column(TypeName = "money")]
        public decimal? AmountField19 { get; set; }

        /// <summary>
        /// Placeholder to store amount
        /// </summary>
        [Column(TypeName = "money")]
        public decimal? AmountField20 { get; set; }

        /// <summary>
        /// Placeholder to store boolean
        /// </summary>
        [Column(TypeName = "bit")]
        public bool? BoolField1 { get; set; }

        /// <summary>
        /// Placeholder to store boolean
        /// </summary>
        [Column(TypeName = "bit")]
        public bool? BoolField2 { get; set; }

        /// <summary>
        /// Placeholder to store boolean
        /// </summary>
        [Column(TypeName = "bit")]
        public bool? BoolField3 { get; set; }

        /// <summary>
        /// Placeholder to store boolean
        /// </summary>
        [Column(TypeName = "bit")]
        public bool? BoolField4 { get; set; }

        /// <summary>
        /// Placeholder to store boolean
        /// </summary>
        [Column(TypeName = "bit")]
        public bool? BoolField5 { get; set; }

        /// <summary>
        /// Placeholder to store boolean
        /// </summary>
        [Column(TypeName = "bit")]
        public bool? BoolField6 { get; set; }

        /// <summary>
        /// Placeholder to store boolean
        /// </summary>
        [Column(TypeName = "bit")]
        public bool? BoolField7 { get; set; }

        /// <summary>
        /// Placeholder to store boolean
        /// </summary>
        [Column(TypeName = "bit")]
        public bool? BoolField8 { get; set; }

        /// <summary>
        /// Placeholder to store boolean
        /// </summary>
        [Column(TypeName = "bit")]
        public bool? BoolField9 { get; set; }

        /// <summary>
        /// Placeholder to store boolean
        /// </summary>
        [Column(TypeName = "bit")]
        public bool? BoolField10 { get; set; }

        /// <summary>
        /// Placeholder to store list field value
        /// </summary>
        [StringLength(200)]
        public string ListField1 { get; set; }

        /// <summary>
        /// Placeholder to store list field value
        /// </summary>
        [StringLength(200)]
        public string ListField2 { get; set; }

        /// <summary>
        /// Placeholder to store list field value
        /// </summary>
        [StringLength(200)]
        public string ListField3 { get; set; }

        /// <summary>
        /// Placeholder to store list field value
        /// </summary>
        [StringLength(200)]
        public string ListField4 { get; set; }

        /// <summary>
        /// Placeholder to store list field value
        /// </summary>
        [StringLength(200)]
        public string ListField5 { get; set; }

        /// <summary>
        /// Placeholder to store list field value
        /// </summary>
        [StringLength(200)]
        public string ListField6 { get; set; }

        /// <summary>
        /// Placeholder to store list field value
        /// </summary>
        [StringLength(200)]
        public string ListField7 { get; set; }

        /// <summary>
        /// Placeholder to store list field value
        /// </summary>
        [StringLength(200)]
        public string ListField8 { get; set; }

        /// <summary>
        /// Placeholder to store list field value
        /// </summary>
        [StringLength(200)]
        public string ListField9 { get; set; }

        /// <summary>
        /// Placeholder to store list field value
        /// </summary>
        [StringLength(200)]
        public string ListField10 { get; set; }
    }
}