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
        public CoreDispatcher dispatcher;
        public void Init(TextBlock ContentTB){
            dispatcher = Windows.UI.Core.CoreWindow.GetForCurrentThread().Dispatcher;

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
                        tb.Text += message + "\n";
                    });
               // }
            }
            catch(Exception e){}
        }
    }
}
