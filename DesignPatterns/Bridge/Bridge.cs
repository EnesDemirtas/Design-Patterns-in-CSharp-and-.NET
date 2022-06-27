using Autofac;

namespace Bridge;
// Rendering example, you can render an object different ways, e.g. represented as a vector or a raster

public class Bridge {
    public interface IRenderer {
        void RenderCircle(float radius); // Circle is only one example, we may need other shapes
    }

    public class VectorRenderer : IRenderer {
        public void RenderCircle(float radius) {
            Console.WriteLine($"Drawing a circle of radius {radius}");
        }
    }

    public class RasterRenderer : IRenderer {
        public void RenderCircle(float radius) {
            Console.WriteLine($"Drawing pixels for circle with radius {radius}");
        }
    }
    
    // Bridging
    // Don't let the Shape decide the different ways in which it can be drawn
    // Instead, you have to build a bridge between the shape and whoever is drawing, which is IRenderer
    public abstract class Shape {
        protected IRenderer Renderer;
        protected Shape(IRenderer renderer) {
            this.Renderer = renderer;
        }
        // Just render it however the Renderer specify
        public abstract void Draw();
        public abstract void Resize(float factor);
    }

    public class Circle : Shape {
        private float _radius;

        public Circle(IRenderer renderer, float radius) : base(renderer) {
            this._radius = radius;
        }
        
        public override void Draw() {
            Renderer.RenderCircle(_radius);
        }

        public override void Resize(float factor) {
            _radius *= factor;
        }
    }

    public static void main() {
        /* Without Dependency Injection
        IRenderer renderer = new RasterRenderer();
        IRenderer renderer = new VectorRenderer();
        var circle = new Circle(renderer, 5);
        
        circle.Draw();
        circle.Resize(2);
        circle.Draw();
        */

        var cb = new ContainerBuilder();
        cb.RegisterType<VectorRenderer>().As<IRenderer>().SingleInstance();
        cb.Register((c, p) => 
            new Circle(c.Resolve<IRenderer>(), 
                p.Positional<float>(0)));

        using (var c = cb.Build()) {
            var circle = c.Resolve<Circle>(new PositionalParameter(0, 5.0f));
            circle.Draw();
            circle.Resize(2);
            circle.Draw();
        }
    }
}