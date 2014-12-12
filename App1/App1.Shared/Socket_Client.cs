using System;
using System.Collections.Generic;
using System.Text;

using Windows.ApplicationModel.Core;
using Windows.Networking;
using Windows.Networking.Sockets;
using Windows.Storage.Streams;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace App1
{

    class Socket_Client
    {
        private DataWriter dataWriter;
        private DataReader dataReader;

        private TextBlock textBlock;

        string Username;
        DatagramSocket udpSocket;


        public void Initialize( TextBlock tb, string username )
        {
            if (udpSocket == null)
            {
                udpSocket = new DatagramSocket();
                udpSocket.MessageReceived += SocketOnMessageReceived;
            }

            textBlock = tb;
            tb.Text += "Initialization succeeded\n\r";

            Username = username;
            SendMessage("Hello", 1025);
        }

        private async void SendMessage(string message, int port)
        {
            var socket = new DatagramSocket();

            socket.MessageReceived += SocketOnMessageReceived;

            using (var stream = await socket.GetOutputStreamAsync(new HostName("255.255.255.255"), port.ToString()))
            {
                using (var writer = new DataWriter(stream))
                {
                    var data = Encoding.UTF8.GetBytes(message);

                    writer.WriteBytes(data);
                    await writer.StoreAsync();
                }
            }
        }

        private async void SocketOnMessageReceived(DatagramSocket sender, DatagramSocketMessageReceivedEventArgs args)
        {
            var result = args.GetDataStream();
            var resultStream = result.AsStreamForRead(1024);

            using (var reader = new StreamReader(resultStream))
            {
                var text = await reader.ReadToEndAsync();
                Deployment.Current.Dispatcher.BeginInvoke(() =>
                {
                    // Do what you need to with the resulting text
                    // Doesn't have to be a messagebox
                    MessageBox.Show(text);
                });
            }
        }
    }
}
