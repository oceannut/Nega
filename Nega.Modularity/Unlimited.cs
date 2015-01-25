using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nega.Modularity
{
    internal class Unlimited : IModuleLicence
    {

        private static readonly Unlimited solo = new Unlimited();

        private Unlimited() { }

        internal static Unlimited Solo
        {
            get { return solo; }
        }

        public string Sn
        {
            get { return string.Empty; }
        }

        public DateTime Deadline
        {
            get { return DateTime.MaxValue; }
        }

        public string User
        {
            get { return string.Empty; }
        }

    }
}
