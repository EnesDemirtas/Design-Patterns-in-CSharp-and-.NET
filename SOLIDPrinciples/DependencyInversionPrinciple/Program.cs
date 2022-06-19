namespace DependencyInversionPrinciple;

public class Program {
    // Dependency Inversion Principle states that high level parts of the system should not depend on low 
    // level parts of the system directly, instead they should depend on an abstraction. 
    
    // In this example, high-level class Research depends on the abstraction IRelationshipBrowser rather than 
    // low-level class Relationships directly. 

    public enum Relationship {
        Parent,
        Child,
        Sibling
    }

    public class Person {
        public string Name;
    }
    
    // low-level
    public class Relationships : IRelationshipBrowser {
        private List<(Person, Relationship, Person)> _relations = new();

        public void AddParentAndChild(Person parent, Person child) {
            _relations.Add((parent, Relationship.Parent, child));
            _relations.Add((child, Relationship.Child, parent));
        }

        // public List<(Person, Relationship, Person)> Relations => _relations;
        
        public IEnumerable<Person> FindAllChildrenOf(string name) {
            return _relations
                .Where(x => x.Item1.Name == name && x.Item2 == Relationship.Parent)
                .Select(r => r.Item3);
        }
    }
    public interface IRelationshipBrowser {
        IEnumerable<Person> FindAllChildrenOf(string name);
    }
    public class Research {
        // public Research(Relationships relationships) {
        //     var relations = relationships.Relations;
        //     foreach (var r in relations.Where(x => x.Item1.Name == "John" && x.Item2 == Relationship.Parent)) {
        //         Console.WriteLine($"John has a child called {r.Item3.Name}");
        //     }
        // }

        public Research(IRelationshipBrowser browser) {
            foreach (var p in browser.FindAllChildrenOf("John"))
                Console.WriteLine($"John has a child called {p.Name}");
        }
    }

    
    public static void Main() {
        var parent = new Person { Name = "John" };
        var child1 = new Person { Name = "Eli" };
        var child2 = new Person { Name = "Jane" };

        var relationships = new Relationships();
        relationships.AddParentAndChild(parent, child1);
        relationships.AddParentAndChild(parent, child2);

        new Research(relationships);
    }
}