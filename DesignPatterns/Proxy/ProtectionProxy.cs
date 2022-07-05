namespace Proxy;

// Permissions, authentications
public class ProtectionProxy {
    public interface ICar {
        public void Drive();
    }

    public class Car : ICar {
        public void Drive() {
            Console.WriteLine("Car is being driven");
        }
    }

    public class Driver {
        public int Age { get; set; }
    }
    
    // Replicate the API like decorator, but don't add a new member, add a new functionality
    public class CarProxy : ICar {
        private Driver driver;
        private Car car = new();

        public CarProxy(Driver driver) {
            this.driver = driver;
        }

        public void Drive() {
            if (driver.Age >= 16)
                car.Drive();
            else {
                Console.WriteLine("Too young to drive");
            }
        }
    }

    public static void main() {
        ICar car = new CarProxy(new Driver{Age = 16});
        car.Drive();
    }
}