namespace Factories; 

public class FactoryMethod {
    public class Point {
        private double x, y;
        // Constructor became "PRIVATE"
        // It is no longer available to external use
        private Point(double x, double y) {
            this.x = x;
            this.y = y;
        }
        // Factory Method
        // Name of the factory method is not directly tied to the name of the containing class
        public static Point NewCartesianPoint(double x, double y) {
            return new Point(x, y);
        }
        
        // Factory Method
        public static Point NewPolarPoint(double rho, double theta) {
            return new Point(rho * Math.Cos(theta), rho * Math.Sin(theta));
        }

        public override string ToString() {
            return $"{nameof(x)}: {x}, {nameof(y)}: {y}";
        }
    }
    public static void main() {
        var point = Point.NewPolarPoint(1.0, Math.PI/2);
        var point2 = Point.NewCartesianPoint(3, 5);
        
        Console.WriteLine(point);
        Console.WriteLine(point2);
    }

}
