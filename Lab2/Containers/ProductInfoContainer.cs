using System.Collections.Generic;
using System.Xml.Linq;

namespace Lab2.Containers
{
    internal class ProductInfoContainer
    {
        public XElement Product { get; set; }

        public List<XElement> Manufacturers { get; set; }

        public XElement Storage { get; set; }
    }
}
