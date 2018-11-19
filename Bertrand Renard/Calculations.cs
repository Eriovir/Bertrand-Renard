using System;
using System.Collections.Generic;

namespace Bertrand_Renard
{
    public static class Calculations
    {
        private static List<double> List_Without_Digit(List<double> list, double i) // Returns a list without i
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

        private static void Find_Combi(List<double> digits, double nb, double current, string result, ref List<string> output_list) // Fill output_list with all solutions
        {
            // Percentage(calls, max_calls);
            if (digits.Count != 0)
            {
                string combi = "";
                string res;
                foreach (double i in digits)
                {
                    List<double> otherdigits = List_Without_Digit(digits, i);
                    if (current + i == nb)
                    {
                        res = result + " + " + i.ToString();
                        output_list.Add(res);
                    }
                    if (current - i == nb)
                    {
                        res = result + " - " + i.ToString();
                        output_list.Add(res);
                    }
                    if (current * i == nb)
                    {
                        res = result + " * " + i.ToString();
                        output_list.Add(res);
                    }
                    if (i != 0 && current / i == Math.Truncate(current / i) && current / i == nb)
                    {
                        res = result + " / " + i.ToString();
                        output_list.Add(res);
                    }
                    if (otherdigits.Count != 0)
                    {
                        Find_Combi(otherdigits, nb, current + i, "(" + result + ") + " + i.ToString(), ref output_list);
                        Find_Combi(otherdigits, nb, current - i, "(" + result + ") - " + i.ToString(), ref output_list);
                        Find_Combi(otherdigits, nb, current * i, "(" + result + ") * " + i.ToString(), ref output_list);
                        Find_Combi(otherdigits, nb, current + i, "(" + result + ") / " + i.ToString(), ref output_list);
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
            List<string> result = new List<string>();
            // List<int> maxs = new List<int> { 1, 17, 409, 13089, 523561, 25130929 };
            int cou = alldigits.Count;
            Find_Combi(alldigits, tofind, start, "", ref result); // Fill result with solutions
            if (result.Count != 0)
            {
                int j = 0;
                int pos = 0;
                int min = result[0].Length;
                while (j < result.Count) // Determine shortest solution
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
            return "Il n'y a pas de solution.";
        }

        private static string Format_Solution(string solution) // Get solution to be more readable
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
                while (z < solution.Length)
                {
                    if (solution[z] == '(')
                        open_par++;
                    if (solution[z] == ')')
                        close_par++;
                    z++;
                }
                for (int m = 0; m < open_par - close_par; m++) // Remove excessive open parenthesis
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

        private static void Percentage(double current, double max) // Displays % but multiplies time by 120
        {
            Console.CursorLeft = 0;
            Console.Write(Math.Round((current/max * 100)).ToString() + "%");
        }
    }
}
