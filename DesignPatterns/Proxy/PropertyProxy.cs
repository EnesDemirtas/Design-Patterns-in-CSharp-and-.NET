namespace Proxy; 

// A property proxy is the idea of using an object as a property instead of just a literal value
public class PropertyProxy {
    public class Property<T> : IEquatable<Property<T>> where T : new() { // where T : new() means T will be an object
        private T value;

        public T Value {
            get => value;
            set {
                if (Equals(this.value, value)) return; // If the value doesn't change, do nothing, just return
                Console.WriteLine($"Assigning value to {value}");
                this.value = value;
            }
        }

        public Property() : this(Activator.CreateInstance<T>()) { // Makes an instance of T, this(default(T)) is also ok
            
        }

        public Property(T value) {
            this.value = value;
        }

        public static implicit operator T(Property<T> property) {
            return property.value; // int n = p_int;
        }

        public static implicit operator Property<T>(T value) {
            return new Property<T>(value); // Property<int> p = 123;
        }
        
        // Generate equality members
        public bool Equals(Property<T>? other) {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return EqualityComparer<T>.Default.Equals(value, other.value);
        }

        public override bool Equals(object? obj) {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Property<T>)obj);
        }

        public override int GetHashCode() {
            return value.GetHashCode();
        }

        public static bool operator ==(Property<T>? left, Property<T>? right) {
            return Equals(left, right);
        }

        public static bool operator !=(Property<T>? left, Property<T>? right) {
            return !Equals(left, right);
        }
    }
    
    // Game example
    public class Creature {
        private Property<int> agility = new();

        public int Agility {
            get => agility.Value;
            set => agility.Value = value;
        }
    }

    public static void main() {
        var c = new Creature();
        c.Agility = 10;
        c.Agility = 10;
    }
}