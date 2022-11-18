using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nosferatu.Elements.Inherits
{
    public class Element
    {
        protected Point Position;
        protected Size Size;
        protected string? Name;
        protected Element? Parent;
        protected List<Element>? Children;
    }
}
