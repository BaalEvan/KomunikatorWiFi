using System;
using System.Collections.Generic;
using System.Text;

namespace App1
{
    public class Message
    {
        public int MessageID;
        public User UserInfo;
        public string Content;

        public Message( int ID, string Content )
        {
            MessageID = ID;
            this.Content = Content;
        }
    }
}
