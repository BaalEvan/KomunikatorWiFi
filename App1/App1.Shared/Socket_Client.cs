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

using Windows.UI.Notifications;

using Newtonsoft.Json;

namespace App1
{

    public class Socket_Client
    {
        private DataWriter dataWriter;
        private DataReader dataReader;

        private CoreDispatcher dispatcher;

      //  private TextBlock textBlock;
        public static Log log;
        User userInfo;
        DatagramSocket udpSocket;
        Windows.Storage.ApplicationDataContainer roamingSettings = Windows.Storage.ApplicationData.Current.RoamingSettings;
        Windows.Storage.StorageFolder roamingFolder = Windows.Storage.ApplicationData.Current.RoamingFolder;
        public void Initialize(TextBlock tb)
        {
            dispatcher = Windows.UI.Core.CoreWindow.GetForCurrentThread().Dispatcher;

            if (udpSocket == null)
            {
                udpSocket = new DatagramSocket();
                udpSocket.MessageReceived += SocketOnMessageReceived;
            }
            log.ShowDebug("Initialization succeeded");
//            MyYear.Text = roamingSettings.Values["yearOfBirth"].ToString();
  //          MyDesc.Text = roamingSettings.Values["description"].ToString();
    //        isMale = Convert.ToBoolean(roamingSettings.Values["sex"]);
            userInfo = new User();
            userInfo.Username = roamingSettings.Values["userName"].ToString();
            userInfo.Address = FindIPAddress();
            Message hello = new Message(1, "");

            StartListening(1990);
            SendMessage(hello, 1990);
            log.ShowDebug("Sent hello message");
        }

        private async void StartListening(int port)
        {
            await udpSocket.BindServiceNameAsync(port.ToString());
            log.ShowDebug("Started listening on port " + port.ToString());
        }

        public async void SendMessage(Message message, int port = 1990, string address = "255.255.255.255")
        {
            var socket = new DatagramSocket();

            message.UserInfo = userInfo;
            string outmessage = JsonConvert.SerializeObject(message);

            using (var stream = await socket.GetOutputStreamAsync(new HostName(address), port.ToString()))
            {
                using (var writer = new DataWriter(stream))
                {
                    var data = Encoding.UTF8.GetBytes(outmessage);

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
                Message received = JsonConvert.DeserializeObject<Message>(text);

                switch (received.MessageID)
                {
                    case 1: // Hello
                        {
                            User newUser = received.UserInfo;
                            if (newUser.Address != userInfo.Address)
                            {
                                log.ShowDebug("Received hello message from " + newUser.Username);

                                await dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                                {
                                    for (int i = 0; i < BackLobby.userList.Count; ++i)
                                    {
                                        if (BackLobby.userList[i].Address == newUser.Address)
                                            return;
                                    }

                                    BackLobby.userList.Add(newUser);

                                    Message response = new Message(2, "");
                                    SendMessage(response, 1990, newUser.Address);
                                });
                            }
                        }
                        break;

                    case 2: // Hello answer
                        {
                            User newUser = received.UserInfo;
                            await dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                            {
                                if (!BackLobby.userList.Contains(newUser))
                                    BackLobby.userList.Add(newUser);
                            });
                        }
                        break;

                    case 3: // Message
                        await dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                            {
                                log.ShowDebug(received.UserInfo.Username + ": " + received.Content);
                            });
                        break;

                    case 4: // Changed user info
                        {
                            User info = received.UserInfo;
                            await dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                                {
                                    for (int i = 0; i < BackLobby.userList.Count; ++i)
                                    {
                                        if (BackLobby.userList[i].Address == info.Address)
                                        {
                                            BackLobby.userList[i] = info;
                                            break;
                                        }
                                    }

                                });
                        }
                        break;

                    case 90: // Disconnect
                        {
                            User info = received.UserInfo;
                            await dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                                {
                                    for( int i = 0; i < BackLobby.userList.Count; ++i )
                                    {
                                        if (BackLobby.userList[i].Address == info.Address)
                                        {
                                            BackLobby.userList.RemoveAt(i);
                                            log.ShowDebug("User " + info.Username + " has disconnected.");
                                            break;
                                        }
                                    }

                                });
                        }
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
