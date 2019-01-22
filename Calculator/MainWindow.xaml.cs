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
        // constructor
        public MainWindow()
        {
            InitializeComponent();

            this.actuallOption = new calcOptions();
            this.actuallOption = calcOptions.Null;
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
            if (this.actuallOption == calcOptions.Null) this.actuallChoiceSign.Text = "";
            if (this.actuallOption == calcOptions.Add) this.actuallChoiceSign.Text = "+";
            if (this.actuallOption == calcOptions.Subtract) this.actuallChoiceSign.Text = "-";
            if (this.actuallOption == calcOptions.Multiply) this.actuallChoiceSign.Text = "*";
            if (this.actuallOption == calcOptions.Divide) this.actuallChoiceSign.Text = "/";
            if (this.actuallOption == calcOptions.Power) this.actuallChoiceSign.Text = "x^2";
            if (this.actuallOption == calcOptions.Element) this.actuallChoiceSign.Text = "/x";
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
        // add comma
        private void addComma()
        {
            if (!this.sumTextBox.Text.Contains(","))
            {
                this.sumTextBox.Text += ",";
            }
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

        // count 
        private void count()
        {
            switch(actuallOption)
            {
                case calcOptions.Add:
                    calc.addValue(this.localSum);
                    break;
                case calcOptions.Subtract:
                    calc.subtractValue(this.localSum);
                    break;
                case calcOptions.Multiply:
                    calc.multiplyValue(this.localSum);
                    break;
                case calcOptions.Divide:
                    calc.divideValue(this.localSum);
                    break;
                case calcOptions.Power:
                    calc.powerValue(this.localSum);
                    break;
                case calcOptions.Element:
                    calc.elementValue(this.localSum);
                    break;
            }

            this.localSum = calc.returnSum();
            this.holderTextBlock.Text = "";
            this.sumTextBox.Text = calc.returnSum().ToString();
            this.actuallOption = calcOptions.Null;
            actuallOptionSign();
        }

        // get result when you click '=' or 'enter'
        private void getResults()
        {
            // count local sum with calc object sum
            // if holder is not empty
            if (this.holderTextBlock.Text != "") count();

            startNewType = true;
            checkData();
        }


        // set calc option and update text box
        private void setOption(calcOptions opt)
        {
            // save local value
            startNewType = false;

            // replace local sum with calc object sum
            // for input new value...
            if (this.holderTextBlock.Text == "") replace();
            this.actuallOption = opt;
            // update act. sign
            actuallOptionSign();
        }

        #region option buttons
        // comma
        private void CommaButton_Click(object sender, RoutedEventArgs e)
        {
            addComma();
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
            getResults();
        }

        #region default options
        // option for multiply *
        private void MultiplyButton_Click(object sender, RoutedEventArgs e)
        {
            setOption(calcOptions.Multiply);
        }
        // option for divide /
        private void DivideButton_Click(object sender, RoutedEventArgs e)
        {
            setOption(calcOptions.Divide);
        }
        // option for adding +
        private void PlusButton_Click(object sender, RoutedEventArgs e)
        {
            setOption(calcOptions.Add);
        }
        // option for substract -
        private void MinusButton_Click(object sender, RoutedEventArgs e)
        {
            setOption(calcOptions.Subtract);
        }
        #endregion

        // option x^2
        // *this option works in another way
        private void PowerButton_Click(object sender, RoutedEventArgs e)
        {
            // set act. option
            this.actuallOption = calcOptions.Power;
            // update act. sign
            actuallOptionSign();

            // count
            calc.powerValue(this.localSum);
            this.localSum = calc.returnSum();
            this.sumTextBox.Text = calc.returnSum().ToString();
            this.actuallOption = calcOptions.Null;
            actuallOptionSign();
        }
        // option /x
        // *this option works in another way
        private void ElementButton_Click(object sender, RoutedEventArgs e)
        {
            // set act. option
            this.actuallOption = calcOptions.Element;
            // update act. sign
            actuallOptionSign();

            // count
            calc.elementValue(this.localSum);
            this.localSum = calc.returnSum();
            this.sumTextBox.Text = this.localSum.ToString();
            this.actuallOption = calcOptions.Null;
            actuallOptionSign();
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




        private void checkData()
        {
            Console.WriteLine("===================");
            Console.WriteLine("holderTextBlock: " + this.holderTextBlock.Text);
            Console.WriteLine("sumTextBox: " + this.sumTextBox.Text);
            Console.WriteLine("Local sum: " + this.localSum);
            calc.showSum();
        }
        private void Check_Click(object sender, RoutedEventArgs e)
        {
            checkData();
        }

        // type from keyboard
        private void CalculatorForm_KeyDown(object sender, KeyEventArgs e)
        {
            // for escape (clear data)
            if(e.Key == Key.Escape)
            {
                clearData();
            }
            // check for numbers [0-9]
            if ((e.Key >= Key.D0 && e.Key <= Key.D9) || (e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9))
            {
                switch(e.Key)
                {
                    case Key.D0:
                    case Key.NumPad0:
                        addNumber("0");
                    break;
                    case Key.D1:
                    case Key.NumPad1:
                        addNumber("1");
                        break;
                    case Key.D2:
                    case Key.NumPad2:
                        addNumber("2");
                        break;
                    case Key.D3:
                    case Key.NumPad3:
                        addNumber("3");
                        break;
                    case Key.D4:
                    case Key.NumPad4:
                        addNumber("4");
                        break;
                    case Key.D5:
                    case Key.NumPad5:
                        addNumber("5");
                        break;
                    case Key.D6:
                    case Key.NumPad6:
                        addNumber("6");
                        break;
                    case Key.D7:
                    case Key.NumPad7:
                        addNumber("7");
                        break;
                    case Key.D8:
                    case Key.NumPad8:
                        addNumber("8");
                        break;
                    case Key.D9:
                    case Key.NumPad9:
                        addNumber("9");
                        break;
                }
            }
            // check for comma
            if(e.Key == Key.OemComma || e.Key == Key.Decimal)
            {
                addComma();
            }
            // check for options
            if(e.Key == Key.Divide || e.Key == Key.OemQuestion || e.Key == Key.Multiply || e.Key == Key.Subtract || e.Key == Key.OemMinus || e.Key == Key.Add || e.Key == Key.OemMinus)
            {
                switch(e.Key)
                {
                    case Key.Divide:
                    case Key.OemQuestion:
                        setOption(calcOptions.Divide);
                        break;
                    case Key.Multiply:
                        setOption(calcOptions.Multiply);
                        break;
                    case Key.Subtract:
                    case Key.OemMinus:
                        setOption(calcOptions.Subtract);
                        break;
                    case Key.Add:
                    case Key.OemPlus:
                        setOption(calcOptions.Add);
                        break;
                }
            }
        }
    }
}
