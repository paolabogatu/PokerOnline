using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PokerOnline
{
    public partial class ChatWindow : Form
    {
        private Socket socket;
        private string playerName;

        public ChatWindow(string playerName)
        {
            InitializeComponent();

            this.playerName = playerName;
        }

        public void SetSocket(Socket socket)
        {
            this.socket = socket;
        }

        public void AddMessage(string message)
        {
            chatTextBox.AppendText(message + "\n");
        }

        private void sendButton_Click(object sender, EventArgs e)
        {
            SendMessage();
        }

        private void sendTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                SendMessage();
            }
        }

        private void SendMessage()
        {
            if (!String.IsNullOrEmpty(sendTextBox.Text))
            {
                string message = String.Format("{0}: {1}", playerName, sendTextBox.Text);

                socket.Send(Encoding.ASCII.GetBytes("CHAT|" + message));

                AddMessage(message);

                sendTextBox.Text = "";
            }
        }
    }
}
