using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;

using Nega.Common;

namespace Nega.Entlib
{

    public class ResourceAuthorizationCallHandler : ICallHandler
    {

        private readonly IClientFinder clientFinder;
        private readonly IAuthenticationProvider authenticationProvider;
        private readonly IResourceAuthorizationProvider resourceAuthorizationProvider;

        public ResourceAuthorizationCallHandler(IUnityContainer container)
        {
            if (container == null)
            {
                throw new ArgumentNullException();
            }

            this.clientFinder = container.Resolve<IClientFinder>();
            this.authenticationProvider = container.Resolve<IAuthenticationProvider>();
            this.resourceAuthorizationProvider = container.Resolve<IResourceAuthorizationProvider>();
        }

        public int Order { get; set; }

        public IMethodReturn Invoke(IMethodInvocation input, GetNextHandlerDelegate getNext)
        {
            if (getNext == null)
            {
                throw new ArgumentNullException("getNext");
            }

            //Console.WriteLine("begin authorization");

            var methodCustomAttributes = input.MethodBase.GetCustomAttributes(typeof(ResourceAttribute), false);
            if (methodCustomAttributes != null && methodCustomAttributes.Count() > 0)
            {
                ResourceAttribute resourceAttribute = methodCustomAttributes[0] as ResourceAttribute;
                Check(resourceAttribute, input);
            }
            else
            {
                var objectCustomAttributes = input.MethodBase.DeclaringType.GetCustomAttributes(typeof(ResourceAttribute), false);
                if (objectCustomAttributes != null && objectCustomAttributes.Count() > 0)
                {
                    ResourceAttribute resourceAttribute = objectCustomAttributes[0] as ResourceAttribute;
                    Check(resourceAttribute, input);
                }
                else
                {
                    Check(null, input);
                }
            }

            IMethodReturn result = getNext()(input, getNext);

            //Console.WriteLine("end authorization");

            return result;
        }

        private void Check(ResourceAttribute resourceAttribute, IMethodInvocation input)
        {
            Client client = this.clientFinder.OperationClient;
            if (client == null)
            {
                throw new SecurityException();
            }

            string name = null;
            int method = 0;
            bool checkAccess = true;
            if (resourceAttribute != null)
            {
                name = resourceAttribute.Name;
                method = resourceAttribute.Method;
                checkAccess = resourceAttribute.CheckAccess;
            }
            if (!checkAccess)
            {
                return;
            }
            if (string.IsNullOrWhiteSpace(name))
            {
                name = input.MethodBase.DeclaringType.Name;
            }
            if (method < 1)
            {
                string methodName = input.MethodBase.Name.ToLower();
                if (methodName.StartsWith("save"))
                {
                    method = ResourceMethod.CREATE;
                }
                else if (methodName.StartsWith("update"))
                {
                    method = ResourceMethod.UPDATE;
                }
                else if (methodName.StartsWith("get") || methodName.StartsWith("retrieve"))
                {
                    method = ResourceMethod.RETRIEVE;
                }
                else if (methodName.StartsWith("delete"))
                {
                    method = ResourceMethod.DELETE;
                }
                else if (methodName.StartsWith("count"))
                {
                    method = ResourceMethod.COUNT;
                }
                else if (methodName.StartsWith("list"))
                {
                    method = ResourceMethod.LIST;
                }
                else
                {
                    return;
                }
            }

            string[] roles = this.authenticationProvider.ListRoles(client.Username);
            GenericPrincipal principal = new GenericPrincipal(new GenericIdentity(client.Username), roles);
            IEnumerable<ResourceAccess> accessList = this.resourceAuthorizationProvider.ListResourceAccesses(name, method);
            ResourcePermission permission = new ResourcePermission(principal, accessList);
            permission.Demand();

        }

    }

}
