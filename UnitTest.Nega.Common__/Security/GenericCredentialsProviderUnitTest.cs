using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Nega.Common;

namespace UnitTest.Nega.Common__.Security
{
    [TestClass]
    public class GenericCredentialsProviderUnitTest
    {
        [TestMethod]
        public void TestMethod1()
        {

            GenericCredentialsProvider provider = new GenericCredentialsProvider();
            string s1 = provider.GenerateUserToken("zsp");
            string s2 = provider.GenerateUserToken("zsp");
            Assert.AreNotEqual(s1, s2);
            string s3 = provider.GenerateUserToken("lj");
            Assert.AreNotEqual(s1, s3);
            Assert.AreNotEqual(s2, s3);

            Console.WriteLine(s1);
            Console.WriteLine(s2);
            Console.WriteLine(s3);
            Console.WriteLine();

        }
    }
}
