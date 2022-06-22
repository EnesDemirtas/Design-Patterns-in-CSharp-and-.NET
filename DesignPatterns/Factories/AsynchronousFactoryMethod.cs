namespace Factories; 

public class AsynchronousFactoryMethod {
    public class OldFoo {
        public OldFoo() {
            // await Task.Delay(1000); is not allowed inside of the constructor, this process may be a webpage loading etc.
        }
        
        public async Task<OldFoo> InitAsync() {
            await Task.Delay(1000);
            return this;
        }
    }

    public static async Task main() {
        // not the best way
        var foo = new OldFoo();
        await foo.InitAsync();
        
        // better way using Factory method, code is below
        Foo betterFoo = await Foo.CreateAsync();
    }

    public class Foo {
        private Foo() {
            
        }

        private async Task<Foo> InitAsync() {
            await Task.Delay(1000);
            return this;
        }

        public static Task<Foo> CreateAsync() {
            var result = new Foo();
            return result.InitAsync();
        }
    }
}