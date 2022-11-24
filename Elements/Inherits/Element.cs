using System.Drawing;
using System.Numerics;

namespace Nosferatu.Elements.Inherits
{
    public class Element
    {
        public static List<string> GlobalAttributeNames = new List<string>();

        [HTMLName("id")]
        public string? Name { get; set; }

        [HTMLName("class")]
        public string? Class { get; set; }

        [HTMLName("width")]
        public int Width { get; set; }

        [HTMLName("height")]
        public int Height { get; set; }

        [HTMLName("center")]
        public bool Center { get; set; }

        [HTMLName("left")]
        public bool Left { get; set; }

        [HTMLName("right")]
        public bool Right { get; set; }

        [HTMLName("justify")]
        public bool Justify { get; set; }

        [HTMLName("bgcolor")]
        public Color BackgroundColor { get; set; }

        public Vector4 Margin { get; set; } = new Vector4();
        public Vector4 Padding { get; set; } = new Vector4();
        public Vector2 Position { get; set; } = new Vector2();
        public Vector4 Border { get; set; } = new Vector4();
        public Color[] BorderColor { get; set; } = new Color[4];
        public JavaScript? JavaScript { get; set; }
        public Element? Parent { get; set; }
        public List<Element> Children { get; set; } = new List<Element>();
    }
}
