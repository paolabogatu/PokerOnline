using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PokerOnline
{
    class Client
    {
        private int portNumber;
        private Socket socket;
        private ChatWindow chatWindow;
        private byte[] dataBuffer;
        private string playerName;

        public Client(string ipAddress, string _playerName)
        {
            playerName = _playerName;
            portNumber = 1500;
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            dataBuffer = new byte[256];

            Thread thread = new Thread(() => StartChatWindow());
            thread.Start();

            Thread.Sleep(1500);

            while (! socket.Connected)
            {
                socket.Connect(IPAddress.Parse(ipAddress), portNumber);
            }

            chatWindow.SetSocket(socket);
            chatWindow.AddMessage("Connected to server.");

            socket.BeginReceive(dataBuffer, 0, dataBuffer.Length, SocketFlags.None, new AsyncCallback(ReceivedNewDataPacket), null);
        }

        private void ReceivedNewDataPacket(IAsyncResult AR)
        {
            try
            {
                int receivedDataSize = socket.EndReceive(AR);
                byte[] realDataBuffer = new byte[receivedDataSize];

                Array.Copy(dataBuffer, realDataBuffer, receivedDataSize);

                socket.BeginReceive(dataBuffer, 0, dataBuffer.Length, SocketFlags.None, new AsyncCallback(ReceivedNewDataPacket), null);

                HandleReceivedData(realDataBuffer);
            }
            catch (SocketException socketEx)
            {
                //
            }
        }

        private void HandleReceivedData(byte[] data)
        {
            string dataMessage = Encoding.ASCII.GetString(data);
            string[] dataComponents = dataMessage.Split('|');

            switch (dataComponents[0])
            {
                case "CHAT":
                    {
                        chatWindow.AddMessage(dataComponents[1]);

                        break;
                    }
            }
        }

        private void StartChatWindow()
        {
            chatWindow = new ChatWindow(playerName);

            Application.EnableVisualStyles();
            Application.Run(chatWindow);
        }
    }
}
