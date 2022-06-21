namespace Builder;

public class FunctionalBuilder {
    // A classic Person class
    public class Person {
        public string Name, Position;
        
        public override string ToString() {
            return $"{nameof(Name)}: {Name}, {nameof(Position)}: {Position}";
        }
    }

    // TSubject : The object that will be handled/processed, e.g. Person
    // TSelf : Class name of the builder, e.g. PersonBuilder
    public abstract class FunctionalBuilderClass<TSubject, TSelf>
        where TSelf : FunctionalBuilderClass<TSubject, TSelf>
        where TSubject : new() {
        
        private readonly List<Func<TSubject, TSubject>> _actions = new();
        public TSelf Do(Action<TSubject> action) => AddAction(action);
        public TSubject Build() 
            => _actions.Aggregate(new TSubject(), (p, f) => f(p)); 
    
        private TSelf AddAction(Action<TSubject> action) {
            _actions.Add(p => {
                action(p);
                return p;
            });
            return (TSelf) this;
        }
    }

    public sealed class PersonBuilder : FunctionalBuilderClass<Person, PersonBuilder> {
        public PersonBuilder Called(string name)
            => Do(p => p.Name = name);
    }

    
    public static void Main() {
        var person = new PersonBuilder().WorksAsA("Developer").Called("Angela").Build();
        var ps = new PersonBuilder().Called("Jane").WorksAsA("Dev").Build();
        Console.WriteLine(person);
        Console.WriteLine(ps);
    }
}

public static class PersonBuilderExtensions {
    public static FunctionalBuilder.PersonBuilder WorksAsA(this FunctionalBuilder.PersonBuilder builder, string position)
        => builder.Do(p => p.Position = position);
}