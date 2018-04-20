using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Threading;
using System.Windows.Forms;


namespace CS408_PROJSTEP1_CLIENTSIDE2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        static bool terminating = false;
        static Socket client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        private void connectButton_Click(object sender, EventArgs e)
        {
            Thread thrReceive; //this thread will allow us to receive any time the server broadcasts.

            string serverIP;
            string username;
            int serverPort;

            try
            {
                //prompt for IP and port (of server)                
                serverIP = ipaddressText.Text;
                //note: if you are testing on the same PC, you may try 127.0.0.1 as server's IP. It should work.                               
                serverPort = Convert.ToInt32(portnumberText.Text);
                username = usernameText.Text;
                client.Connect(serverIP, serverPort);
                SendMessage(username); //sends the username
                thrReceive = new Thread(new ThreadStart(Receive));
                thrReceive.IsBackground = true;
                thrReceive.Start();

            }
            catch
            {
                richTextBox.AppendText("Cannot connected to the specified server\n");
                richTextBox.AppendText("terminating...");
            }
        }

        //this function sends the given message via the socket
        public void SendMessage(string message)
        {
            try
            {
                byte[] buffer = Encoding.Default.GetBytes(message);

                //we can send a byte[] 
                client.Send(buffer);
            }
            catch
            {
                richTextBox.AppendText("Your message could not be sent.\n");
                richTextBox.AppendText("connection lost...");
                terminating = true;
                client.Close();
            }
        }

        //this function will be used in the thread
        public void Receive()
        {
            bool connected = true;
            richTextBox.AppendText("Connected to the server.\n");

            while (connected)
            {
                try
                {
                    byte[] buffer = new byte[1024];

                   int rec = client.Receive(buffer);

                    if (rec <= 0)
                    {
                        throw new SocketException();
                    }

                    string newmessage = Encoding.Default.GetString(buffer);
                    //richTextBox.AppendText(newmessage);
                    newmessage = newmessage.Substring(0, newmessage.IndexOf("\0"));
                    richTextBox.AppendText(newmessage);
                }
                catch (Exception e)
                {
                    richTextBox.AppendText("{0} Exception caught." + e);
                    //SendMessage("TerminateMe");
                    if (!terminating)
                        richTextBox.AppendText("Connection has been terminated...\n");
                    connected = false;
                }
            }
        }

        private void disconnectButton_Click(object sender, EventArgs e)
        {
            terminating = true;
            richTextBox.AppendText("Goodbye\n");
            client.Close();
        }

        private void listplayersButton_Click(object sender, EventArgs e)
        {
            SendMessage("Give");
        }

        private void InviteButton_Click(object sender, EventArgs e)
        {
            string PersonToInvite = inviteBox.Text;
            string pass = "Invite" + PersonToInvite;
            SendMessage(pass);
        }

        private void AcceptButton_Click(object sender, EventArgs e)
        {
            SendMessage("Accept");
        }

        private void RejectButton_Click(object sender, EventArgs e)
        {
            SendMessage("Reject");
        }

        private void SurrenderButton_Click(object sender, EventArgs e)
        {
            SendMessage("Surrender");
        }

        private void guessButton_Click(object sender, EventArgs e)
        {
            string tempInteger = guessTextBox.Text;
            richTextBox.AppendText(tempInteger + "the text box\n");
            int myInt;
            bool isValid = int.TryParse(tempInteger, out myInt);
            if(isValid)
            {
                if(myInt < 101 && myInt > 0)
                {
                    richTextBox.AppendText(myInt + "the integer\n");
                    string pass = "Guess" + tempInteger;
                    richTextBox.AppendText(pass + "the pass\n");
                    SendMessage(pass);
                }
                else
                {
                    richTextBox.AppendText("It has to be between 0 and 100.\n");
                }
            }
            else
            {
                richTextBox.AppendText("It is not an integer.\n");
            }
        }
    }
}
