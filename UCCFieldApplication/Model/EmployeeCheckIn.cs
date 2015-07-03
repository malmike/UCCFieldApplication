using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UCCFieldApplication.Model
{
    [Table(Name = "EmployeeCheckIn")]
    public class EmployeeCheckIn : INotifyPropertyChanged, INotifyPropertyChanging
    {

        private int _id;

        [Column(IsPrimaryKey = true, DbType = "INT NOT NULL Identity", IsDbGenerated = true ,CanBeNull = false, AutoSync = AutoSync.OnInsert)]
        public int ID
        {
            get
            {
                return _id;
            }
            set
            {
                if (_id != value)
                {
                    NotifyPropertyChanging("ID");
                    _id = value;
                    NotifyPropertyChanged("ID");
                }
            }
        }

        private int _checkID;

        [Column(DbType = "INT", CanBeNull = false)]
        public int CheckID
        {
            get
            {
                return _checkID;
            }
            set
            {
                if (_checkID != value)
                {
                    NotifyPropertyChanging("CheckID");
                    _checkID = value;
                    NotifyPropertyChanged("CheckID");
                }
            }
        }

        private string _employeeName;

        [Column(DbType = "nvarchar(255)", CanBeNull = false)]
        public string EmployeeName
        {
            get
            {
                return _employeeName;
            }
            set
            {
                if (_employeeName != value)
                {
                    NotifyPropertyChanging("EmployeeName");
                    _employeeName = value;
                    NotifyPropertyChanged("EmployeeName");
                }
            }
        }


        private string _checkType;

        [Column(DbType = "nvarchar(255)", CanBeNull = false)]
        public string CheckType
        {
            get
            {
                return _checkType;
            }
            set
            {
                if (_checkType != value)
                {
                    NotifyPropertyChanging("CheckType");
                    _checkType = value;
                    NotifyPropertyChanged("CheckType");
                }
            }
        }

        private string _location;

        [Column(DbType = "nvarchar(255)", CanBeNull = false)]
        public string Location
        {
            get
            {
                return _location;
            }
            set
            {
                if (_location != value)
                {
                    NotifyPropertyChanging("Location");
                    _location = value;
                    NotifyPropertyChanged("Location");
                }
            }
        }

        private string _reason;

        [Column(DbType = "nvarchar(255)", CanBeNull = false)]
        public string Reason
        {
            get
            {
                return _reason;
            }
            set
            {
                if (_reason != value)
                {
                    NotifyPropertyChanging("Reason");
                    _reason = value;
                    NotifyPropertyChanged("Reason");
                }
            }
        }

        private string _approval;

        [Column(DbType = "nvarchar(255)", CanBeNull = false)]
        public string Approval
        {
            get
            {
                return _approval;
            }
            set
            {
                if (_approval != value)
                {
                    NotifyPropertyChanging("Approval");
                    _approval = value;
                    NotifyPropertyChanged("Approval");
                }
            }
        }

        public event PropertyChangingEventHandler PropertyChanging;

        // Used to notify the data context that a data context property is about to change
        private void NotifyPropertyChanging(string propertyName)
        {
            if (PropertyChanging != null)
            {
                PropertyChanging(this, new PropertyChangingEventArgs(propertyName));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        // Used to notify the page that a data context property changed
        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

    }
}
