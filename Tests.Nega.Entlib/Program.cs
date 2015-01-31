using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;
using Microsoft.Practices.EnterpriseLibrary.Logging;

using Nega.Entlib;
using Microsoft.Practices.EnterpriseLibrary.PolicyInjection;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;

namespace Tests.Nega.Entlib
{
    class Program
    {
        static void Main(string[] args)
        {
            //UsingPIABWithContainer();

            Console.WriteLine();
            Console.WriteLine(new string('=', 79));
            Console.WriteLine();

            UsingPIABWithFacade();

            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }

        static void UsingPIABWithContainer()
        {
            ConfigureLogger();
            using (var container = new UnityContainer())
            {

                container.AddNewExtension<Interception>();

                container.RegisterType<InterceptableObj>(new Interceptor<TransparentProxyInterceptor>(), 
                    new InterceptionBehavior<PolicyInjectionBehavior>());
                container.Configure<Interception>().AddPolicy("Save")
                     .AddMatchingRule<MemberNameMatchingRule>(new InjectionConstructor(new InjectionParameter("Save*")))
                     .AddCallHandler<TransactionCallHandler>(new ContainerControlledLifetimeManager(), new InjectionConstructor());
                container.Configure<Interception>().AddPolicy("Update")
                     .AddMatchingRule<MemberNameMatchingRule>(new InjectionConstructor(new InjectionParameter("Update*")))
                     .AddCallHandler<TransactionCallHandler>(new ContainerControlledLifetimeManager(), new InjectionConstructor());


                var obj = container.Resolve<InterceptableObj>();

                // Use the interceptable type.
                Console.WriteLine("*** Invoking the Save method ***");
                obj.Save();
                Console.WriteLine("*** Invoking the SaveSomething method ***");
                obj.SaveSomething();
                Console.WriteLine("*** Invoking the SaveAnotherthing method ***");
                obj.SaveAnotherthing();
                Console.WriteLine("*** Invoking the UpdateSomething method ***");
                obj.UpdateSomething();
                Console.WriteLine("*** Invoking the DeleteSomething method ***");
                obj.DeleteSomething();
                Console.WriteLine("*** Invoking the GetSomething method ***");
                obj.GetSomething();

            }
        }

        static void UsingPIABWithFacade()
        {
            // Configure the logger first.
            ConfigureLogger();

            // Bootstrap the Policy Injection Application Block
            // and configure the interceptable type.
            // This example loads the configuration from the config file.
            PolicyInjection.SetPolicyInjector(new PolicyInjector(new SystemConfigurationSource(false)), false);

            var obj = PolicyInjection.Create<InterceptableObj>();

            // Use the interceptable type.
            Console.WriteLine("*** Invoking the Save method ***");
            obj.Save();
            Console.WriteLine("*** Invoking the SaveSomething method ***");
            obj.SaveSomething();
            Console.WriteLine("*** Invoking the SaveAnotherthing method ***");
            obj.SaveAnotherthing();
            Console.WriteLine("*** Invoking the UpdateSomething method ***");
            obj.UpdateSomething();
            Console.WriteLine("*** Invoking the DeleteSomething method ***");
            obj.DeleteSomething();
            Console.WriteLine("*** Invoking the GetSomething method ***");
            obj.GetSomething();

        }

        private static void ConfigureLogger()
        {
            //var configuration = new LoggingConfiguration();
            //configuration.AddLogSource("default", new ConsoleTraceListener());
            //configuration.DefaultSource = "default";
            //var logWriter = new LogWriter(configuration);
            //Logger.SetLogWriter(logWriter, false);
        }

    }


    public class InterceptableObj : MarshalByRefObject
    {
        public void Save() { }

        public void SaveSomething() { }

        public void SaveAnotherthing() { }

        public void UpdateSomething() { }

        [TransactionCallHandler]
        public void DeleteSomething() { }

        public object GetSomething() { return new object(); }
    }

}
