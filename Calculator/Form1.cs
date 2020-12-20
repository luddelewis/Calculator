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
        public int inputMode = 1;
        public double firstNumber;
        public double secondNumber;
        public string result;
        public string btnPressed = "0";
        public string memory = "0";
        public string twoTermOperation;
        public string singleTermOperation;
        public string lastDisplay = "0";
        public int numberOfOperations;
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
            if (lastDisplay != "=")
            {
                secondNumber = double.Parse(memory);
            }
           

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
                    else Display("Anton.A moment");// Internskämt då Anton har dividerat med noll ett flertal gånger
                    break;
                case "":
                    result = Displaybox.Text;
                    break;
            }
            
            firstNumber = double.Parse(result);
            memory = result;
            Displaybox.Text = result;
            lastDisplay = result;
            numberOfOperations = 1;

        }
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
                        result="Anton.A moment";
                    }
                    else
                    {
                        result = (1 / singleTermParameter).ToString();
                    }
                    break;
            }
            memory = result;
            Displaybox.Text = result;
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
                    Clear();
                    break;
                case "←":
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
            switch ((int)e.KeyValue)
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


            }
            InputInterperter(btnPressed);

        }
    }
}
