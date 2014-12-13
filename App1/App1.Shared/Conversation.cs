using System;
using System.Collections.Generic;
using System.Text;

namespace App1
{
    class ConversationMessage
    {
        public bool Incoming { get; set; }
        public string Message { get; set; }
        public DateTime Time { get; set; }


        public string Align
        {
            get
            {
                if (Incoming)
                {
                    return "Left";
                }
                else
                {
                    return "Right";
                }
            }
        }



        public ConversationMessage(bool incoming, string message)
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
    static class ConversationList
    {
        public static List<Conversation> ListOfConversation = new List<Conversation>();

        public static Conversation GetIfExist(User Info)
        {
            var r = ListOfConversation.Find(ele => ele.userInfo.Address == Info.Address);
            if (r == null) r = new Conversation { userInfo = Info, messageList = new List<ConversationMessage>() };
            return r;
        }
    }
}
