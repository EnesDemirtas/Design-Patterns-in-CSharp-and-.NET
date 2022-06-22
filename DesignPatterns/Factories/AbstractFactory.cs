namespace Factories.AbstractFactory {

    // The only real use of an abstract factory is to give out abstract objects as opposed to concrete objects
    // We do not return the types of the object we're creating, we return abstract classes or interfaces

    public interface IHotDrink {
        void Consume();
    }

    internal class Tea : IHotDrink {
        public void Consume() {
            Console.WriteLine("This tea is nice I'd prefer it with milk");
        }
    }

    internal class Coffee : IHotDrink {
        public void Consume() {
            Console.WriteLine("This coffee is sensational");
        }
    }

    public interface IHotDrinkFactory {
        IHotDrink Prepare(int amount);
    }

    internal class TeaFactory : IHotDrinkFactory {
        public IHotDrink Prepare(int amount) {
            Console.WriteLine($"Put in a tea bag, boil water, pour {amount} ml, add lemon, enjoy!");
            return new Tea();
        }
    }

    internal class CoffeeFactory : IHotDrinkFactory {
        public IHotDrink Prepare(int amount) {
            Console.WriteLine($"Grind some beans, boil water, pour {amount} ml, add cream and sugar, enjoy!");
            return new Coffee();
        }
    }
    
    public class HotDrinkMachine {
        public enum AvailableDrink {
            Tea,
            Coffee
        }

        private Dictionary<AvailableDrink, IHotDrinkFactory> _factories = new();

        public HotDrinkMachine() {
            foreach (AvailableDrink drink in Enum.GetValues(typeof(AvailableDrink))) {
                var factory = (IHotDrinkFactory)Activator
                    .CreateInstance(Type.GetType("Factories." + Enum.GetName(typeof(AvailableDrink), drink) + "Factory"));
                _factories.Add(drink, factory);
            }
        }

        public IHotDrink MakeDrink(AvailableDrink drink, int amount) {
            return _factories[drink].Prepare(amount);
        }
    }


    class AbstractFactory {
        public static void main() {
            var machine = new HotDrinkMachine();
            var drink = machine.MakeDrink(HotDrinkMachine.AvailableDrink.Tea, 100);
            drink.Consume();
        }
    }
}