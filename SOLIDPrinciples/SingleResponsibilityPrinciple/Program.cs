namespace SingleResponsibilityPrinciple {
    public class Program {
        
        public class Journal {
            private readonly List<string> _entries = new();
            private static int _count;

            public int AddEntry(string text) {
                _entries.Add($"{++_count}: {text}");
                return _count; //memento pattern
            }

            public void RemoveAt(int index) {
                _entries.RemoveAt(index);
            }

            public override string ToString() {
                return string.Join(Environment.NewLine, _entries);
            }

            // Persistence methods
            public void Save(string filename) {
                File.WriteAllText(filename, ToString());
            }

            public static void Load(string filename) {
                
            }

            public void Load(Uri uri) {
                
            }
            
            
            // Each class should has only one reason to change.
            // To achieve that, create another classes to separate the features.
        }

        public class Persistence {
            public void SaveToFile(Journal journal, string filename, bool overwrite = false) {
                if (overwrite || !File.Exists(filename))
                    File.WriteAllText(filename, journal.ToString());
            }
        }


        public static void Main() {
            var journal = new Program.Journal();
            journal.AddEntry("Today is a nice day.");
            journal.AddEntry("Today is a terrific day.");
            Console.WriteLine(journal);

            var p = new Persistence();
            var filename = @"/tmp/journal.txt";
            p.SaveToFile(journal, filename, true);
        }


    }

}