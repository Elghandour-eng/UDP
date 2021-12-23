using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace Server
{
    public partial class Form1 : Form
    {
        Socket serverSocket;
        IPEndPoint serverAdress;
        Thread thread;
        void startServer() 
        {
            serverSocket = new Socket(AddressFamily.InterNetwork,SocketType.Dgram,ProtocolType.Udp);
            serverAdress = new IPEndPoint(IPAddress.Parse("127.0.0.1"),6767);
            serverSocket.Bind(serverAdress);
            thread = new Thread(new ThreadStart(Listening));
            thread.Start();
        }
        bool isRunning = false;
        void Listening() 
        {
            while (isRunning)
            {
                byte[] buffer = new byte[4096];
                EndPoint clientAddress = new IPEndPoint(IPAddress.Any, 0);
                int bytesCount = serverSocket.ReceiveFrom(buffer, ref clientAddress);
                string msg = Encoding.ASCII.GetString(buffer, 0, bytesCount);
                label2.Invoke(new Action(delegate 
                {
                    label2.Text=clientAddress.ToString()+":"+msg;
                }));
                
            }
       
        }
        void stopServer()
        {
            isRunning = false;
            thread.Abort();
            serverSocket.Close();
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;
            button2.Enabled = true;
            label1.Text = "Connected";
            label1.ForeColor = Color.Green;
            isRunning = true;
            startServer();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            button1.Enabled = true;
            button2.Enabled = false;
            label1.Text = "Dis Connected";
            label1.ForeColor = Color.Red;
            isRunning =false;
            stopServer();
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
