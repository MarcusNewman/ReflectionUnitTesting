using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Reflection;
using System;

[TestClass]
public class MethodExistsUnitTests : ReflectionAssertBaseUnitTests
{
    string methodName = "MethodExists";

    [TestMethod]
    public void MethodExistsMethodShouldExist()
    {
        MethodShouldExist(methodName);
    }

    [TestMethod]
    public void MethodExistsShouldTakeTwoParameters()
    {
        MethodShouldTakeNumberOfParameters(methodName, 2);
    }

    [TestMethod]
    public void MethodExistsShouldAcceptAnExtentionParameter()
    {
        var methodInfo = GetMethod(methodName);
        var type = typeof(System.Runtime.CompilerServices.ExtensionAttribute);
        var isExtention = methodInfo.IsDefined(type, false);
        var message = methodName + " should accept an extention parameter.";
        Assert.IsTrue(isExtention, message);
    }

    [TestMethod]
    public void MethodExistsShouldAcceptATypeParameter()
    {
        var expected = typeof(Type);
        var parameter = GetParameter(methodName);
        var actual = parameter.ParameterType;
        var message = methodName + " should accept a type parameter.";
        Assert.AreEqual(expected, actual, message);
    }

    [TestMethod]
    public void MethodExistsShouldAcceptAFirstParameterNamedType()
    {
        var expected = "type";
        var parameter = GetParameter(methodName);
        var actual = parameter.Name;
        var message = methodName + " should accept a parameter named " + expected + ".";
        Assert.AreEqual(expected, actual, message);
    }

    [TestMethod]
    public void MethodExistsShouldAcceptASecondParameterOfTypeString()
    {
        var expected = typeof(string);
        var parameter = GetParameter(methodName, 2);
        var actual = parameter.ParameterType;
        var message = methodName + " should accept an string parameter.";
        Assert.AreEqual(expected, actual, message);
    }

    [TestMethod]
    public void MethodExistsShouldAcceptASecondParameterNamedMethodName()
    {
        var expected = "methodName";
        var parameter = GetParameter(methodName, 2);
        var actual = parameter.Name;
        var message = methodName + " should accept a parameter named " + expected + ".";
        Assert.AreEqual(expected, actual, message);
    }

    [TestMethod]
    public void MethodExistsShouldReturnAMethodInfo()
    {
        var expected = typeof(MethodInfo);
        var method = GetMethod(methodName);
        var actual = method.ReturnType;
        var messege = methodName + " should return a MethodInfo.";
        Assert.AreEqual(expected, actual, messege);
    }

    [TestMethod]
    public void MethodExistsShouldReturnTheCorrectMethodInfoWithAValidMethodName()
    {
        var expected = methodName;
        var methodInfo = InvokeMethodExists(methodName);
        var actual = methodInfo.Name;
        var messege = methodName + " should return the correct method with a valid methodName.";
        Assert.AreEqual(expected, actual, messege);
    }

    [TestMethod]
    public void MethodExistsShouldThrowAnAssertFailedExceptionWithAnInvalidMethodName()
    {
        var expectedType = typeof(AssertFailedException);
        var value = TryInvokeMethodExistsWithInvalidName();
        var message = " should throw an AssertFailedException with an invalid methodName";
        Assert.IsInstanceOfType(value, expectedType, message);
    }

    [TestMethod]
    public void MethodExistsErrorMessageShouldBeMethodShouldExist()
    {
        var actualException = TryInvokeMethodExistsWithInvalidName();
        var actual = actualException.Message;
        var expected = invalidName + " method should exist.";
        var message = methodName + " error message should be " + expected;
        Assert.AreEqual(expected, actual, message);
    }

    MethodInfo InvokeMethodExists(string invokedMethodName)
    {
        var methodInfo = GetMethod(methodName);
        var type = GetType(namespaceName, typeName);
        var parameters = new object[] { type, invokedMethodName };
        return (MethodInfo)methodInfo.Invoke(null, parameters);
    }

    Exception TryInvokeMethodExistsWithInvalidName()
    {
        Exception actualException = null;
        try
        {
            InvokeMethodExists(invalidName);
        }
        catch (TargetInvocationException exception)
        {
            //Return InnerException to skip the target of the invocation layer.
            actualException = exception.InnerException;
        }
        return actualException;
    }
}