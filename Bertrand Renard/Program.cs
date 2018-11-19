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
            Predict_Time();
            Stopwatch sw = new Stopwatch();
            bool exit = false;
            bool keep_list = false;

            while (!exit)
            {
                string solution;

                to_find = 0;
                Get_to_find(); // Get objective

                if (!keep_list)
                {
                    digits = new List<double>();
                    Get_Digits();  // Fill digits list
                }

                double time = a * Math.Pow(b, digits.Count) / 1000;
                if (time > 1)
                    Console.WriteLine("Temps nécessaire : moins de {0} secondes", Math.Truncate(time).ToString());
                else
                    Console.WriteLine("Temps nécessaire : environ {0} millisecondes", (time * 1000).ToString());
                sw.Restart();

                solution = Calculations.Solution(to_find, 0, digits);
                Console.WriteLine();
                Console.WriteLine(solution + " -> temps : " + sw.ElapsedMilliseconds.ToString() + "ms");
                Console.WriteLine();

                Console.WriteLine("Pour fermer appyer sur q, pour recommencer appuyer sur une autre touche, appuyer sur k pour conserver la liste");
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
                Console.WriteLine("Quel chiffre Bertrand-Renardiser ?");
                try
                {
                    to_find = double.Parse(Console.ReadLine());
                    has_chosen = true;
                }
                catch (Exception e)
                {
                    Console.WriteLine("Mets un bon chiffre ??");
                }
            }
        }

        static void Get_Digits()
        {
            string digit = "";
            while (true)
            {
                Console.WriteLine("Quel chiffre ajouter à la liste pour Bertrand-Renardiser (supporte le format x, y, z ...) ? mets y si c'est fini");
                Console.Write("Chiffres actuels : ");
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
                            Console.WriteLine("Il n'y a aucun chiffre dans la liste.");
                        }
                    }
                    else if (digit.Contains(","))
                    {
                        int j = 0;
                        while (j < digit.Length) // Add format being like 3, 7, 9, 2...
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
                    Console.WriteLine("Mets un bon chiffre ??");
                }
            }
        }

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

            b = time2 / time1;
            a = time2 / Math.Pow(b, 5) * 0.3;
        }
    }
}
