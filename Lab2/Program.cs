using System;
using System.Text;

namespace Lab2
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            Console.WriteLine("LINQ to XML\n");

            Lists lists = new Lists();
            Console.WriteLine("Виберіть джерело даних:");
            Console.WriteLine("1 - Дані за замовчуванням, 2 - Дані ввести з клавіатури");
            string answer;
            do
            {
                answer = Console.ReadLine();
                switch (answer)
                {
                    case "1":
                        lists.InitializeDefault();
                        break;
                    case "2":
                        lists.InitializeKeyboard();
                        break;
                    default:
                        Console.WriteLine("Неправильний вибір. Спробуйте ще раз:");
                        break;
                }
            } while (answer != "1" && answer != "2");

            lists.WriteToXmlFiles();

            Console.WriteLine("Вміст створених XML файлів:");
            XmlFileLoader loader = new XmlFileLoader();
            loader.LoadXmlFiles();
            loader.DisplayXmlFilesContent();

            Query query = new Query();
            query.ExecuteQueries();

            Console.WriteLine("\nНатисніть будь-яку клавішу, щоб вийти...");
            Console.ReadKey();
        }
    }
}