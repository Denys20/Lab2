using System.Collections.Generic;
using System.Xml;

namespace Lab2
{
    internal class XmlFileCreator
    {
        // product.xml
        // storage.xml
        // manufacturer.xml
        // productManufacturer.xml

        public void CreateProductXmlFile(List<Product> products)
        {
            XmlWriterSettings settings = new XmlWriterSettings() { Indent = true };
            using (XmlWriter writer = XmlWriter.Create("product.xml", settings))
            {
                writer.WriteStartElement("products");
                foreach (var item in products)
                {
                    writer.WriteStartElement("product");
                    writer.WriteAttributeString("id", item.ProductId.ToString());
                    writer.WriteElementString("name", item.Name);
                    writer.WriteElementString("cost", item.Cost.ToString());
                    writer.WriteElementString("quantity", item.Quantity.ToString());
                    writer.WriteElementString("storageId", item.StorageId.ToString());
                    writer.WriteStartElement("datesArrival");
                    foreach (var date in item.DatesArrival)
                    {
                        writer.WriteElementString("date", date.ToShortDateString());
                    }
                    writer.WriteEndElement();
                    writer.WriteEndElement();
                }
                writer.WriteEndElement();
            }
        }

        public void CreateStorageXmlFile(List<Storage> storages)
        {
            XmlWriterSettings settings = new XmlWriterSettings() { Indent = true };
            using (XmlWriter writer = XmlWriter.Create("storage.xml", settings))
            {
                writer.WriteStartElement("storages");
                foreach (var item in storages)
                {
                    writer.WriteStartElement("storage");
                    writer.WriteAttributeString("id", item.StorageId.ToString());
                    writer.WriteElementString("name", item.Name);
                    writer.WriteStartElement("products");
                    foreach (var product in item.Products)
                    {
                        writer.WriteElementString("productId", product.ProductId.ToString());
                    }
                    writer.WriteEndElement();
                    writer.WriteEndElement();
                }
                writer.WriteEndElement();
            }
        }

        public void CreateManufacturerXmlFile(List<Manufacturer> manufacturers)
        {
            XmlWriterSettings settings = new XmlWriterSettings() { Indent = true };
            using (XmlWriter writer = XmlWriter.Create("manufacturer.xml", settings))
            {
                writer.WriteStartElement("manufacturers");
                foreach (var item in manufacturers)
                {
                    writer.WriteStartElement("manufacturer");
                    writer.WriteAttributeString("id", item.ManufacturerId.ToString());
                    writer.WriteElementString("name", item.Name);
                    writer.WriteEndElement();
                }
                writer.WriteEndElement();
            }
        }

        public void CreateProductManufacturerXmlFile(List<ProductManufacturer> productManufacturers)
        {
            XmlWriterSettings settings = new XmlWriterSettings() { Indent = true };
            using (XmlWriter writer = XmlWriter.Create("productManufacturer.xml", settings))
            {
                writer.WriteStartElement("productManufacturers");
                foreach (var item in productManufacturers)
                {
                    writer.WriteStartElement("productManufacturer");
                    writer.WriteElementString("productId", item.ProductId.ToString());
                    writer.WriteElementString("manufacturerId", item.ManufacturerId.ToString());
                    writer.WriteEndElement();
                }
                writer.WriteEndElement();
            }
        }

    }
}
