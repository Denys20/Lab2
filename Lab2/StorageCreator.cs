using System;
using System.Collections.Generic;

namespace Lab2
{
    internal class StorageCreator
    {
        Storage Create(int id)
        {
            Storage storage = new Storage();
            storage.StorageId = id;

            Console.WriteLine("Введіть назву:");
            storage.Name = Input.GetName();

            return storage;
        }

        public List<Storage> GetListStorage()
        {
            List<Storage> storages = new List<Storage>();

            Console.WriteLine("Введіть кількість складів:");
            int number = Input.GetNumber(0, 100);

            for (int i = 1; i <= number; i++)
            {
                Console.WriteLine("Введіть новий склад:");
                storages.Add(Create(i));
                Console.WriteLine();
            }

            return storages;
        }
    }
}
