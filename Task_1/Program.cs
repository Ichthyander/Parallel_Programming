using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*1.    Сформировать массив случайных целых чисел (размер  задается пользователем). 
 * Вычислить сумму чисел массива и максимальное число в массиве.  
 * Реализовать  решение  задачи  с  использованием  механизма  задач продолжения.*/

namespace Task_1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введите размерность массива");
            int size = Convert.ToInt32(Console.ReadLine());

            //Формирование массива
            Func<object, int[]> func1 = new Func<object, int[]>(GetArray);
            Task<int[]> task1 = new Task<int[]>(func1, size);
         
            //Нахождение суммы элементов массива
            Func<Task<int[]>, int> func2 = new Func<Task<int[]>, int>(GetSum);
            Task<int> task2 = task1.ContinueWith(func2);

            //Нахождение максимумального элемента массива
            Func<Task<int[]>, int> func3 = new Func<Task<int[]>, int>(GetMax);
            Task<int> task3 = task1.ContinueWith(func3);

            task1.Start();

            //task1.Wait();
            Print(task1);

            //task2.Wait();
            int sum = task2.Result;
            
            //task3.Wait();
            int max = task3.Result;

            Console.WriteLine("Сумма элементов в массиве - {0}\nМаксимальное значение - {1}", sum, max);

            Console.ReadKey();
        }

        static int[] GetArray(object objSize)
        {
            int size = (int)objSize;
            Random random = new Random();
            int[] array = new int[size];
            for (int i = 0; i < size; i++)
            {
                array[i] = random.Next(-100, 100);
            }
            return array;
        }

        static int GetSum(Task<int[]> task)
        {
            int[] array = task.Result;
            int sum = 0;
            foreach (int i in array)
            {
                sum += i;
            }
            return sum;
        }

        static int GetMax(Task<int[]> task)
        {
            int[] array = task.Result;
            int max = -1000000;
            foreach (int i in array)
            {
                max = max < i ? i : max;
            }
            return max;
        }

        static void Print(Task<int[]> task)
        {
            int[] array = task.Result;
            Console.WriteLine("Исходный массив");
            foreach (int element in array)
            {
                Console.Write($"{element} ");
            }
            Console.WriteLine();
        }
    }
}
