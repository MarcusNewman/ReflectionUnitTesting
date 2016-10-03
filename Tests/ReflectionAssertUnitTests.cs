using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Reflection;
using System;

[TestClass]
public class ReflectionAssertUnitTests : ReflectionAssertBaseUnitTests
{
    public string typeName = "ReflectionAssert";

    [TestMethod]
    public void ReflectionAssertTypeShouldExist()
    {
        var type = GetType(namespaceName, typeName);
        var message = typeName + " type should exist.";
        Assert.IsNotNull(type, message);
    }

}