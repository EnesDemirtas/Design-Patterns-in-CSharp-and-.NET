using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;

namespace Prototype.CopyThroughSerialization { 

    // Fool deep copy
    // A serializer serializes the entire tree 
    // To do that, we need to implement a serializer and deserializer
    // This is how it is done in the real world.

    public static class ExtensionMethods {
        // Binary serializer
        public static T DeepCopy<T>(this T self) {
            var stream = new MemoryStream();
            var formatter = new BinaryFormatter(); // BinaryFormatting is just an example, we can other formatting methods too.
            formatter.Serialize(stream, self);
            stream.Seek(0, SeekOrigin.Begin);
            object copy = formatter.Deserialize(stream);
            stream.Close();
            return (T) copy;
        }
        
        // We don't need to use [Serializable] annotation anymore
        // However, our objects need to have parameterless constructors
        public static T DeepCopyXml<T>(this T self) {
            using (var ms = new MemoryStream()) { // using statement will close the stream automatically
                var s = new XmlSerializer(typeof(T));
                s.Serialize(ms, self);
                ms.Position = 0; // Same as Seek()
                return (T) s.Deserialize(ms);
            }
            
        }
    }
    [Serializable]
    public class Person {
        public string[] Names;
        public Address Address;

        public Person() {
            
        }
        
        public Person(string[] names, Address address) {
            Names = names;
            Address = address;
        }

        public Person(Person other) {
            Names = other.Names;
            Address = new Address(other.Address);
        }


        public override string ToString() {
            return $"{nameof(Names)}: {string.Join(" ", Names)}, {nameof(Address)}: {Address}";
        }
        
    }
    
    [Serializable]
    public class Address {
        public string StreetName;
        public int HouseNumber;

        public Address() {
            
        }

        public Address(string streetName, int houseNumber) {
            StreetName = streetName;
            HouseNumber = houseNumber;
        }

        public Address(Address other) {
            StreetName = other.StreetName;
            HouseNumber = other.HouseNumber;
        }
        

        public override string ToString() {
            return $"{nameof(StreetName)}: {StreetName}, {nameof(HouseNumber)}: {HouseNumber}";
        }
        
    }

    public class Program {

        public static void Main() {
            var alexa = new Person(new[] { "Alexa", "Smith" }, new Address("7 Street", 33));
            var jane = alexa.DeepCopyXml();
            jane.Names[0] = "Jane";
            jane.Address.HouseNumber = 66;

            Console.WriteLine(alexa);
            Console.WriteLine(jane);

        }
    }

}

