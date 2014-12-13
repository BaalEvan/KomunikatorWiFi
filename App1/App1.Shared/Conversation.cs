using System;
using System.Collections.Generic;
using System.Text;

namespace App1
{
    class ConversationMessage
    {
        public bool Incoming;
        public string Message;
        public DateTime Time;

        public ConversationMessage( bool incoming, string message )
        {
            Incoming = incoming;
            Message = message;
            Time = DateTime.Now;
        }
    }

    class Conversation
    {
        public User userInfo;

        public List<ConversationMessage> messageList;

        public void AddMessage( bool In, string Mes )
        {
            messageList.Add(new ConversationMessage(In, Mes));
        }
    }
}
