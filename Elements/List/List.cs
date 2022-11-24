using Nosferatu.Elements.Inherits;

namespace Nosferatu.Elements.List
{
    public class List : Container
    {
        public string Type { get; set; }
        public int ItemIndent { get; set; }
        public int Start { get; set; }

        public List(string type)
        {
            Type = type;
        }
    }

    public class HeaderType
    {
        public static string Ordered { get; set; } = "x.";
        public static string Unordered { get; set; } = "•";
        public static string Descriptive { get; set; } = "";
    }
}
