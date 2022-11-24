using Nosferatu.Elements.Inherits;

namespace Nosferatu.Elements
{
    public class Text : Textile
    {
        [HTMLName("href")]
        public string? Hyperlink { get; set; }

        [HTMLName("datetime")]
        public DateTime? DateTime { get; set; }
    }
}
