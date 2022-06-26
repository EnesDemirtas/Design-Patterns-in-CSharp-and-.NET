using Autofac;
using MoreLinq;
using NUnit.Framework;

namespace Singleton;

public class SingletonInDependencyInjection {
    public interface IDatabase {
        int GetPopulation(string city);
    }
    
    public class OrdinaryDatabase : IDatabase {
        private Dictionary<string, int> _capitals;

        public OrdinaryDatabase() {
            Console.WriteLine("Initializing database");
            _capitals = File.ReadAllLines("capitals.txt").Batch(2).ToDictionary(list => list.ElementAt(0),
                list => int.Parse(list.ElementAt(1)));
        }

        public int GetPopulation(string city) {
            return _capitals[city];
        }

    }
    
    // All tests are passed
    [TestFixture]
    public class SingletonTests {
        [Test]
        public void ConfigurablePopulationTest() {
            var rf = new ConfigurableRecordFinder(new DummyDatabase());
            var names = new[] {"alpha", "gamma"};
            int total = rf.GetTotalPopulation(names);
            Assert.That(total, Is.EqualTo(4));
        }

        [Test]
        public void DIPopulationTest() {
            var cb = new ContainerBuilder();
            cb.RegisterType<OrdinaryDatabase>().As<IDatabase>().SingleInstance();
            cb.RegisterType<ConfigurableRecordFinder>();
            using (var c = cb.Build()) {
                var rf = c.Resolve<ConfigurableRecordFinder>();
            }
        }
    }


    // Instead of hard coded dependency, we use dependency injection
    public class ConfigurableRecordFinder {
        private IDatabase _database;
        public ConfigurableRecordFinder(IDatabase database) {
            _database = database;
        }
        public int GetTotalPopulation(IEnumerable<string> names) {
            int result = 0;
            foreach (var name in names)
                result += _database.GetPopulation(name);
            return result;
        }
    }

    public class DummyDatabase : IDatabase {
        public int GetPopulation(string city) {
            return new Dictionary<string, int> {["alpha"] = 1, ["beta"] = 2, ["gamma"] = 3}[city];
        }
    }

    public static void main() {

    }
}