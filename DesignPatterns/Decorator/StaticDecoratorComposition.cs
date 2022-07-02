namespace Decorator; 

public class StaticDecoratorComposition {
    public abstract class Shape {
        public abstract string AsString();
    }

    public class Circle : Shape {
        private float _radius;

        public Circle() : this(0) {
            
        }

        public Circle(float radius) {
            _radius = radius;
        }

        public void Resize(float factor) {
            _radius *= factor;
        }

        public override string AsString() => $"a circle with radius {_radius}";
    }

    public class Square : Shape {
        private float _side;

        public Square() : this(0.0f) {
            
        }
        
        public Square(float side) {
            this._side = side;
        }

        public override string AsString() => $"A square with side {_side}";
    }
    
    public class ColoredShape : Shape {
        private Shape _shape;
        private string _color;

        public ColoredShape(Shape shape, string color) {
            _shape = shape;
            _color = color;
        }

        public override string AsString() {
            return $"{_shape.AsString()} has the color {_color}";
        }
    }

    public class TransparentShape : Shape {
        private Shape _shape;
        private float _transparency;

        public TransparentShape(Shape shape, float transparency) {
            _shape = shape;
            _transparency = transparency;
        }
        public override string AsString() {
            return $"{_shape.AsString()} has {_transparency * 100}% transparency";
        }
    }

    public class ColoredShape<T> : Shape where T : Shape, new() {
        private string color;
        private T shape = new T();

        public ColoredShape() : this("black") { // Black is default value
            
        }

        public ColoredShape(string color) {
            this.color = color; 
        }
        public override string AsString() {
            return $"{shape.AsString()} has the color {color}";

        }
    }
    
    public class TransparentShape<T> : Shape where T : Shape, new() {
        private float transparency;
        private T shape = new T();

        public TransparentShape() : this(0) { // Black is default value
            
        }

        public TransparentShape(float transparency) {
            this.transparency = transparency; 
        }
        public override string AsString() {
            return $"{shape.AsString()} has {transparency * 100.0f}% transparency";

        }
    }

    public static void main() {
        var circle = new TransparentShape<ColoredShape<Circle>>(0.3f);
        Console.WriteLine(circle.AsString());
    }
}