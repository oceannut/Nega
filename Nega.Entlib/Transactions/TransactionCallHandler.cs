using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.Unity.InterceptionExtension;

namespace Nega.Entlib
{

    [ConfigurationElementType(typeof(TransactionCallHandlerData))]
    public class TransactionCallHandler : ICallHandler
    {

        private TransactionScopeOption transactionScopeOption = TransactionScopeOption.Required;
        private TransactionOptions transactionOptions;
        private EnterpriseServicesInteropOption interopOption = EnterpriseServicesInteropOption.None;

        public int Order { get; set; }

        public TransactionCallHandler() { }

        public TransactionCallHandler(NameValueCollection attributes)
        {

        }

        public TransactionCallHandler(TransactionOptions transactionOptions)
        {
            this.transactionOptions = transactionOptions;
        }

        public TransactionCallHandler(TransactionScopeOption transactionScopeOption, 
            TransactionOptions transactionOptions, 
            EnterpriseServicesInteropOption interopOption)
        {
            this.transactionScopeOption = transactionScopeOption;
            this.transactionOptions = transactionOptions;
            this.interopOption = interopOption;
        }

        public IMethodReturn Invoke(IMethodInvocation input, GetNextHandlerDelegate getNext)
        {
            if (getNext == null)
            {
                throw new ArgumentNullException("getNext");
            }

            
            using (TransactionScope scope = CreateTransactionScope())
            {
                Console.WriteLine("my god, begin transaction");

                IMethodReturn result = getNext()(input, getNext);
                if (result.Exception == null)
                {
                    scope.Complete();
                }

                Console.WriteLine("yeah god, end transaction");

                return result;
            }
        }

        private TransactionScope CreateTransactionScope()
        {
            return new TransactionScope(transactionScopeOption, transactionOptions, interopOption);
        }

    }

}
