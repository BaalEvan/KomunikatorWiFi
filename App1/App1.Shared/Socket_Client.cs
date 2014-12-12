using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Net;
using System.Windows;
using System.IO;

using Windows.ApplicationModel.Core;
using Windows.Networking;
using Windows.Networking.Sockets;
using Windows.Storage.Streams;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Core;

using Newtonsoft.Json;

namespace App1
{

    class Socket_Client
    {
        private DataWriter dataWriter;
        private DataReader dataReader;

        private TextBlock textBlock;

        User userInfo;
        DatagramSocket udpSocket;

        public void Initialize( TextBlock tb )
        {
            if (udpSocket == null)
            {
                udpSocket = new DatagramSocket();
                udpSocket.MessageReceived += SocketOnMessageReceived;
            }

            textBlock = tb;
            tb.Text += "Initialization succeeded\n\r";

            userInfo = new User();
            userInfo.Username = "Imie";
            Message hello = new Message(1, JsonConvert.SerializeObject(userInfo));

            SendMessage(JsonConvert.SerializeObject(hello), 1990);
            StartListening(1990);
        }

        private async void StartListening( int port )
        {
            try
            {
                await udpSocket.BindServiceNameAsync(port.ToString());
            }
            catch
            {

            }
            finally
            {
                textBlock.Text += "Started listening on port " + port.ToString() + "\n\r";
            }
            
        }

        private async void SendMessage(string message, int port, string address = "255.255.255.255")
        {
            var socket = new DatagramSocket();

            //socket.MessageReceived += SocketOnMessageReceived;

            using (var stream = await socket.GetOutputStreamAsync(new HostName(address), port.ToString()))
            {
                using (var writer = new DataWriter(stream))
                {
                    var data = Encoding.UTF8.GetBytes(message);

                    writer.WriteBytes(data);
                    await writer.StoreAsync();
                    textBlock.Text += "Sent hello message";
                }
            }
        }

        private async void SocketOnMessageReceived(DatagramSocket sender, DatagramSocketMessageReceivedEventArgs args)
        {
            textBlock.Text += "Received message";
            /*try
            {
                var result = args.GetDataStream();
                var resultStream = result.AsStreamForRead(1024);

                using (var reader = new StreamReader(resultStream))
                {
                    var text = await reader.ReadToEndAsync();
                    textBlock.Text += text;

                    Message received = JsonConvert.DeserializeObject<Message>(text);

                    textBlock.Text += received.Content + "\n\r";

                    switch (received.MessageID)
                    {
                        case 1: // Hello
                            User newUser = JsonConvert.DeserializeObject<User>(received.Content);
                            textBlock.Text += "Received hello message from " + newUser.Username + "\n\r";
                            break;

                        case 2: // Hello answer

                            break;
                    }
                }
            }
            catch
            {
                textBlock.Text += "Error";
            }*/
        }
    }
}
