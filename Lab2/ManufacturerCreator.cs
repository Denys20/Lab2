using System;
using System.Collections.Generic;

namespace Lab2
{
    internal class ManufacturerCreator
    {
        Manufacturer Create(int id)
        {
            Manufacturer manufacturer = new Manufacturer();
            manufacturer.ManufacturerId = id;

            Console.WriteLine("Введіть назву:");
            manufacturer.Name = Input.GetName();

            return manufacturer;
        }

        public List<Manufacturer> GetListManufacturer()
        {
            List<Manufacturer> manufacturers = new List<Manufacturer>();

            Console.WriteLine("Введіть кількість виробників:");
            int number = Input.GetNumber(0, 100);

            for (int i = 1; i <= number; i++)
            {
                Console.WriteLine("Введіть нового виробника:");
                manufacturers.Add(Create(i));
                Console.WriteLine();
            }

            return manufacturers;
        }
    }
}
