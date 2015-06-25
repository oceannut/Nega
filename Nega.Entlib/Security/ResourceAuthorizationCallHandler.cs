using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Practices.Unity.InterceptionExtension;

using Microsoft.Practices.Unity;

using Nega.Common;

namespace Nega.Entlib
{

    public class ResourceAuthorizationCallHandler : ICallHandler
    {

        private readonly IPrincipalProvider principalProvider;
        private readonly IResourceAuthorizationProvider resourceAuthorizationProvider;

        public ResourceAuthorizationCallHandler(IUnityContainer container)
        {
            if (container == null)
            {
                throw new ArgumentNullException();
            }

            this.principalProvider = container.Resolve<IPrincipalProvider>();
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
            string name = null;
            string method = null;
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
            //if (string.IsNullOrWhiteSpace(method))
            //{
            //    string methodName = input.MethodBase.Name.ToLower();
            //    if (methodName.StartsWith(Resource.METHOD_SAVE))
            //    {
            //        method = Resource.METHOD_SAVE;
            //    }
            //    else if (methodName.StartsWith(Resource.METHOD_UPDATE))
            //    {
            //        method = Resource.METHOD_UPDATE;
            //    }
            //    else if (methodName.StartsWith(Resource.METHOD_GET))
            //    {
            //        method = Resource.METHOD_GET;
            //    }
            //    else if (methodName.StartsWith(Resource.METHOD_DELETE))
            //    {
            //        method = Resource.METHOD_DELETE;
            //    }
            //    else if (methodName.StartsWith(Resource.METHOD_LIST))
            //    {
            //        method = Resource.METHOD_LIST;
            //    }
            //    else
            //    {
            //        return;
            //    }
            //}

            IEnumerable<ResourceAccess> accessList = resourceAuthorizationProvider.ListResourceAccess(name, method);
            ResourcePermission permission = new ResourcePermission(principalProvider.Principal, accessList);
            permission.Demand();

        }

    }

}
