namespace Decorator; 

// Java and .NET is lack of multiple inheritance which is sometimes very useful
// We have three classes, bird, lizard, and a DRAGON. Dragon can be inherited from bird and lizard
// But we cannot inherit a class from two classes, they can have only one parent
public class MultipleInheritanceWithInterfaces {
    public interface IBird {
        public void Fly();
        int Weight { get; set; }
    }
    public class Bird : IBird {
        public int Weight { get; set; }
        public void Fly() {
            Console.WriteLine($"Soaring in the sky with weight {Weight}");
        }
    }

    public interface ILizard {
        public void Crawl();
        int Weight { get; set; }
    }
    
    public class Lizard : ILizard {
        public int Weight { get; set; }
        public void Crawl() {
            Console.WriteLine($"Crawling in the dirt with weight {Weight}");
        }
    }

    public class Dragon : IBird, ILizard {
        private Bird _bird = new();
        private Lizard _lizard = new();
        private int weight;
        public void Fly() {
            _bird.Fly();
        }

        public void Crawl() {
            _lizard.Crawl();
        }
        
        public int Weight {
            get { return weight; }
            set {
                weight = value;
                _bird.Weight = value;
                _lizard.Weight = value;
            }
        }
    }

    public static void main() {
        var d = new Dragon();
        d.Weight = 256;
        d.Fly();
        d.Crawl();
    }
}