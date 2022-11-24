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
            List<string> tags = new List<string>();

            int index = html.IndexOf('<');
            int endIndex = html.IndexOf('>', index + 1);
            string tag = html.Substring(index + 1, endIndex - 1);
            html = html.Substring(endIndex + 1, html.Length - endIndex - 1);

            while (true)
            {
                index = html.IndexOf('<');

                if (html[index + 1] == '/')
                {
                    string content = html.Substring(0, index);
                    tags.Add(tag + $@" nosContent=""{content}""");
                }

                endIndex = html.IndexOf('>', index + 1);
                tag = html.Substring(index + 1, endIndex - index - 1);
                html = html.Substring(endIndex + 1, html.Length - endIndex - 1);

                if (html.Length == 0)
                    break;
            }

            foreach (string tag_ in tags)
            {
                string Tcontent = tag_.Substring(tag_.IndexOf(' ') + 1);
                string Theader = tag_.Substring(0, tag_.IndexOf(' '));
                int attributeCount = Tcontent.Count(x => x == '"') / 2;
                Element element = htmlElement;

                if (Theader != "title" && Theader != "html")
                    element = ElementKey[Theader];
                else if (Theader == "head" || Theader == "base" || Theader == "body")
                    break;


                while (attributeCount != 0)
                {
                    int quoteIndex = Tcontent.IndexOf('"');
                    string content = Tcontent.Substring(quoteIndex + 1, Tcontent.Length - Tcontent.IndexOf('"', quoteIndex) - 2);
                    string attribute = Tcontent.Substring(0, quoteIndex - 1);
                   
                    foreach (PropertyInfo property in element.GetType().GetProperties())
                    {
                        if (property.CustomAttributes.Count() == 0 || 
                            property.CustomAttributes.First().Constructor.DeclaringType
                                                     .ToString() != "Nosferatu.Elements.Inherits.HTMLName")
                            continue;

                        System.Collections.ObjectModel.ReadOnlyCollection<CustomAttributeTypedArgument> attrData = 
                            (System.Collections.ObjectModel.ReadOnlyCollection<CustomAttributeTypedArgument>)property.CustomAttributes.First().ConstructorArguments.First().Value;

                        string realAttr = attrData.First().ToString().Replace(@"""", "");

                        if (realAttr == attribute)
                            property.SetValue(element, content);
                    }

                    attributeCount--;
                }

                if (element != htmlElement)
                    htmlElement.Children.Add(element);
            }

            return htmlElement;
        }
    }
}
