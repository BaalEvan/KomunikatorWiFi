using System;
using System.Collections.Generic;
using System.Text;
using Windows.UI.Xaml.Controls;

namespace App1
{
    public class Log
    {
        public TextBlock tb;

        public void Init(TextBlock ContentTB){
            tb = ContentTB;
        }

        public void ShowDebug(string message){
            System.Diagnostics.Debug.WriteLine(message);
            try
            {
                if (tb != null)
                {
                    tb.Text += message+"\n";
                }
            }
            catch(Exception e){}
        }
    }
}
