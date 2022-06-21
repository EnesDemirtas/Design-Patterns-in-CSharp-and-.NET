namespace Builder; 

public class FluentBuilderInheritanceWithRecursiveGenerics {
    // Assume you have Person class
    public class Person {
        public string Name;
        public string Position;
        
        // Added for solution subsequently
        public class Builder : CorrectPersonJobBuilder<Builder> {
        }
        // Added for solution subsequently
        public static Builder New => new Builder();

        public override string ToString() {
            return $"{nameof(Name)}: {Name}, {nameof(Position)}: {Position}";
        }
    }
    
    // And a personal info builder
    public class PersonInfoBuilder {
        protected Person person = new(); // Not private, because we're using inheritance
        
        // Classic fluent builder approach, it's ok so far.
        public PersonInfoBuilder Called(string name) {
            person.Name = name;
            return this;
        }
    }
    
    // Assume you have a new builder, but all the info from PersonInfoBuilder, so let's inherit from it
    public class PersonJobBuilder : PersonInfoBuilder {
        public PersonJobBuilder WorksAsA(string position) {
            person.Position = position;
            return this;
        }
    }
    
    // Until here, this approach is wrong because you are degrading your builder from a PersonJobBuilder to PersonInfoBuilder.

    // How can a derived class propagate the information about the return type to its own base class, recursively? 
    // Solution is shown below.

    public abstract class PersonBuilder {
        protected Person person = new();

        public Person Build() {
            return person;
        }
    }

    public class CorrectPersonInfoBuilder<TSelf> : PersonBuilder where TSelf : CorrectPersonInfoBuilder<TSelf> {
        public TSelf Called(string name) {
            person.Name = name;
            return (TSelf) this;
        }
    }

    public class CorrectPersonJobBuilder<TSelf> : CorrectPersonInfoBuilder<CorrectPersonJobBuilder<TSelf>>
        where TSelf : CorrectPersonJobBuilder<TSelf> {
        public TSelf WorksAsA(string position) {
            person.Position = position;
            return (TSelf) this;
        }
    }

    public static void Main() {
        var builder = new PersonJobBuilder();
        // Try initialize a new person using PersonJobBuilder
        // builder.Called("John").WorksAsA("Developer"); The problem is that Called method returns PersonInfoBuilder 
        // and it has no idea about WorksAsA method. 

        var person = Person.New.Called("John").WorksAsA("Developer").Build();
        Console.WriteLine(person);
    }
}