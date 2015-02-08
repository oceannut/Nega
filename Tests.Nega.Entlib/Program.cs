using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;
using Microsoft.Practices.EnterpriseLibrary.Logging;
using Microsoft.Practices.EnterpriseLibrary.PolicyInjection;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;

using Nega.Entlib;

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

            //UsingPIABWithFacade();

            Test();

            //Test2();

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

            var obj2 = PolicyInjection.Create<InterceptableObj2>();
            Console.WriteLine("*** Invoking the Save method ***");
            obj2.Save();
            Console.WriteLine("*** Invoking the second Save method ***");
            obj2.Save(new object());
            Console.WriteLine("*** Invoking the SaveAnother method ***");
            obj2.SaveAnother();

            Console.WriteLine();
            Console.WriteLine(new string('-', 79));
            Console.WriteLine();

            var obj = PolicyInjection.Create<InterceptableObj>();
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

        static void Test()
        {
            using (var container = new UnityContainer())
            {

                container.AddNewExtension<Interception>();

                container.RegisterType<IEmpolyeeService, EmpolyeeServiceImpl>(new Interceptor<InterfaceInterceptor>(),
                    new InterceptionBehavior<PolicyInjectionBehavior>());
                //container.RegisterType<IEmpolyeeService, EmpolyeeServiceImpl>();
                container.Configure<Interception>().AddPolicy("Save")
                     .AddMatchingRule<MemberNameMatchingRule>(new InjectionConstructor(new InjectionParameter("Save*")))
                     .AddCallHandler<TransactionCallHandler>(new ContainerControlledLifetimeManager(), new InjectionConstructor());
                container.Configure<Interception>().AddPolicy("Update")
                     .AddMatchingRule<MemberNameMatchingRule>(new InjectionConstructor(new InjectionParameter("Update*")))
                     .AddCallHandler<TransactionCallHandler>(new ContainerControlledLifetimeManager(), new InjectionConstructor());


                var obj = container.Resolve<IEmpolyeeService>();

                // Use the interceptable type.
                Console.WriteLine("*** Invoking the Save method ***");
                obj.SaveEmpolyee(new Emoployee("Tom"));
                Console.WriteLine("*** Invoking the Update method ***");
                obj.UpdateEmpolyee(new Emoployee("Jerry"));
                Console.WriteLine("*** Invoking the Get method ***");
                obj.GetEmpolyee();

            }
        }

        static void Test2()
        {
            using (var container = new UnityContainer())
            {

                container.AddNewExtension<Interception>();

                container.RegisterType<Drawer>(new Interceptor<VirtualMethodInterceptor>(),
                    new InterceptionBehavior<PolicyInjectionBehavior>());
                container.Configure<Interception>().AddPolicy("Draw")
                     .AddMatchingRule<MemberNameMatchingRule>(new InjectionConstructor(new InjectionParameter("Draw*")))
                     .AddCallHandler<TransactionCallHandler>(new ContainerControlledLifetimeManager(), new InjectionConstructor());
                container.Configure<Interception>().AddPolicy("Update")
                     .AddMatchingRule<MemberNameMatchingRule>(new InjectionConstructor(new InjectionParameter("Update*")))
                     .AddCallHandler<TransactionCallHandler>(new ContainerControlledLifetimeManager(), new InjectionConstructor());


                var obj = container.Resolve<Drawer>();

                // Use the interceptable type.
                Console.WriteLine("*** Invoking the DrawSomthing method ***");
                obj.DrawSomthing();
                Console.WriteLine("*** Invoking the DrawCircle method ***");
                obj.DrawCircle();
                Console.WriteLine("*** Invoking the Update method ***");
                obj.Update();

            }
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

    public class InterceptableObj2 : MarshalByRefObject
    {
        public void Save() { }

        public void Save(object o) { }

        public void SaveAnother() { }

        public void Modified() { }

    }

    public class Emoployee
    {
        public Emoployee(string name)
        {
            Console.WriteLine("Hi, my name is " + name);
        }
    }

    public interface IEmpolyeeService
    {
        void SaveEmpolyee(Emoployee entity);

        void UpdateEmpolyee(Emoployee entity);

        Emoployee GetEmpolyee();
    }

    public class EmpolyeeServiceImpl : IEmpolyeeService
    {

        public void SaveEmpolyee(Emoployee entity)
        {
            Console.WriteLine("save employee");
        }

        public void UpdateEmpolyee(Emoployee entity)
        {
            Console.WriteLine("update employee");
        }

        public Emoployee GetEmpolyee()
        {
            Console.WriteLine("get employee");
            return new Emoployee("Always");
        }
    }

    public class Drawer
    {
        public virtual void DrawSomthing()
        {
            Console.WriteLine("draw somthing");
        }

        public void DrawCircle()
        {
            Console.WriteLine("draw circle");
        }

        public void Update()
        {
            Console.WriteLine("update");
        }
    }

}
