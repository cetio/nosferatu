using System.Drawing;

namespace Nosferatu.Elements.Inherits
{
    public class Textile : Element
    {
        [HTMLName("color")]
        public Color TextColor { get; set; }

        [HTMLName("font")]
        public Font? Font { get; set; }

        [HTMLName("nosContent")]
        public string? Content { get; set; }

        [HTMLName("important", "b")]
        public bool Bold { get; set; }

        [HTMLName("del", "s")]
        public bool Strikeout { get; set; }

        [HTMLName("em", "i")]
        public bool Italic { get; set; }

        [HTMLName("u")]
        public bool Underline { get; set; }
    }
}
