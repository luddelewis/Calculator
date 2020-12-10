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
        public double firstNumber;
        public double secondNumber;
        public string result;
        public string btnPressed = "0";
        public string memory = "0";
        public string operation;
        public string lastDisplay = "0";
        public int numberOfOperations;
        public void Display(string dsp)
        {
            firstEntry = false;
            Displaybox.Text += dsp;
            memory += dsp;
            lastDisplay = dsp;

        }
        public void Clear()
        {
            Displaybox.Text = "0";
            lastDisplay = "0";
            btnPressed = "0";
            memory = "0";
            numberOfOperations = 0;
            firstEntry = true;
        }
        public void TwoTermOperator(double firstNumberParameter)
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
            Display(result);
            memory = result;
        }
        private void Button_Click(object sender, EventArgs e)
        {
            btnPressed = ((Button)sender).Text;
            switch (btnPressed)
            {
                case string n when (double.TryParse(n, out _) && n!="0"):
                    if (lastDisplay == "0")
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
                        TwoTermOperator(firstNumber);
                        numberOfOperations--;
                    }
                    firstNumber = double.Parse(memory);
                    Display(btnPressed);
                    operation = btnPressed;
                    memory = "";
                    break;
                case "=":
                    TwoTermOperator(firstNumber);
                    break;
                case "C":
                    Clear();
                    break;
            }
              
        }
        
    }
}
