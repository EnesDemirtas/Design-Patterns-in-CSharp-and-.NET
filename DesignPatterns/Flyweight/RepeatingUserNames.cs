using JetBrains.dotMemoryUnit;
using JetBrains.Profiler.Api;
using NUnit.Framework;

namespace Flyweight;

public class RepeatingUserNames {
    public class User {
        private string fullName;

        public User(string fullName) {
            this.fullName = fullName;
        }
    }

    public class OptimizedUser {
        private static List<string> strings = new();
        private int[] names;

        public OptimizedUser(string fullName) {
            int getOrAdd(string s) {
                int idx = strings.IndexOf(s);
                if (idx != -1) return idx;
                else {
                    strings.Add(s);
                    return strings.Count - 1;
                }
            }

            names = fullName.Split(' ').Select(getOrAdd).ToArray();
        }

        public string FullName => string.Join(" ", names.Select(i => strings[i]));
    }

    [TestFixture]
    public class Demo {
        public static void main() {
            
        }

        [Test] // Result is 1655033 bytes
        public void TestUser() {
            var firstNames = Enumerable.Range(0, 50).Select(_ => RandomString());
            var lastNames = Enumerable.Range(0, 50).Select(_ => RandomString());

            var users = new List<User>();

            foreach (var firstName in firstNames) {
                foreach (var lastName in lastNames) {
                    users.Add(new User($"{firstName} {lastName}"));
                }
            }

            ForceGC();  // Garbage Collector
            dotMemory.Check(memory => {
                Console.WriteLine(memory.SizeInBytes);
            });
        }
        
        [Test] // Result is 1296991 bytes 
        public void TestOptimizedUser() {
            var firstNames = Enumerable.Range(0, 50).Select(_ => RandomString());
            var lastNames = Enumerable.Range(0, 50).Select(_ => RandomString());

            var users = new List<OptimizedUser>();

            foreach (var firstName in firstNames) {
                foreach (var lastName in lastNames) {
                    users.Add(new OptimizedUser($"{firstName} {lastName}"));
                }
            }

            ForceGC();  // Garbage Collector
            dotMemory.Check(memory => {
                Console.WriteLine(memory.SizeInBytes);
            });
        }

        private void ForceGC() {
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
        }

        private string RandomString() {
            Random rand = new Random();
            return new string(Enumerable.Range(0, 10).Select(i => (char) ('a' + rand.Next(26))).ToArray());
        }
    }
}