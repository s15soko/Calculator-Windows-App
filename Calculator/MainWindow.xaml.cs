using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;



namespace Calculator
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        CalculatorController calc;
        calcOptions actuallOption;
        double localSum;
        bool startNewType;

        // main window
        public MainWindow()
        {
            InitializeComponent();

            this.actuallOption = new calcOptions();
            this.actuallOption = calcOptions.NULL;
            this.calc = new CalculatorController();
            this.localSum = 0;
            this.holderTextBlock.Text = "";
            this.actuallChoiceSign.Text = "";
            this.sumTextBox.Text = "0";
            this.startNewType = true;
        }
        // update actuall choice sign
        private void actuallOptionSign()
        {
            if (this.actuallOption == calcOptions.NULL) this.actuallChoiceSign.Text = "";
            if (this.actuallOption == calcOptions.PLUS) this.actuallChoiceSign.Text = "+";
            if (this.actuallOption == calcOptions.MINUS) this.actuallChoiceSign.Text = "-";
            if (this.actuallOption == calcOptions.MULTIPLY) this.actuallChoiceSign.Text = "*";
            if (this.actuallOption == calcOptions.DIVIDE) this.actuallChoiceSign.Text = "/";
        }

        // convert string data to double type
        private double convertToDouble(string str)
        {
            double result;
            Double.TryParse(str, out result);
            return result;
        }

        // clear calc data
        private void clearData()
        {
            calc.setZero();
            this.localSum = 0;
            this.holderTextBlock.Text = "";
            this.actuallChoiceSign.Text = "";
            this.sumTextBox.Text = "0";
        }

        // set actuall sum
        // parse from string to double type
        // set in calculator controller value
        private void setLocalSum()
        {
            this.localSum = convertToDouble(this.sumTextBox.Text);
        }

        // add number to sum text box
        private void addNumber(string number)
        {
            if (startNewType)
            {
                this.sumTextBox.Text = "";
                calc.setZero();
                startNewType = false;
            }

            if (this.sumTextBox.Text != "0")
            {
                this.sumTextBox.Text += number;
            }
            else this.sumTextBox.Text = number;

            // set local sum
            // convert sum text box from string to double type
            setLocalSum();
        }

        // replace local sum with calc controller sum
        // and then set local sum to 0
        private void replace()
        {
            calc.setSum(this.localSum);

            this.holderTextBlock.Text = this.sumTextBox.Text;
            this.localSum = 0;
            this.sumTextBox.Text = "0";
        }

        private void count()
        {
            switch(actuallOption)
            {
                case calcOptions.PLUS:
                    calc.addValue(this.localSum);
                    break;
                case calcOptions.MINUS:
                    calc.subtractValue(this.localSum);
                    break;
                case calcOptions.MULTIPLY:
                    calc.multiplyValue(this.localSum);
                    break;
                case calcOptions.DIVIDE:
                    calc.divideValue(this.localSum);
                    break;
                case calcOptions.POWER:
                    calc.powerValue(this.localSum);
                    break;
                case calcOptions.ELEMENT:
                    calc.elementValue(this.localSum);
                    break;
            }

            this.localSum = calc.returnSum();
            this.holderTextBlock.Text = "";
            this.sumTextBox.Text = calc.returnSum().ToString();
            this.actuallOption = calcOptions.NULL;
            actuallOptionSign();
        }


        #region option buttons
        // comma
        private void CommaButton_Click(object sender, RoutedEventArgs e)
        {
            if(!this.sumTextBox.Text.Contains(","))
            {
                this.sumTextBox.Text += ",";
            }
        }
        // inverse
        private void InverseButton_Click(object sender, RoutedEventArgs e)
        {
            calc.inverseSum();
        }
        // clear
        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            clearData();
        }
        // option =
        private void EqualButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.holderTextBlock.Text != "") count();
            startNewType = true;
            checkkkk();

        }

        // set calc option and update text box
        private void setOption(calcOptions opt)
        {
            // replace local sum with calc object sum
            // for input new value...
            startNewType = false;
            if (this.holderTextBlock.Text == "") replace();
            this.actuallOption = opt;
            actuallOptionSign();
        }

        // option *
        private void MultiplyButton_Click(object sender, RoutedEventArgs e)
        {
            setOption(calcOptions.MULTIPLY);
        }
        // option /
        private void DivideButton_Click(object sender, RoutedEventArgs e)
        {
            setOption(calcOptions.DIVIDE);
        }
        // option +
        private void PlusButton_Click(object sender, RoutedEventArgs e)
        {
            setOption(calcOptions.PLUS);
        }
        // option -
        private void MinusButton_Click(object sender, RoutedEventArgs e)
        {
            setOption(calcOptions.MINUS);
        }
        // option x^2
        private void PowerButton_Click(object sender, RoutedEventArgs e)
        {

        }
        // option /x
        private void ElementButton_Click(object sender, RoutedEventArgs e)
        {

        }
        #endregion

        #region buttons [0-9]
        // 0
        private void Btn_nZero_Click(object sender, RoutedEventArgs e)
        {
            addNumber("0");
        }
        // 1
        private void Btn_zOne_Click(object sender, RoutedEventArgs e)
        {
            addNumber("1");
        }
        // 2
        private void Btn_nTwo_Click(object sender, RoutedEventArgs e)
        {
            addNumber("2");
        }
        // 3
        private void Btn_nThree_Click(object sender, RoutedEventArgs e)
        {
            addNumber("3");
        }
        // 4
        private void Btn_nFour_Click(object sender, RoutedEventArgs e)
        {
            addNumber("4");
        }
        // 5
        private void Btn_nFive_Click(object sender, RoutedEventArgs e)
        {
            addNumber("5");
        }
        // 6
        private void Btn_nSix_Click(object sender, RoutedEventArgs e)
        {
            addNumber("6");
        }
        // 7
        private void Btn_nSeven_Click(object sender, RoutedEventArgs e)
        {
            addNumber("7");
        }
        // 8
        private void Btn_nEight_Click(object sender, RoutedEventArgs e)
        {
            addNumber("8");
        }
        // 9
        private void Btn_nNine_Click(object sender, RoutedEventArgs e)
        {
            addNumber("9");
        }

        #endregion


        private void checkkkk()
        {
            Console.WriteLine("===================");
            Console.WriteLine("holderTextBlock: " + this.holderTextBlock.Text);
            Console.WriteLine("sumTextBox: " + this.sumTextBox.Text);
            Console.WriteLine("Local sum: " + this.localSum);
            calc.showSum();
        }

        private void Check_Click(object sender, RoutedEventArgs e)
        {
            checkkkk();
        }
    }
}
