namespace Proxy; 
// Composite Proxy: Array of Structures - Structures of Array paradox
public class CompositeProxy {
    // Game example
    /*
    public class Creature {
        public byte Age;
        public int X, Y;
    }

    public static void main() {
        var creatures = new Creature[100]; // AoS -> Array of Structure
        Age X Y Age X Y Age X Y
        
        this is much better for cpu
        Age Age Age Age
        X X X X X X
        Y Y Y Y Y Y
        foreach (var c in creatures) {
            c.X++;
        }
    }
    */

    public class Creatures {
        private readonly int size;
        private byte[] age;
        private int[] x, y;

        public Creatures(int size) {
            this.size = size;
            age = new byte[size];
            x = new int[size];
            y = new int[size];
        }

        public struct CreatureProxy {
            private readonly Creatures creatures;
            private readonly int index;
            public CreatureProxy(Creatures creatures, int index) {
                this.creatures = creatures;
                this.index = index;
            }

            public ref byte Age => ref creatures.age[index];
            public ref int X => ref creatures.x[index];
            public ref int y => ref creatures.y[index];
        }

        public IEnumerator<CreatureProxy> GetEnumerator() {
            for (int pos = 0; pos < size; ++pos) {
                yield return new CreatureProxy(this, pos);
            }
        }
    }
    // Performance improvement
    public static void main() {
        var creatures = new Creatures(100); // SoA -> Structure of Array
        foreach (var c in creatures) {
            c.X++;
        }
    }
}