using Autofac;

namespace Decorator; 

public class DecoratorInDependencyInjection {
    public interface IReportingService {
        void Report();
    }

    public class ReportingService : IReportingService {
        public void Report() {
            Console.WriteLine("Here is your report");
        }
    }
    
    // Now we need a component which is able to provide Reporting and also supports logging as well.
    public class ReportingServiceWithLogging : IReportingService {
        private IReportingService decorated;
        
        public ReportingServiceWithLogging(IReportingService decorated) {
            this.decorated = decorated;
        }

        public void Report() {
            Console.WriteLine("Commencing log...");
            decorated.Report();
            Console.WriteLine("Ending log...");
        }
    }

    public static void main() {
        var b = new ContainerBuilder();
        /*
        b.RegisterType<ReportingServiceWithLogging>().As<IReportingService>();
        The problem is that if you create Register a ReportingServiceWithLogging, it creates another IReportingService
        This generate an infinite loop 
        */
        b.RegisterType<ReportingService>().Named<IReportingService>("reporting");
        b.RegisterDecorator<IReportingService>(
            (context, service) => new ReportingServiceWithLogging(service), "reporting"
            );

        using (var c = b.Build()) {
            var r = c.Resolve<IReportingService>();
            r.Report();
        }
    }
}