using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace InstructorApp
{
    public partial class InstructorForm : Form
    {
        public bool exitStatus = false;
        public const int BYTE_SIZE = 1024;
        public const int PORT_NUMBER = 8888;
        // listens for and accept incoming connection requests
        private TcpListener serverListener;
        // TcpClient is used to connect with the TcpListener object
        private TcpClient serverSocket;
        // set up data stream object
        private NetworkStream netStream;
        // set up thread to run ReceiveStream() method
        private Thread serverThread = null;
        // set up delegate 
        // a delegate is a reference variable to a method
        // and used for a call back by the delegate object
        // delegate ref variable is declared in SetText() method below
        delegate void SetTextCallback(string text);

        public const int TOTAL_ROWS = 30;
        public const int TOTAL_COLS = 5;

        private List<MathQues> questionList;
        private LinkedList<MathQues> questionLinkedList;
        private BinaryTree<MathQues> questionBinaryTree;
        //private HashSet<string> questionStrHashSet;
        private Hashtable questionHashtable;

        private MathQues currentQuestion;
        public InstructorForm()
        {
            InitializeComponent();

            instructor_DataGridView.BackgroundColor = Color.White;

            // clear all text boxes
            FirstNo_TextBox.Text = "";
            SecondNo_TextBox.Text = "";
            Answer_TextBox.Text = "";

            questionList = new List<MathQues>();
            questionLinkedList = new LinkedList<MathQues>();
            questionBinaryTree = new BinaryTree<MathQues>();
            //questionStrHashSet = new HashSet<string>();
            questionHashtable = new Hashtable();
            
            currentQuestion = null;
            
            DisplayTable();
            // run server
            StartServer();
        }
        private bool ValidateMathQuestion()
        {
            bool isFirstOperandValid = !string.IsNullOrWhiteSpace(FirstNo_TextBox.Text);
            bool isSecondOperandValid = !string.IsNullOrWhiteSpace(SecondNo_TextBox.Text);
            Regex regex = new Regex(@"^[0-9]+$");
            bool isFirstOperandNumeric = regex.IsMatch(FirstNo_TextBox.Text);
            bool isSecondOperandNumeric = regex.IsMatch(SecondNo_TextBox.Text);
            int errorCount = 0;
            string errorMessage = "";

            if (!isFirstOperandValid)
            {
                errorMessage += "First number is missing.\n";
                errorCount++;
            }
            else if (!isFirstOperandNumeric)
            {
                errorMessage += "First number is not numeric.\n";
                errorCount++;
            }

            if (!isSecondOperandValid)
            {
                errorMessage += "Second number is missing.\n";
                errorCount++;
            }
            else if (!isSecondOperandNumeric)
            {
                errorMessage += "Second number is not numeric.\n";
                errorCount++;
            }

            if (errorCount > 0)
            {
                MessageBox.Show(errorMessage, $"{errorCount} error(s) detected!", MessageBoxButtons.OK);
                return false;
            }

            return true;
        }

        private void StartServer()
        {
            try
            {
                // create listener and start
                serverListener = new TcpListener(IPAddress.Loopback, PORT_NUMBER);
                serverListener.Start();
                // create acceptance socket
                // this creates a socket connection for the server
                serverSocket = serverListener.AcceptTcpClient();
                // create stream
                netStream = serverSocket.GetStream();
                // set up thread to run ReceiveStream() method
                serverThread = new Thread(ReceiveStream);
                // start thread
                serverThread.Start();
                //FirstNo_TextBox.Text += Environment.NewLine;
            }
            catch (Exception e)
            {
                // display exception message
                //SystemMsg_TextBox.Text = e.StackTrace;
            }
        }

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
                    this.SetText(Encoding.ASCII.GetString(bytesReceived,
                    0, bytesRead));
                }
                catch (System.IO.IOException)
                {
                    //Console.WriteLine("Client has exited!");
                    exitStatus = true;
                }
            }
        }
        private void SetText(string recievedStr)
        {
            SetTextCallback d = new SetTextCallback(SetText);
            //this.Invoke(d, new object[] { recievedStr });
            if (recievedStr == "n")
            {                
                questionLinkedList.AddLast(currentQuestion);
                ClearTextBoxes();
                answerCheckWrong();
            }
            else if (recievedStr == "y")
            {
                ClearTextBoxes();
                answerCheckRight();
            }
        }

        private void ClearTextBoxes()
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new Action(ClearTextBoxes));
            }
            else
            {
                FirstNo_TextBox.Text = "";
                SecondNo_TextBox.Text = "";
                Answer_TextBox.Text = "";
                Send_Button.Enabled = true;
            }
        }

        private void answerCheckRight()
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new Action(answerCheckRight));
            }
            else
            {
                BinaryTree_TextBox.Text = "Notice: Student has answered correctly!";
            }                
        }
        private void answerCheckWrong()
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new Action(answerCheckWrong));
            }
            else
            {
                BinaryTree_TextBox.Text = "Notice: Student has answered incorrectly!";
            }
        }


        private void Send_Button_Click(object sender, EventArgs e)
        {
            if (ValidateMathQuestion())
            {
                    int firstOperand = int.Parse(FirstNo_TextBox.Text);
                    int secondOperand = int.Parse(SecondNo_TextBox.Text);
                    string selectedOperator = Operator_ComboBox.SelectedItem.ToString();

                    int answer = 0;
                    switch (selectedOperator)
                    {
                        case "+":
                            answer = firstOperand + secondOperand;
                            break;
                        case "-":
                            answer = firstOperand - secondOperand;
                            break;
                        case "x":
                            answer = firstOperand * secondOperand;
                            break;
                        case "/":
                            if (secondOperand != 0)
                            {
                                answer = firstOperand / secondOperand;
                            }
                            else
                            {
                                MessageBox.Show("Cannot divide by zero.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                            break;
                    }

                    
                    currentQuestion = new MathQues(firstOperand, selectedOperator, secondOperand, answer);
                    string questionString = currentQuestion.QuestionToSend();
                    if (questionHashtable.ContainsKey(questionString))
                    {
                        MessageBox.Show("The same math question has already been entered.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    questionList.Add(currentQuestion);
                    DisplayTable();
                    questionBinaryTree.Add(currentQuestion);
                    
                    //questionStrHashSet.Add(questionString);
                    questionHashtable.Add(currentQuestion.QuestionToSend(), currentQuestion);

                    Answer_TextBox.Text = answer.ToString();
                    string questionToSend = currentQuestion.QuestionToSend();
                    byte[] bytesToSend = Encoding.ASCII.GetBytes(questionToSend);
                    netStream.Write(bytesToSend, 0, bytesToSend.Length);

                    Send_Button.Enabled = false;
            }
        }

        private void DisplayTable()
        {
            if (questionList.Count > 0)
            {

                DataTable dataTable = new DataTable();
                dataTable.Columns.Add("Number 1");
                dataTable.Columns.Add("Math");
                dataTable.Columns.Add("Number 2");
                dataTable.Columns.Add("=");
                dataTable.Columns.Add("Answer");
                for (int i = 0; i < questionList.Count; i++)
                {
                    string[] questionStrs = questionList[i].GetStrArray();
                    
                    dataTable.Rows.Add(questionStrs);
                }
                instructor_DataGridView.DataSource = dataTable;
            }
        }

        private void InstructorApp_FormClosed(object sender, FormClosedEventArgs e)
        {
            // terminate thread if still running
            if (serverThread.IsAlive)
            {
                Console.WriteLine("Server thread is alive");
                serverThread.Interrupt();
                if (serverThread.IsAlive)
                {
                    Console.WriteLine("Server thread is now terminated");
                }
            }
            else
            {
                Console.WriteLine("Server thread is terminated");
            }
            // close the application for good
            Environment.Exit(0);
        }

        private void BubbleSort_Button_Click(object sender, EventArgs e)
        {
            if (questionList.Count == 0)
            {
                MessageBox.Show("No math questions have been set up!", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MathQues[] mathQuesArray = questionList.ToArray();
                for (int i = 0; i < mathQuesArray.Length; i++)
                {
                    for (int j = 0; j < mathQuesArray.Length - 1; j++)
                    {
                        if (mathQuesArray[i].Answer < mathQuesArray[j].Answer)
                        {
                            // swap values
                            MathQues temp = mathQuesArray[i];
                            mathQuesArray[i] = mathQuesArray[j];
                            mathQuesArray[j] = temp;
                        }
                    }
                }
                questionList.Clear();
                questionList.AddRange(mathQuesArray);
                DisplayTable();
            }
        }

        private void SelectionSort_Button_Click(object sender, EventArgs e)
        {
            if (questionList.Count == 0)
            {
                MessageBox.Show("No math questions have been set up!", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MathQues[] mathQuesArray = questionList.ToArray();
                for (int i = 0; i < mathQuesArray.Length - 1; i++)
                {
                    for (int j = i + 1; j < mathQuesArray.Length; j++)
                    {
                        if (mathQuesArray[j].Answer > mathQuesArray[i].Answer)
                        {
                            // swap values
                            MathQues temp = mathQuesArray[j];
                            mathQuesArray[j] = mathQuesArray[i];
                            mathQuesArray[i] = temp;
                        }
                    }
                }
                questionList.Clear();
                questionList.AddRange(mathQuesArray);
                DisplayTable();
            }             
        }

        private void Insertion_Button_Click(object sender, EventArgs e)
        {
            if (questionList.Count == 0)
            {
                MessageBox.Show("No math questions have been set up!", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MathQues[] mathQuesArray = questionList.ToArray();
                for (int i = 1; i < mathQuesArray.Length; i++)
                {
                    for (int j = i; j > 0; j--)
                    {
                        if (mathQuesArray[j].Answer < mathQuesArray[j - 1].Answer)
                        {
                            // swap values
                            MathQues temp = mathQuesArray[j];
                            mathQuesArray[j] = mathQuesArray[j - 1];
                            mathQuesArray[j - 1] = temp;

                        }
                    }
                }
                questionList.Clear();
                questionList.AddRange(mathQuesArray);
                DisplayTable();
            }                
        }

        private void LinkedListDis_Button_Click(object sender, EventArgs e)
        {
            if (questionLinkedList.Count == 0 & questionList.Count == 0)
            {
                LinkedList_TextBox.Text = "No math questions answered!";
            }
            else if (questionLinkedList.Count == 0 & questionList.Count > 0)
            {
                LinkedList_TextBox.Text = "All math questions answered correctly!";
            }
            else if (questionLinkedList.Count > 0)
            {
                string wrongQues = "";
                foreach (MathQues question in questionLinkedList)
                {
                    wrongQues += question.QuestionToSend() + "; ";

                }

                LinkedList_TextBox.Text = "HEAD <-> " + wrongQues + " <-> TAIL";
            }
        }

        private void LinkedListExit_Button_Click(object sender, EventArgs e)
        {
            if (serverThread.IsAlive)
            {
                Console.WriteLine("Server thread is alive");
                serverThread.Interrupt();
                if (serverThread.IsAlive)
                {
                    Console.WriteLine("Server thread is now terminated");
                }
            }
            else
            {
                Console.WriteLine("Server thread is terminated");
            }
            // close the application for good
            Environment.Exit(0);
        }

        private bool quesFormatChecked()
        {
            if (questionList.Count == 0)
            {
                MessageBox.Show("No math questions have been set up!", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            else
            {
                string quesToSearch = BinarySearch_TextBox.Text;
                string pattern = @"^\d\s[-+x/]\s\d\s=\s\d";

                if (Regex.IsMatch(quesToSearch, pattern))
                {
                    return true;
                }
                else
                {
                    // Display an error message
                    MessageBox.Show("Invalid question format! Expected format should be like: 4 + 5 = 9", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

            }

        }

        private void BinarySearch_Button_Click(object sender, EventArgs e)
        {
            if (quesFormatChecked())
            {
                string[] mathQuesStrArray = new string[questionList.Count];
                BinaryTree_TextBox.Text = "";
                for (int i = 0; i < questionList.Count; i++)
                {
                    mathQuesStrArray[i] = questionList[i].QuestionToSend();
                }
                Array.Sort(mathQuesStrArray);

                string mathQuesSearch = BinarySearch_TextBox.Text;
                int posFound = BinarySearch(mathQuesStrArray, mathQuesSearch);

                if (posFound >= 0)
                {
                    BinaryTree_TextBox.Text = "Math question found in index position " + posFound + "!";
                }
                else
                {
                    BinaryTree_TextBox.Text = "Math question not found!";
                }
            }            
        }

        static int BinarySearch(string[] strArray, string mathQuesSearch)
        {
            // break up input string "3 + 4 = 7" will be "3", "+", "4", "=", "7"
            //string[] mathQSplit = mathQToSearch.Split(' ');
            // get integer answer
            //int answer = Int32.Parse(mathQSplit[4]);
            bool foundStatus = false;
            int first = 0;
            int last = strArray.Length - 1;
            int mid;

            int posFound = -1;

            // loop while foundStatus is false AND first is less than or equal to last

            while (!foundStatus && first <= last)
            {
                // get mid index value
                mid = (first + last) / 2;

                // check if number to search is less than the value positioned in the middle of the sorted array
                // if it is, then change the last position to that of the middle less 1
                // this way, last becomes the last value in the sorted upper half of the array
                if (mathQuesSearch.CompareTo(strArray[mid]) < 0)
                {
                    last = mid - 1;

                }
                // check if number to search is greater than the value positioned in the middle of the sorted array
                // if it is, then change the first position to that of the middle plus 1
                // this way, first becomes the first value in the sorted lower half of the array
                else if (mathQuesSearch.CompareTo(strArray[mid]) > 0)
                {
                    first = mid + 1;

                }
                // otherwise, the value has been found
                else
                {
                    foundStatus = true;
                    posFound = mid;
                }
            }
            return posFound;
        }

        private void HashSearch_Button_Click(object sender, EventArgs e)
        {
            if (quesFormatChecked())
            {
                string mathQToSearch = BinarySearch_TextBox.Text;
                string[] parts = mathQToSearch.Split(' ');

                if (parts.Length == 5 && parts[1] == "+" && parts[3] == "=")
                {
                    int leftOperand = int.Parse(parts[0]);
                    int rightOperand = int.Parse(parts[2]);
                    int answer = int.Parse(parts[4]);

                    string formattedMathQ = $"{answer}({leftOperand}+{rightOperand})";

                    bool hashFound = HashSearch(questionHashtable, mathQToSearch);
                    if (hashFound)
                    {
                        BinaryTree_TextBox.Text = formattedMathQ + " found!";
                    }
                    else
                    {
                        BinaryTree_TextBox.Text = formattedMathQ + " not found!";
                    }
                }
                
            }
            
        }
        
        static bool HashSearch(Hashtable hashObj, string mathQToSearch)
        {
            return hashObj.ContainsKey(mathQToSearch);

        }

        private void PreOrDisplay_Button_Click(object sender, EventArgs e)
        {            
            BinaryTree_TextBox.Text = "";
            if (questionBinaryTree.Count == 0)
            {
                BinaryTree_TextBox.Text = "No math questions have been set up!";
            }
            else
            {
                questionBinaryTree.TraversalString = "";
                questionBinaryTree.Preorder(questionBinaryTree.GetRoot());
                BinaryTree_TextBox.Text = "PRE-ORDER: " + questionBinaryTree.TraversalString;
            }
            
        }

        private void InOrDisplay_Button_Click(object sender, EventArgs e)
        {
            BinaryTree_TextBox.Text = "";
            if (questionBinaryTree.Count == 0)
            {
                BinaryTree_TextBox.Text = "No math questions have been set up!";
            }
            else
            {
                questionBinaryTree.TraversalString = "";
                questionBinaryTree.Inorder(questionBinaryTree.GetRoot());
                BinaryTree_TextBox.Text = "IN-ORDER: " + questionBinaryTree.TraversalString;
            }
                
        }

        private void PostOrDisplay_Button_Click(object sender, EventArgs e)
        {
            BinaryTree_TextBox.Text = "";
            if (questionBinaryTree.Count == 0)
            {
                BinaryTree_TextBox.Text = "No math questions have been set up!";
            }
            else
            {
                questionBinaryTree.TraversalString = "";
                questionBinaryTree.Postorder(questionBinaryTree.GetRoot());
                BinaryTree_TextBox.Text = "POST-ORDER: " + questionBinaryTree.TraversalString;

            }
            
        }

        private void PreOrSave_Button_Click(object sender, EventArgs e)
        {
            BinaryTree_TextBox.Text = "";
            if (questionBinaryTree.Count == 0)
            {
                BinaryTree_TextBox.Text = "No math questions have been set up!";
            }
            else
            {
                questionBinaryTree.TraversalString = "";
                questionBinaryTree.Preorder(questionBinaryTree.GetRoot());
                BinaryTree_TextBox.Text = "PRE-ORDER: " + questionBinaryTree.TraversalString;
                string dataToWrite = BinaryTree_TextBox.Text;
                SaveFileDialog saveFileDialog = new SaveFileDialog();

                string documentsFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                saveFileDialog.InitialDirectory = documentsFolder;

                saveFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*) | *.*";
                saveFileDialog.FilterIndex = 2;
                saveFileDialog.RestoreDirectory = true;

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string currentFilePath = saveFileDialog.FileName;
                    try
                    {
                        File.WriteAllText(currentFilePath, dataToWrite);
                        MessageBox.Show("Pre-Order data saved to " + currentFilePath, "File Save Successful!");
                    }
                    catch (Exception exc)
                    {
                        MessageBox.Show("Error saving file: " + exc.Message, "File Save Error");
                    }
                }
            }         
        }

        private void InOrSave_Button_Click(object sender, EventArgs e)
        {
            BinaryTree_TextBox.Text = "";
            if (questionBinaryTree.Count == 0)
            {
                BinaryTree_TextBox.Text = "No math questions have been set up!";
            }
            else
            {
                questionBinaryTree.TraversalString = "";
                questionBinaryTree.Inorder(questionBinaryTree.GetRoot());
                BinaryTree_TextBox.Text = "IN-ORDER: " + questionBinaryTree.TraversalString;
                string dataToWrite = BinaryTree_TextBox.Text;

                SaveFileDialog saveFileDialog = new SaveFileDialog();

                string documentsFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                saveFileDialog.InitialDirectory = documentsFolder;

                saveFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*) | *.*";
                saveFileDialog.FilterIndex = 2;
                saveFileDialog.RestoreDirectory = true;

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string currentFilePath = saveFileDialog.FileName;
                    try
                    {
                        File.WriteAllText(currentFilePath, dataToWrite);
                        MessageBox.Show("In-Order data saved to " + currentFilePath, "File Save Successful!");
                    }
                    catch (Exception exc)
                    {
                        MessageBox.Show("Error saving file: " + exc.Message, "File Save Error");
                    }
                }
            }
            
        }

        private void PostOrSave_Bbutton_Click(object sender, EventArgs e)
        {
            BinaryTree_TextBox.Text = "";
            if (questionBinaryTree.Count == 0)
            {
                BinaryTree_TextBox.Text = "No math questions have been set up!";
            }
            else
            {
                questionBinaryTree.TraversalString = "";
                questionBinaryTree.Postorder(questionBinaryTree.GetRoot());
                BinaryTree_TextBox.Text = "POST-ORDER: " + questionBinaryTree.TraversalString;
                string dataToWrite = BinaryTree_TextBox.Text;
                SaveFileDialog saveFileDialog = new SaveFileDialog();

                string documentsFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                saveFileDialog.InitialDirectory = documentsFolder;

                saveFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*) | *.*";
                saveFileDialog.FilterIndex = 2;
                saveFileDialog.RestoreDirectory = true;

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string currentFilePath = saveFileDialog.FileName;
                    try
                    {
                        File.WriteAllText(currentFilePath, dataToWrite);
                        MessageBox.Show("Post-Order data saved to " + currentFilePath, "File Save Successful!");
                    }
                    catch (Exception exc)
                    {
                        MessageBox.Show("Error saving file: " + exc.Message, "File Save Error");
                    }
                }
            }
            
        }
    }
}
