using System.Xml.Linq;

namespace Lab2.Containers
{
    internal class StorageContainer
    {
        public XElement Storage { get; set; }

        public int TotalProducts { get; set; }

        public decimal TotalCost { get; set; }
    }
}
