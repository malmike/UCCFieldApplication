using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using UCCFieldApplication.Resources;
using System.Net.Http;
using UCCFieldApplication.ViewModels;
using System.Threading.Tasks;
using Microsoft.Phone.Notification;
using System.Text;

namespace UCCFieldApplication
{
    public partial class MainPage : PhoneApplicationPage
    {
        SharedInformation sharedInformation = SharedInformation.getInstance();
        AppSettings appSettings = new AppSettings();
        private HttpClient httpClient;
        private HttpResponseMessage response;

        /// Holds the push channel that is created or found.
        HttpNotificationChannel pushChannel;

        // The name of our push channel.
        string channelName = "ToastSampleChannel";

        // This is the url that will checked by the php file
        //private String phpAddress = "http://localhost/UCCFieldProject/Mobile/RetrieveEmployeeDetails/getEmployeeDetails.php";
        private String phpAddress = "http://localhost:29776/UCCFieldProjectPHP/Mobile/RetrieveEmployeeDetails/getEmployeeDetails.php";
        
        //private String phpAddress2 = "http://localhost/UCCFieldProject/Mobile/InsertEmployee/insertEmployee.php";
        private String phpAddress2 = "http://localhost:29776/UCCFieldProjectPHP/Mobile/InsertEmployee/insertEmployee.php";
       
        //private String phpAddress3 = "http://localhost/UCCFieldProject/Mobile/RetrieveEmployeeDetails/getSupervisor.php";
        private String phpAddress3 = "http://localhost:29776/UCCFieldProjectPHP/Mobile/RetrieveEmployeeDetails/getSupervisor.php";


        // Constructor
        public MainPage()
        {
            InitializeComponent();

            DataContext = App.ViewModel;

            // Try to find the push channel.
            pushChannel = HttpNotificationChannel.Find(channelName);

            // If the channel was not found, then create a new connection to the push service.
            if (pushChannel == null)
            {
                pushChannel = new HttpNotificationChannel(channelName);

                // Register for all the events before attempting to open the channel.
                pushChannel.ChannelUriUpdated += new EventHandler<NotificationChannelUriEventArgs>(PushChannel_ChannelUriUpdated);
                pushChannel.ErrorOccurred += new EventHandler<NotificationChannelErrorEventArgs>(PushChannel_ErrorOccurred);

                // Register for this notification only if you need to receive the notifications while your application is running.
                pushChannel.ShellToastNotificationReceived += new EventHandler<NotificationEventArgs>(PushChannel_ShellToastNotificationReceived);

                pushChannel.Open();


                // Bind this new channel for toast events.
                pushChannel.BindToShellToast();

            }
            else
            {
                // The channel was already open, so just register for all the events.
                pushChannel.ChannelUriUpdated += new EventHandler<NotificationChannelUriEventArgs>(PushChannel_ChannelUriUpdated);
                pushChannel.ErrorOccurred += new EventHandler<NotificationChannelErrorEventArgs>(PushChannel_ErrorOccurred);

                // Register for this notification only if you need to receive the notifications while your application is running.
                pushChannel.ShellToastNotificationReceived += new EventHandler<NotificationEventArgs>(PushChannel_ShellToastNotificationReceived);

                // Display the URI for testing purposes. Normally, the URI would be passed back to your web service at this point.
                //System.Diagnostics.Debug.WriteLine(pushChannel.ChannelUri.ToString());
                //MessageBox.Show(String.Format("Channel Uri is {0}", pushChannel.ChannelUri.ToString()));
            }


            // Sample code to localize the ApplicationBar
            //BuildLocalizedApplicationBar();

            httpClient = new HttpClient();

            // Add a user-agent header
            var headers = httpClient.DefaultRequestHeaders;

            // HttpProductInfoHeaderValueCollection is a collection of 
            // HttpProductInfoHeaderValue items used for the user-agent header

            headers.UserAgent.ParseAdd("ie");
            headers.UserAgent.ParseAdd("Mozilla/5.0 (compatible; MSIE 10.0; Windows NT 6.2; WOW64; Trident/6.0)");

        }

        void PushChannel_ChannelUriUpdated(object sender, NotificationChannelUriEventArgs e)
        {

            Dispatcher.BeginInvoke(() =>
            {
                // Display the new URI for testing purposes.   Normally, the URI would be passed back to your web service at this point.
                System.Diagnostics.Debug.WriteLine(e.ChannelUri.ToString());
                MessageBox.Show(String.Format("Channel Uri is {0}",
                    e.ChannelUri.ToString()));

            });
        }


        void PushChannel_ErrorOccurred(object sender, NotificationChannelErrorEventArgs e)
        {
            // Error handling logic for your particular application would be here.
            Dispatcher.BeginInvoke(() =>
                MessageBox.Show(String.Format("A push notification {0} error occurred.  {1} ({2}) {3}",
                    e.ErrorType, e.Message, e.ErrorCode, e.ErrorAdditionalData))
                    );
        }

        void PushChannel_ShellToastNotificationReceived(object sender, NotificationEventArgs e)
        {
            StringBuilder message = new StringBuilder();
            string relativeUri = string.Empty;

            message.AppendFormat("Received Toast {0}:\n", DateTime.Now.ToShortTimeString());

            // Parse out the information that was part of the message.
            foreach (string key in e.Collection.Keys)
            {
                //if (key != "wp:Param")
                message.AppendFormat("{0}\n",e.Collection[key]);

                if (string.Compare(
                    key,
                    "wp:Param",
                    System.Globalization.CultureInfo.InvariantCulture,
                    System.Globalization.CompareOptions.IgnoreCase) == 0)
                {
                    relativeUri = e.Collection[key];
                    AppSettings settings = new AppSettings();
                    settings.storeNavigationSettings(relativeUri); 
                }

            }

            
            // Display a dialog of all the fields in the toast.
            Dispatcher.BeginInvoke(() => MessageBox.Show(message.ToString()));
            

            
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                           
        }


        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            AppSettings appSettings = new AppSettings();

            if (appSettings.verifyRegistrationSettings())
            {
                sharedInformation.retrieveEmployeeDetails();
                sharedInformation.employeeData();

                GetSupervisorDetails(sharedInformation.empData.EmpFn);

                sharedInformation.jsn = await connectHttp(phpAddress3, sharedInformation.empData.EmpFn, null);

                NavigationService.Navigate(new Uri("/UserPage.xaml", UriKind.RelativeOrAbsolute));

            }
        }


        public async void GetSupervisorDetails(string employeeID)
        {
            string responseText = await connectHttp(phpAddress3, employeeID, null);
            sharedInformation.retrieveSupervisorDetails(responseText);
        }

        public async void RegisterUser(object sender, RoutedEventArgs e)
        {

            string responseText = await connectHttp(phpAddress, UserId.Text, null);

            await connectHttp(phpAddress2, responseText, pushChannel.ChannelUri.ToString());
            appSettings.storeRegistrationSettings(responseText.ToString());
            sharedInformation.retrieveEmployeeDetails();
            sharedInformation.employeeData();
            GetSupervisorDetails(sharedInformation.empData.EmpFn);

            sharedInformation.jsn = await connectHttp(phpAddress3, sharedInformation.empData.EmpFn, null);

            NavigationService.Navigate(new Uri("/UserPage.xaml", UriKind.RelativeOrAbsolute));

        }

 
        
        private async Task<string> connectHttp(string address, string x, string pushURI)
        {
            response = new HttpResponseMessage();

            Uri resourceUri;
            if (!Uri.TryCreate(address.Trim(), UriKind.Absolute, out resourceUri))
            {
                return "Invalid URI, please re-enter a valid URI";

            }
            if (resourceUri.Scheme != "http" && resourceUri.Scheme != "https")
            {
                return "Only 'http' and 'https' schemes supported. Please re-enter URI";
            }
            // ---------- end of test---------------------------------------------------------------------

            string responseText;
            Result.Text = "Waiting for response ...";

            try
            {
                MultipartFormDataContent content = new MultipartFormDataContent();
                content.Add((new StringContent(x, System.Text.Encoding.UTF8, "text/plain")), "UserID");
                if (pushURI != null)
                {
                    content.Add((new StringContent(pushURI, System.Text.Encoding.UTF8, "text/plain")), "PushURI");
                }
                response = await httpClient.PostAsync(resourceUri, content);
                response.EnsureSuccessStatusCode();
                responseText = await response.Content.ReadAsStringAsync();

                return responseText;

            }
            catch (Exception ex)
            {
                // Need to convert int HResult to hex string
                Result.Text = "Error = " + ex.HResult.ToString("X") +
                    "  Message: " + ex.Message;
                responseText = "";

                return responseText;
            }
            
        }

    }

    
}