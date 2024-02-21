using InstructorApp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StudentApp
{
    public partial class StudentForm : Form
    {
        public bool exitStatus = false;
        public const int BYTE_SIZE = 1024;
        public const string HOST_NAME = "localhost";
        public const int PORT_NUMBER = 8888;
        // set up a client connection for TCP network service
        private TcpClient clientSocket;
        // set up data stream object
        private NetworkStream netStream;
        // set up thread to run ReceiveStream() method
        private Thread clientThread = null;
        // set up delegate 
        delegate void SetTextCallback(string text);

        private int correctAnswer;
       
        public StudentForm()
        {
            InitializeComponent();

            // clear all text boxes
            SF_Que_TextBox.Text = "";
            SF_Answer_TextBox.Text = "";
            // start client
            StartClient();
            
        }
        private void StartClient()
        {
            try
            {
                // create TCPClient object (as the socket)
                clientSocket = new TcpClient(HOST_NAME, PORT_NUMBER);
                // create stream
                netStream = clientSocket.GetStream();
                // set up thread to run ReceiveStream() method
                clientThread = new Thread(ReceiveStream);
                // start thread
                clientThread.Start();
                SF_Que_TextBox.Text += Environment.NewLine;
            }
            catch (Exception e)
            {
                // display exception message

            }
        }
        // this method runs as a thread (called by serverThread)
        public void ReceiveStream()
        {
            byte[] bytesReceived = new byte[BYTE_SIZE];
            // loop to read any incoming messages
            while (!exitStatus)
            {
                try
                {
                    int bytesRead = netStream.Read(bytesReceived, 0,
                    bytesReceived.Length);
                    string receivedQues = Encoding.ASCII.GetString(bytesReceived, 0, bytesRead);
                    string questionToShow = receivedQues.Split('=')[0].Trim();
                    int correctAnswer = int.Parse(receivedQues.Split('=')[1].Trim());
                    this.correctAnswer = correctAnswer;

                    SetText(questionToShow);
                }
                catch (System.IO.IOException)
                {
                    Console.WriteLine("Server has exited!");
                    exitStatus = true;
                }
            }
        }

        private void SetSubmitButtonEnabled(bool isEnabled)
        {
            SF_Submit_Button.Enabled = isEnabled;
        }
        private void SetText(string questionToShow)
        {            
            // InvokeRequired compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // if these threads are different, it returns true.
            if (this.SF_Que_TextBox.InvokeRequired)
            {
                // d is a Delegate reference to the SetText() method
                SetTextCallback d = new SetTextCallback(SetText);
                this.Invoke(d, new object[] { questionToShow });
            }
            else
            {
                this.SF_Que_TextBox.Text += questionToShow + " = ? " + Environment.NewLine;
                SetSubmitButtonEnabled(true);
            }
        }

        private bool ValidateAnswer()
        {
            string answerText = SF_Answer_TextBox.Text.Trim();

            if (string.IsNullOrEmpty(answerText))
            {
                MessageBox.Show("The answer cannot be empty, please input the answer for the question!", "Error!", MessageBoxButtons.OK);
                return false;
            }

            Regex regex = new Regex(@"^[0-9]+$");
            if (!regex.IsMatch(answerText))
            {
                MessageBox.Show("The anwer is not numeric!", "Error!", MessageBoxButtons.OK);
                return false;
            }
            else
            {
                return true;
            }
            
        }

        private void SF_Submit_Button_Click(object sender, EventArgs e)
        {
            if (ValidateAnswer())
            {
                int studentAnswer = int.Parse(SF_Answer_TextBox.Text);
                if (studentAnswer == correctAnswer)
                {
                    MessageBox.Show("Correct!", "Correct Answer", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    byte[] bytesToSend = Encoding.ASCII.GetBytes("y");
                    netStream.Write(bytesToSend, 0, bytesToSend.Length);
                    SF_Answer_TextBox.Text = "";
                    SF_Que_TextBox.Text = "";
                    SF_Submit_Button.Enabled = false;
                }
                else
                {
                    MessageBox.Show("Incorrect!", "Incorrect Answer", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    byte[] bytesToSend = Encoding.ASCII.GetBytes("n");
                    netStream.Write(bytesToSend, 0, bytesToSend.Length);
                    SF_Answer_TextBox.Text = "";
                    SF_Que_TextBox.Text = "";
                    SF_Submit_Button.Enabled = false;
                }

            }
        }

        private void StudentApp_FormClosed(object sender, FormClosedEventArgs e)
        {
            // terminate thread if still running
            if (clientThread.IsAlive)
            {
                Console.WriteLine("Client thread is alive");
                clientThread.Interrupt();
                if (clientThread.IsAlive)
                {
                    Console.WriteLine("Client thread is now terminated");
                }
            }
            else
            {
                Console.WriteLine("Client thread is terminated");
            }
            // close the application for good
            Environment.Exit(0);
        }

        private void SF_Exit_Button_Click(object sender, EventArgs e)
        {
            // terminate thread if still running
            if (clientThread.IsAlive)
            {
                Console.WriteLine("Client thread is alive");
                clientThread.Interrupt();
                if (clientThread.IsAlive)
                {
                    Console.WriteLine("Client thread is now terminated");
                }
            }
            else
            {
                Console.WriteLine("Client thread is terminated");
            }
            // close the application for good
            Environment.Exit(0);
        }
    }
    
}
