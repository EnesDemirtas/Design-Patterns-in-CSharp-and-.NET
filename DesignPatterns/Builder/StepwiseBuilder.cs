namespace Builder; 

public class StepwiseBuilder {
    // How to get the builder to perform a set of steps one after another in a specific order 
    // Assume that depending on the car type, you can only select a certain wheel size
    // It means that first we obtain the car type and then a size between appropriate wheel sizes depends on the car type
    // To achieve that, we'll use Interface Segregation Principle!
    public enum CarType {
        Sedan,
        Crossover
    }

    public class Car {
        public CarType Type;
        public int WheelSize;
    }

    public interface ISpecifyCarType {
        ISpecifyWheelSize OfType(CarType type);
    }

    public interface ISpecifyWheelSize {
        IBuildCar WithWheels(int size);
    }

    public interface IBuildCar {
        public Car Build();
    }

    public class CarBuilder {
        private class Impl : ISpecifyCarType, ISpecifyWheelSize, IBuildCar {
            private Car _car = new();
            public ISpecifyWheelSize OfType(CarType type) {
                _car.Type = type;
                return this;
            }

            public IBuildCar WithWheels(int size) {
                switch (_car.Type) {
                    case CarType.Crossover when size < 17 || size > 20:
                    case CarType.Sedan when size < 15 || size > 17:
                        throw new ArgumentException($"Wrong size of wheel for {_car.Type}.");
                }

                _car.WheelSize = size;
                return this;
            }

            public Car Build() {
                return _car;
            }
        }

        public static ISpecifyCarType Create() {
            return new Impl();
        }
    }

    public static void Main(string[] args) {
        var car = CarBuilder.Create()
            .OfType(CarType.Sedan)
            .WithWheels(14)
            .Build();
    }
}