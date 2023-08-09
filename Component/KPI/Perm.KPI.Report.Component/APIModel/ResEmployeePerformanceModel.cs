using Perm.KPI.Report.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Perm.KPI.Report.Component.APIModel
{
    public class ResEmployeePerformanceModel : View_EmployeePerformanceModel
    {
        public int TaskPoint { get; set; }
        public int AttendancePoint { get; set; }
        public double RatingPoint { get; set; }
    }
}
