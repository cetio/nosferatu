using Nosferatu.Elements.Inherits;

namespace Nosferatu.Elements
{
    public class Details : Container
    {
        [HTMLName("summary")]
        public string Summary = "Details";
        [HTMLName("open")]
        public bool Open { get; set; }
    }
}
