using App1.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics.Display;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace App1
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Lobby : Page
    {
        private NavigationHelper navigationHelper;
        private ObservableDictionary defaultViewModel = new ObservableDictionary();

        public Lobby()
        {
            this.InitializeComponent();

            this.navigationHelper = new NavigationHelper(this);
            this.navigationHelper.LoadState += this.NavigationHelper_LoadState;
            this.navigationHelper.SaveState += this.NavigationHelper_SaveState;
        }

        /// <summary>
        /// Gets the <see cref="NavigationHelper"/> associated with this <see cref="Page"/>.
        /// </summary>
        public NavigationHelper NavigationHelper
        {
            get { return this.navigationHelper; }
        }

        /// <summary>
        /// Gets the view model for this <see cref="Page"/>.
        /// This can be changed to a strongly typed view model.
        /// </summary>
        public ObservableDictionary DefaultViewModel
        {
            get { return this.defaultViewModel; }
        }

        /// <summary>
        /// Populates the page with content passed during navigation.  Any saved state is also
        /// provided when recreating a page from a prior session.
        /// </summary>
        /// <param name="sender">
        /// The source of the event; typically <see cref="NavigationHelper"/>
        /// </param>
        /// <param name="e">Event data that provides both the navigation parameter passed to
        /// <see cref="Frame.Navigate(Type, Object)"/> when this page was initially requested and
        /// a dictionary of state preserved by this page during an earlier
        /// session.  The state will be null the first time a page is visited.</param>
        private void NavigationHelper_LoadState(object sender, LoadStateEventArgs e)
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
        private void NavigationHelper_SaveState(object sender, SaveStateEventArgs e)
        {
        }

        #region NavigationHelper registration

        /// <summary>
        /// The methods provided in this section are simply used to allow
        /// NavigationHelper to respond to the page's navigation methods.
        /// <para>
        /// Page specific logic should be placed in event handlers for the  
        /// <see cref="NavigationHelper.LoadState"/>
        /// and <see cref="NavigationHelper.SaveState"/>.
        /// The navigation parameter is available in the LoadState method 
        /// in addition to page state preserved during an earlier session.
        /// </para>
        /// </summary>
        /// <param name="e">Provides data for navigation methods and event
        /// handlers that cannot cancel the navigation request.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            this.navigationHelper.OnNavigatedTo(e);
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
            this.navigationHelper.OnNavigatedFrom(e);
        }


        #endregion
		
		 private void Sex_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            if (isMale==false)
            {
                isMale = true   ;
                She.Opacity = 0.25f;
                He.Opacity = 0.74f;
            }
            else {
                isMale = false;

                She.Opacity = 0.74f;
                He.Opacity = 0.25f;
            }// TODO: Add event handler implementation here.
        }

		 private void Save_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
		 {
		    roamingSettings.Values["userName"] = MyName.Text;
            roamingSettings.Values["yearOfBirth"] = MyYear.Text  ;
            roamingSettings.Values["description"] = MyDesc.Text ;
            roamingSettings.Values["sex"] = isMale ? 1 : 0 ;
		 }

		 private void Grid_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
		 {
         }

		 private void UserListView_ItemClick(object sender, Windows.UI.Xaml.Controls.ItemClickEventArgs e)
		 {
             
             Frame.Navigate(typeof(Talk),e.ClickedItem as User);

		 }


    }
}
