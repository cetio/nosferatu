using Nosferatu.Elements.Inherits;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nosferatu.Elements
{
    public class HTML : Element
    {
        [HTMLName("nosContent")]
        public string? Title { get; set; }
    }
}
