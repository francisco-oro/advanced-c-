using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DynamicProgramming
{
    internal class DynamicXmlNode : DynamicObject
    {
        private XElement node;

        public DynamicXmlNode(XElement node)
        {
            this.node = node;
        }

        public override bool TryGetMember(GetMemberBinder binder, out object? result)
        {
            var element = node.Element(binder.Name);
            if (element != null)
            {
                result = new DynamicXmlNode(element);
                return true;
            }

            else
            {
                var attribute = node.Attribute(binder.Name);
                if (attribute != null)
                {
                    result = attribute.Value;
                    return true;
                }

                else
                {
                    result = null;
                    return false;
                }
            }
        }
    }

    internal class DynamicForXMLParsingDemo
    {
        public static void Main(string[] args)
        {
            var xml = @"
<people>
    <person name='Francisco' />
</people>
";

            var node = XElement.Parse(xml);
            var name = node.Element("person")?.Attribute("name");
            Console.WriteLine(name?.Value);

            //x.person.name
            dynamic dyn = new DynamicXmlNode(node);
            Console.WriteLine(dyn.person.name.ToString());
        }
    }
}
