namespace Nosferatu.Elements.List
{
    public class Item : Text
    {
        public bool Description { get; set; }
        public string Header { get; set; } = "x.";
        public int Index { get; set; }

        public Item()
        {
            List _parent = (List)Parent;
            Header = _parent.Type;
            Index += _parent.Start;
        }
    }
}
