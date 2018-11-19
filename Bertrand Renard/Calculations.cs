using System;
using System.Collections.Generic;

namespace Bertrand_Renard
{
    public static class Calculations
    {
        // Returns a list without i
        private static List<double> List_Without_Digit(List<double> list, double i)
        {
            List<double> without_i = new List<double>();
            int without_i_length = list.Count;
            int j = 0;

            while (j < without_i_length && list[j] != i)
            {
                without_i.Add(list[j]);
                j++;
            }

            j++;

            while (j < without_i_length)
            {
                without_i.Add(list[j]);
                j++;
            }
            return without_i;
        }

		// Fill output_list with all solutions
        private static void Find_Combi(List<double> digits, double nb, double current, string result, ref List<string> output_list)
        {
            if (digits.Count != 0)
            {
                string combi = "";
                string res;
                foreach (double i in digits)
                {
                    List<double> otherdigits = List_Without_Digit(digits, i);
                    string i_to_string = i.ToString();

                    // If the number is equal to the one we are seeking
                    // Add the string "result" to the list
                    if (current + i == nb)
                    {
                        res = result + " + " + i_to_string;
                        output_list.Add(res);
                    }
                    if (current - i == nb)
                    {
                        res = result + " - " + i_to_string;
                        output_list.Add(res);
                    }
                    if (current * i == nb)
                    {
                        res = result + " * " + i_to_string;
                        output_list.Add(res);
                    }
                    if (i != 0 && current / i == Math.Truncate(current / i) && current / i == nb)
                    {
                        res = result + " / " + i_to_string;
                        output_list.Add(res);
                    }

                    // Call the function on every operator and complete the result string
                    if (otherdigits.Count != 0)
                    {
                        Find_Combi(otherdigits, nb, current + i, "(" + result + ") + " + i_to_string, ref output_list);
                        Find_Combi(otherdigits, nb, current - i, "(" + result + ") - " + i_to_string, ref output_list);
                        Find_Combi(otherdigits, nb, current * i, "(" + result + ") * " + i_to_string, ref output_list);
                        Find_Combi(otherdigits, nb, current + i, "(" + result + ") / " + i_to_string, ref output_list);
                        Find_Combi(otherdigits, nb - current, 0, result + ")", ref output_list);
                        Find_Combi(otherdigits, nb - current, 0, result + ")", ref output_list);
                        Find_Combi(otherdigits, nb - current, 0, result + ")", ref output_list);
                        Find_Combi(otherdigits, nb - current, 0, result + ")", ref output_list);
                    }
                }
            }
        }

        public static string Solution(double tofind, double start, List<double> alldigits) // Get solution
        {
            // List<int> maxs = new List<int> { 1, 17, 409, 13089, 523561, 25130929 };
            // Found experimentally, these are the maximal number of times Find_Combi() can be called 

            // List which will be filled with solution strings
            List<string> result = new List<string>();
            int cou = alldigits.Count;

            // Fill result with solutions
            Find_Combi(alldigits, tofind, start, "", ref result);
            if (result.Count != 0)
            {
                int j = 0;
                int pos = 0;
                int min = result[0].Length;

                // Determine shortest solution
                while (j < result.Count) 
                {
                    string curr = result[j];
                    if (curr.Length < min)
                    {
                        pos = j;
                        min = curr.Length;
                    }
                    j++;
                }

                string solution = result[pos];
                return Format_Solution(solution);
            }
            return "There is no solution.";
        }


        // Get solution to be more readable
        private static string Format_Solution(string solution)
        {
            bool changed = true;
            string solution_changed;
            while (changed)
            {
                changed = false;
                solution_changed = solution.Replace("()", "");
                if (solution != solution_changed)
                {
                    solution = solution_changed;
                    changed = true;
                }
                int open_par = 0;
                int close_par = 0;
                int z = 0;

                // Count every parenthesis
                while (z < solution.Length)
                {
                    if (solution[z] == '(')
                        open_par++;
                    if (solution[z] == ')')
                        close_par++;
                    z++;
                }

                // Remove excessive open parenthesis
                for (int m = 0; m < open_par - close_par; m++)
                {
                    changed = true;
                    if (solution[0] == '(')
                    {
                        solution = solution.Substring(1);
                    }
                }
            }
            return solution;
        }

        // Displays % but increases time by  a factor 120
        // Therefore it is not used in the function
        // In order to use it, a ref long has to be passed as an argument of Find_Combi()
        // This long has to be incremented in every loop
        // Max is the maximal number of times Find_Combi() can be called
        // Max can be determined by hand, I did it and stored it in a commented list in Solution()
        private static void Percentage(double current, double max)
        {
            Console.CursorLeft = 0;
            Console.Write(Math.Round((current/max * 100)).ToString() + "%");
        }
    }
}
