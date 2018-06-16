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
    class Server
    {
        private int portNumber;
        private Socket serverSocket;
        private Socket clientSocket;
        private ChatWindow chatWindow;
        private byte[] dataBuffer;
        private string playerName;

        public Server(string _playerName)
        {
            portNumber = 1500;
            serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            dataBuffer = new byte[256];
            playerName = _playerName;

            Thread thread = new Thread(() => StartChatWindow());
            thread.Start();

            Thread.Sleep(1500);

            serverSocket.Bind(new IPEndPoint(IPAddress.Any, portNumber));
            serverSocket.Listen(1);
            
            chatWindow.AddMessage("Server has started. Waiting for player 2.");

            clientSocket = serverSocket.Accept();

            chatWindow.SetSocket(clientSocket);
            chatWindow.AddMessage("Second player has joined.");

            clientSocket.BeginReceive(dataBuffer, 0, dataBuffer.Length, SocketFlags.None, new AsyncCallback(ReceivedNewDataPacket), null);
        }

        private void ReceivedNewDataPacket(IAsyncResult AR)
        {
            try
            {
                int receivedDataSize = clientSocket.EndReceive(AR);
                byte[] realDataBuffer = new byte[receivedDataSize];

                Array.Copy(dataBuffer, realDataBuffer, receivedDataSize);

                clientSocket.BeginReceive(dataBuffer, 0, dataBuffer.Length, SocketFlags.None, new AsyncCallback(ReceivedNewDataPacket), null);

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
