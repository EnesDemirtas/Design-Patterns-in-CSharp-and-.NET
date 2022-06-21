namespace Builder; 

public class FacetedBuilder {
    // Sometimes a single builder is not enough
    // You need several builders which are responsible for building up several different aspects of a particular objects
    // To do that, we'll use Facade pattern combined with Builder pattern
    
    public class Person {
        // address
        public string StreetAddress, Postcode, City;
        
        // employment
        public string CompanyName, Position;
        public int AnnualIncome;

        public override string ToString() {
            return $"{nameof(StreetAddress)}: {StreetAddress}, {nameof(Postcode)}: {Postcode}, {nameof(City)}: {City}, " +
                   $"{nameof(CompanyName)}: {CompanyName}, {nameof(Position)}: {Position}, {nameof(AnnualIncome)}: {AnnualIncome}";
        }
    }

    // A facade keeps a reference to the object that's being built up
    // A facade allows you access to those sub builders
    public class PersonBuilder { // Facade
        // Reference object !!! All actions will be applied to this object by reference
        protected Person person = new();

        public PersonJobBuilder Works => new PersonJobBuilder(person);
        public PersonAddressBuilder Lives => new PersonAddressBuilder(person);

        // Provides an implicit convert from PersonBuilder to Person
        public static implicit operator Person(PersonBuilder pb) {
            return pb.person;
        }
    }

    public class PersonJobBuilder : PersonBuilder {
        public PersonJobBuilder(Person person) {
            this.person = person;
        }

        public PersonJobBuilder At(string companyName) {
            person.CompanyName = companyName;
            return this;
        }

        public PersonJobBuilder AsA(string position) {
            person.Position = position;
            return this;
        }

        public PersonJobBuilder Earning(int amount) {
            person.AnnualIncome = amount;
            return this;
        }
    }

    public class PersonAddressBuilder : PersonBuilder {
        public PersonAddressBuilder(Person person) {
            this.person = person;
        }

        public PersonAddressBuilder At(string streetAddress) {
            person.StreetAddress = streetAddress;
            return this;
        }

        public PersonAddressBuilder WithPostcode(string postcode) {
            person.Postcode = postcode;
            return this;
        }

        public PersonAddressBuilder In(string city) {
            person.City = city;
            return this;
        }
    }

    public static void Main() {
        var builder = new PersonBuilder();
        // Both builders are inherited from PersonBuilder and this class contains both builders' reference
        // Thanks to this approach, we can use them together below
        Person person = builder
            .Lives
                .At("7 Street")
                .In("Ankara")
                .WithPostcode("06490")
            .Works
                .At("Netflix")
                .AsA("ML Engineer")
                .Earning(200000);
        Console.WriteLine(person);
    }
}