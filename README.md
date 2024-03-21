# **MathQuiz**

MathQuiz is a C# application that allows instructors to set up math questions and receive answers from students. It includes functionalities for sending math questions to students, receiving answers via a TCP server, validating student answers, and providing feedback on correctness.

# Features
* Set up math questions with operands and operators
* Send math questions to students via TCP network
* Receive student answers and validate correctness
* Provide feedback to students on the correctness of their answers

# Installation
* Clone or download the repository to your local machine
* Open the solution file (MathQuizApp.sln) in Visual Studio
* Build the solution to restore dependencies and compile the application

# Usage
* Launch the student application

  ![image](https://github.com/YunChen2023/MathQuiz/assets/143974178/15965d8d-e9d4-42b4-b92a-d11ae99a174d)

* Enter the mathematical question by providing the left operand (“1”), selecting the operator (“+”), and inputting the right operand (“1”) individually within the instructor application. Afterward, click the "SEND" button to transmit the question to the student application

  ![image](https://github.com/YunChen2023/MathQuiz/assets/143974178/8079079a-3bed-463c-9c1e-3a031bc362b6)

* The mathematical question forwarded from the instructor app is exhibited in the Question Text Box of the student app as "1 + 1 = ?". In the event that the student attempts to submit their response without providing an answer, an error message box will appear, displaying the prompt: "The answer cannot be empty; please input the answer for the question!"

  ![image](https://github.com/YunChen2023/MathQuiz/assets/143974178/6896a47e-3c41-4892-ba7b-e525b0128aba)

* In an alternative scenario, if the student submits a non-numeric response, an error message box will be triggered, presenting the message: "The answer is not numeric!"

  ![image](https://github.com/YunChen2023/MathQuiz/assets/143974178/635972da-8402-4989-be94-27f1854f198d)

* If the student enters a numerical value in the Answer Text Box and subsequently clicks the Submit button, but the provided answer does not match the correct solution for the presented question, a message box will be displayed with the message "Incorrect!". Otherwise, a message box will be displayed with the message "Correct!"

  ![image](https://github.com/YunChen2023/MathQuiz/assets/143974178/13a0d361-c5a8-4657-a326-ca32b9fbdad7)

  ![image](https://github.com/YunChen2023/MathQuiz/assets/143974178/501344e7-99c9-4215-87b4-8d1d1e8ac8fe)






