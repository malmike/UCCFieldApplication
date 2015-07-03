using System;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UCCFieldApplication.Models;

namespace UCCFieldApplication.ViewModels
{
    class AppSettings
    {
        public void storeRegistrationSettings(string json)
        {
            IsolatedStorageSettings settings = IsolatedStorageSettings.ApplicationSettings;
            settings["employeeDetails"] = json;
        }



        public string retrieveRegistrationSettings()
        {
            IsolatedStorageSettings settings = IsolatedStorageSettings.ApplicationSettings;
            if (settings.Contains("employeeDetails"))
            {
                return settings["employeeDetails"].ToString();

            }
            else
            {
                return null;
            }

        }

        public bool verifyRegistrationSettings()
        {
            IsolatedStorageSettings settings = IsolatedStorageSettings.ApplicationSettings;
            try
            {
                if(settings.Contains("employeeDetails") & (settings["employeeDetails"].ToString() != null || settings["employeeDetails"].ToString() != ""))
                    return true;
                return false;

            }
            catch
            {
                return false;
            }
                    
        }

        public void storeNavigationSettings(string navURI)
        {
            IsolatedStorageSettings settings = IsolatedStorageSettings.ApplicationSettings;
            settings["navigationDetails"] = navURI;
        }

        public string retrieveNavigationSettings()
        {
            IsolatedStorageSettings settings = IsolatedStorageSettings.ApplicationSettings;
            if (settings.Contains("navigationDetails"))
            {
                return settings["navigationDetails"].ToString();

            }
            else
            {
                return null;
            }

        }

        public bool checkNavSettings()
        {
            IsolatedStorageSettings settings = IsolatedStorageSettings.ApplicationSettings;
            if (settings.Contains("navigationDetails"))
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public void deleteNavigationSetting()
        {
            IsolatedStorageSettings settings = IsolatedStorageSettings.ApplicationSettings;
            if (settings.Contains("navigationDetails"))
            {
                settings.Remove("navigationDetails");
            }
        }

    }
}
