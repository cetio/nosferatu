using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nosferatu.Elements.Inherits
{
    public class HTMLName : Attribute
    {
        public string[] Names { get; set; }

        public HTMLName(params string[] names)
        {
            Names = names;

            foreach (string name in names)
                Element.GlobalAttributeNames.Add(name);
        }
    }
}
