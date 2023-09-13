using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace калькулятор_множеств
{
    class Program
    {
            static void Main(string[] args)
            {
                int[] Universum = new int[0];
                int[][] Sets = new int[0][];
                EventHandler(ref Universum, ref Sets);
            }

        static int InputMessage(string tmp, int min, int max)
        {
            int temp;
            bool t;
            do
            {
                Console.WriteLine(tmp);
                string f = Console.ReadLine();
                t = int.TryParse(f, out temp);
                if (!t )
                    Console.WriteLine("Введено не число!");
                else if (temp > max || temp < min)
                    Console.WriteLine("Число не подходит!");
                

            }
            while (!t || temp > max || temp < min);
            return temp;
        }
        static void EventHandler(ref int[] Universum, ref int[][] Sets)
            {
                int x;
            bool UniCreated = false, ArraysCreated = false;
            bool t = true;
                while (t)
                {
                    Console.WriteLine("1 - Создать универсум\n2 - Создать подмножества универсума\n3 - Проверить, находится ли число в одном из подмножеств\n4 - Вывести универсум\n5 - Вывести подмножества\n6 - Действия с множествами\r\n7 - Проверить включено ли множество в другое множество\r\n8 - Очистить консоль\n9 - Выход");
                x = InputMessage("Введите пункт меню: ", 1, 9);
                    switch (x)
                    {
                        case 1: Create_Uni( ref Universum, ref UniCreated); break;
                        case 2: Create_Sets(ref Universum, ref Sets, UniCreated, ref ArraysCreated); break;
                        case 3: CheckIfContains(ref Sets, ArraysCreated); break;
                        case 4: ShowUniversum(Universum, UniCreated); break;
                        case 5: ShowArrays(Sets, ArraysCreated); break;
                        case 6: OperationEventHandler( Universum,  Sets, ArraysCreated); break;
                        case 7: CheckIfOn(Sets, UniCreated, ArraysCreated); break;
                        case 8: Console.Clear(); break;
                        case 9: t = false; break;
                        default: break;
                    }
                }
            }

            static void OperationEventHandler( int[] Universum,  int[][] Sets, bool Created)
            {
                if (!Created)
                {
                    Console.WriteLine("Сначала создайте множества!");
                    return;
                }
                int x;
                Console.WriteLine("1 - Объединение (U)\r\n2 - Пересечение (*)\r\n3 - Разность (\\)\r\n4 - Симметрическая разность (▲)\r\n5 - Дополнение\r\n6 - Назад");
            x = InputMessage("Введите операцию над множествами: ", 1, 6);
                switch (x)
                {
                    case 1: Combining_Sets(Sets); break;
                    case 2: Peresechenie(Sets); break;
                    case 3: Raznost(Sets); break;
                    case 4: SimRaznost(Sets); break;
                    case 5: Dopolnenie(Sets, Universum); break;
                    default: break;
                }
            }

            static void Create_Uni( ref int[] a, ref bool UniCreated)
            {
                int Choise;
                Console.WriteLine("1 - Задать границы универсума самому\r\n2 - Задать границы универсума случайно\r\n");
                Choise = InputMessage("Сделайте выбор: ", 1, 2);
                Console.WriteLine("");

                switch (Choise)
                {
                    case 1: CreateMySelf(ref a); UniCreated = true;  break;
                    case 2: CreateRandomly(ref a); UniCreated = true; break;
                    default: break;
                }

            }
            static void CreateMySelf(ref int[] a)
            {
                int left_border, right_border;

                 left_border = InputMessage("Введите нижнюю границу универсума:\r\n> ", int.MinValue, int.MaxValue);
                right_border = InputMessage("Введите верхнюю границу универсума:\r\n> ", int.MinValue, int.MaxValue);
               if(right_border < left_border)
                {
                Console.WriteLine("Границы были введены неправильно, они меняются местами: ");
                int swap = left_border;
                 left_border= swap;
                right_border = swap;

                }
                Array.Resize<int>(ref a, right_border - left_border + 1);
                for (int i = 0; i < a.Length; i++)
                {
                    a[i] = i + left_border;
                }
            }
            static void CreateRandomly(ref int[] a)
            {
                Random rnd = new Random();
                int n = Math.Abs(rnd.Next(0, 1000));
                int k = Math.Abs(rnd.Next(n, n + 100));
                Array.Resize<int>(ref a, k - n + 1);
                for (int i = 0; i < a.Length; i++)
                {
                    a[i] = i + n;
                }
                Console.WriteLine($"Нижняя граница = {n}\r\nВерхняя граница = {k}\r\n");
            }


        static void Create_Sets(ref int[] uni, ref int[][] Sets, bool UniCreated, ref bool ArraysCreated)
        {
            if (!UniCreated)
            {
                Console.WriteLine("Сначала создайте универсум!");
                return;
            }
            int Choise, n;
           
                n = InputMessage("Сколько множеств создать? (не более 5)", 1, 5);
                Console.WriteLine("1 - Создать множества самому\r\n2 - Создать случайные множества");
                Choise = InputMessage("Сделайте выбор: ", 1, 2);
                switch (Choise)
                {
                    case 1: CreateArraysMySelf(ref uni, n, ref Sets); ArraysCreated = true; break;
                    case 2: CreateArraysRandomly(ref uni, n, ref Sets); ArraysCreated = true; break;
                    default: break;
                }
          
        }

     

        static void CreateArraysMySelf(ref int[] universum, int n, ref int[][] Massivi)
            {
                Massivi = new int[n][];
                for (int i = 0; i < n; i++)
                {
                    int set_lenght;

                    set_lenght = InputMessage($"Введите длину {i + 1}-го множества", 1, universum.Length);
                    Array.Resize<int>(ref Massivi[i], set_lenght);
                    Console.WriteLine($"Введите элементы {i + 1}-го множества");
                    for (int j = 0; j < set_lenght; j++)
                    {
                        int number;
                        do
                        {
                        Console.Write($"Введите {j + 1} элемент {i + 1} множества: ");
                        } while (!int.TryParse(Console.ReadLine(), out number) || !universum.Contains(number) || (Massivi[i].Contains(number)&&j!=0));
                        
                        Massivi[i][j] = number;
                    }
                }
            }
            static void CreateArraysRandomly(ref int[] a, int n, ref int[][] Massivi)
            {
                Massivi = new int[n][];
            Random rand = new Random();
            for (int i = 0; i < n; i++)
                {
                    int k = rand.Next(1, a.Length);
                
                    Array.Resize<int>(ref Massivi[i], k);
                    for (int j = 0; j < k; j++)
                    {
                        int number = rand.Next(0, a.Length);
                        if (!Massivi[i].Contains(a[number]))
                        {
                            Massivi[i][j] = a[number];
                        }
                        else j -= 1;
                    }
                }
            }

            static void CheckIfContains(ref int[][] Sets, bool Created)
            {
                if (!Created)
                {
                    Console.WriteLine("Сначала создайте подмножесва!");
                    return;
                }
               
                int n;
                bool flag = false;
            n = InputMessage("Введите число:", int.MinValue, int.MaxValue);
                for (int i = 0; i < Sets.Length; i++)
                {
                    if (Sets[i].Contains(n))
                    {
                        Console.WriteLine($"Число {n} содержится в {i + 1}-м множестве");
                        flag = true;
                    }
                }
                if (!flag)
                {
                    Console.WriteLine($"Число {n} не содержится ни в одном из множеств");
                }
            }

            static void ShowUniversum(int[] a, bool Created)
            {
                if (!Created)
                {
                    Console.WriteLine("Сначала создайте универсум!");
                    return;
                }
                Console.Write("Универсум: ");
                for (int i = 0; i < a.Length; i++)
                {
                    Console.Write($"{a[i]} ");
                }
                Console.WriteLine();
            }
            static void ShowArrays(int[][] a, bool Created)
            {
                if (!Created)
                {
                    Console.WriteLine("Сначала создайте подмножества!");
                    return;
                }
                Console.WriteLine("Подмножества: ");
                for (int i = 0; i < a.Length; i++)
                {
                    Console.Write($"{i + 1}. ");
                    for (int j = 0; j < a[i].Length; j++)
                    {
                        Console.Write($"{a[i][j]} ");

                    }
                    Console.WriteLine("\r\n");
                }
            }

            static void Combining_Sets(int[][] Sets)
            {
                Console.WriteLine("Введите номера множеств, которые хотите объеденить:");

                int first_set, second_set;
            first_set = InputMessage("Первое множество: ", 1, Sets.Length);
            second_set = InputMessage("Второе множество: ", 1, Sets.Length);
            first_set -= 1;
            second_set -= 1;
                Console.WriteLine("Получившееся множество: ");
                for (int i = 0; i < Sets[first_set].Length; i++)
                {
                    if (!Sets[second_set].Contains(Sets[first_set][i]))
                    {
                        Console.Write($"{Sets[first_set][i]} ");
                    }
                }
                for (int i = 0; i < Sets[second_set].Length; i++)
                {
                    Console.Write($"{Sets[second_set][i]} ");
                }
                Console.WriteLine();
            }
            static void Peresechenie(int[][] Sets)
            {
                Console.WriteLine("Введите номера множеств, которые будут пересекаться:");
            int first_set, second_set;
            first_set = InputMessage("Первое множество: ", 1, Sets.Length);
            second_set = InputMessage("Второе множество: ", 1, Sets.Length);
            first_set -= 1;
            second_set -= 1;
            Console.WriteLine("Получившееся множество: ");
                for (int i = 0; i < Sets[first_set].Length; i++)
                {
                    if (Sets[second_set].Contains(Sets[first_set][i]))
                    {
                        Console.Write($"{Sets[first_set][i]} ");
                    }
                }
                Console.WriteLine();
            }
            static void Raznost(int[][] Sets)
            {
               int first_set, second_set;
            first_set = InputMessage("Введите номер множества из которого будет вычитаться: ", 1, Sets.Length);
            second_set = InputMessage("Введите номер множества которое будет вычитаться: ", 1, Sets.Length);
            first_set -= 1;
            second_set -= 1;
                Console.WriteLine("Получившееся множество: ");
                for (int i = 0; i < Sets[first_set].Length; i++)
                {
                    if (!Sets[second_set].Contains(Sets[first_set][i]))
                    {
                        Console.Write($"{Sets[first_set][i]} ");
                    }
                }
                Console.WriteLine();
            }
            static void SimRaznost(int[][] Sets)
            {
                Console.WriteLine("Введите номера множеств, с которыми будет производиться симметрическое вычитание:");
               
            int first_set, second_set;
            first_set = InputMessage("Первое множество: ", 1, Sets.Length);
            second_set = InputMessage("Второе множество: ", 1, Sets.Length);
            first_set -= 1;
            second_set -= 1;
            Console.WriteLine("Получившееся множество: ");
                for (int i = 0; i < Sets[first_set].Length; i++)
                {
                    if (!Sets[second_set].Contains(Sets[first_set][i]))
                    {
                        Console.Write($"{Sets[first_set][i]} ");
                    }
                }
                for (int i = 0; i < Sets[second_set].Length; i++)
                {
                    if (!Sets[first_set].Contains(Sets[second_set][i]))
                    {
                        Console.Write($"{Sets[second_set][i]} ");
                    }
                }
                Console.WriteLine();
            }
            static void Dopolnenie(int[][] Sets, int[] Universum)
            {
                
                int set;
            set = InputMessage("Введиет номер множества: ", 1, Sets.Length);
                set -= 1;
                Console.WriteLine("Получившееся множество: ");
                for (int i = 0; i < Universum.Length; i++)
                {
                    if (!Sets[set].Contains(Universum[i]))
                    {
                        Console.Write($"{Universum[i]} ");
                    }
                }
                Console.WriteLine();
            }

            static void CheckIfOn(int[][] Sets, bool UniCreated, bool ArraysCreated)
            {
            if (!UniCreated)
            {
                Console.WriteLine("Сначала создайте универсум!");
                return;
            }
            if (!ArraysCreated)
            {
                Console.WriteLine("Сначала создайте подмножесва!");
                return;
            }
            Console.WriteLine("Введите номера множеств, которые хотите проверить:");
            int first_set, second_set, choise;
            first_set = InputMessage("Первое множество: ", 1, Sets.Length);
            second_set = InputMessage("Второе множество: ", 1, Sets.Length);
            first_set -= 1;
            second_set -= 1;
            if (second_set == first_set)
            {
                Console.WriteLine( "Ввели всего одно подмножество, хотите проверить его с самим собой?");
                 choise = InputMessage("1. Да\n2. Нет", 1, 2);
                if (choise == 2)
                    return;
            }
          
           
            if (Sets[first_set].Length > Sets[second_set].Length)
                {
                    for (int i = 0; i < Sets[second_set].Length; i++)
                    {
                        if (!Sets[first_set].Contains(Sets[second_set][i]))
                        {
                            Console.WriteLine("Множество 2 не включено в множество 1");
                            return;
                        }
                    }
                    Console.WriteLine("Множество 2 включено в множество 1");
                }
                else if (Sets[second_set].Length > Sets[first_set].Length)
                {
                    for (int i = 0; i < Sets[first_set].Length; i++)
                    {
                        if (!Sets[second_set].Contains(Sets[first_set][i]))
                        {
                            Console.WriteLine("Множество 1 не включено в множество 2");
                            return;
                        }
                    }
                    Console.WriteLine("Множество 1 включено в множество 2");
                }
                else
                {
                    for (int i = 0; i < Sets[first_set].Length; i++)
                    {
                        if (!Sets[second_set].Contains(Sets[first_set][i]))
                        {
                            Console.WriteLine("Множество 1 не включено в множество 2");
                            return;
                        }
                    }
                    Console.WriteLine("Множество включены друг в друга");
                }
            }
        }
    }

