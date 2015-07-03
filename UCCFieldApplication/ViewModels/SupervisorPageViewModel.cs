using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UCCFieldApplication.Model;

namespace UCCFieldApplication.ViewModels
{
    public class SupervisorPageViewModel
    {
        private EmployeeCheckInDataContext context;

        public ObservableCollection<EmployeeCheckIn> Employee { get; set; }

        public bool IsDataLoaded { get; private set; }

        public SupervisorPageViewModel()
        {
            this.Employee = new ObservableCollection<EmployeeCheckIn>();
            context = new EmployeeCheckInDataContext(EmployeeCheckInDataContext.DBConnectionString);
            if (!context.DatabaseExists())
            {
                context.CreateDatabase();
                context.SubmitChanges();
            }
        }

        public void LoadEmployeeCheckInData()
        {
            if (context.Employees.Count() > 0)
            {
                List<EmployeeCheckIn> employeeCIO = context.Employees.ToList<EmployeeCheckIn>();
                Employee = new ObservableCollection<EmployeeCheckIn>(employeeCIO);

            }
            IsDataLoaded = true;
        }

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
