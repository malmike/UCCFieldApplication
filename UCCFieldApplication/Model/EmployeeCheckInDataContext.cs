using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UCCFieldApplication.Model
{
    class EmployeeCheckInDataContext : DataContext
    {

        public Table<EmployeeCheckIn> Employees;

        public static string DBConnectionString = "Data Source=isostore:/Employees.sdf";

        public EmployeeCheckInDataContext(string connectionString)
            : base(connectionString)
        {
            this.Employees = this.GetTable<EmployeeCheckIn>();
        }

    }
}
