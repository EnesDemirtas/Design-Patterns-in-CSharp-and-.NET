namespace Prototype;

public class CopyConstructor {

    public class Person {
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

        public override string ToString() {
            return $"{nameof(Names)}: {string.Join(" ", Names)}, {nameof(Address)}: {Address}";
        }
        
    }

    public class Address {
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


        public override string ToString() {
            return $"{nameof(StreetName)}: {StreetName}, {nameof(HouseNumber)}: {HouseNumber}";
        }
        
    }

        public static void Main() {
            var alexa = new Person(new []{"Alexa", "Smith"}, new Address("7 Street", 33));
            var jane = new Person(alexa);
            jane.Address.HouseNumber = 66;
            
            Console.WriteLine(alexa);
            Console.WriteLine(jane);

        }
}