namespace Factories.AbstractFactoryAndOCP {

    // In the AbstractFactory.cs, we obviously violate the Open-Closed Principle 
    // We fix it in here

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
        // public enum AvailableDrink {
        //     Tea,
        //     Coffee
        // }

        // private Dictionary<AvailableDrink, IHotDrinkFactory> _factories = new();
        //
        // public HotDrinkMachine() {
        //     foreach (AvailableDrink drink in Enum.GetValues(typeof(AvailableDrink))) {
        //         var factory = (IHotDrinkFactory)Activator
        //             .CreateInstance(Type.GetType("Factories.AbstractFactoryAndOCP." + Enum.GetName(typeof(AvailableDrink), drink) + "Factory"));
        //         _factories.Add(drink, factory);
        //     }
        // }
        //
        // public IHotDrink MakeDrink(AvailableDrink drink, int amount) {
        //     return _factories[drink].Prepare(amount);
        // }

        private List<Tuple<string, IHotDrinkFactory>> _factories = new();
        
        public HotDrinkMachine() {
            foreach (var t in typeof(HotDrinkMachine).Assembly.GetTypes()) {
                // IsAssignableFrom = how you test whether something implements an interface 
                if (typeof(IHotDrinkFactory).IsAssignableFrom(t) && !t.IsInterface) {
                    _factories.Add(Tuple.Create(
                    t.Name.Replace("Factory", string.Empty),
                    (IHotDrinkFactory)Activator.CreateInstance(t)
                        ));
                }
            }
        }

        public IHotDrink MakeDrink() {
            Console.WriteLine("Available drinks:");
            for (var index = 0; index < _factories.Count; index++) {
                var tuple = _factories[index];
                Console.WriteLine($"{index}: {tuple.Item1}");
            }

            while (true) {
                string s;
                if ((s = Console.ReadLine()) != null && int.TryParse(s, out int i) && i >= 0 && i < _factories.Count) {
                    Console.WriteLine("Specify amount:");
                    s = Console.ReadLine();
                    if (s != null && int.TryParse(s, out int amount) && amount > 0) {
                        return _factories[i].Item2.Prepare(amount);
                    }
                }
                
                Console.WriteLine("Incorrect input, try again!");
            }
        } 
    }


    class AbstractFactoryAndOCP {
        public static void main() {
            var machine = new HotDrinkMachine();
            var drink = machine.MakeDrink();
            drink.Consume();
        }
    }
}