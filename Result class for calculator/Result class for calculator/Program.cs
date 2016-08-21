using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Result_class_for_calculator
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("write in me string");
            string Line;//основная строка (которую вводим мы)
            Line = Console.ReadLine();
            //mathematic_line math = new mathematic_line();
            //Console.WriteLine("Answer = " + math.Result(Line));
            string[] mass = Line.Split(new char[] { ' ', '\n', '\t' },StringSplitOptions.RemoveEmptyEntries); 
            for (int i=0;i< mass.Length;i++)
            {
                Console.Write("{0}) - {1} \n", i, mass[i]);
                //Line += mass[i];
            }
            Console.WriteLine(Line);
            Console.ReadKey();
        }
        public bool LineValidat (string str,ref string Cstr)//проверка коректности ввода
        {
            if (str.IndexOf(' ')>0||str.IndexOf('\n')>0|| str.IndexOf('\t') > 0)
            {
                string[] mass = str.Split(new char[] { ' ', '\n', '\t' });
            }
            return true;
        }
    }
    class mathematic_line// класс для расчета сирокового уравнения
    {
        public string Result(string str)
        {
            string Answer = "";
            string Numbers;
            bool check = false;
            Console.WriteLine(str);
            string[] num = str.Split(new char[] { '+', '-', '/', '*' }, StringSplitOptions.RemoveEmptyEntries);// стераем все лишнее кроме цыафер
            if (num.Length > 1)//проверка не состоит ли наша очищеная строчка из одного числа
            {
                if ((str.IndexOf("*")) > -1)//проверка на наличие в строке умножения
                {
                    for (int i = 0; i < num.Length - 1; i++)
                    {
                        string g = num[i] + "*" + num[i + 1];//строчка умножкния
                        if (str.IndexOf(g) > -1)
                        {
                            Answer = calc("*", num[i], num[i + 1], false);
                            str = str.Replace(g, rev(Answer));
                            str = Result(str);
                            break;
                        }
                    }
                }
                if ((str.IndexOf("/")) > -1)
                {
                    for (int i = 0; i < num.Length - 1; i++)
                    {
                        string g = num[i] + "/" + num[i + 1];//строчка умножкния
                        if (str.IndexOf(g) > -1)
                        {
                            Answer = calc("/", num[i], num[i + 1], false);
                            str = str.Replace(g, Answer);
                            str = Result(str);
                        }
                    }
                }
                if ((str.IndexOf("+")) > -1)
                {
                    for (int i = 0; i < num.Length - 1; i++)
                    {
                        string g = num[i] + "+" + num[i + 1];//строчка умножкния
                        if (str.IndexOf(g) > -1)
                        {
                            Answer = calc("+", num[i], num[i + 1], false);
                            str = str.Replace(g, rev(Answer));
                            str = Result(str);
                        }
                    }
                }
                if ((str.IndexOf("-")) > -1)
                {
                    for (int i = 0; i < num.Length - 1; i++)
                    {
                        string g = num[i] + "-" + num[i + 1];//строчка умножкния
                        if (str.IndexOf(g) > -1)
                        {
                            Answer = calc("-", num[i], num[i + 1], false);
                            str = str.Replace(g, rev(Answer));
                            str = Result(str);
                        }
                    }
                }

            }
            return str;
        }
        
        private string calc(string action, string num1, string num2, bool divisibility)
        {
            int SomeTemp = 0;//переменная добавления в следующий разряд
            string answer = "";
            int Smtmvr;//переменная остачи при операциях 
            if (action == "*")
            {
                string[] CalcVariable = new string[num1.Length];
                for (int n1 = num1.Length - 1; n1 > -1; n1--)
                {
                    for (int i = 0; i < num1.Length - n1 - 1; i++)
                        CalcVariable[n1] += "0";
                    SomeTemp = 0;
                    for (int n2 = num2.Length - 1; n2 > -1; n2--)
                    {
                        Smtmvr = ((Convert.ToInt32(num1[n1]) - 48) * (Convert.ToInt32(num2[n2]) - 48)) + SomeTemp;
                        CalcVariable[n1] += Convert.ToString(Smtmvr % 10);
                        SomeTemp = Smtmvr / 10;
                        if (n2 == 0 && SomeTemp > 0)
                            CalcVariable[n1] += SomeTemp;
                    }
                }
                answer = CalcVariable[0];
                for (int ClVr = 1; ClVr < CalcVariable.Length; ClVr++)
                {
                    if (answer.Length > CalcVariable[ClVr].Length)
                    {
                        int test = answer.Length - CalcVariable[ClVr].Length;
                        for (int i = 0; i < test; i++)
                            CalcVariable[ClVr] += "0";
                    }
                    else if (answer.Length < CalcVariable[ClVr].Length)
                    {
                        int test = CalcVariable[ClVr].Length - answer.Length;
                        for (int i = 0; i < test; i++)
                            answer += "0";
                    }
                    SomeTemp = 0;
                    CalcVariable[0] = "";
                    for (int i = 0; i < answer.Length; i++)
                    {
                        Smtmvr = ((Convert.ToInt32(answer[i]) - 48) + (Convert.ToInt32(CalcVariable[ClVr][i]) - 48)) + SomeTemp;
                        CalcVariable[0] += Convert.ToString(Smtmvr % 10);
                        SomeTemp = Smtmvr / 10;
                        if (i == answer.Length - 1 && SomeTemp > 0)
                            CalcVariable[0] += SomeTemp;
                    }
                    answer = CalcVariable[0];
                }
            }
            //30 |3000
            //
            //
            else if (action == "/")
            {
                int VarDev = 0, Lnum1 = num1.Length, Lnum2 = num2.Length;
                string VarNum2 = num2, TVN = "0";
                if (Lnum1 < Lnum2)
                {
                    int test = Lnum2 - Lnum1;
                    for (int i = 0; i < test; i++)
                    {
                        num1 = "0" + num1;
                        answer += answer == "" ? "0." : "0";
                    }
                }
                for (int i = 0; i < Lnum1 - Lnum2; i++)
                {
                    num2 += "0";
                }
                while (true)
                {
                    VarDev = 0;
                    while (!Compare(VarNum2, num1))
                    {
                        VarDev++;
                        TVN = VarNum2;
                        VarNum2 = calc("*", VarNum2, "" + VarDev, false);
                    }

                    num1 = calc("-", num1, TVN, false);
                    VarDev--;
                    answer += (answer == "" && VarDev == 0) ? "0." : "" + VarDev;
                    if (calc("-", num1, TVN, false) == "0") break;
                }
            }
            else if (action == "+")
            {
                if (num1.Length > num2.Length)
                {
                    int test = num1.Length - num2.Length;
                    for (int i = 0; i < test; i++)
                        num2 = "0" + num2;
                }
                else if (num1.Length < num2.Length)
                {
                    int test = num2.Length - num1.Length;
                    for (int i = 0; i < test; i++)
                        num1 = "0" + num1;
                }
                for (int i = num1.Length - 1; i > -1; i--)
                {
                    Smtmvr = ((Convert.ToInt32(num1[i]) - 48) + (Convert.ToInt32(num2[i]) - 48)) + SomeTemp;
                    answer += Convert.ToString(Smtmvr % 10);
                    SomeTemp = Smtmvr / 10;
                    if (i == 0 && SomeTemp > 0)
                        answer += SomeTemp;
                }
            }
            else if (action == "-")
            {
                int a = num1.Length, b = num2.Length;
                string znak = "";
                if (a > b)
                {
                    int test = a - b;
                    for (int i = 0; i < test; i++)
                        num2 = "0" + num2;
                }
                else
                {
                    int test = b - a;
                    for (int i = 0; i < test; i++)
                        num1 = "0" + num1;
                    if (!Compare(num1, num2))
                    {
                        string ffo = num1;
                        num1 = num2;
                        num2 = ffo;
                        znak = "-";
                    }
                }
                for (int i = num1.Length - 1; i > -1; i--)
                {
                    Smtmvr = (Convert.ToInt32(num1[i]) - 48) - (Convert.ToInt32(num2[i]) - 48) + SomeTemp;
                    if (Smtmvr < 0)
                        SomeTemp = -1;
                    else
                        SomeTemp = 0;
                    answer += (Smtmvr < 0 ? (10 + Smtmvr) : Smtmvr);
                }
                int QuaOfNull = 0;
                for (int i = answer.Length - 1; i > -1; i--)
                {
                    if (answer[i] != '0')
                        break;
                    QuaOfNull++;
                }
                if (QuaOfNull != 0)
                    answer = answer.Remove(answer.Length - QuaOfNull);
                answer += znak;
                if (answer == "" || answer == "-")
                    answer = "0";
            }
            return answer;
        }
        private string rev(string str)
        {
            char[] arr = str.ToCharArray();
            Array.Reverse(arr);
            return new string(arr);
        }
        private bool Compare(string a, string b)
        {
            if (a.Length > b.Length)
                return true;
            else if (a.Length < b.Length)
                return false;
            for (int i = 0; i < a.Length; i++)
            {
                if ((Convert.ToInt32(a[i]) - 48) > (Convert.ToInt32(b[i]) - 48))
                    return true;
                else if ((Convert.ToInt32(a[i]) - 48) < (Convert.ToInt32(b[i]) - 48))
                    return false;
            }
            return true;
        }
        //string
        //point
        //\
        //±


        //-
        //+
        //*
    }
}
