using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ReflectionUnitTesting.Tests
{
    [TestClass]
    public class MemberExistsUnitTests : ReflectionAssertUnitTests
    {
        public string typeName = "MemberExists";

        [TestMethod]
        public void MemberExitsTypeShouldExist()
        {
            var type = GetType(namespaceName, typeName);
            var message = typeName + " type should exist.";
            Assert.IsNotNull(type, message);
        }

    }
}
