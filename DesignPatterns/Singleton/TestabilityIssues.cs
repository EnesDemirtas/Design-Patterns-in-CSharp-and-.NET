using MoreLinq;
using NUnit.Framework;

namespace Singleton;

public class TestabilityIssues {
    public interface IDatabase {
        int GetPopulation(string city);
    }

    public class SingletonDatabase : IDatabase {
        private Dictionary<string, int> _capitals;
        private static int instanceCount;
        public static int Count => instanceCount;

        private SingletonDatabase() {
            instanceCount++;
            Console.WriteLine("Initializing database");
            _capitals = File.ReadAllLines("capitals.txt").Batch(2).ToDictionary(list => list.ElementAt(0),
                list => int.Parse(list.ElementAt(1)));
        }

        public int GetPopulation(string city) {
            return _capitals[city];
        }
        private static Lazy<SingletonDatabase> instance = new(() => new SingletonDatabase());

        public static SingletonDatabase Instance => instance.Value;
    }
    // All tests are passed
    [TestFixture]
    public class SingletonTests {
        [Test]
        public void IsSingletonTest() {
            var database1 = SingletonDatabase.Instance;
            var database2 = SingletonDatabase.Instance;
            
            Assert.That(database1, Is.SameAs(database2));
            Assert.That(SingletonDatabase.Count, Is.EqualTo(1));
        }
        
        // Being dependent on a live database is the main problem. Seoul may will be deleted by anyone
        // We should fake the database, but we cannot. Because the RecordFinder has a hard coded reference to the instance
        // (Line 59).
        [Test]
        public void SingletonTotalPopulationTest() {
            var rf = new SingletonRecordFinder();
            var names = new[] {"Seoul", "Mexico City"};
            int total = rf.GetTotalPopulation(names);
            Assert.That(total, Is.EqualTo(17500000 + 17400000));
        }
    }
    
    // Assume that we have another component which consumes a singleton, need a database to work with
    public class SingletonRecordFinder {
        public int GetTotalPopulation(IEnumerable<string> names) {
            int result = 0;
            foreach (var name in names)
                result += SingletonDatabase.Instance.GetPopulation(name);
            return result;
        }
    }

    public static void main() {
        var database = SingletonDatabase.Instance;
        var city = "Sao Paulo";
        Console.WriteLine($"{city} has population {database.GetPopulation(city)}");
    }
}