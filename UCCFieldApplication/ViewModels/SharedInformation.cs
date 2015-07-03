using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UCCFieldApplication.Models;

namespace UCCFieldApplication.ViewModels
{
    class SharedInformation
    {
        private static SharedInformation instance = new SharedInformation();
        GetEmployeeDetails getEmployeeDetails = new GetEmployeeDetails();
        AppSettings appSettings;
        public SupervisorDetails supervisorDetails { get; private set; }
        public ObservableCollection<EmployeeDetails> employeeDetails { get; private set; }
        public EmployeeDetails empData { get; private set; }

        public string jsn { get; set; }

        private SharedInformation() { }

        public static SharedInformation getInstance()
        {
            return instance;
        }

        public void retrieveEmployeeDetails()
        {
            appSettings = new AppSettings();

            this.employeeDetails = getEmployeeDetails.DataElements(appSettings.retrieveRegistrationSettings());
      
        }

        public void retrieveSupervisorDetails(String json)
        {
            this.supervisorDetails = getEmployeeDetails.SupervisorDataElements(json);
        }


        public void employeeData()
        {
            empData = getEmployeeDetails.emp;
            
        }

        

    }

}
