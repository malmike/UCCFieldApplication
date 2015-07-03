using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UCCFieldApplication.Models
{
    public class EmployeeDetails
    {
        public string Serial { get; set; }
        public string EmpFn { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string IsSupervisor { get; set; }
    }

    public class SupervisorDetails
    {
        public string Serial { get; set; }
        public string EmpFn { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

    public class RootObject
    {
        public List<EmployeeDetails> data { get; set; }
    }

    public class SupervisorRootObject
    {
        public List<SupervisorDetails> data { get; set; }
    }

}
