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
using Newtonsoft.Json;
using Windows.Phone.UI.Input;
using Windows.UI.Popups;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace App1
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        Socket_Client client;
        public static BackLobby backLobby;
        Log log;
        User userInfo;

        private void CommandHandler1(IUICommand command)
        {
            var label = command.Label;
            switch (label)
            {
                case "Yes":
                    {
                        client.SendMessage(new Message(90,""));
                        Application.Current.Exit();
                        break;
                    }
                case "No":
                    {
                        break;
                    }

            }

        }

        private async void HardwareButtons_BackPressed(object sender, BackPressedEventArgs e)
        {
            e.Handled = true;
            MessageDialog dlg = new MessageDialog("Are you sure you want to quit?", "Warning");
            dlg.Commands.Add(new UICommand("Yes", new UICommandInvokedHandler(CommandHandler1)));
            dlg.Commands.Add(new UICommand("No", new UICommandInvokedHandler(CommandHandler1)));

            await dlg.ShowAsync();
        }

        public MainPage()
        {
            this.InitializeComponent();

            HardwareButtons.BackPressed += HardwareButtons_BackPressed;

            this.NavigationCacheMode = NavigationCacheMode.Required;


            Windows.Storage.ApplicationDataContainer roamingSettings = Windows.Storage.ApplicationData.Current.RoamingSettings;
            Windows.Storage.StorageFolder roamingFolder = Windows.Storage.ApplicationData.Current.RoamingFolder;
         //   roamingSettings.Values["userName"] = "Baal";
           // roamingSettings.Values["yearOfBirth"] = "1995";
           // roamingSettings.Values["description"] = "King of The World";
           // roamingSettings.Values["sex"] = 1;

            log = new Log();
            log.Init(ContentTbl);
            Socket_Client.log = log;
            ContentTbl.Text = "";

            client = new Socket_Client();
            client.Initialize(ContentTbl);

            backLobby = new BackLobby();
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            // TODO: Prepare page for display here.

            // TODO: If your application contains multiple pages, ensure that you are
            // handling the hardware Back button by registering for the
            // Windows.Phone.UI.Input.HardwareButtons.BackPressed event.
            // If you are using the NavigationHelper provided by some templates,
            // this event is handled for you.
        }

        private void Send_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
        	// TODO: Add event handler implementation here.
        //    client.SendMessage(InputTbx.Text);
            Message mes = new Message(3, InputTbx.Text);
            client.SendMessage(mes);
            InputTbx.Text = "";
            
        }

        private void Button_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Lobby));// TODO: Add event handler implementation here.
        }

        private void Button1_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Talk));// TODO: Add event handler implementation here.
        }
    }
}
