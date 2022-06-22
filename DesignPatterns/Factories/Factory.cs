using System.Diagnostics;

namespace Factories; 

public class Factory {
    // To comply with the Separation of Concerns and Single Responsibility Principle, we can separate the factory methods.
    // However, the constructor is "PUBLIC" now. 
    public static class PointFactory {
        public static Point NewCartesianPoint(double x, double y) {
            return new Point(x, y);
        }
        
        public static Point NewPolarPoint(double rho, double theta) {
            return new Point(rho * Math.Cos(theta), rho * Math.Sin(theta));
        }
    }
    public class Point {
        private double x, y;

        public Point(double x, double y) {
            this.x = x;
            this.y = y;
        }


        public override string ToString() {
            return $"{nameof(x)}: {x}, {nameof(y)}: {y}";
        }
    }

    public static void main() {
        var point = PointFactory.NewPolarPoint(1.0, Math.PI/2);
        Console.WriteLine(point);
        
        // Now, default constructor can be accessed directly
        var point2 = new Point(1, 2);
    }
}