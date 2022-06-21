using System.Text;

namespace Builder;

public class WithoutBuilder {
    public static void main() {
        var hi = "Hello";
        var sb = new StringBuilder();
        sb.Append("<p>");
        sb.Append(hi);
        sb.Append("</p>");
        Console.WriteLine(sb);

        var words = new[] { "Hello", "World", "!"};
        sb.Clear();
        sb.Append("<ul>");
        foreach (var word in words) {
            sb.Append($"<li>{word}</li>");
        }
        sb.Append("</ul>");
        Console.WriteLine(sb);
    }
}