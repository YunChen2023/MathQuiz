namespace NUnit_BinarySearchTest
{
    public class MathQues : IComparable<MathQues>
    {
        // public properties
        // integer LeftOperand
        public int LeftOperand { get; set; }
        // string MathOperator
        public string MathOperator { get; set; }
        // integer RightOperand
        public int RightOperand { get; set; }
        // integer Answer
        public int Answer { get; set; }

        // constructor adding values to properties (e.g. input: (3, "+", 4, 7)
        public MathQues(int leftOperand, string mathOperator, int rightOperand, int answer)
        {
            LeftOperand = leftOperand;
            MathOperator = mathOperator;
            RightOperand = rightOperand;
            Answer = answer;
        }

        public override string ToString()
        {
            return Answer + "(" + LeftOperand + MathOperator + RightOperand + ")";
        }

        public string QuestionToSend()
        {
            return LeftOperand + " " + MathOperator + " " + RightOperand + " = " + Answer;
        }

        public string[] GetStrArray()
        {
            string LOpStr = LeftOperand.ToString();
            string ROpStr = RightOperand.ToString();
            string ansStr = Answer.ToString();
            string[] strArray = new string[5];
            strArray[0] = LOpStr;
            strArray[1] = MathOperator;
            strArray[2] = ROpStr;
            strArray[3] = "=";
            strArray[4] = ansStr;
            return strArray;
        }
        // CompareTo() implemented method from IComparable
        public int CompareTo(MathQues other)
        {
            if (Answer == other.Answer)
            {
                return 0;
            }
            else if (Answer < other.Answer)
            {
                return -1;
            }
            else
            {
                return 1;
            }
        }

        public static int BinarySearch(string[] strArray, string mathQuesSearch)
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
        public class Tests
        {
            private List<MathQues> questionList;
            [SetUp]
            public void Setup()
            {
                questionList = new List<MathQues>();
                questionList.Add(new MathQues(4, "+", 5, 9));
                questionList.Add(new MathQues(4, "+", 1, 5));
                questionList.Add(new MathQues(4, "+", 2, 6));
                questionList.Add(new MathQues(4, "+", 3, 7));
                questionList.Sort();
            }

            [Test]
            public void Test1()
            {
                string[] mathQuesStrArray = new string[questionList.Count];
                for (int i = 0; i < questionList.Count; i++)
                {
                    mathQuesStrArray[i] = questionList[i].QuestionToSend();
                }
                string mathQuesSearch = "4 + 5 = 9";
                int actualResult = BinarySearch(mathQuesStrArray, mathQuesSearch);
                int expectResult = 3;
                Assert.AreEqual(actualResult, expectResult);
            }

            [Test]
            public void Test2()
            {
                string[] mathQuesStrArray = new string[questionList.Count];
                for (int i = 0; i < questionList.Count; i++)
                {
                    mathQuesStrArray[i] = questionList[i].QuestionToSend();
                }
                string mathQuesSearch = "4 + 1 = 5";
                int actualResult = BinarySearch(mathQuesStrArray, mathQuesSearch);
                int expectResult = 0;
                Assert.AreEqual(actualResult, expectResult);
            }

            [Test]
            public void Test3()
            {
                string[] mathQuesStrArray = new string[questionList.Count];
                for (int i = 0; i < questionList.Count; i++)
                {
                    mathQuesStrArray[i] = questionList[i].QuestionToSend();
                }
                string mathQuesSearch = "4 + 2 = 6";
                int actualResult = BinarySearch(mathQuesStrArray, mathQuesSearch);
                int expectResult = 1;
                Assert.AreEqual(actualResult, expectResult);
            }
        }
    }
}