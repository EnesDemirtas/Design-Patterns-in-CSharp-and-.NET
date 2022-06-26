using System.Collections;
using System.Collections.ObjectModel;
using MoreLinq;

namespace Adapter;

// As mentioned in VectorRasterDemo.cs, the problem of adapter design is unnecessary computing the same values
// To get rid of this, we'll use caching here
public class AdapterCaching {
    public class Point {
        public int X, Y;

        public Point(int x, int y) {
            X = x;
            Y = y;
        }

        protected bool Equals(Point other) {
            return X == other.X && Y == other.Y;
        }

        public override bool Equals(object? obj) {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Point)obj);
        }

        public override int GetHashCode() {
            return HashCode.Combine(X, Y);
        }
    }
    
    
    public class Line {
        public Point Start, End;

        public Line(Point start, Point end) {
            Start = start;
            End = end;
        }

        protected bool Equals(Line other) {
            return Start.Equals(other.Start) && End.Equals(other.End);
        }

        public override bool Equals(object? obj) {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Line)obj);
        }

        public override int GetHashCode() {
            return HashCode.Combine(Start, End);
        }
    }

    public class VectorObject : Collection<Line> {
        
    }

    public class VectorRectangle : VectorObject {
        public VectorRectangle(int x, int y, int width, int height) {
            Add(new Line(new Point(x, y), new Point(x + width, y)));
            Add(new Line(new Point(x + width, y), new Point(x + width, y + height)));
            Add(new Line(new Point(x, y), new Point(x, y + height)));
            Add(new Line(new Point(x, y + height), new Point(x + width, y + height)));
        }
    }

    public class LineToPointAdapter : IEnumerable<Point> {
        private static int _count;

        private int linehash;
        // Cache
        // The key is the hash code of the Line
        // GetHashCode methods of Line and Point will give us the hash code
        private static Dictionary<int, List<Point>> _cache = new();

        public LineToPointAdapter(Line line) {
            var hash = line.GetHashCode();
            linehash = hash;
            if (_cache.ContainsKey(hash)) return;

            Console.WriteLine($"{++_count}: Generating points for line [{line.Start.X},{line.Start.Y}]-[{line.End.X},{line.End.Y}]");

            var points = new List<Point>();
            
            int left = Math.Min(line.Start.X, line.End.X);
            int right = Math.Max(line.Start.X, line.End.X);
            int top = Math.Min(line.Start.Y, line.End.Y);
            int bottom = Math.Max(line.Start.Y, line.End.Y);
            int dx = right - left;
            int dy = line.End.Y - line.Start.Y;

            if (dx == 0)
            {
                for (int y = top; y <= bottom; ++y)
                {
                    points.Add(new Point(left, y));
                }
            } else if (dy == 0)
            {
                for (int x = left; x <= right; ++x)
                {
                    points.Add(new Point(x, top));
                }
            }
            
            _cache.Add(hash, points);
        }

        public IEnumerator<Point> GetEnumerator() {
            return _cache[linehash].GetEnumerator();
            // return _cache.Values.SelectMany(x => x).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return GetEnumerator();
        }
    }

    private static readonly List<VectorObject> _vectorObjects = new List<VectorObject> {
        new VectorRectangle(1, 1, 10, 10),
        new VectorRectangle(3, 3, 6, 6)
    };
    
    public static void DrawPoint(Point p) {
        Console.Write(".");
    }

    private static void Draw() {
        foreach (var vo in _vectorObjects) {
            foreach (var line in vo) {
                var adapter = new LineToPointAdapter(line);
                adapter.ForEach(DrawPoint);
            }
        }
    }
    
    public static void main() {
        Draw();
        Draw();
    }
}