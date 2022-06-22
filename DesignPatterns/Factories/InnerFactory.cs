using System.Diagnostics;

namespace Factories; 

public class InnerFactory {
    
    // The problem is that our Constructor was public

    // Converting the identifier from public to internal provides that it allows the use of the constructor within the 
    // current assembly, but doesn't allow the use of the constructor within anybody who's actually consuming 
    // that assembly from the outside. 
    
    // If you want to have this particular constraint right inside your own assembly, the approach is to allow the factory
    // to somehow access a private constructor. The only way point factory can access the private constructor is 
    // point factory should be an inner class of point. 
    public class Point {
        private double x, y;

        private Point(double x, double y) {
            this.x = x;
            this.y = y;
        }


        public override string ToString() {
            return $"{nameof(x)}: {x}, {nameof(y)}: {y}";
        }

        public static Point Origin => new Point(0, 0); // Property
        public static Point Origin2 = new Point(0, 0); // Field, this approach is better, it initialize once and you use this single instance everywhere 
        
        public static class Factory {
            public static Point NewCartesianPoint(double x, double y) {
                return new Point(x, y);
            }
        
            public static Point NewPolarPoint(double rho, double theta) {
                return new Point(rho * Math.Cos(theta), rho * Math.Sin(theta));
            }
        }
    }

    public static void main() {
        var point = Point.Factory.NewPolarPoint(1.0, Math.PI/2);
        Console.WriteLine(point);

        var origin = Point.Origin; // Property
    }
}