namespace Decorator; 

public class DynamicDecoratorComposition {
    public interface IShape {
        string AsString();
    }

    public class Circle : IShape {
        private float _radius;

        public Circle(float radius) {
            _radius = radius;
        }

        public void Resize(float factor) {
            _radius *= factor;
        }

        public string AsString() => $"a circle with radius {_radius}";
        }

    public class Square : IShape {
        private float _side;

        public Square(float side) {
            this._side = side;
        }

        public string AsString() => $"A square with side {_side}";
    }

    public class ColoredShape : IShape {
        private IShape _shape;
        private string _color;

        public ColoredShape(IShape shape, string color) {
            _shape = shape;
            _color = color;
        }

        public string AsString() {
            return $"{_shape.AsString()} has the color {_color}";
        }
    }

    public class TransparentShape : IShape {
        private IShape _shape;
        private float _transparency;

        public TransparentShape(IShape shape, float transparency) {
            _shape = shape;
            _transparency = transparency;
        }
        public string AsString() {
            return $"{_shape.AsString()} has {_transparency * 100}% transparency";
        }
    }

    public static void main() {
        // Applying one decorator to the another
        var square = new Square(2.56f);
        Console.WriteLine(square.AsString());

        var redSquare = new ColoredShape(square, "Red");    
        Console.WriteLine(redSquare.AsString());

        var redHalfTransparentSquare = new TransparentShape(redSquare, 0.5f);
        Console.WriteLine(redHalfTransparentSquare.AsString());
    }
}