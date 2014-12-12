using System;
using System.Collections.Generic;
using System.Text;

namespace App1
{
    class Message
    {
        public int MessageID;
        public string Content;

        public Message( int ID, string Content )
        {
            MessageID = ID;
            this.Content = Content;
        }
    }
}
