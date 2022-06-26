

using MoreLinq;

namespace Singleton;

// For some components it only makes sense to have one in the system, e.g. DB repository, object factory, etc.
// E.g. the constructor call is expensive. We only do it once, we provide everyone with the same instance
// Want to prevent anyone creating additional copies
// Need to take care of lazy instantiation and thread safety
public class SingletonImplementation {
    public interface IDatabase {
        int GetPopulation(string city);
    }

    public class SingletonDatabase : IDatabase {
        private Dictionary<string, int> _capitals;

        private SingletonDatabase() {
            Console.WriteLine("Initializing database");
            _capitals = File.ReadAllLines("capitals.txt").Batch(2).ToDictionary(list => list.ElementAt(0),
                list => int.Parse(list.ElementAt(1)));
        }

        public int GetPopulation(string city) {
            return _capitals[city];
        }
        // Lazy initialization of an object means that its creation is deferred until it is first used!
        private static Lazy<SingletonDatabase> instance = new(() => new SingletonDatabase());

        public static SingletonDatabase Instance => instance.Value;
    }

    public static void main() {
        var database = SingletonDatabase.Instance;
        var city = "Sao Paulo";
        Console.WriteLine($"{city} has population {database.GetPopulation(city)}");
    }
}