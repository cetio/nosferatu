using Nosferatu.Elements.Inherits;

namespace Nosferatu.Elements
{
    public class Button : Textile
    {
        [HTMLName("disabled")]
        public bool Disabled { get; set; }
    }
}
