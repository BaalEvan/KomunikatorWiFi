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

using Newtonsoft.Json;

namespace App1
{

    class Socket_Client
    {
        private DataWriter dataWriter;
        private DataReader dataReader;

        //  private TextBlock textBlock;

        User userInfo;
        DatagramSocket udpSocket;

        public void Initialize(TextBlock tb)
        {
            if (udpSocket == null)
            {
                udpSocket = new DatagramSocket();
                udpSocket.MessageReceived += SocketOnMessageReceived;
            }

            //     textBlock = tb;
            //  tb.Text += "Initialization succeeded\n\r";
            System.Diagnostics.Debug.WriteLine("Initialization succeeded");

            userInfo = new User();
            userInfo.Username = "Imie";
            userInfo.Address = FindIPAddress();
            Message hello = new Message(1, JsonConvert.SerializeObject(userInfo));

            SendMessage(hello, 1990);
            System.Diagnostics.Debug.WriteLine("Sent hello message");
            StartListening(1990);
        }

        private async void StartListening(int port)
        {
            await udpSocket.BindServiceNameAsync(port.ToString());
            System.Diagnostics.Debug.WriteLine("Started listening on port " + port.ToString());
        }

        public async void SendMessage(Message message, int port = 1990, string address = "255.255.255.255")
        {
            var socket = new DatagramSocket();

            message.UserInfo = userInfo;
            string outmessage = JsonConvert.SerializeObject(message);


            //socket.MessageReceived += SocketOnMessageReceived;

            using (var stream = await socket.GetOutputStreamAsync(new HostName(address), port.ToString()))
            {
                using (var writer = new DataWriter(stream))
                {
                    var data = Encoding.UTF8.GetBytes(outmessage);

                    writer.WriteBytes(data);
                    await writer.StoreAsync();
                    //     textBlock.Text += "Sent hello message";
                }
            }
        }

        private async void SocketOnMessageReceived(DatagramSocket sender, DatagramSocketMessageReceivedEventArgs args)
        {
            //    textBlock.Text += "Received message";
            System.Diagnostics.Debug.WriteLine("Received message");

            var result = args.GetDataStream();
            var resultStream = result.AsStreamForRead(1024);

            using (var reader = new StreamReader(resultStream))
            {
                var text = await reader.ReadToEndAsync();

                Message received = JsonConvert.DeserializeObject<Message>(text);

                switch (received.MessageID)
                {
                    case 1: // Hello
                        User newUser = JsonConvert.DeserializeObject<User>(received.Content);
                        System.Diagnostics.Debug.WriteLine("Received hello message from " + newUser.Username);
                        break;

                    case 2: // Hello answer

                        break;

                    case 3: // Message

                        System.Diagnostics.Debug.WriteLine(received.UserInfo.Username + ": "+ received.Content);
                        break;

                }
            }
        }

        public static string FindIPAddress()
        {
            List<string> ipAddresses = new List<string>();
            var hostnames = Windows.Networking.Connectivity.NetworkInformation.GetHostNames();
            foreach (var hn in hostnames)
            {
                //IanaInterfaceType == 71 => Wifi
                //IanaInterfaceType == 6 => Ethernet (Emulator)
                if (hn.IPInformation != null &&
                    (hn.IPInformation.NetworkAdapter.IanaInterfaceType == 71
                    || hn.IPInformation.NetworkAdapter.IanaInterfaceType == 6))
                {
                    string ipAddress = hn.DisplayName;
                    ipAddresses.Add(ipAddress);
                }
            }

            if (ipAddresses.Count < 1)
            {
                return null;
            }
            else if (ipAddresses.Count == 1)
            {
                return ipAddresses[0];
            }
            else
            {
                //if multiple suitable address were found use the last one
                //(regularly the external interface of an emulated device)
                return ipAddresses[ipAddresses.Count - 1];
            }
        }
    }
}
