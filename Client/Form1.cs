using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Net; 
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
    public partial class Form1 : Form
    {
        Socket clientSocket;
        IPEndPoint serverAddress;
        public Form1()
        {
            InitializeComponent();
            clientSocket = new Socket(AddressFamily.InterNetwork,SocketType.Dgram,ProtocolType.Udp);
            serverAddress = new IPEndPoint(IPAddress.Parse("127.0.0.1"),6767);         
        
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Trim() == null)
            {
                MessageBox.Show("Empty");
                button1.Focus();
                return;
                    
            }
            byte[] buffer = Encoding.ASCII.GetBytes(textBox1.Text.Trim());
            clientSocket.SendTo(buffer, serverAddress);
            textBox1.Text = "";
        }
    }
}
