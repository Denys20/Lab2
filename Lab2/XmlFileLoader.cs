using System;
using System.Xml;

namespace Lab2
{
    internal class XmlFileLoader
    {
        // product.xml
        // storage.xml
        // manufacturer.xml
        // productManufacturer.xml

        public XmlDocument ProductXml { get; private set; }

        public XmlDocument StorageXml { get; private set; }

        public XmlDocument ManufacturerXml { get; private set; }

        public XmlDocument ProductManufacturerXml { get; private set; }

        public XmlFileLoader()
        {
            ProductXml = new XmlDocument();
            StorageXml = new XmlDocument();
            ManufacturerXml = new XmlDocument();
            ProductManufacturerXml = new XmlDocument();
        }
        
        public void LoadXmlFiles()
        {
            ProductXml.Load("product.xml");
            StorageXml.Load("storage.xml");
            ManufacturerXml.Load("manufacturer.xml");
            ProductManufacturerXml.Load("productManufacturer.xml");
        }

        public void ConsoleWriteXmlFileContent(XmlDocument xmlDoc)
        {
            if (xmlDoc != null)
            {
                XmlNode root = xmlDoc.DocumentElement;
                if (root.HasChildNodes)
                {
                    for (int i = 0; i < root.ChildNodes.Count; i++)
                    {
                        Console.WriteLine(root.ChildNodes[i].OuterXml);
                    }
                }
            }
        }

        public void DisplayXmlFilesContent()
        {
            Console.WriteLine("Products");
            ConsoleWriteXmlFileContent(ProductXml);
            Console.WriteLine();

            Console.WriteLine("Storages");
            ConsoleWriteXmlFileContent(StorageXml);
            Console.WriteLine();

            Console.WriteLine("Manufacturers");
            ConsoleWriteXmlFileContent(ManufacturerXml);
            Console.WriteLine();

            Console.WriteLine("ProductManufacturers");
            ConsoleWriteXmlFileContent(ProductManufacturerXml);
            Console.WriteLine();
        }
    }
}
