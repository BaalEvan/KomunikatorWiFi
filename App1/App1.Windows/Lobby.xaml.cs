using App1.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234237

namespace App1
{
    /// <summary>
    /// A basic page that provides characteristics common to most applications.
    /// </summary>
    public sealed partial class Lobby : Page
    {

        private NavigationHelper navigationHelper;
        private ObservableDictionary defaultViewModel = new ObservableDictionary();

        /// <summary>
        /// This can be changed to a strongly typed view model.
        /// </summary>
        public ObservableDictionary DefaultViewModel
        {
            get { return this.defaultViewModel; }
        }

        /// <summary>
        /// NavigationHelper is used on each page to aid in navigation and 
        /// process lifetime management
        /// </summary>
        public NavigationHelper NavigationHelper
        {
            get { return this.navigationHelper; }
        }
        Socket_Client client;
        public static BackLobby backLobby;
        Log log;
        User userInfo;
        public Lobby()
        {
            this.InitializeComponent();
            this.navigationHelper = new NavigationHelper(this);
            this.navigationHelper.LoadState += navigationHelper_LoadState;
            this.navigationHelper.SaveState += navigationHelper_SaveState;


            

            Windows.Storage.ApplicationDataContainer roamingSettings = Windows.Storage.ApplicationData.Current.RoamingSettings;
            Windows.Storage.StorageFolder roamingFolder = Windows.Storage.ApplicationData.Current.RoamingFolder;
            if (roamingSettings.Values["userName"] == null) roamingSettings.Values["userName"] = "";
            if (roamingSettings.Values["yearOfBirth"] == null) roamingSettings.Values["yearOfBirth"] = DateTime.Now.Year;
            if (roamingSettings.Values["description"] == null) roamingSettings.Values["description"] = "";
            if (roamingSettings.Values["sex"] == null) roamingSettings.Values["sex"] = 1;
            TextBlock ContentTbl = new TextBlock();
            log = new Log();
            log.Init(ContentTbl,UserListView);
            Socket_Client.log = log;
            ContentTbl.Text = "";

            client = new Socket_Client();
            client.Initialize(ContentTbl);

            backLobby = new BackLobby();





        }

        /// <summary>
        /// Populates the page with content passed during navigation. Any saved state is also
        /// provided when recreating a page from a prior session.
        /// </summary>
        /// <param name="sender">
        /// The source of the event; typically <see cref="NavigationHelper"/>
        /// </param>
        /// <param name="e">Event data that provides both the navigation parameter passed to
        /// <see cref="Frame.Navigate(Type, Object)"/> when this page was initially requested and
        /// a dictionary of state preserved by this page during an earlier
        /// session. The state will be null the first time a page is visited.</param>
        private void navigationHelper_LoadState(object sender, LoadStateEventArgs e)
        {
        }

        /// <summary>
        /// Preserves state associated with this page in case the application is suspended or the
        /// page is discarded from the navigation cache.  Values must conform to the serialization
        /// requirements of <see cref="SuspensionManager.SessionState"/>.
        /// </summary>
        /// <param name="sender">The source of the event; typically <see cref="NavigationHelper"/></param>
        /// <param name="e">Event data that provides an empty dictionary to be populated with
        /// serializable state.</param>
        private void navigationHelper_SaveState(object sender, SaveStateEventArgs e)
        {
        }

        #region NavigationHelper registration

        /// The methods provided in this section are simply used to allow
        /// NavigationHelper to respond to the page's navigation methods.
        /// 
        /// Page specific logic should be placed in event handlers for the  
        /// <see cref="GridCS.Common.NavigationHelper.LoadState"/>
        /// and <see cref="GridCS.Common.NavigationHelper.SaveState"/>.
        /// The navigation parameter is available in the LoadState method 
        /// in addition to page state preserved during an earlier session.

        TextBox MyName = new TextBox();
        TextBox MyYear = new TextBox();
        TextBox MyDesc = new TextBox();

        Button She = new Button();
        Button He = new Button();

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            navigationHelper.OnNavigatedTo(e);
           // HardwareButtons.BackPressed += HardwareButtons_BackPressed;

            //  Windows.Storage.ApplicationDataContainer roamingSettings = Windows.Storage.ApplicationData.Current.RoamingSettings;
            //   Windows.Storage.StorageFolder roamingFolder = Windows.Storage.ApplicationData.Current.RoamingFolder;
            UserListView.ItemsSource = BackLobby.userList;
            MyName.Text = roamingSettings.Values["userName"].ToString();
            MyYear.Text = roamingSettings.Values["yearOfBirth"].ToString();
            MyDesc.Text = roamingSettings.Values["description"].ToString();
            isMale = Convert.ToBoolean(roamingSettings.Values["sex"]);
            if (isMale == true)
            {
                She.Opacity = 0.25f;
                He.Opacity = 0.74f;
            }
            else
            {
                She.Opacity = 0.74f;
                He.Opacity = 0.25f;
            }
        }

        Windows.Storage.ApplicationDataContainer roamingSettings = Windows.Storage.ApplicationData.Current.RoamingSettings;
        Windows.Storage.StorageFolder roamingFolder = Windows.Storage.ApplicationData.Current.RoamingFolder;
        public bool isMale;

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            navigationHelper.OnNavigatedFrom(e);
         //   HardwareButtons.BackPressed -= HardwareButtons_BackPressed;
        }

        #endregion

        private void Sex_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            if (isMale == false)
            {
                isMale = true;
                She.Opacity = 0.25f;
                He.Opacity = 0.74f;
            }
            else
            {
                isMale = false;

                She.Opacity = 0.74f;
                He.Opacity = 0.25f;
            }// TODO: Add event handler implementation here.
        }

        private void Save_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            roamingSettings.Values["userName"] = Socket_Client.userInfo.Username = MyName.Text;
            roamingSettings.Values["yearOfBirth"] = MyYear.Text;
            Socket_Client.userInfo.Year = int.Parse(MyYear.Text.ToString());
            roamingSettings.Values["description"] = Socket_Client.userInfo.Description = MyDesc.Text;
            roamingSettings.Values["sex"] = Socket_Client.userInfo.Sex = isMale ? 1 : 0;

            Socket_Client.SendMessage(new Message(4, ""));
        }

        private void Grid_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
        }

        private void UserListView_ItemClick(object sender, Windows.UI.Xaml.Controls.ItemClickEventArgs e)
        {

            Frame.Navigate(typeof(Talk), e.ClickedItem as User);

        }

    }
}
