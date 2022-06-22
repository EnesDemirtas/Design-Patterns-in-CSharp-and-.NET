namespace Factories;

public class WithoutFactory {
    // Sometimes constructor for building an object is not helpful
    // Check the simple example below

    public class Point {
        private double x, y;
        
        // Constructor for Cartesian system
        
        public Point(double x, double y) {
            this.x = x;
            this.y = y;
        }
        
        // Constructor for Polar Coordinate system
        /*
        public Point(double rho, double theta) {
            this.x = rho;
            this.y = theta;
        }
        */
        
        // The problem is that we're not allowed to use same signatures on Constructors
        // Let's consider the approach below
        // This approach is not healthy, violates O of SOLID, and maybe cause "optional parameter hell". 
        
        /// <summary>
        /// Initializes a point from either Cartesian or Polar 
        /// </summary>
        /// <param name="a">x if Cartesian, rho if Polar</param>
        /// <param name="b">y if Cartesian, theta if Polar</param>
        /// <param name="system">Coordinate system</param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public Point(double a, double b, CoordinateSystem system = CoordinateSystem.Cartesian) {
            switch (system) {
                case CoordinateSystem.Cartesian:
                    x = a;
                    y = b;
                    break;
                case CoordinateSystem.Polar:
                    x = a * Math.Cos(b);
                    y = a * Math.Sin(b);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(system), system, null);
            }
        }
    }

    public enum CoordinateSystem {
        Cartesian,
        Polar
    }
}