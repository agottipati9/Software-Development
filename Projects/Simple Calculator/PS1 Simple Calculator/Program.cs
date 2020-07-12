using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PS1_Simple_Calculator
{
    class Program
    {
        static void Main(string[] args)
        {

        }

    }

    /**
     * Represents a simple calculator that can compute the total cost, sales tax, and tip of an item.
     */
    class Calculator
    {

        /**
         * Adds two numbers together and returns the sum.
         */
        public double add(double x, double y)
        {
            return x + y;
        }

        /**
         * Subtracts two numbers returns the difference.
         */
        public double subtract(double x, double y)
        {
            return x - y;
        }

        /**
         * Multiplys two numbers and returns the product.
         */
        public double multiply(double x, double y)
        {
            return x * y;
        }

        /**
         * Divides two numbers and returns the quotient.
         * If the divisor is equivalent to 0, then a DivideByZeroException will be thrown.
         */
        public double divide(double x, double y)
        {
            if(y == 0)
            {
                throw new DivideByZeroException("Cannot divide by zero.");
            }

            return x / y;
        }

        /**
         * Raises the first parameter to the second parameter and returns the value.
         */
        public double exponent(double x, double y)
        {
            return Math.Pow(x, y);
        }

        /**
         * Computes the square root of the input. If input is negative,
         * then an InvalidOperation Exception is thrown.
         */
        public double sqrt(double x)
        {
            if (x < 0)
                throw new InvalidOperationException("Quotient cannot be imaginary.");

            return Math.Sqrt(x);
        }

        /**
         * Computes the yth root of x. If the power is even and the base is negative, then
         * an Invalid Operation Exception is thrown.
         */
        public double root(double x, double y)
        {
            if((y % 2 == 0) && (x < 0))
                throw new InvalidOperationException("Quotient cannot be imaginary.");

            return Math.Pow(x, 1 / y);
        }

        /**
         * Computes the absolute value of the input.
         */
        public double absoluteVal(double x)
        {
            return Math.Abs(x);
        }

        /**
         * Computes the sales tax given the cost and the tax percentage.
         */
        public double salesTax(double cost, double tax_percent)
        {
            return cost * tax_percent;
        }

        /**
         * Computes the tip given the cost and the tip percentage.
         */
        public double tip(double cost, double tip_percent)
        {
            return cost * tip_percent;
        }

        /**
         * Computes the total cost given the cost and tax percentage.
         */
        public double totalCost(double cost, double tax_percent)
        {
            double tax = salesTax(cost, tax_percent);

            return cost + tax;
        }

        /**
         * Computes the total cost given the cost, tax percentage, and tip percentage.
         */
        public double totalCost(double cost, double tax_percent, double tip_percent)
        {
            double tax = salesTax(cost, tax_percent);
            double tip_ = tip(cost, tip_percent);

            return cost + tax + tip_;
        }
    }

}
