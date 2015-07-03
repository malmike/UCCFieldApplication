using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using UCCFieldApplication.Model;


namespace UCCFieldApplication.ViewModels
{
    class EmployeeCheckInViewModel
    {
        private EmployeeCheckInDataContext context = new EmployeeCheckInDataContext(EmployeeCheckInDataContext.DBConnectionString);
        public ObservableCollection<EmployeeCheckIn> employeeCheck { get; set; }
        public bool IsDataLoaded { get; private set; }

        private EmployeeCheckIn employee = new EmployeeCheckIn();
        
        private EmployeeCheckIn _employeeCheckIn;

        public EmployeeCheckIn EmployeeCheckIn
        {
            get
            {
                return _employeeCheckIn;
            }
            set
            {
                _employeeCheckIn = value;
                NotifyPropertyChanged("EmployeeCheckIn");
            }
        }


        public EmployeeCheckInViewModel()
        {
            this.EmployeeCheckIn = new EmployeeCheckIn();
           
        }

        public EmployeeCheckInViewModel(int ID)
        {
            this.EmployeeCheckIn = context.Employees.Where(b => b.ID == ID).FirstOrDefault();
            
            context.SubmitChanges();

        }

        public void Save()
        {

            if (EmployeeCheckIn.ID <= 0)
            {
                context.Employees.InsertOnSubmit(this.EmployeeCheckIn);
            }

            context.SubmitChanges();
        }

        //public string getFinalValue()
        //{
        //    return context.Employees.Take(100).ToList().Last().EmployeeName;
        //}


        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (null != handler)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        
    }
}
