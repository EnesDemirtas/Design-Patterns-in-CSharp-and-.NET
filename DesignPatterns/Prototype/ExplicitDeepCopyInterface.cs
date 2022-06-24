namespace Prototype;

public class ExplicitDeepCopyInterface {
    
    // Let's implement our own Deep Copy Interface
    public interface IPrototype<T> {
        T DeepCopy();
    }

    public class Person : IPrototype<Person> {
        public string[] Names;
        public Address Address;
        
        public Person(string[] names, Address address) {
            Names = names;
            Address = address;
        }
        // An easy solution is CopyConstructor which is a C++ concept. We create a constructor that takes another person
        // that is initialized before. For reference data types, we have to apply this method to them.
        public Person(Person other) {
            Names = other.Names;
            Address = new Address(other.Address);
        }

        public Person DeepCopy() {
            return new Person(Names, Address.DeepCopy());
        }

        public override string ToString() {
            return $"{nameof(Names)}: {string.Join(" ", Names)}, {nameof(Address)}: {Address}";
        }
        
    }

    public class Address : IPrototype<Address> {
        public string StreetName;
        public int HouseNumber;

        public Address(string streetName, int houseNumber) {
            StreetName = streetName;
            HouseNumber = houseNumber;
        }

        public Address(Address other) {
            StreetName = other.StreetName;
            HouseNumber = other.HouseNumber;
        }


        public Address DeepCopy() {
            return new Address(StreetName, HouseNumber);
        }

        public override string ToString() {
            return $"{nameof(StreetName)}: {StreetName}, {nameof(HouseNumber)}: {HouseNumber}";
        }
        
    }

    public static void Main() {
        var alexa = new Person(new []{"Alexa", "Smith"}, new Address("7 Street", 33));
        var jane = alexa.DeepCopy();
        jane.Address.HouseNumber = 66;
            
        Console.WriteLine(alexa);
        Console.WriteLine(jane);

    }
}