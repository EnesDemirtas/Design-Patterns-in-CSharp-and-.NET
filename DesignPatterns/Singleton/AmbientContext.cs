using System.Text;

namespace Singleton; 

// Some attributes of a class may act differently depend on the context, check the main method for an example
// To control it, we create a context class and specify this attributes
public class AmbientContext {
    // The solution class
    public sealed class BuildingContext : IDisposable {
        public int WallHeight;
        private static Stack<BuildingContext> _stack = new();

        static BuildingContext() {
            new BuildingContext(0);
        }

        public BuildingContext(int wallHeight) {
            WallHeight = wallHeight;
            _stack.Push(this);
        }

        public static BuildingContext Current => _stack.Peek();

        public void Dispose() {
            if (_stack.Count > 1) {
                _stack.Pop();
            }
        }
    }
    
    public class Building {
        public List<Wall> Walls = new();

        public override string ToString() {
            var sb = new StringBuilder();
            foreach (var wall in Walls) {
                sb.AppendLine(wall.ToString());
            }
            return sb.ToString();
        }
    }
    
    // A struct is a collection of variables of different data types under a single unit.
    // Structures are of value types while classes are of reference types.
    // Simple and limited variant of class
    public struct Point {
        private int x, y;
        public Point(int x, int y) {
            this.x = x;
            this.y = y;
        }

        public override string ToString() {
            return $"{nameof(x)}: {x}, {nameof(y)}: {y}";
        }
    }
    
    public class Wall {
        public Point Start, End;
        public int Height;

        public Wall(Point start, Point end) {
            Start = start;
            End = end;
            Height = BuildingContext.Current.WallHeight;
        }

        public override string ToString() {
            return $"{nameof(Start)}: {Start}, {nameof(End)}: {End}, {nameof(Height)}: {Height}";
        }
    }

    public static void main() {
        var house = new Building();
        
        // Ground floor
        // house.Walls.Add(new Wall(new Point(0,0), new Point(5000, 0), 3000));
        // house.Walls.Add(new Wall(new Point(0,0), new Point(0, 4000), 3000));
        
        // First floor
        // house.Walls.Add(new Wall(new Point(0,0), new Point(6000, 0), 4000));
        // house.Walls.Add(new Wall(new Point(0,0), new Point(0, 4000), 4000));
        
        // Turn back to ground floor to add a new wall
        // house.Walls.Add(new Wall(new Point(5000,0), new Point(5000, 4000), 3000));
        
        // The trick is that for every floor, walls' height are same, but we are specifying it every time 
        // Latest usage is as follows:

        var newHouse = new Building();
        
        // gnd 3000
        using (new BuildingContext(3000)) {
            newHouse.Walls.Add(new Wall(new Point(0,0), new Point(5000, 0)));
            newHouse.Walls.Add(new Wall(new Point(0,0), new Point(0, 4000)));
            // First floor 3500
            using (new BuildingContext(3500)) {
                newHouse.Walls.Add(new Wall(new Point(0,0), new Point(6000, 0)));
                newHouse.Walls.Add(new Wall(new Point(0,0), new Point(0, 4000)));   
            }

            newHouse.Walls.Add(new Wall(new Point(5000,0), new Point(5000, 4000)));
        }
        Console.WriteLine(newHouse);
    }
}