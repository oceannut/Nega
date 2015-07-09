using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using R = Nega.Common.Properties.Resources;

namespace Nega.Common
{

    public class ResourceMethod
    {

        public const int CREATE = 0x1;
        public const int UPDATE = 0x2;
        public const int RETRIEVE = 0x4;
        public const int DELETE = 0x8;
        public const int COUNT = 0x10;
        public const int LIST = 0x20;

        public const int SIGNIN = 0x100;
        public const int SIGNOUT = 0x200;

        public static readonly ResourceMethod METHOD_CREATE = new ResourceMethod(CREATE, R.MethodCreate);
        public static readonly ResourceMethod METHOD_UPDATE = new ResourceMethod(UPDATE, R.MethodUpdate);
        public static readonly ResourceMethod METHOD_RETRIEVE = new ResourceMethod(RETRIEVE, R.MethodRetrieve);
        public static readonly ResourceMethod METHOD_DELETE = new ResourceMethod(DELETE, R.MethodDelete);
        public static readonly ResourceMethod METHOD_COUNT = new ResourceMethod(COUNT, R.MethodCount);
        public static readonly ResourceMethod METHOD_LIST = new ResourceMethod(LIST, R.MethodList);

        public int Key { get; private set; }

        public string Description { get; private set; }

        public ResourceMethod(int key, string description)
        {
            if (key < 0x1)
            {
                throw new ArgumentOutOfRangeException();
            }

            this.Key = key;
            this.Description = description;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            if (ReferenceEquals(this, obj))
            {
                return true;
            }
            ResourceMethod target = obj as ResourceMethod;
            if (target == null)
            {
                return false;
            }
            return this.Key == target.Key;
        }

        public override int GetHashCode()
        {
            return this.Key;
        }

        public override string ToString()
        {
            return this.Description;
        }

    }

}
