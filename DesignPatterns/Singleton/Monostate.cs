namespace Singleton; 

// If we want to singleton, why don't we make the whole thing static? 
// Because a static class with static members is terrible
// It doesn't even have a constructor. So, whenever we actually use this class, we end up referring to it by its name.
// Therefore, we cannot use things like dependency injection

// Mono state is a pattern that is a variation of Singleton. 
// Mono state also tries to use static members in a bizarre way

// State of the CEO being static, but being exposed in a non static way
// It works to a certain degree and it works even though you allow people to call the constructor
public class Monostate {
    
    // Only one CEO 
    public class CEO {
        // Attributes are static, Properties are non static
        private static string _name;
        private static int _age;

        public string Name {
            get => _name;
            set => _name = value;
        }

        public int Age {
            get => _age;
            set => _age = value;
        }

        public override string ToString() {
            return $"{nameof(Name)}: {Name}, {nameof(Age)}: {Age}";
        }
    }
    
    // We can create more than one objects, but they refer to same data (because that fields are static)
    public static void main() {
        var ceo = new CEO();
        ceo.Name = "Donald Trump";
        ceo.Age = 65;

        var ceo2 = new CEO();
        Console.WriteLine(ceo2);
    }
}