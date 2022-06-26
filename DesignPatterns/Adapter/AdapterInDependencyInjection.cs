using Autofac;
using Autofac.Features.Metadata;

namespace Adapter; 
// Adapter and Autofac package
// Autofac is an IoC container 
public class AdapterInDependencyInjection {
    public interface ICommand {
        void Execute();
    }

    class SaveCommand : ICommand {
        public void Execute() {
            Console.WriteLine("Saving a file");
        }
    }

    class OpenCommand : ICommand {
        public void Execute() {
            Console.WriteLine("Opening a file");
        }
    }

    public class Button {
        private ICommand _command;
        private string _name;

        public Button(ICommand command, string name) {
            _command = command;
            _name = name;
        }

        public void Click() {
            _command.Execute();
        }

        public void PrintMe() {
            Console.WriteLine($"I'm a button called {_name}");
        }
    }

    public class Editor {
        private IEnumerable<Button> _buttons;

        public IEnumerable<Button> Buttons => _buttons;

        public Editor(IEnumerable<Button> buttons) {
            _buttons = buttons;
        }

        public void ClickAll() {
            foreach (var button in _buttons) {
                button.Click();
            }
        }
    }

    public static void main() {
        var b = new ContainerBuilder();
        b.RegisterType<SaveCommand>().As<ICommand>().WithMetadata("Name", "Save");
        b.RegisterType<OpenCommand>().As<ICommand>().WithMetadata("Name", "Open");
        // b.RegisterType<Button>();
        //b.RegisterAdapter<ICommand, Button>(command => new Button(command)); // without RegisterAdapter command, we'd have only one button
        b.RegisterAdapter<Meta<ICommand>, Button>(command => new Button(command.Value, (string) command.Metadata["Name"]));
        b.RegisterType<Editor>();
        using (var c = b.Build()) {
            var editor = c.Resolve<Editor>();
            // editor.ClickAll();
            foreach (var button in editor.Buttons) {
                button.PrintMe();
            }
        }
    }
}