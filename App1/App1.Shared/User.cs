using System;
using System.Collections.Generic;
using System.Text;
using Windows.UI;

namespace App1
{
    public class User
    {
        public string Username{get;set;}
        public int Sex{get;set;}
        public int Year{get;set;}
        public string Description{get;set;}

        public string Address{get;set;}
        public string GetSexChar { get { if (Sex == 0) { return "♀"; } else { return "♂"; } } }
        public Color GetSexColor { get { if (Sex == 1) { return Color.FromArgb(74,0,116,255); } else { return Color.FromArgb(74,209,0,255); } } }
    }
}
