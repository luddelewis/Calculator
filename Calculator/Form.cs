using System;
using System.Windows.Forms;

namespace Calculator
{
    public partial class Calculator : Form
    {
        public Calculator()
        {
            InitializeComponent();
        }
        public bool firstEntry = true;
        public int inputMode = 1;//Specifies which inputmode to use
        public double firstNumber; //Used to store the first number for use in operations
        public double secondNumber;//Used to store the second number for use in operations
        public string result;// Used to store result of operations
        public string btnPressed = "0";// Input variable, used to store which button was last pressed
        public string memory = "0";//Used to store enterd. ex when + is pressed memory gets saved as furst number and then gets cleared, when = is pressed memory is stored as second number and cleared
        public string twoTermOperation;//Used to specify which twotermoperator is currently being used ex +
        public string singleTermOperation;//Used to specify which singletermoperator is currently being used ex x^2
        public string lastDisplay = "0"; //Used to store the last displayed character
        public int numberOfOperations;//Counter for how many twotermoperators have been enterd
        //Function for displaying the inputed numbers
        private void Display(string dsp)
        {
            firstEntry = false;
            if (inputMode == 1)// For when the input should be added to whats displaying
            {
                Displaybox.Text += dsp;
                inputMode = 1;
            }
            else if (inputMode == 0)//For when the only the input should be displayed. ex. after a twotermoperator is inputted.
            {
                Displaybox.Text = dsp;
                inputMode = 1;
            }
            //NOTE: inputMode=2 is used when the display should not be changed ex. during a twotermoperation
            if (double.TryParse(dsp, out _) || dsp == ",")//If the input is a number or a comma it gets added to memory
            {
                memory += dsp;
            }
            lastDisplay = dsp;
        }
        //Function for resetting the program gets run whenever C button is pressed
        private void Clear()
        {
            Displaybox.Text = "0";
            lastDisplay = "0";
            btnPressed = "0";
            memory = "0";
            numberOfOperations = 0;
            firstEntry = true;
            twoTermOperation = "";
            inputMode = 1;
        }
        //Function for calculating the value of a two term operation
        private void TwoTermOperator(double firstNumberParameter)
        {
            //If statement to make sure repeated "=" works properly
            if (lastDisplay != "=")
            {
                secondNumber = double.Parse(memory);
            }

            //Just calculates the result
            switch (twoTermOperation)
            {
                case "+":
                    result = ((firstNumberParameter + secondNumber).ToString());

                    break;
                case "-":
                    result = ((firstNumberParameter - secondNumber).ToString());
                    break;
                case "*":
                    result = ((firstNumberParameter * secondNumber).ToString());
                    break;
                case "÷":
                    if (secondNumber != 0)
                    {
                        result = ((firstNumberParameter / secondNumber).ToString());
                    }
                    else result="Anton ERR";// Internskämt då Anton har dividerat med noll ett flertal gånger
                    break;
                case "":
                    result = Displaybox.Text;
                    break;
            }
            memory = result;
            if(memory!="Anton ERR")
            {
                firstNumber = double.Parse(memory);
                lastDisplay = memory;
            }
            Displaybox.Text = memory;
            numberOfOperations = 1;

        }
        //Funciton for calculating singletermoperations
        private void SingleTermOperator(double singleTermParameter)
        {
            switch (singleTermOperation)
            {
                case "±":
                    result = (singleTermParameter * -1).ToString();
                    break;
                case "√":
                    result = (Math.Sqrt(singleTermParameter)).ToString();
                    break;
                case "x^2":
                    result = (Math.Pow(singleTermParameter,2)).ToString();
                    break;
                case "1/x":
                    if (singleTermParameter == 0)
                    {
                        result="Anton ERR";
                    }
                    else
                    {
                        result = (1 / singleTermParameter).ToString();
                    }
                    break;
            }
            memory = result;
            if (result == "Anton ERR")
            {
                Displaybox.Text = result;
            }
            else
            {
                memory = result;
                lastDisplay = memory;
                Displaybox.Text = memory;

            }
            
         
        }
        //The InputInterperter runs after each buttons press to determine what to do
        private void InputInterperter(string input)
        {
            switch (btnPressed)
            {
                case string n when (double.TryParse(n, out _) && n != "0"):
                    if (lastDisplay == "0" && firstEntry)
                    {
                        Displaybox.Text = Displaybox.Text.Substring(0, Displaybox.Text.Length - 1);
                    }
                    Display(btnPressed);
                    break;
                case "0":
                    if (!firstEntry)
                    {
                        Display(btnPressed);
                    }
                    break;
                case ",":
                    if (lastDisplay != ",")
                    {
                        Display(btnPressed);
                    }
                    break;
                case string n when (n == "+" || n == "-" || n == "*" || n == "÷"):
                    //if statement to change twotermoperator and prevent crash if two twotermoperations are enterd after eachother
                    if (lastDisplay == "+" || lastDisplay == "-" || lastDisplay == "*" || lastDisplay == "*")
                    {
                        twoTermOperation = btnPressed;
                    }
                    else
                    {
                        numberOfOperations++;
                        // if statement for when more than one twotermoperator inputted ex. 5+5+,
                        if (numberOfOperations == 2)
                        {
                            //calculates the first two numbers and changes the firstnumber to the result
                            TwoTermOperator(firstNumber);
                            twoTermOperation = btnPressed;
                            inputMode = 2;
                            firstNumber = double.Parse(memory);
                        }
                        else
                        {
                            //else for when there is only one two term operator
                            firstNumber = double.Parse(memory);
                            inputMode = 0;
                        }
                        twoTermOperation = btnPressed;
                        memory = "";
                        lastDisplay = btnPressed;
                    }
                    break;
                case "=":
                    numberOfOperations = 0;
                    TwoTermOperator(firstNumber);
                    lastDisplay = "=";
                    numberOfOperations = 0;
                    break;
                case "C":
                    Clear();//Resets Program
                    break;
                case "←":
                    //Removes last characterf from memory and displaybox.text
                    if (double.TryParse(memory, out _) || lastDisplay == "," && inputMode == 1)
                    {
                        memory = memory.Substring(0, memory.Length - 1);
                        Displaybox.Text = Displaybox.Text.Substring(0, Displaybox.Text.Length - 1);
                    }
                    else if (inputMode == 2)
                    {
                        memory = memory.Substring(0, memory.Length - 1);
                    }
                    if (Displaybox.Text == "")
                    {
                        Displaybox.Text = "0";
                        lastDisplay = "0";
                        firstEntry = true;
                    }
                    break;
                case "CE":
                    //Clears the current enterd number
                    if (double.TryParse(memory, out _) || lastDisplay == "," && inputMode == 1)
                    {
                        memory = "0";
                        Displaybox.Text = "0";
                    }
                    else if (inputMode == 2)
                    {
                        memory = "0";
                    }
                    if (Displaybox.Text == "")
                    {
                        Displaybox.Text = "0";

                    }
                    lastDisplay = "0";
                    firstEntry = true;
                    break;
                case string n when (n == "±" ||n== "√"||n== "x^2"||n== "1/x"):
                    singleTermOperation = btnPressed;
                    SingleTermOperator(double.Parse(memory));

                    break;
            }
        }
        //Whenever a button is clicked its text gets saved to btnpressed and InputInterperter is run
        private void Button_Click(object sender, EventArgs e)
        {
            btnPressed = ((Button)sender).Text;
            InputInterperter(btnPressed);


        }
        //Whenever a keyboard key is pressed the corresponding input is fed to the program. Translates the keyvalue to the corresponding Btnpress value
        private void Calculator_KeyDown(object sender, KeyEventArgs e)
        {
            switch ((int)e.KeyValue)//Translates keyvalues to btnpresses
            {
                case int n when (n >= 48 && n <= 57):
                    btnPressed = ((int)e.KeyValue - 48).ToString();
                    break;
                case int n when (n >= 96 && n <= 105):
                    btnPressed = ((int)e.KeyValue - 96).ToString();
                    break;
                case 107:
                    btnPressed = "+";
                    break;
                case 109:
                    btnPressed = "-";
                    break;
                case 106:
                    btnPressed = "*";
                    break;
                case 111:
                    btnPressed = "÷";
                    break;
                case 13:
                    btnPressed = "=";
                    break;
                case 27:
                    btnPressed = "C";
                    break;
                case 110:
                    btnPressed = ",";
                    break;
                case 8:
                    btnPressed = "←";
                    break;
                case 46:
                    btnPressed = "C";
                    break;


            }
            InputInterperter(btnPressed);

        }
    }
}
