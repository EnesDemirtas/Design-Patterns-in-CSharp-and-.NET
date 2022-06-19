using System.Security.Cryptography;

namespace InterfaceSegregationPrinciple;

public class Program {
    // Your interfaces should be segregated so that nobody who implements your interface has to implement functions
    // which they don't actually need.
    
    // Instead of huge interfaces that contain too much stuff, divide the functionalities into different interfaces. 
    public class Document {
        
    }
    
    // Problematic interface
    public interface IMachine {
        void Print(Document document); 
        void Scan(Document document);
        void Fax(Document document);
    }

    public class MultiFunctionPrinter : IMachine {
        public void Print(Document document) {
            //
        }

        public void Scan(Document document) {
            //
        }

        public void Fax(Document document) {
           //
        }
    }
    
    // An old-fashion printer cannot do all of these 3 functions...
    public class OldFashionedPrinter : IMachine {
        public void Print(Document document) {
            // 
        }

        public void Scan(Document document) {
            throw new NotImplementedException();
        }

        public void Fax(Document document) {
            throw new NotImplementedException();
        }
    }
    
    // Create more atomic interfaces. 
    
    public interface IPrinter {
        void Print(Document document);
    }

    public interface IScanner {
        void Scan(Document document);
    }

    public class Photocopier : IPrinter, IScanner {
        public void Print(Document document) {
            throw new NotImplementedException();
        }

        public void Scan(Document document) {
            throw new NotImplementedException();
        }
    }

    public interface IMultiFunctionDevice : IScanner, IPrinter //...
    {
        
    }
    
    public class MultiFunctionMachine : IMultiFunctionDevice {
        private IPrinter _printer;
        private IScanner _scanner;

        public MultiFunctionMachine(IPrinter printer, IScanner scanner) {
            _printer = printer;
            _scanner = scanner;
        }

        public void Print(Document document) {
            _printer.Print(document);
        }

        public void Scan(Document document) {
            _scanner.Scan(document);
        }     // decorator

    }
    

    public static void Main() {
        
    }
}