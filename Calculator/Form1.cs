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
        public bool multipleOperation;
        public int inputMode = 1;
        public double firstNumber;
        public double secondNumber;
        public string result;
        public string btnPressed = "0";
        public string memory = "0";
        public string operation;
        public string lastDisplay = "0";
        public int numberOfOperations;

        private void Display(string dsp)
        {
            firstEntry = false;
            if (inputMode == 1)
            {
                Displaybox.Text += dsp;
                inputMode = 1;
            }
            else if (inputMode == 0)
            {
                Displaybox.Text = dsp;
                inputMode = 1;
            }
            if (double.TryParse(dsp, out _) || dsp == ",")
            {
                memory += dsp;
            }
            lastDisplay = dsp;

        }
        private void Clear()
        {
            Displaybox.Text = "0";
            lastDisplay = "0";
            btnPressed = "0";
            memory = "0";
            numberOfOperations = 0;
            firstEntry = true;
            operation = "";
            inputMode = 1;
        }
        private void TwoTermOperator(double firstNumberParameter)
        {

            secondNumber = double.Parse(memory);
            Displaybox.Text = "";
            switch (operation)
            {
                case "+":
                    result = ((firstNumberParameter + secondNumber).ToString());

                    break;
                case "-":
                    result = ((firstNumberParameter - secondNumber).ToString());
                    break;
                case "x":
                    result = ((firstNumberParameter * secondNumber).ToString());
                    break;
                case "÷":
                    if (secondNumber != 0)
                    {
                        result = ((firstNumberParameter / secondNumber).ToString());
                    }
                    else Display("Anton.A moment");
                    break;

            }
            if (multipleOperation)
            {
                multipleOperation = false;
                inputMode = 0;

            }
            numberOfOperations = 0;
            memory = result;
            Displaybox.Text = result;
        }
        private void SingleTermOperator(double SingleTermParameter)
        {
            switch (operation)
            {
                case "±":
                    result = (SingleTermParameter * -1).ToString();
                    break;
                case "√":
                    result = (Math.Sqrt(SingleTermParameter)).ToString();
                    break;
                case "x^2":
                    result = (Math.Pow(SingleTermParameter,2)).ToString();
                    break;
                case "1/x":
                    result = (1 / SingleTermParameter).ToString();
                    break;
            }
            memory = result;
            Displaybox.Text = result;
              
        }
        private void Input_Interpreter(string input)
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
                case string n when (n == "+" || n == "-" || n == "x" || n == "÷"):
                    numberOfOperations++;
                    if (numberOfOperations == 2)
                    {
                        multipleOperation = true;
                        TwoTermOperator(firstNumber);
                        numberOfOperations--;
                        firstNumber = double.Parse(memory);
                        inputMode = 2;

                    }
                    else
                    {
                        firstNumber = double.Parse(memory);
                        inputMode = 0;
                    }

                    operation = btnPressed;
                    memory = "0";
                    break;
                case "=":
                    TwoTermOperator(firstNumber);
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
                    operation = btnPressed;
                    SingleTermOperator(double.Parse(memory));
                    break;
            }
        }
        private void Button_Click(object sender, EventArgs e)
        {
            btnPressed = ((Button)sender).Text;
            Input_Interpreter(btnPressed);


        }
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
                    btnPressed = "x";
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
            Input_Interpreter(btnPressed);

        }


    }
}
