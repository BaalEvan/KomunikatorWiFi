using System;
using System.Collections.Generic;
using System.Text;
using Windows.UI.Core;
using Windows.UI.Xaml.Controls;

namespace App1
{
    public class Log
    {
        public TextBlock tb;
        public ListView lb;

        public CoreDispatcher dispatcher;
        public void Init(TextBlock ContentTB,ListView Lobby){
            dispatcher = Windows.UI.Core.CoreWindow.GetForCurrentThread().Dispatcher;
            lb = Lobby;
            tb = ContentTB;
        }

        public async void ShowDebug(string message){
            System.Diagnostics.Debug.WriteLine(message);
            try
            {
               // if (tb != null)
                //{
                    await dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                    {
                        lb.ItemsSource = new List<string>();
                        lb.ItemsSource = BackLobby.userList;
                        tb.Text += message + "\n";
                    });
               // }
            }
            catch(Exception e){}
        }

        public async void Reload()
        {
            try
            {
                // if (tb != null)
                //{
                await dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                {
                    lb.ItemsSource = new List<string>();
                    lb.ItemsSource = BackLobby.userList;
                });
                // }
            }
            catch (Exception e) { }
        }
    }
}
