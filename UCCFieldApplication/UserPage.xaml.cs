using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using UCCFieldApplication.ViewModels;
using Microsoft.Phone.Notification;
using System.Text;
using System.Net.Http;
using UCCFieldApplication.Models;

namespace UCCFieldApplication
{
    public partial class UserPage : PhoneApplicationPage
    {
        SharedInformation sharedInfo = SharedInformation.getInstance();

        private HttpClient httpClient;
        private HttpResponseMessage response;

        /// Holds the push channel that is created or found.
        HttpNotificationChannel pushChannel;

        // This is the url that will checked by the php file
        //private String phpAddress = "http://localhost/UCCFieldProject/Mobile/PushNotifications/employeeCheckIn.php";
        private String phpAddress = "http://localhost:29776/UCCFieldProjectPHP/Mobile/PushNotifications/employeeCheckIn.php";



        public UserPage()
        {
            InitializeComponent();

            httpClient = new HttpClient();

            // Add a user-agent header
            var headers = httpClient.DefaultRequestHeaders;

            // HttpProductInfoHeaderValueCollection is a collection of 
            // HttpProductInfoHeaderValue items used for the user-agent header

            headers.UserAgent.ParseAdd("ie");
            headers.UserAgent.ParseAdd("Mozilla/5.0 (compatible; MSIE 10.0; Windows NT 6.2; WOW64; Trident/6.0)");
        }


        

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            employee.ItemsSource = sharedInfo.employeeDetails;
            supervisorName.Text = sharedInfo.supervisorDetails.FirstName + "    " + sharedInfo.supervisorDetails.LastName;
            AppSettings settings = new AppSettings();
            EmployeeCheckInViewModel viewModel = new EmployeeCheckInViewModel();

            //if (!settings.checkNavSettings())
            //{
            //    string empFN = viewModel.getFinalValue();

            //}
           
        }

        private void checkingIn(object sender, RoutedEventArgs e)
        {
            string employeeName = sharedInfo.empData.FirstName + " " + sharedInfo.empData.LastName;
            connectHttp(phpAddress, sharedInfo.empData.EmpFn, employeeName, sharedInfo.supervisorDetails.EmpFn, locationBox.Text, reasonBox.Text, "1");
            locationBox.Text = "";
            reasonBox.Text = "";             
        }

        private void checkingOut(object sender, RoutedEventArgs e)
        {
            string employeeName = sharedInfo.empData.FirstName + " " + sharedInfo.empData.LastName;
            connectHttp(phpAddress, sharedInfo.empData.EmpFn, employeeName, sharedInfo.supervisorDetails.EmpFn, locationBox.Text, reasonBox.Text, "0");
            locationBox.Text = "";
            reasonBox.Text = "";
        }


        private async void connectHttp(string address, string UserID, string EmployeeName, string SupervisorID, string Location, string Reason, string CheckType)
        {
            response = new HttpResponseMessage();
            string responseText;

            Uri resourceUri;
            if (!Uri.TryCreate(address.Trim(), UriKind.Absolute, out resourceUri))
            {
                //return "Invalid URI, please re-enter a valid URI";
                return;

            }
            if (resourceUri.Scheme != "http" && resourceUri.Scheme != "https")
            {
                //return "Only 'http' and 'https' schemes supported. Please re-enter URI";
                return;
            }
            // ---------- end of test---------------------------------------------------------------------

            try
            {
                MultipartFormDataContent content = new MultipartFormDataContent();
                content.Add((new StringContent(UserID, System.Text.Encoding.UTF8, "text/plain")), "UserID");
                content.Add((new StringContent(EmployeeName, System.Text.Encoding.UTF8, "text/plain")), "EmployeeName");
                content.Add((new StringContent(SupervisorID, System.Text.Encoding.UTF8, "text/plain")), "SupervisorID");
                content.Add((new StringContent(Location, System.Text.Encoding.UTF8, "text/plain")), "Location");
                content.Add((new StringContent(Reason, System.Text.Encoding.UTF8, "text/plain")), "Reason");
                content.Add((new StringContent(CheckType, System.Text.Encoding.UTF8, "text/plain")), "CheckType");
              
                response = await httpClient.PostAsync(resourceUri, content);
                response.EnsureSuccessStatusCode();
                responseText = await response.Content.ReadAsStringAsync();

            }
            catch (Exception ex)
            {
                // Need to convert int HResult to hex string
                //Result.Text = "Error = " + ex.HResult.ToString("X") +
                //    "  Message: " + ex.Message;
                responseText = "";

            }

        }

        private void supervisorPage(object sender, System.Windows.Input.GestureEventArgs e)
        {
            AppSettings settings = new AppSettings();
            if(settings.retrieveNavigationSettings() == null)
            {
                NavigationService.Navigate(new Uri("/SupervisorPage.xaml", UriKind.RelativeOrAbsolute));
            }
            else
            {
                NavigationService.Navigate(new Uri(settings.retrieveNavigationSettings(), UriKind.RelativeOrAbsolute));
            }
        }

       

    }
}