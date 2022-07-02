using System.Runtime.Serialization;
using System.Text;

namespace Decorator; 

public class AdapterDecorator {

    public class MyStringBuilder {
        private StringBuilder _builder = new(); // This and its methods form the Decorator 
        
        // First, we need an implicit conversion from a string
        public static implicit operator MyStringBuilder(string s) {
            var msb = new MyStringBuilder();
            msb.Append(s);
            return msb;
        }
        
        // We want + operator 
        public static MyStringBuilder operator +(MyStringBuilder msb, string s) {
            msb.Append(s);
            return msb;
        }

        public override string ToString() {
            return _builder.ToString();
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context) {
            ((ISerializable)_builder).GetObjectData(info, context);
        }

        public StringBuilder Append(bool value) {
            return _builder.Append(value);
        }

        public StringBuilder Append(byte value) {
            return _builder.Append(value);
        }

        public StringBuilder Append(char value) {
            return _builder.Append(value);
        }
        

        public StringBuilder Append(char value, int repeatCount) {
            return _builder.Append(value, repeatCount);
        }

        public StringBuilder Append(char[]? value) {
            return _builder.Append(value);
        }

        public StringBuilder Append(char[]? value, int startIndex, int charCount) {
            return _builder.Append(value, startIndex, charCount);
        }

        public StringBuilder Append(decimal value) {
            return _builder.Append(value);
        }

        public StringBuilder Append(double value) {
            return _builder.Append(value);
        }

        public StringBuilder Append(short value) {
            return _builder.Append(value);
        }

        public StringBuilder Append(int value) {
            return _builder.Append(value);
        }

        public StringBuilder Append(long value) {
            return _builder.Append(value);
        }

        public StringBuilder Append(object? value) {
            return _builder.Append(value);
        }

        public StringBuilder Append(ReadOnlyMemory<char> value) {
            return _builder.Append(value);
        }

        public StringBuilder Append(ReadOnlySpan<char> value) {
            return _builder.Append(value);
        }

        public StringBuilder Append(sbyte value) {
            return _builder.Append(value);
        }

        public StringBuilder Append(float value) {
            return _builder.Append(value);
        }

        public StringBuilder Append(string? value) {
            return _builder.Append(value);
        }

        public StringBuilder Append(string? value, int startIndex, int count) {
            return _builder.Append(value, startIndex, count);
        }

        public StringBuilder Append(StringBuilder? value) {
            return _builder.Append(value);
        }

        public StringBuilder Append(StringBuilder? value, int startIndex, int count) {
            return _builder.Append(value, startIndex, count);
        }

        public StringBuilder Append(ushort value) {
            return _builder.Append(value);
        }

        public StringBuilder Append(uint value) {
            return _builder.Append(value);
        }

        public StringBuilder Append(ulong value) {
            return _builder.Append(value);
        }

        public StringBuilder Append(ref StringBuilder.AppendInterpolatedStringHandler handler) {
            return _builder.Append(ref handler);
        }

        public StringBuilder Append(IFormatProvider? provider, ref StringBuilder.AppendInterpolatedStringHandler handler) {
            return _builder.Append(provider, ref handler);
        }

        public StringBuilder AppendFormat(IFormatProvider? provider, string format, object? arg0) {
            return _builder.AppendFormat(provider, format, arg0);
        }

        public StringBuilder AppendFormat(IFormatProvider? provider, string format, object? arg0, object? arg1) {
            return _builder.AppendFormat(provider, format, arg0, arg1);
        }

        public StringBuilder AppendFormat(IFormatProvider? provider, string format, object? arg0, object? arg1, object? arg2) {
            return _builder.AppendFormat(provider, format, arg0, arg1, arg2);
        }

        public StringBuilder AppendFormat(IFormatProvider? provider, string format, params object?[] args) {
            return _builder.AppendFormat(provider, format, args);
        }

        public StringBuilder AppendFormat(string format, object? arg0) {
            return _builder.AppendFormat(format, arg0);
        }

        public StringBuilder AppendFormat(string format, object? arg0, object? arg1) {
            return _builder.AppendFormat(format, arg0, arg1);
        }

        public StringBuilder AppendFormat(string format, object? arg0, object? arg1, object? arg2) {
            return _builder.AppendFormat(format, arg0, arg1, arg2);
        }

        public StringBuilder AppendFormat(string format, params object?[] args) {
            return _builder.AppendFormat(format, args);
        }

        public StringBuilder AppendJoin(char separator, params object?[] values) {
            return _builder.AppendJoin(separator, values);
        }

        public StringBuilder AppendJoin(char separator, params string?[] values) {
            return _builder.AppendJoin(separator, values);
        }

        public StringBuilder AppendJoin(string? separator, params object?[] values) {
            return _builder.AppendJoin(separator, values);
        }

        public StringBuilder AppendJoin(string? separator, params string?[] values) {
            return _builder.AppendJoin(separator, values);
        }

        public StringBuilder AppendJoin<T>(char separator, IEnumerable<T> values) {
            return _builder.AppendJoin(separator, values);
        }

        public StringBuilder AppendJoin<T>(string? separator, IEnumerable<T> values) {
            return _builder.AppendJoin(separator, values);
        }

        public StringBuilder AppendLine() {
            return _builder.AppendLine();
        }

        public StringBuilder AppendLine(string? value) {
            return _builder.AppendLine(value);
        }

        public StringBuilder AppendLine(ref StringBuilder.AppendInterpolatedStringHandler handler) {
            return _builder.AppendLine(ref handler);
        }

        public StringBuilder AppendLine(IFormatProvider? provider, ref StringBuilder.AppendInterpolatedStringHandler handler) {
            return _builder.AppendLine(provider, ref handler);
        }

        public StringBuilder Clear() {
            return _builder.Clear();
        }

        public void CopyTo(int sourceIndex, char[] destination, int destinationIndex, int count) {
            _builder.CopyTo(sourceIndex, destination, destinationIndex, count);
        }

        public void CopyTo(int sourceIndex, Span<char> destination, int count) {
            _builder.CopyTo(sourceIndex, destination, count);
        }

        public int EnsureCapacity(int capacity) {
            return _builder.EnsureCapacity(capacity);
        }

        public bool Equals(ReadOnlySpan<char> span) {
            return _builder.Equals(span);
        }

        public bool Equals(StringBuilder? sb) {
            return _builder.Equals(sb);
        }

        public StringBuilder.ChunkEnumerator GetChunks() {
            return _builder.GetChunks();
        }

        public StringBuilder Insert(int index, bool value) {
            return _builder.Insert(index, value);
        }

        public StringBuilder Insert(int index, byte value) {
            return _builder.Insert(index, value);
        }

        public StringBuilder Insert(int index, char value) {
            return _builder.Insert(index, value);
        }

        public StringBuilder Insert(int index, char[]? value) {
            return _builder.Insert(index, value);
        }

        public StringBuilder Insert(int index, char[]? value, int startIndex, int charCount) {
            return _builder.Insert(index, value, startIndex, charCount);
        }

        public StringBuilder Insert(int index, decimal value) {
            return _builder.Insert(index, value);
        }

        public StringBuilder Insert(int index, double value) {
            return _builder.Insert(index, value);
        }

        public StringBuilder Insert(int index, short value) {
            return _builder.Insert(index, value);
        }

        public StringBuilder Insert(int index, int value) {
            return _builder.Insert(index, value);
        }

        public StringBuilder Insert(int index, long value) {
            return _builder.Insert(index, value);
        }

        public StringBuilder Insert(int index, object? value) {
            return _builder.Insert(index, value);
        }

        public StringBuilder Insert(int index, ReadOnlySpan<char> value) {
            return _builder.Insert(index, value);
        }

        public StringBuilder Insert(int index, sbyte value) {
            return _builder.Insert(index, value);
        }

        public StringBuilder Insert(int index, float value) {
            return _builder.Insert(index, value);
        }

        public StringBuilder Insert(int index, string? value) {
            return _builder.Insert(index, value);
        }

        public StringBuilder Insert(int index, string? value, int count) {
            return _builder.Insert(index, value, count);
        }

        public StringBuilder Insert(int index, ushort value) {
            return _builder.Insert(index, value);
        }

        public StringBuilder Insert(int index, uint value) {
            return _builder.Insert(index, value);
        }

        public StringBuilder Insert(int index, ulong value) {
            return _builder.Insert(index, value);
        }

        public StringBuilder Remove(int startIndex, int length) {
            return _builder.Remove(startIndex, length);
        }

        public StringBuilder Replace(char oldChar, char newChar) {
            return _builder.Replace(oldChar, newChar);
        }

        public StringBuilder Replace(char oldChar, char newChar, int startIndex, int count) {
            return _builder.Replace(oldChar, newChar, startIndex, count);
        }

        public StringBuilder Replace(string oldValue, string? newValue) {
            return _builder.Replace(oldValue, newValue);
        }

        public StringBuilder Replace(string oldValue, string? newValue, int startIndex, int count) {
            return _builder.Replace(oldValue, newValue, startIndex, count);
        }

        public string ToString(int startIndex, int length) {
            return _builder.ToString(startIndex, length);
        }

        public int Capacity {
            get => _builder.Capacity;
            set => _builder.Capacity = value;
        }

        public char this[int index] {
            get => _builder[index];
            set => _builder[index] = value;
        }

        public int Length {
            get => _builder.Length;
            set => _builder.Length = value;
        }

        public int MaxCapacity => _builder.MaxCapacity;
    }


    public static void main() {
        /* Inefficient way, because strings are immutable, wasting a lot of memory
        var s = "hello"; // StringBuilder s = "hello"; doesn't work because there no conversion between string and StringBuilder
        s += "world";
        Console.WriteLine(s);
        */
        MyStringBuilder s = "hello"; // Valid because we wrote an implicit conversion, line 12
        s += "world"; // Valid because we wrote the + operator for this class, line 19
        Console.WriteLine(s);
    }
}