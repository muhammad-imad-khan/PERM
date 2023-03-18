namespace Perm.Common.APIModel
{
    public class ReqCustomFieldModel
    {
        public string ColumnName { get; set; }

        /// <summary>
        /// STRING Field property to hold value of any string column
        /// </summary>
        public string StringField { get; set; }

        /// <summary>
        /// NUMBER Field property to hold value of any number column
        /// </summary>
        public decimal? NumberField { get; set; }

        /// <summary>
        /// AMOUNT Field property to hold value of any amount column
        /// </summary>
        public decimal? AmountField { get; set; }

        /// <summary>
        /// DATE Field property to hold value of any date column
        /// </summary>
        public DateTime? DateField { get; set; }

        /// <summary>
        /// BOOL Field property to hold value of any bool column
        /// </summary>
        public bool? BoolField { get; set; }

        /// <summary>
        /// LIST Field property to hold value of any list column
        /// </summary>
        public string ListField { get; set; }
    }
}