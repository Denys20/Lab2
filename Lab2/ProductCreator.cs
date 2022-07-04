using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab2
{
    internal class ProductCreator
    {
        Product Create(int id, List<Storage> storages, List<Manufacturer> manufacturers, List<ProductManufacturer> productManufacturers)
        {
            Product product = new Product();
            product.ProductId = id;

            Console.WriteLine("Введіть назву:");
            product.Name = Input.GetName();

            Console.WriteLine("Введіть вартість:");
            product.Cost = Input.GetNumber(0m, 1000000000000m);

            Console.WriteLine("Введіть кількість:");
            product.Quantity = Input.GetNumber(0, 1000000);

            Console.WriteLine("Виберіть склад:");
            foreach (var storage in storages)
            {
                Console.Write("{0} - {1} ", storage.StorageId, storage.Name);
            }
            Console.WriteLine();
            product.StorageId = Input.GetNumber(0, storages.Count + 1);

            Console.WriteLine("Введіть дати надходження:");
            product.DatesArrival = new List<DateTime>();
            do
            {
                Console.WriteLine("Введіть дату: dd/mm/yyyy");
                DateTime date = Input.GetDate(DateTime.MinValue, DateTime.Now);
                product.DatesArrival.Add(date);
                Console.WriteLine("0 - далі, 1 - Додати дату");
            } while (Console.ReadLine() != "0");

            Console.WriteLine("Виробники:");
            foreach (var manufacturer in manufacturers)
            {
                Console.Write("{0} - {1} ", manufacturer.ManufacturerId, manufacturer.Name);
            }
            Console.WriteLine();
            do
            {
                Console.WriteLine("Виберіть виробника:");
                int manufacturerId = Input.GetNumber(0, manufacturers.Count + 1);

                ProductManufacturer pm = new ProductManufacturer()
                {
                    ProductId = product.ProductId,
                    ManufacturerId = manufacturerId
                };
                productManufacturers.Add(pm);

                Console.WriteLine("0 - далі, 1 - Додати виробника");
            } while (Console.ReadLine() != "0");

            return product;
        }

        public List<Product> GetListProduct(List<Storage> storages, List<Manufacturer> manufacturers, List<ProductManufacturer> productManufacturers)
        {
            List<Product> products = new List<Product>();
            Console.WriteLine("Введіть кількість товарів:");
            int number = Input.GetNumber(0, 100);

            for (int i = 1; i <= number; i++)
            {
                Console.WriteLine("Введіть новий товар");
                products.Add(Create(i, storages, manufacturers, productManufacturers));
                Console.WriteLine();
            }
            foreach (var item in storages)
            {
                item.Products = products.Where(x => x.StorageId == item.StorageId).ToList();
            }

            return products;
        }
    }
}
