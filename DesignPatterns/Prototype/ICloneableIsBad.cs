namespace Prototype;

public class ICloneableIsBad {
    // The problem of ICloneable interface is that we cannot specify the type of the clone process
    // Deep Copy or Shallow Copy
    
    // Shallow Copy: Copying the value type fields of the current object to new object. But when the data is reference
    // type, then the only reference is copied but not the referred object itself.
    
    // Deep Copy: Creating a new object and then copying the fields of the current object to the newly created object
    // to make a complete copy of the internal reference types. If the specified field is a value type, then a bit-by-bit
    // copy of the field will be performed. If the specified field is a reference type, then a new copy of the referred object is performed.
    public class Person : ICloneable {
        public string[] Names;
        public Address Address;

        public Person(string[] names, Address address) {
            Names = names;
            Address = address;
        }

        public override string ToString() {
            return $"{nameof(Names)}: {string.Join(" ", Names)}, {nameof(Address)}: {Address}";
        }
        
        
        public object Clone() {
            // return new Person(Names, Address); Shallow copy
            return new Person(Names, (Address) Address.Clone()); // Deep copy
        }
    }

    public class Address : ICloneable {
        public string StreetName;
        public int HouseNumber;

        public Address(string streetName, int houseNumber) {
            StreetName = streetName;
            HouseNumber = houseNumber;
        }

        public override string ToString() {
            return $"{nameof(StreetName)}: {StreetName}, {nameof(HouseNumber)}: {HouseNumber}";
        }

        public object Clone() {
            return new Address(StreetName, HouseNumber);
        }
    }

        public static void Main() {
            var john = new Person(new []{"Alexa", "Smith"}, new Address("7 Street", 33));
            // Now, suppose that we want to create a new person who's address is same with John
            var jane = john;
            john.Names[0] = "Jane";
            Console.WriteLine(john);   
            Console.WriteLine(jane);
            // Obviously this is totally wrong because we're copying the reference object and we're changing the John too
        }
}