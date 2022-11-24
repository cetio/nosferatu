using Nosferatu.Elements;
using Nosferatu.Elements.Inherits;
using Nosferatu.Elements.List;
using System.Drawing;
using System.Reflection;
using System.Reflection.Metadata;
using Image = Nosferatu.Elements.Image;

namespace Nosferatu
{
    public class ParseElements
    {
        public static Dictionary<string, Element> ElementKey = new Dictionary<string, Element>()
        {
            { "p", new Text() },
            { "a", new Text() },
            { "button", new Button() },
            { "details", new Details() },
            { "div", new Div() },
            { "img", new Image() },
            { "ul", new List(HeaderType.Unordered) },
            { "ol", new List(HeaderType.Ordered) },
        };

        // i know this code is fucking disgusting, I'll refactor & improve performance eventually (:
        public static HTML Build(string html)
        {
            HTML htmlElement = new HTML();
            List<string> tagAttrPairs = new List<string>();

            while (true)
            {
                int firstCloser = html.IndexOf('>') - 1;
                string initialTag = html.Substring(1, firstCloser);
                html = html.Substring(firstCloser + 2);

                if (html.Length == 0)
                    break;

                string expectedContent = html.Substring(0, html.IndexOf('<'));
                html = html.Substring(html.IndexOf('<'));

                if (initialTag.StartsWith("/") == false)
                    tagAttrPairs.Add(initialTag + $@" nosContent=""{expectedContent}""");
            }

            foreach (string tagAttrPair in tagAttrPairs)
            {
                string attributes = tagAttrPair.Substring(tagAttrPair.IndexOf(' ') + 1);
                string tag = tagAttrPair.Substring(0, tagAttrPair.IndexOf(' '));
                int attributeCount = attributes.Count(x => x == '"') / 2;
                Element element = htmlElement;

                if (tag == "head" || tag == "base" || tag == "body" || tag.StartsWith("!DOC"))
                    continue;
                else if (tag != "title" && tag != "html")
                    element = ElementKey[tag];


                while (attributeCount != 0)
                {
                    int quoteIndex = attributes.IndexOf('"');
                    string content = attributes.Substring(quoteIndex + 1, attributes.IndexOf('"', quoteIndex + 1) - quoteIndex - 1);
                    string attribute = attributes.Substring(0, quoteIndex - 1);

                    if (attributeCount > 1)
                        attributes = attributes.Substring(attributes.IndexOf('"', quoteIndex + 1) + 2);

                    foreach (PropertyInfo property in element.GetType().GetProperties())
                    {
                        if (property.CustomAttributes.Count() == 0 || 
                            property.CustomAttributes.First().Constructor.DeclaringType
                                                     .ToString() != "Nosferatu.Elements.Inherits.HTMLName")
                            continue;

                        System.Collections.ObjectModel.ReadOnlyCollection<CustomAttributeTypedArgument> attrData = 
                            (System.Collections.ObjectModel.ReadOnlyCollection<CustomAttributeTypedArgument>)property.CustomAttributes.First().ConstructorArguments.First().Value;

                        foreach (object attr in attrData)
                        {
                            if (attr.ToString().Replace(@"""", "") == attribute)
                                property.SetValue(element, ConvertAttrType(content, property.PropertyType));
                        }
                    }

                    attributeCount--;
                }

                if (element != htmlElement)
                    htmlElement.Children.Add(element);
            }

            return htmlElement;
        }

        private static dynamic ConvertAttrType(string data, Type T)
        {
            if (T == typeof(string))
                return data;
            else if (T == typeof(int))
                return Convert.ToInt32(data);
            else if (T == typeof(bool))
                return Convert.ToBoolean(data);
            else if (T == typeof(float))
                return Convert.ToSingle(data);
            else if (T == typeof(double))
                return Convert.ToDouble(data);
            else if (T == typeof(Color))
            {
                if (data.StartsWith('#'))
                    return Color.FromArgb(Convert.ToInt32(data, 16));
                else if (data.StartsWith("rgb"))
                {
                    data = data.Replace("rgb(", "").Replace(")", "");
                    int[] rgb = data.Split(",").Select(c => int.Parse(c)).ToArray();
                    return Color.FromArgb(rgb[0], rgb[1], rgb[2]);
                }
                else
                {

                }
            }

            // this should never happen
            return null;
        }
    }
}
