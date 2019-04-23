using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Calculater
{
    public partial class Form1 : Form
    {
        // 紀錄數字
        Double value = 0;
        // 運算 + - * /
        String operation = "";
        // 是否可運算
        bool operation_pressd = false;

        public Form1()
        {
            InitializeComponent();
        }


        private void Number_Click(object sender, EventArgs e)
        {
            if (result.Text == "0" )
            {
                result.Clear();
            }
            Button b = (Button)sender;
            result.Text += b.Text;
        }

        private void Zero_Click(object sender, EventArgs e)
        {
            if (result.Text == "0" || result.Text == "")
            {

            }
            else
            {
                result.Text += "0";
            }
        }

        private void Double_Zero_Click(object sender, EventArgs e)
        {
            if (result.Text == "0" || result.Text == "")
            {
                result.Text = "0";
            }
            else
            {
                result.Text += "00";
            }
        }

        private void Dot_Click(object sender, EventArgs e)
        {
            if (result.Text.Contains("."))
            {

            }
            else
            {
                result.Text += ".";
            }
        }

        private void Operation_Click(object sender, EventArgs e)
        {
            
            Button b = (Button)sender;
            operation = b.Text;
            operation_pressd = true;

            if (result.Text != "")
            {
                value = Double.Parse(result.Text);
                PreResult.Text = b.Text + value;
            }
            result.Clear();

            
        }

        private void Delete_Click(object sender, EventArgs e)
        {
            int len = result.Text.Length - 1;

            result.Text = result.Text.Remove(len);
        }

        private void Delete_All_Click(object sender, EventArgs e)
        {
            value = 0;
            result.Clear();
            
        }

        private void Answer_Click(object sender, EventArgs e)
        {
            switch (operation)
            {
                case "+":
                    result.Text = (value + Double.Parse(result.Text)).ToString();
                    break;
                case "-":
                    result.Text = (value - Double.Parse(result.Text)).ToString();
                    break;
                case "*":
                    result.Text = (value * Double.Parse(result.Text)).ToString();
                    break;
                case "/":
                    result.Text = (value / Double.Parse(result.Text)).ToString();
                    break;
                default:
                    break;
            }

            operation_pressd = false;
            PreResult.Text = "";
        }

        private void Exit(object sender, EventArgs e)
        {
            Close();
        }

    }
}


