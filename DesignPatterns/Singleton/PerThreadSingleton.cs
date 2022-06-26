namespace Singleton; 

// Other approaches lazy initialization, lazy and thread safety
// This approach gives us one singleton per thread, while others give only one singleton at all
// In short, one singleton instance for each thread
public class PerThreadSingleton {
    public sealed class PerThreadSingletonClass {
        private static ThreadLocal<PerThreadSingletonClass>
            _threadInstance = new ThreadLocal<PerThreadSingletonClass>(() => new PerThreadSingletonClass());
        
        public int Id;
        
        private PerThreadSingletonClass() {
            Id = Thread.CurrentThread.ManagedThreadId; // To verify the singleton 
        }

        public static PerThreadSingletonClass Instance => _threadInstance.Value;
    }

    public static void main() {
        var t1 = Task.Factory.StartNew(() => {
            Console.WriteLine($"t1: {PerThreadSingletonClass.Instance.Id}");
        });
        
        var t2 = Task.Factory.StartNew(() => {
            Console.WriteLine($"t2: {PerThreadSingletonClass.Instance.Id}");
            // To check that we get the same instance cause of per-thread singleton  
            Console.WriteLine($"t2: {PerThreadSingletonClass.Instance.Id}");
        });
        Task.WaitAll(t1, t2);

    }
}