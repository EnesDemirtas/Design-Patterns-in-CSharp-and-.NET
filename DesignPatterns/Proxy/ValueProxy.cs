using System.Diagnostics;

namespace Proxy.ValueProxy {
// Value Proxy is a proxy that is typically constructed over a primitive type
// We may want stronger typing
    [DebuggerDisplay("{value*100.0f}%")]
    public struct Percentage : IEquatable<Percentage> {
        private readonly float value;

        internal Percentage(float value) {
            this.value = value;
        }
        
        public static float operator *(float f, Percentage p) {
            return f * p.value;
        }
        
        public static Percentage operator +(Percentage f, Percentage p) {
            return new Percentage(f.value + p.value);
        }

        public override string ToString() {
            return $"{value*100}%";
        }
        
        // Generate equality members
        public bool Equals(Percentage other) {
            return value.Equals(other.value);
        }

        public override bool Equals(object? obj) {
            return obj is Percentage other && Equals(other);
        }

        public override int GetHashCode() {
            return value.GetHashCode();
        }

        public static bool operator ==(Percentage left, Percentage right) {
            return left.Equals(right);
        }

        public static bool operator !=(Percentage left, Percentage right) {
            return !left.Equals(right);
        }
    }

    public static class PercentageExtensions {
        public static Percentage Percent(this int value) {
            return new Percentage(value / 100.0f);
        }
        
        public static Percentage Percent(this float value) {
            return new Percentage(value / 100.0f);
        }

    }

    public class Program {
        public static void main() {
            Console.WriteLine(10f * 5.Percent());

            Console.WriteLine(2.Percent() + 3.Percent());
        }
    }
}