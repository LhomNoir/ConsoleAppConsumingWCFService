using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MyWcfService.Helpers
{
    public static class ListExtensions
    {
        public static string IdsToXml(this IEnumerable<int> ids)
        {
            var idList = ids.ToList();
            if (!idList.Any())
                return new XElement("Ids").ToString();

            var xmlElements = new XElement("Ids", idList.Select(i => new XElement("x", new XAttribute("i", i))));
            return xmlElements.ToString();
        }

        public static string ToXml<T>(this T items)
        {
            return Serializer.SerializeObject(items);
        }
    }
}
