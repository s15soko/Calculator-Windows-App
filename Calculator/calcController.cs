using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Calculator
{
    public class CalculatorController
    {
        private double sum;

        // Class constructor
        public CalculatorController()
        {
            // set initial value to total
            this.sum = 0.0f;
        }


        // +
        public void addValue(double value)
        {
            this.sum += value;
        }
        // -
        public void subtractValue(double value)
        {
            this.sum -= value;
        }
        // *
        public void multiplyValue(double value)
        {
            this.sum *= value;
        }
        // /
        public void divideValue(double value)
        {
            this.sum /= value;
        }
        // x^2
        public void powerValue(double value)
        {
            this.sum = value*value;
        }
        // /x
        public void elementValue(double value)
        {
            this.sum = Math.Sqrt(value);
        }


        // set sum
        public void setSum(double get_sum)
        {
            this.sum = get_sum;
        }
        // return sum
        public double returnSum()
        {
            return this.sum;
        }
        // show sum (console)
        public void showSum()
        {
            Console.WriteLine("Calc Sum: " + this.sum);
        }
        // set zero
        public void setZero()
        {
            this.sum = 0;
        }
    }
}
