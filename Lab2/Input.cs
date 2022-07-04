using System;

namespace Lab2
{
    internal class Input
    {
        public static string GetName()
        {
            string name = Console.ReadLine();

            while (string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("Неправильне введення. Спробуйте ще раз:");
                name = Console.ReadLine();
            }

            return name;
        }

        public static DateTime GetDate(DateTime lower, DateTime upper)
        {
            string answer;
            DateTime date;
            bool incorrectInput;

            do
            {
                incorrectInput = true;
                answer = Console.ReadLine();
                if (DateTime.TryParse(answer, out date) && date > lower && date <= upper)
                    incorrectInput = false;
                else
                    Console.WriteLine("Неправильне введення. Спробуйте ще раз:");
            } while (incorrectInput);

            return date;
        }

        public static int GetNumber(int lower, int upper)
        {
            string answer;
            int number;
            bool incorrectInput;

            do
            {
                incorrectInput = true;
                answer = Console.ReadLine();
                if (int.TryParse(answer, out number) && number > lower && number < upper)
                    incorrectInput = false;
                else
                    Console.WriteLine("Неправильне введення. Спробуйте ще раз:");
            } while (incorrectInput);

            return number;
        }

        public static decimal GetNumber(decimal lower, decimal upper)
        {
            string answer;
            decimal number;
            bool incorrectInput;

            do
            {
                incorrectInput = true;
                answer = Console.ReadLine();
                if (decimal.TryParse(answer, out number) && number > lower && number < upper)
                    incorrectInput = false;
                else
                    Console.WriteLine("Неправильне введення. Спробуйте ще раз:");
            } while (incorrectInput);

            return number;
        }


    }
}
