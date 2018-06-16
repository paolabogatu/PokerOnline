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

        public Server()
        {
            Thread thread = new Thread(() => StartChatWindow());
            thread.Start();

            Thread.Sleep(1500);

            portNumber = 1500;
            serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            serverSocket.Bind(new IPEndPoint(IPAddress.Any, portNumber));
            serverSocket.Listen(1);
            
            chatWindow.AddMessage("Server has started. Waiting for player 2.");

            clientSocket = serverSocket.Accept();
            
            chatWindow.AddMessage("Player 2 has joined.");
        }

        private void StartChatWindow()
        {
            chatWindow = new ChatWindow();

            Application.EnableVisualStyles();
            Application.Run(chatWindow);
        }
    }
}
