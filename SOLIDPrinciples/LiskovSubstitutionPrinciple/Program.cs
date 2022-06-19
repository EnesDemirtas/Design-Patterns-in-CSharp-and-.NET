namespace LiskovSubstitutionPrinciple;

public class Program {
    
    // According to Liskov Substitution Principle, you should be able to substitute a base type for a subtype. 
    // To achieve that, virtual and override keywords are used in this example. virtual means that this attribute is 
    // override-able and override means that I'm overriding this attribute. Finally, when an upcasting happens, 
    // program choose the appropriate one. 
    public class Rectangle {
        public virtual int Width { get; set; }
        public virtual int Height { get; set; }

        public Rectangle() {
            
        }

        public Rectangle(int width, int height) {
            Width = width;
            Height = height;
        }

        public override string ToString() {
            return $"{nameof(Width)}: {Width}, {nameof(Height)}: {Height}";
        }
    }

    public class Square : Rectangle {
        public override int Width {
            set {
                base.Width = base.Height = value;
            }
        }

        public override int Height {
            set {
                base.Height = base.Width = value;
            }
        }
    }

    public static int Area(Rectangle r) => r.Width * r.Height;
    public static void Main() {
        Rectangle rc = new Rectangle(2, 3);
        Console.WriteLine($"{rc} has area {Area(rc)}");

        Rectangle sq = new Square();
        sq.Width = 4;
        Console.WriteLine($"{sq} has area {Area(sq)}");

    }
    
}