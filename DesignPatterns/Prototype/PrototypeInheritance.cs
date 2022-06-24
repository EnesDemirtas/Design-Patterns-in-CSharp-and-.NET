namespace Prototype.PrototypeInheritance {
    // If you are ok with your fully initializing constructors, do not implement this
    // If your prototype inheritance hierarchy is big enough, you better use this structure

    public interface IDeepCopyable<T>
        where T : new() // T has to implement a default constructor 
    {
        void CopyTo(T target); // Every single class in the inheritance hierarchy has to be able to copy its own internal state into some target
        
        // Default interface member
        public T DeepCopy() {
            T t = new();
            CopyTo(t);
            return t;
        }
    }
    
    public class Address : IDeepCopyable<Address> {
        public string StreetName;
        public int HouseNumber;

        public Address() {
            
        }

        public Address(string streetName, int houseNumber) {
            StreetName = streetName;
            HouseNumber = houseNumber;
        }

        public void CopyTo(Address target) {
            target.StreetName = StreetName;
            target.HouseNumber = HouseNumber;
        }

        // public Address DeepCopy() {
        //     return (Address) MemberwiseClone();  Check the description of the MemberwiseClone method
        // }

        public override string ToString() {
            return $"{nameof(StreetName)}: {StreetName}, {nameof(HouseNumber)}: {HouseNumber}";
        }
    }

    public class Person : IDeepCopyable<Person> {
        public string[] Names;
        public Address Address;

        public Person() {
            
        }

        public Person(string[] names, Address address) {
            Names = names;
            Address = address;
        }

        public void CopyTo(Person target) {
            target.Names = (string[])Names.Clone();
            target.Address = Address.DeepCopy();
        }

        // public Person DeepCopy() {
             // Names.Clone() is valid because despite that it's a shallow copy, the types are trivially copyable
             // This means that we can create a new array and stick the same old references in there
             // Strings have value semantics
        //   return new Person((string[]) Names.Clone(), Address.DeepCopy());
        // }

        public override string ToString() {
            return $"{nameof(Names)}: {string.Join(" ", Names)}, {nameof(Address)}: {Address}";
        }
    }
    

    public class Employee : Person, IDeepCopyable<Employee> {
        public int Salary;

        public Employee() {
            
        }

        public Employee(string[] names, Address address, int salary) : base(names, address) {
            Salary = salary;
        }
        

        public override string ToString() {
            return $"{base.ToString()}, {nameof(Salary)}: {Salary}";
        }

        public void CopyTo(Employee target) {
            base.CopyTo(target);
            target.Salary = Salary;
        }
    }
    
    
    public static  class ExtensionMethods {
        public static T DeepCopy<T>(this IDeepCopyable<T> item) where T : new() {
            return item.DeepCopy();
        }

        public static T DeepCopy<T>(this T person) where T : Person, new() {
            return ((IDeepCopyable<T>) person).DeepCopy();
        }
    }


    public class Program {
        public static void main() {
            var john = new Employee();
            john.Names = new[] { "John", "Doe" };
            john.Address = new Address {
                StreetName = "7 Street",
                HouseNumber = 33
            };
            john.Salary = 10000;

            var copy = john.DeepCopy();
            copy.Names[1] = "Smith";
            copy.Address.HouseNumber = 66;
            copy.Salary = 20000;
        
            Console.WriteLine(john);
            Console.WriteLine(copy);
        }
    }
    
}
