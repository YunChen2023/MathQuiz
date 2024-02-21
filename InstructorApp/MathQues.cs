using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstructorApp
{
    /// <summary>
    /// Represents a mathematical question with operands, an operator, and an answer.
    /// </summary>
    internal class MathQues : IComparable<MathQues>
    {
        /// <summary> 
        /// Gets or sets the left operand of the mathematical question.
        /// </summary>
        // public properties
        // integer LeftOperand
        public int LeftOperand { get; set; }

        /// <summary>
        /// Gets or sets the mathematical operator of the question.
        /// </summary>
        // string MathOperator
        public string MathOperator { get; set; }

        /// <summary>
        /// Gets or sets the right operand of the mathematical question.
        /// </summary>
        // integer RightOperand
        public int RightOperand { get; set; }

        /// <summary>
        /// Gets or sets the answer to the mathematical question.
        /// </summary>
        // integer Answer
        public int Answer { get; set; }

        /// <summary>
        /// Initializes a new instance of the MathQues class.
        /// </summary>
        /// <param name="leftOperand">The left operand of the mathematical question.</param>
        /// <param name="mathOperator">The mathematical operator of the question.</param>
        /// <param name="rightOperand">The right operand of the mathematical question.</param>
        /// <param name="answer">The answer to the mathematical question.</param>
        // constructor adding values to properties (e.g. input: (3, "+", 4, 7)
        public MathQues(int leftOperand, string mathOperator, int rightOperand, int answer)
        {
            LeftOperand = leftOperand;
            MathOperator = mathOperator;
            RightOperand = rightOperand;
            Answer = answer;
        }

        /// <summary>
        /// Returns a string representation of the mathematical question and its answer.
        /// </summary>
        /// <returns>A string in the format: "Answer(LeftOperandMathOperatorRightOperand)".</returns>
        public override string ToString()
        {
            return Answer + "(" + LeftOperand + MathOperator + RightOperand + ")";
        }

        /// <summary>
        /// Generates a formatted string representation of the mathematical question.
        /// </summary>
        /// <returns>A string in the format: "LeftOperand MathOperator RightOperand = Answer".</returns>
        public string QuestionToSend()
        {
            return LeftOperand + " " + MathOperator + " " + RightOperand + " = " + Answer;
        }

        /// <summary>
        /// Retrieves an array of strings representing the components of the mathematical question.
        /// </summary>
        /// <returns>An array containing the left operand, operator, right operand, "=", and the answer.</returns>
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

        /// <summary>
        /// Compares the current instance with another MathQues object and returns an integer
        /// that indicates whether the current instance precedes, follows, or occurs in the same position
        /// in the sort order as the other object.
        /// </summary>
        /// <param name="other">The other MathQues object to compare with.</param>
        /// <returns>A value that indicates the relative order of the objects being compared.
        /// Returns 0 if the answers are equal, -1 if the current instance's answer is less, and 1 if it is greater.
        /// </returns>
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
    }
}
