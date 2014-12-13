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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace App1
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.

    public class asd {
        public string message { get; set; }
        public string pos { get; set; }
    }
    
    /// </summary>
    public sealed partial class BlankPage1 : Page
    {
        public BlankPage1()
        {
            this.InitializeComponent();
            Lista.ItemsSource = new List<asd>() { new asd { message = "sadsd", pos = "Left" }, new asd { message = "sadsd", pos = "Left" }, new asd { message = "sadsd", pos = "Right" }, new asd { message = "sadsd", pos = "Left" }, new asd { message = "sadsd" }, new asd { message = "sadsd" }, new asd { message = "sadsd" }, new asd { message = "sadsd" }, new asd { message = "sadsd" }, new asd { message = "sadsd" } };
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }
    }
}
