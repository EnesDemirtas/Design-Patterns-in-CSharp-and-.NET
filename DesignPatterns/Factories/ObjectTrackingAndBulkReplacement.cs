using System.Text;

namespace Factories; 

public class ObjectTrackingAndBulkReplacement {
    public interface ITheme {
        string TextColor { get; }
        string BgrColor { get; }
    }

    class LightTheme : ITheme {
        public string TextColor => "black";
        public string BgrColor => "white";
    }

    class DarkTheme : ITheme {
        public string TextColor => "white";
        public string BgrColor => "dark gray";
    }

    // Depending on a bool value, specifying whether we want dark theme or not, we would return the appropriate theme
    // In addition, we're going to keep a weak reference to every single object created inside the factory
    public class TrackingThemeFactory {
        private readonly List<WeakReference<ITheme>> _themes = new();

        public ITheme CreateTheme(bool dark) {
            ITheme theme = dark ? new DarkTheme() : new LightTheme();
            _themes.Add(new WeakReference<ITheme>(theme));
            return theme;
        }
        
        // An attribute that returns the theme type (dark or light) of the existing object
        // NOTE: We keep the objects' reference by the WeakReference<>(). They can be collected by the garbage collector
        // NOTE: We use "Weak Reference" so as not to extend the lifetime of the constructed object because otherwise 
        // objects will live for as long as the factory lives.
        public string Info {
            get {
                var sb = new StringBuilder();
                foreach (var reference in _themes) {
                    if (reference.TryGetTarget(out var theme)) {
                        bool dark = theme is DarkTheme;
                        sb.Append(dark ? "Dark" : "Light")
                            .AppendLine(" theme");
                    }
                }

                return sb.ToString();
            }
        }
    }

    // Bulk Replacement logic
    public class ReplaceableThemeFactory {
        private readonly List<WeakReference<Ref<ITheme>>> _themes = new();

        private ITheme CreateThemeImpl(bool dark) {
            return dark ? new DarkTheme() : new LightTheme();
        }

        public Ref<ITheme> CreateTheme(bool dark) {
            var r = new Ref<ITheme>(CreateThemeImpl(dark));
            _themes.Add(new(r));
            return r;
        }

        public void ReplaceTheme(bool dark) {
            foreach (var wr in _themes) {
                if (wr.TryGetTarget(out var reference)) {
                    reference.Value = CreateThemeImpl(dark);
                }
            }
        }
    }
    
    // Wrapper class, to get the type of the object by Value attribute
    // where T : class means that it is going to work with only reference types
    public class Ref<T> where T : class {
        public T Value;

        public Ref(T value) {
            Value = value;
        }
    }

    public static void main() {
        var factory = new TrackingThemeFactory();
        var theme1 = factory.CreateTheme(false);
        var theme2 = factory.CreateTheme(true);
        Console.WriteLine(factory.Info);

        var factory2 = new ReplaceableThemeFactory();
        var magicTheme = factory2.CreateTheme(true);
        Console.WriteLine(magicTheme.Value.BgrColor);
        factory2.ReplaceTheme(false);
        Console.WriteLine(magicTheme.Value.BgrColor);
    }
}