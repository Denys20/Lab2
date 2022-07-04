using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Lab2.Containers;

namespace Lab2
{
    internal class Query
    {
        public readonly XDocument prodManufactXml = XDocument.Load("productManufacturer.xml");

        public readonly XDocument manufacturerXml = XDocument.Load("manufacturer.xml");

        public readonly XDocument productXml = XDocument.Load("product.xml");

        public readonly XDocument storageXml = XDocument.Load("storage.xml");

        public void ExecuteQueries()
        {
            Console.WriteLine("\n1");
            foreach (var item in GetAllManufacturer())
            {
                Console.WriteLine(item.ToString());
            }

            Console.WriteLine("\n2");
            foreach (var item in GetAllProductsSortedDescByCost())
            {
                Console.WriteLine(item.ToString());
            }

            Console.WriteLine("\n3");
            foreach (var item in GetAllProductsSortedByName())
            {
                Console.WriteLine(item.ToString());
            }

            Console.WriteLine("\n4");
            foreach (var item in GetManufacturersNameStartsWith("В"))
            {
                Console.WriteLine(item.ToString());
            }

            Console.WriteLine("\n5");
            foreach (
                var item in GetProductsCostGreaterThan(30m))
            {
                Console.WriteLine(item.ToString());
            }

            Console.WriteLine("\n6");
            Console.WriteLine("Загальна кількість товарів = {0}", GetTotalNumberOfProducts());

            Console.WriteLine("\n7");
            foreach (var item in GetProductsGroupStorage())
            {
                Console.WriteLine("Склад: {0}", item.Key);
                foreach (var product in item.Value)
                {
                    Console.WriteLine("\t{0} - {1} (штук)",
                        product.Element("name").Value, product.Element("quantity").Value);
                }
            }

            Console.WriteLine("\n8");
            foreach (var item in GetProductManufacturer())
            {
                Console.WriteLine("{0} - {1}",
                    item.Product.Element("name").Value, item.Manufacturer.Element("name").Value);
            }

            Console.WriteLine("\n9");
            foreach (var item in GetProductGroupManufacturer())
            {
                Console.WriteLine("{0}", item.Key.Element("name").Value);
                foreach (var product in item.Value)
                {
                    Console.WriteLine("\t{0}", product.Element("name").Value);
                }
            }

            Console.WriteLine("\n10");
            Console.WriteLine("Усі назви:");
            foreach (var item in GetAllSortedNames())
            {
                Console.WriteLine(item);
            }

            Console.WriteLine("\n11");
            foreach (var item in GetFullProductInfo())
            {
                Console.Write("{0} на {1}. Виробники:",
                    item.Product.Element("name").Value, item.Storage.Element("name").Value);
                foreach (var manufacturer in item.Manufacturers)
                {
                    Console.Write(" {0}", manufacturer.Element("name").Value);
                }
                Console.WriteLine();
            }

            Console.WriteLine("\n12");
            foreach (var item in GetStorageTotalProductsAndCost())
            {
                Console.WriteLine("{0}: {1} товарів, загальною вартістю {2}",
                    item.Storage.Element("name").Name, item.TotalProducts, item.TotalCost);
            }
            Console.WriteLine("\n13");
            foreach (var item in GetMostExpensiveProductByManufacturer())
            {
                Console.WriteLine("{0}: \n\tТовар з найвищою вартістю: {1}, {2}",
                    item.Manufacturer.Element("name").Value,
                    item.Product.Element("name").Value,
                    item.Product.Element("cost").Value);
            }
            Console.WriteLine("\n14");
            foreach (var item in GetProductWithMoreThanOneManufacturer())
            {
                Console.WriteLine(item.ToString());
            }

            Console.WriteLine("\n15");
            foreach (var item in GetProductArrivalOnlyThisYear())
            {
                Console.WriteLine("{0}", item.ToString());
            }
        }

        //1
        public IEnumerable<XElement> GetAllManufacturer()
        {
            var query = from m in manufacturerXml.Descendants("manufacturer")
                        select m;
            return query;
        }
        //2
        public IEnumerable<XElement> GetAllProductsSortedDescByCost()
        {
            var query = productXml.Descendants("product")
                .OrderByDescending(p => decimal.Parse(p.Element("cost").Value));
            return query;
        }
        //3
        public IEnumerable<XElement> GetAllProductsSortedByName()
        {
            var query = from p in productXml.Descendants("product")
                        orderby p.Element("name").Value
                        select p;
            return query;
        }
        //4
        public IEnumerable<XElement> GetManufacturersNameStartsWith(string letter)
        {
            var query = manufacturerXml.Descendants("manufacturer")
                .Where(p => p.Element("name").Value.StartsWith(letter));
            return query;
        }
        //5
        public IEnumerable<XElement> GetProductsCostGreaterThan(decimal cost)
        {
            var query = from p in productXml.Descendants("product")
                        where decimal.Parse(p.Element("cost").Value) > cost
                        select p;
            return query;
        }
        //6
        public int GetTotalNumberOfProducts()
        {
            var query = productXml.Descendants("product")
                .Sum(p => int.Parse(p.Element("quantity").Value));
            return query;
        }
        //7
        public Dictionary<int, List<XElement>> GetProductsGroupStorage()
        {
            var query = from p in productXml.Descendants("product")
                        group p by int.Parse(p.Element("storageId").Value);

            return query.ToDictionary(x => x.Key, x => x.ToList());
        }
        //8
        public IEnumerable<ProductManufacturerContainer> GetProductManufacturer()
        {
            var query = from p in productXml.Descendants("product")
                        join pm in prodManufactXml.Descendants("productManufacturer")
                            on int.Parse(p.Attribute("id").Value) equals int.Parse(pm.Element("productId").Value)
                        join m in manufacturerXml.Descendants("manufacturer")
                            on int.Parse(pm.Element("manufacturerId").Value) equals int.Parse(m.Attribute("id").Value)
                        select new ProductManufacturerContainer
                        {
                            Product = p,
                            Manufacturer = m
                        };

            return query;
        }
        //9
        public Dictionary<XElement, List<XElement>> GetProductGroupManufacturer()
        {
            var query = productXml.Descendants("product")
                .Join(prodManufactXml.Descendants("productManufacturer"),
                    p => int.Parse(p.Attribute("id").Value), pm => int.Parse(pm.Element("productId").Value),
                    (p, pm) => new { p, pm })
                .Join(manufacturerXml.Descendants("manufacturer"),
                    t => int.Parse(t.pm.Element("manufacturerId").Value), m => int.Parse(m.Attribute("id").Value),
                    (t, m) => new { t.p, m })
                .GroupBy(x => x.m, x => x.p);

            return query.ToDictionary(x => x.Key, x => x.ToList());
        }
        //10
        public IEnumerable<string> GetAllSortedNames()
        {
            var query = productXml.Descendants("product").Select(p => p.Element("name").Value)
                .Concat(manufacturerXml.Descendants("manufacturer").Select(p => p.Element("name").Value))
                .Concat(storageXml.Descendants("storage").Select(s => s.Element("name").Value))
                .OrderBy(x => x);

            return query;
        }
        //11
        public IEnumerable<ProductInfoContainer> GetFullProductInfo()
        {
            var query = productXml.Descendants("product")
                        .Join(prodManufactXml.Descendants("productManufacturer"),
                            product => int.Parse(product.Attribute("id").Value),
                            link => int.Parse(link.Element("productId").Value),
                            (product, link) => new { product, link })
                        .Join(manufacturerXml.Descendants("manufacturer"),
                            t => int.Parse(t.link.Element("manufacturerId").Value),
                            manufacturer => int.Parse(manufacturer.Attribute("id").Value),
                            (t, manufacturer) => new { t.product, manufacturer })
                        .Join(storageXml.Descendants("storage"),
                            t => int.Parse(t.product.Element("storageId").Value),
                            storage => int.Parse(storage.Attribute("id").Value),
                            (t, storage) => new { t, storage })
                        .GroupBy(x => x.t.product)
                        .Select(x => new ProductInfoContainer
                        {
                            Product = x.Key,
                            Manufacturers = x.Select(m => m.t.manufacturer).ToList(),
                            Storage = x.Select(s => s.storage).FirstOrDefault()
                        });

            return query;
        }
        //12
        public IEnumerable<StorageContainer> GetStorageTotalProductsAndCost()
        {
            var query = from p in productXml.Descendants("product")
                        group p by int.Parse(p.Element("storageId").Value) into partition
                        select new StorageContainer
                        {
                            Storage = storageXml.Descendants("storage")
                                .FirstOrDefault(s => int.Parse(s.Attribute("id").Value) == partition.Key),
                            TotalProducts = partition
                                .Sum(x => int.Parse(x.Element("quantity").Value)),
                            TotalCost = partition
                                .Sum(x => decimal.Parse(x.Element("cost").Value) * int.Parse(x.Element("quantity").Value))
                        };

            return query;
        }
        //13
        public IEnumerable<ProductManufacturerContainer> GetMostExpensiveProductByManufacturer()
        {
            var query = from p in productXml.Descendants("product")
                        join pm in prodManufactXml.Descendants("productManufacturer")
                            on int.Parse(p.Attribute("id").Value) equals int.Parse(pm.Element("productId").Value)
                        group p by int.Parse(pm.Element("manufacturerId").Value) into partition
                        select new ProductManufacturerContainer
                        {
                            Manufacturer = manufacturerXml.Descendants("manufacturer")
                                .FirstOrDefault(m => int.Parse(m.Attribute("id").Value) == partition.Key),
                            Product = partition
                                .OrderBy(x => decimal.Parse(x.Element("cost").Value)).LastOrDefault()
                        };

            return query;
        }
        //14
        public IEnumerable<XElement> GetProductWithMoreThanOneManufacturer()
        {
            var query = from pm in prodManufactXml.Descendants("productManufacturer")
                        group pm by int.Parse(pm.Element("productId").Value) into partition
                        where partition.Count() > 1
                        from p in productXml.Descendants("product")
                        where int.Parse(p.Attribute("id").Value) == partition.Key
                        select p;

            return query;
        }
        //15
        public IEnumerable<XElement> GetProductArrivalOnlyThisYear()
        {
            var query = from p in productXml.Descendants("product")
                        where p.Descendants("datesArrival")
                            .All(d => DateTime.Parse(d.Element("date").Value).Year == DateTime.Now.Year)
                        select p;

            return query;
        }
    }
}
