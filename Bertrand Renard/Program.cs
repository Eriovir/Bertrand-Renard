using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Bertrand_Renard
{
    class Program
    {
        public static double to_find;
        static List<double> digits = new List<double>();
        static double a; // For ab^x
        static double b;
        static void Main(string[] args)
        {
            // Do some rapid calculations to determine the computer capacities
            Predict_Time();

            Stopwatch sw = new Stopwatch();
            bool exit = false;
            bool keep_list = false;

            while (!exit)
            {
                string solution;

                to_find = 0;
                Get_to_find(); // Get number to find

                if (!keep_list)
                {
                    digits = new List<double>();
                    Get_Digits();  // Fill digits list
                }

                double time = a * Math.Pow(b, digits.Count) / 1000;
                if (time > 1)
                    Console.WriteLine("Needed time : less than {0} seconds", Math.Truncate(time).ToString());
                else
                    Console.WriteLine("Needed time : approximately {0} milliseconds", (time * 1000).ToString());
                sw.Restart();

                solution = Calculations.Solution(to_find, 0, digits);
                Console.WriteLine();
                Console.WriteLine(solution + " -> time : " + sw.ElapsedMilliseconds.ToString() + "ms");
                Console.WriteLine();
                
                Console.WriteLine("To quit press q, press k to keep on with the same list, any other key to restart.");
                string exit_string = Console.ReadKey().Key.ToString();
                if (exit_string != "Q")
                {
                    keep_list = false;
                    if (exit_string == "K")
                    {
                        keep_list = true;
                    }
                    Console.Clear();
                }
                else
                {
                    exit = true;
                }
            }
        }

        static void Get_to_find()
        {
            bool has_chosen = false;
            while (!has_chosen)
            {
                Console.WriteLine("What number do you want to find ?");
                try
                {
                    to_find = double.Parse(Console.ReadLine());
                    has_chosen = true;
                }
                catch (Exception e)
                {
                    Console.WriteLine("Please enter a correct number.");
                }
            }
        }

        static void Get_Digits()
        {
            string digit = "";
            while (true)
            {
                Console.WriteLine("What number do you want to add to the list ? Enter Y if there are no more.");
                Console.Write("Current numbers : ");
                int cou = digits.Count;
                if (cou > 0)
                {
                    for (int i = 0; i < cou - 1; i++)
                    {
                        Console.Write(digits[i].ToString() + ", ");
                    }
                    Console.Write(digits[cou - 1].ToString());
                    Console.WriteLine();
                }
                try
                {
                    digit = Console.ReadLine();
                    if (digit.ToLower() == "y")
                    {
                        if (digits.Count > 0)
                        {
                            break;
                        }
                        else
                        {
                            Console.WriteLine("The list is empty.");
                        }
                    }
                    else if (digit.Contains(","))
                    {
                        int j = 0;

                        // Detect format being like 3, 7, 9, 2...
                        while (j < digit.Length)
                        {
                            string current_digit = "";
                            while (j < digit.Length && digit[j] >= 48 && digit[j] <= 57)
                            {
                                current_digit += digit[j];
                                j++;
                            }
                            if (current_digit.Length > 0)
                            {
                                digits.Add(double.Parse(current_digit));
                            }
                            j++;
                        }
                    }
                    else
                    {
                        digits.Add(double.Parse(digit));
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Please enter a correct number.");
                }
            }
        }

        // This function is the result of a calculation
        // It was found using this website https://sciencing.com/exponential-equation-two-points-8117999.html
        static void Predict_Time()
        {
            Stopwatch st = new Stopwatch();
            List<string> list_time_string = new List<string>();
            List<double> list_time_float = new List<double>() { 1, 1, 1, 1 };
            double time1;
            double time2;

            st.Start();
            Calculations.Solution(1, 0, list_time_float);
            time1 = st.ElapsedMilliseconds;

            list_time_float.Add(1);

            st.Restart();
            Calculations.Solution(1, 0, list_time_float);
            time2 = st.ElapsedMilliseconds;

            // Coefficients of a function of form ab^x
            b = time2 / time1;
            a = time2 / Math.Pow(b, list_time_float.Count) * 0.25;  // 0.25 is the correct number
                                                                    // However I do not know why
        }
    }
}
