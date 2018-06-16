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

        public Client(string ipAddress)
        {
            Thread thread = new Thread(() => StartChatWindow());
            thread.Start();

            Thread.Sleep(1500);

            portNumber = 1500;
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            while (! socket.Connected)
            {
                socket.Connect(IPAddress.Parse(ipAddress), portNumber);
            }

            chatWindow.AddMessage("Connected to server.");
        }

        private void StartChatWindow()
        {
            chatWindow = new ChatWindow();

            Application.EnableVisualStyles();
            Application.Run(chatWindow);
        }
    }
}
