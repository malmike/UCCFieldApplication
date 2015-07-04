using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Net.Http;
using System.Threading.Tasks;
using UCCFieldApplication.ViewModels;
using System.ComponentModel;
using System.Collections;
using UCCFieldApplication.Model;

namespace UCCFieldApplication
{
    public partial class SupervisorPage : PhoneApplicationPage
    {
        private HttpClient httpClient;
        private HttpResponseMessage response;
        private EmployeeCheckInViewModel viewModel;
        private GetReason reason = new GetReason();
        SupervisorPageViewModel supViewModel = new SupervisorPageViewModel();


        class PivotCallbacks
        {
            public Action Init { get; set; }
            public Action OnActivated { get; set; }
            public Action<CancelEventArgs> OnBackKeyPress { get; set; }
        }

        //PivotCallbacks _callbacks;


        // This is the url that will checked by the php file
        
        //private string phpAddress = "http://localhost/UCCFieldProject/Mobile/RetrieveEmployeeDetails/getReason.php";
        private string phpAddress = "http://localhost:29776/UCCFieldProjectPHP/Mobile/RetrieveEmployeeDetails/getReason.php";
        private string updateApprovalURI = "http://localhost:29776/UCCFieldProjectPHP/Mobile/ChangeApproval/ChangeApproval.php";
        
        public SupervisorPage()
        {
            InitializeComponent();
            //DataContext = App.ViewModel;

            httpClient = new HttpClient();

            // Add a user-agent header
            var headers = httpClient.DefaultRequestHeaders;

            // HttpProductInfoHeaderValueCollection is a collection of 
            // HttpProductInfoHeaderValue items used for the user-agent header

            headers.UserAgent.ParseAdd("ie");
            headers.UserAgent.ParseAdd("Mozilla/5.0 (compatible; MSIE 10.0; Windows NT 6.2; WOW64; Trident/6.0)");

        }

        protected async override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            viewModel = new EmployeeCheckInViewModel();


            CreateSupervisorApplicationBarItems();
            SupervisorItemActivated();
            //SupervisorBackKeyPressed();
  


            if (this.NavigationContext.QueryString.ContainsKey("checkID"))
            {
                viewModel.EmployeeCheckIn.EmployeeName = this.NavigationContext.QueryString["employeeName"];
                viewModel.EmployeeCheckIn.CheckID = Int32.Parse(this.NavigationContext.QueryString["checkID"]);
                viewModel.EmployeeCheckIn.Location = this.NavigationContext.QueryString["location"];
                viewModel.EmployeeCheckIn.CheckType = this.NavigationContext.QueryString["checkType"];
                viewModel.EmployeeCheckIn.Reason = reason.DataElements(await connectHttp(phpAddress, viewModel.EmployeeCheckIn.CheckID.ToString(), 1));
                viewModel.EmployeeCheckIn.Approval = "Pending";

                viewModel.Save();

                AppSettings settings = new AppSettings();
                settings.deleteNavigationSetting();
            }

            supViewModel.LoadEmployeeCheckInData();
            llsEmployee.ItemsSource = supViewModel.Employee;            

        }

        protected override void OnBackKeyPress(CancelEventArgs e)
        {
            base.OnBackKeyPress(e);
            SupervisorBackKeyPressed(e);
        }

        private async Task<string> connectHttp(string address, string checkID, int value)
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

            try
            {

                MultipartFormDataContent content = new MultipartFormDataContent();
                content.Add((new StringContent(checkID, System.Text.Encoding.UTF8, "text/plain")), "CheckID");

                response = await httpClient.PostAsync(resourceUri, content);
                response.EnsureSuccessStatusCode();
                responseText = await response.Content.ReadAsStringAsync();

                return responseText;

            }
            catch (Exception)
            {
                // Need to convert int HResult to hex string
                responseText = "";

                return responseText;
            }

        }

        private void llsEmployee_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateSupervisorApplicationBar();          
        }

        private void OnItemContentTap(object sender, System.Windows.Input.GestureEventArgs e)
        {

        }

        private void llsEmployee_IsSelectionEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            SetupSupervisorApplicationBar();
        }

        ApplicationBarIconButton select;
        ApplicationBarIconButton accept;
        ApplicationBarIconButton deny;

        private void CreateSupervisorApplicationBarItems()
        {
            select = new ApplicationBarIconButton();
            select.IconUri = new Uri("/Assets/Toolkit.Content/ApplicationBar.Select.png", UriKind.RelativeOrAbsolute);
            select.Text = "select";
            select.Click += OnSelectClick;

            accept = new ApplicationBarIconButton();
            accept.IconUri = new Uri("/Assets/Toolkit.Content/ApplicationBar.Check.png", UriKind.RelativeOrAbsolute);
            accept.Text = "accept";
            accept.Click += OnAcceptClick;

            deny = new ApplicationBarIconButton();
            deny.IconUri = new Uri("/Assets/Toolkit.Content/ApplicationBar.Cancel.png", UriKind.RelativeOrAbsolute);
            deny.Text = "deny";
            deny.Click += OnDenyClick;

            
        }

        void OnSelectClick(object sender, EventArgs e)
        {
            llsEmployee.IsSelectionEnabled = true;
        }

        void OnAcceptClick(object sender, EventArgs e)
        {
            IList source = llsEmployee.ItemsSource as IList;
            List<EmployeeCheckIn> employeeList = new List<EmployeeCheckIn>();
            while (llsEmployee.SelectedItems.Count > 0)
            {
                employeeList.Add((EmployeeCheckIn)llsEmployee.SelectedItems[0]);
                viewModel.updateApproval(((EmployeeCheckIn)llsEmployee.SelectedItems[0]).CheckID, "Accept");

                source.Remove((EmployeeCheckIn)llsEmployee.SelectedItems[0]);
            }
            changeApproval(updateApprovalURI, employeeList, employeeList.Count(), "Accept");
        }

        void OnDenyClick(object sender, EventArgs e)
        {
            IList source = llsEmployee.ItemsSource as IList;
            List<EmployeeCheckIn> employeeList = new List<EmployeeCheckIn>();
            while (llsEmployee.SelectedItems.Count > 0)
            {
                employeeList.Add((EmployeeCheckIn)llsEmployee.SelectedItems[0]);
                viewModel.updateApproval(((EmployeeCheckIn)llsEmployee.SelectedItems[0]).CheckID, "Deny");

                source.Remove((EmployeeCheckIn)llsEmployee.SelectedItems[0]);
            }
            changeApproval(updateApprovalURI, employeeList, employeeList.Count(), "Deny");
        }

        private void SupervisorBackKeyPressed(CancelEventArgs obj)
        {

            if (llsEmployee.IsSelectionEnabled)
            {
                llsEmployee.IsSelectionEnabled = false;
                obj.Cancel = true;
            }
        }

        private void SupervisorItemActivated()
        {
            if (llsEmployee.IsSelectionEnabled)
            {
                llsEmployee.IsSelectionEnabled = false; // Will update the application bar too
            }
            else
            {
                SetupSupervisorApplicationBar();
            }
        }


        private void SetupSupervisorApplicationBar()
        {
            ClearApplicationBar();

            if (llsEmployee.IsSelectionEnabled)
            {
                ApplicationBar.Buttons.Add(accept);
                ApplicationBar.Buttons.Add(deny);
                UpdateSupervisorApplicationBar();
            }
            else
            {
                ApplicationBar.Buttons.Add(select);
            }
            ApplicationBar.IsVisible = true;
        }


        private void UpdateSupervisorApplicationBar()
        {
            if (llsEmployee.IsSelectionEnabled)
            {
                bool hasSelection = ((llsEmployee.SelectedItems != null) && (llsEmployee.SelectedItems.Count > 0));
                accept.IsEnabled = hasSelection;
                deny.IsEnabled = hasSelection;
            }
        }



        void ClearApplicationBar()
        {
            while (ApplicationBar.Buttons.Count > 0)
            {
                ApplicationBar.Buttons.RemoveAt(0);
            }

            while (ApplicationBar.MenuItems.Count > 0)
            {
                ApplicationBar.MenuItems.RemoveAt(0);
            }
        }


        private async void changeApproval(string address, List<EmployeeCheckIn> _empList, int _listLength, string approval)
        {
            response = new HttpResponseMessage();
            

            Uri resourceUri;
            if (!Uri.TryCreate(address.Trim(), UriKind.Absolute, out resourceUri))
            {
                return; //"Invalid URI, please re-enter a valid URI";

            }
            if (resourceUri.Scheme != "http" && resourceUri.Scheme != "https")
            {
                return; //"Only 'http' and 'https' schemes supported. Please re-enter URI";
            }
            // ---------- end of test---------------------------------------------------------------------

            string responseText;

            try
            {
                int x = 0;
                MultipartFormDataContent content = new MultipartFormDataContent();
                foreach (EmployeeCheckIn emp in _empList)
                {
                    content.Add((new StringContent(emp.CheckID.ToString(), System.Text.Encoding.UTF8, "text/plain")), "CheckID"+x);
             
                    x++;
                }
                content.Add((new StringContent(approval, System.Text.Encoding.UTF8, "text/plain")), "Approval");
                content.Add((new StringContent(_listLength.ToString(), System.Text.Encoding.UTF8, "text/plain")), "Limit");


                response = await httpClient.PostAsync(resourceUri, content);
                response.EnsureSuccessStatusCode();
                responseText = await response.Content.ReadAsStringAsync();

                MessageBox.Show(responseText.ToString());
                return; //responseText;

            }
            catch (Exception)
            {
                // Need to convert int HResult to hex string
                responseText = "";

                return; //responseText;
            }

        }

    }
}