using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CodingTaskWindowsForms
{
    public class BusinessLogic
    {
        private string _errorString = "Invalid input";

        public string errorString { get { return _errorString; } }

        /// <summary>
        /// Gets the result of the operations provided.
        /// </summary>
        /// <param name="equation">The equation as a string.</param>
        /// <returns>The result of the equation.</returns>
        public string GetResult(string equation)
        {
            equation = equation.ToLower().Replace(" ", string.Empty).Replace("+-", "-");//.Replace("--", "-");

            int posSqrt = equation.IndexOf("sqrt"),
                posPow = equation.IndexOf("^"),
                posDiv = equation.IndexOf("/"),
                posMult = equation.IndexOf("*"),
                posMinus = equation.IndexOf("-", 1),
                posPlus = equation.IndexOf("+", 1),
                posOperator = -1;
            double number1,
                number2;
            string currentOperation = string.Empty,
                currentResult = string.Empty;

            if (posSqrt > -1)
            {
                string sqrtOriginal = equation.Substring(
                                        posSqrt + 5, // due to the 'sqrt' word and parenthesis
                                        equation.IndexOf(")", posSqrt) - (posSqrt + 5));

                if (!double.TryParse(sqrtOriginal, out number1))
                {
                    return errorString;
                }

                number1 = Math.Sqrt(number1);

                equation = equation.Substring(0, posSqrt) +
                            number1 +
                            equation.Substring(equation.IndexOf(")", posSqrt) + 1);

                return GetResult(equation);
            }


            if (posPow > 0)
            {
                currentOperation = "^";
                posOperator = posPow;
            }
            else if (posDiv > 0)
            {
                currentOperation = "/";
                posOperator = posDiv;
            }
            else if (posMult > 0)
            {
                currentOperation = "*";
                posOperator = posMult;
            }
            else if (posMinus > 0)
            {
                currentOperation = "-";
                posOperator = posMinus;
            }
            else
            {
                if (posPlus > 0)
                {
                    currentOperation = "+";
                    posOperator = posPlus;
                }
                else
                {
                    equation = equation.Replace("+", "");
                }
            }

            if (currentOperation == string.Empty)
            {
                if (Regex.IsMatch(equation, @"^-*[0-9,\.]+$"))
                {
                    return equation;
                }
                else
                {
                    return errorString;
                }
            }
            else
            {
                currentResult = GetNumbers(out number1, out number2, posOperator, equation);

                if (currentResult == string.Empty)
                {
                    return errorString;
                }

                equation = equation.Replace(number1.ToString() + currentOperation + number2.ToString(), (currentResult).ToString());

                return GetResult(equation);
            }
        }

        /// <summary>
        /// Gets the numbers around an operator and the result of the operation.
        /// </summary>
        /// <param name="number1">Variable to store the number to the left.</param>
        /// <param name="number2">Variable to store the number to the right.</param>
        /// <param name="posOperator">Index of the operator in the string.</param>
        /// <param name="equation">The string containing the equation.</param>
        /// <returns>The result of the operation as a string.</returns>
        private string GetNumbers(out double number1, out double number2, int posOperator, string equation)
        {
            bool dotFound = false;
            string number = string.Empty;
            char nextPosition = ' ';
            string currentOperator = equation.Substring(posOperator, 1);

            for (int index = posOperator - 1; index >= 0; index--)
            {
                nextPosition = equation.ElementAt(index);

                if (char.IsNumber(nextPosition) ||
                    (nextPosition == '.' && !dotFound) ||
                    nextPosition == '-' ||
                    nextPosition == '+')
                {
                    if (nextPosition == '.' && !dotFound)
                    {
                        dotFound = true;
                    }

                    number = nextPosition + number;

                    if (nextPosition == '-' || nextPosition == '+')
                    {
                        break;
                    }
                }
                else
                {
                    break;
                }
            }

            if (!double.TryParse(number, out number1))
            {
                number2 = 0;

                return string.Empty;
            }

            dotFound = false;
            number = string.Empty;
            nextPosition = ' ';

            for (int index = posOperator + 1; index < equation.Length; index++)
            {
                nextPosition = equation.ElementAt(index);

                if (char.IsNumber(nextPosition) ||
                (nextPosition == '.' && !dotFound) ||
                ((nextPosition == '-' || nextPosition == '+') && index == posOperator + 1))
                {
                    if (nextPosition == '.' && !dotFound)
                    {
                        dotFound = true;
                    }

                    number = number + nextPosition;
                }
                else
                {
                    break;
                }
            }

            if (!double.TryParse(number, out number2))
            {
                return string.Empty;
            }

            switch (currentOperator)
            {
                case "^":
                    return Math.Pow(number1, number2).ToString();
                case "/":
                    return (number1 / number2).ToString();
                case "*":
                    return (number1 * number2).ToString();
                case "-":
                    return (number1 - number2).ToString();
                case "+":
                    return (number1 + number2).ToString();
            }

            return string.Empty;
        }
    }
}
