using System.Text;

namespace Builder; 

public class Builder {
    public class HtmlElement {
        public string Name, Text;
        public List<HtmlElement> Elements = new();
        private const int IndentSize = 2;

        public HtmlElement() {
            
        }

        public HtmlElement(string name, string text) {
            Name = name;
            Text = text;
        }

        private string ToStringImpl(int indent) {
            var sb = new StringBuilder();
            var i = new string(' ', IndentSize * indent);
            sb.AppendLine($"{i}<{Name}>");

            if (!string.IsNullOrWhiteSpace(Text)) {
                sb.Append(new string(' ', IndentSize * (indent + 1)));
                sb.AppendLine(Text);
            }

            foreach (var e in Elements) {
                sb.Append(e.ToStringImpl(indent + 1));
            }

            sb.AppendLine($"{i}</{Name}>");
            return sb.ToString();
        }

        public override string ToString() {
            return ToStringImpl(0);
        }
    }

    public class HtmlBuilder {
        private readonly string rootName;
        HtmlElement root = new();

        public HtmlBuilder(string rootName) {
            this.rootName = rootName;
            root.Name = rootName;
        }
        
        // If AddChild returns HtmlBuilder again, this is called Fluent Interface or fluent builder
        public void AddChild(string childName, string childText) {
            var e = new HtmlElement(childName, childText);
            root.Elements.Add(e);
            // return this;
        }

        public override string ToString() {
            return root.ToString();
        }

        public void Clear() {
            root = new HtmlElement {Name = rootName};
        }
    }

    public static void Main() {
        var builder = new HtmlBuilder("ul");
        builder.AddChild("li", "Hello");
        builder.AddChild("li", "World");
        // builder.AddChild("li", "Hello").AddChild("li", "World");  Fluent Builder/Interface
        Console.WriteLine(builder.ToString());
    }
}