namespace Composite; 

// Enterprise level, not basic and popular
// Open-Closed Principle and the idea of Specification pattern
public class CompositeSpecification {
    public enum Color {
        Red, Green, Blue
    }
    
    public enum Size {
        Small, Medium, Large, Huge
    }
    
    public class Product {
        public string Name;
        public Color Color;
        public Size Size;
        
        public Product(string name, Color color, Size size) {
            if (name == null) {
                throw new ArgumentNullException(paramName: nameof(name));
            }

            Name = name;
            Color = color;
            Size = size;
        }
    }
    public class ProductFilter {
        public IEnumerable<Product> FilterBySize(IEnumerable<Product> products, Size size) {
            foreach (var p in products)
                if (p.Size == size)
                    yield return p;
        }
        public IEnumerable<Product> FilterByColor(IEnumerable<Product> products, Color color) {
            foreach (var p in products)
                if (p.Color == color)
                    yield return p;
        }
        public IEnumerable<Product> FilterBySizeAndColor(IEnumerable<Product> products, Size size, Color color) {
            foreach (var p in products)
                if (p.Size == size && p.Color == color)
                    yield return p;
        }
    }
    
    
    public interface IFilter<T> {
        IEnumerable<T> Filter(IEnumerable<T> items, ISpecification<T> spec);
    }
    
    public class ColorSpecification : ISpecification<Product> {
        private Color color;
        public ColorSpecification(Color color) {
            this.color = color;
        }

        public override bool IsSatisfied(Product t) {
            return t.Color == color;
        }
    }
    
    public class SizeSpecification : ISpecification<Product> {
        private Size size;
        public SizeSpecification(Size size) {
            this.size = size;
        }

        public override bool IsSatisfied(Product p) {
            return p.Size == size;
        }
    }

    public abstract class ISpecification<T> {
        public abstract bool IsSatisfied(T p);

        public static ISpecification<T> operator &(ISpecification<T> first, ISpecification<T> second) {
            return new AndSpecification<T>(first, second);
        }
    }

    // Keeps a collection of specification
    public abstract class ICompositeSpecification<T> : ISpecification<T> {
        protected readonly ISpecification<T>[] items;

        public ICompositeSpecification(params ISpecification<T>[] items) {
            this.items = items;
        }
    } 
    
    // combinator
    public class AndSpecification<T> : ICompositeSpecification<T> {
        public AndSpecification(params ISpecification<T>[] items) : base(items) {
            
        }
        public override bool IsSatisfied(T t) {
            // Any => OrSpecification
            return items.All(i => i.IsSatisfied(t));
        }
    }
    
    public class BetterFilter : IFilter<Product> {
        public IEnumerable<Product> Filter(IEnumerable<Product> items, ISpecification<Product> spec) {
            foreach (var i in items)
                if (spec.IsSatisfied(i))
                    yield return i;
        }
    }
    

}