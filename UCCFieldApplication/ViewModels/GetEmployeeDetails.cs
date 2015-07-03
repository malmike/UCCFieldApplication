using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UCCFieldApplication.Models;

namespace UCCFieldApplication.ViewModels
{
    class GetEmployeeDetails
    {

        public EmployeeDetails emp { get; private set; }
        public SupervisorDetails supervisor { get; private set; }
        public ObservableCollection<EmployeeDetails> employeeData { get; private set; }
        public ObservableCollection<EmployeeDetails> DataElements(String json)
        {

            try
            {

                var rootObject = JsonConvert.DeserializeObject<RootObject>(json);
                foreach (EmployeeDetails f in rootObject.data)
                {
                    this.emp = f;
                }
                
                employeeData = new ObservableCollection<EmployeeDetails>(rootObject.data);
                return employeeData;

            }
            catch (Exception)
            {
                return null;
            }
        }

        public SupervisorDetails SupervisorDataElements(String json)
        {

            try
            {

                var rootObject = JsonConvert.DeserializeObject<SupervisorRootObject>(json);
                foreach (SupervisorDetails f in rootObject.data)
                {
                    this.supervisor = f;
                }

                return this.supervisor;

            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
