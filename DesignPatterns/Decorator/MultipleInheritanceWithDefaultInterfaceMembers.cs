namespace Decorator; 

// Dragon example using Default Interface Members
public class MultipleInheritanceWithDefaultInterfaceMembers {
    public interface ICreature {
        int Age { get; set; }
    }

    public interface IBird : ICreature {
        void Fly() {
            if (Age >= 10) {
                Console.WriteLine("I'm flying");
            }
        }
    }

    public interface ILizard : ICreature {
        void Crawl() {
            if (Age < 10) {
                Console.WriteLine("I'm crawling");
            }
        }
    }
    
    public class Organism {}

    public class Dragon : Organism, IBird, ILizard{
        public int Age { get; set; }
    }
    
    // Assume we want to add additional behaviors to Dragon
    // one option is inheritance but assume it's not possible because suppose that dragon is inherited from another class
    // First option is Extension method
    // Second is C# default interface methods

    public static void main() {
        var d = new Dragon {Age = 5};
        // ((ILizard)d).Crawl(); You cannot access these methods because they're default interface methods
        if (d is IBird bird) {
            bird.Fly();
        }

        if (d is ILizard lizard) {
            lizard.Crawl();
        }
    }
}