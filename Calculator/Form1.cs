using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Calculator
{
    public partial class Calculator : Form
    {
        public Calculator()
        {
            InitializeComponent();
        }
        public double first_number;
        public double second_number;
        public double result;
        public int operators;
        public string input;
        public string operation;
        public void Display(string dsp)
        {
            
            Displaybox.Text += dsp;
            input += dsp;
        }
       
        public void Clear()
        {
            Displaybox.Text = "";
        }
        public void Basic_operator(char operationparameter)
        {
            first_number = Convert.ToDouble(Displaybox.Text);
            Display(operationparameter.ToString());
            input="";
            operation = operationparameter.ToString();
        }

        private void button_0_Click(object sender, EventArgs e)
        {
            Display("0");
        }

        private void button_1_Click(object sender, EventArgs e)
        {
            Display("1");
        }

        private void button_2_Click(object sender, EventArgs e)
        {
            Display("2");
        }

        private void button_3_Click(object sender, EventArgs e)
        {
            Display("3");
        }

        private void button_4_Click(object sender, EventArgs e)
        {
            Display("4");
        }

        private void button_5_Click(object sender, EventArgs e)
        {
            Display("5");
        }

        private void button_6_Click(object sender, EventArgs e)
        {
            Display("6");
        }

        private void button_7_Click(object sender, EventArgs e)
        {
            Display("7");
        }

        private void button_8_Click(object sender, EventArgs e)
        {
            Display("8");
        }

        private void button_9_Click(object sender, EventArgs e)
        {
            Display("9");
        }

        private void button_comma_Click(object sender, EventArgs e)
        {
            Display(",");
        }

        private void button_add_Click(object sender, EventArgs e)
        {
           Basic_operator('+');
        }

        private void button_sub_Click(object sender, EventArgs e)
        {
            Basic_operator('-');
        }

        private void button_mult_Click(object sender, EventArgs e)
        {
           Basic_operator('x');
        }

        private void button_div_Click(object sender, EventArgs e)
        {
            Basic_operator('÷');
        }

        private void button_equal_Click(object sender, EventArgs e)
        {

            Clear();
            second_number =Convert.ToDouble(input);
            switch(operation)
            {
                case "+":
                    result = first_number+second_number;
                break;
                case "-":
                    result = first_number-second_number;
                break;
                case "x":
                    result = first_number*second_number;
                break;
                case "÷":
                    if(second_number!=0)
                    {
                       result = first_number/second_number;
                    }
                    else Display("Anton.A moment");
                    
                break;
                    
            }
            Display(result.ToString());
            
        }
    }
}
