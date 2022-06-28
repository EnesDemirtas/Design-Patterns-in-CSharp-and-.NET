using System.Text;

namespace Composite;

// Drawing example, have several shapes and grouping them
public class GeometricShapes {
    public class GraphicObject {
        // Using the root class to act as a group for any kind of member, which is part of that group
        // So that we can drag them together
        public virtual string Name { get; set; } = "Group";
        public string Color;

        private Lazy<List<GraphicObject>> children = new Lazy<List<GraphicObject>>();
        public List<GraphicObject> Children => children.Value; // To expose the children
    
        // We need to specify the depth, because there may be several nested groups 
        private void Print(StringBuilder sb, int depth) {
            sb.Append(new string('*', depth))
                .Append(string.IsNullOrWhiteSpace(Color) ? string.Empty : $"{Color} ")
                .AppendLine(Name);

            foreach (var child in Children) {
                child.Print(sb, depth+1);
            }
        }
        
        public override string ToString() {
            var sb = new StringBuilder();  
            Print(sb, 0);
            return sb.ToString();
        }
    }

    public class Circle : GraphicObject {
        public override string Name => "Circle";
    }

    public class Square : GraphicObject {
        public override string Name => "Square";
    }

    public static void main() {
        var drawing = new GraphicObject {Name = "My Drawing"};
        drawing.Children.Add(new Square {Color = "Blue"});
        drawing.Children.Add(new Circle {Color = "Green"});

        var group = new GraphicObject{Name = "My Yellow Shapes Group"};
        group.Children.Add(new Circle {Color = "Yellow"});
        group.Children.Add(new Square {Color = "Yellow"});
        
        drawing.Children.Add(group);
        
        Console.WriteLine(drawing);
    }
}