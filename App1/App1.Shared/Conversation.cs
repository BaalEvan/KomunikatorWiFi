using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace App1
{
    class ConversationMessage
    {
        public bool Incoming { get; set; }
        public string Message { get; set; }
        public DateTime Time { get; set; }

        public float ScaleX
        {
            get { return Incoming ? -1 : 1; }
        }

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

        public int Rotation
        {
            get
            {
                if (Incoming)
                {
                    return 180;
                }
                else
                {
                    return 0;
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

        public ObservableCollection<ConversationMessage> messageList;

        public void AddMessage( bool In, string Mes )
        {
            messageList.Add(new ConversationMessage(In, Mes));
        }
    }
    static class ConversationList
    {
        public static ObservableCollection<Conversation> ListOfConversation = new ObservableCollection<Conversation>();

        public static Conversation GetIfExist(User Info)
        {
            Conversation r = null;
            foreach (var element in ListOfConversation) 
            { 
            if (element.userInfo.Address == Info.Address) r = element;
            }
            //var r = ListOfConversation.Find(ele => ele.userInfo.Address == Info.Address);
            if (r == null)
            {
                r = new Conversation { userInfo = Info, messageList = new ObservableCollection<ConversationMessage>() }; 
            ListOfConversation.Add(r);}
            return r;
        }
    }
}
