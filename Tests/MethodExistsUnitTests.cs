using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Reflection;
using System;

[TestClass]
public class MethodExistsUnitTests : ReflectionAssertBaseUnitTests
{
    string methodName = "MethodExists";

    [TestMethod]
    public void MethodExists_method_should_exist()
    {
        var methodInfo = GetMethod(methodName);
        var messege = methodName + " method should exist.";
        Assert.IsNotNull(methodInfo, messege);
    }

    [TestMethod]
    public void MethodExists_should_accept_two_parameters()
    {
        var expected = 2;
        var parameter = GetParameters(methodName);
        var actual = parameter.Length;
        var messege = methodName + " should accept two parameters.";
        Assert.AreEqual(expected, actual, messege);
    }

    [TestMethod]
    public void MethodExists_should_accept_an_extention_parameter()
    {
        var methodInfo = GetMethod(methodName);
        var type = typeof(System.Runtime.CompilerServices.ExtensionAttribute);
        var isExtention = methodInfo.IsDefined(type, false);
        var message = methodName + " should accept an extention parameter.";
        Assert.IsTrue(isExtention, message);
    }

    [TestMethod]
    public void MethodExists_should_accept_a_type_parameter()
    {
        var expected = typeof(Type);
        var parameter = GetParameter(methodName);
        var actual = parameter.ParameterType;
        var message = methodName + " should accept a type parameter.";
        Assert.AreEqual(expected, actual, message);
    }

    [TestMethod]
    public void MethodExists_should_accept_a_first_parameter_named_type()
    {
        var expected = "type";
        var parameter = GetParameter(methodName);
        var actual = parameter.Name;
        var message = methodName + " should accept a parameter named " + expected + ".";
        Assert.AreEqual(expected, actual, message);
    }

    [TestMethod]
    public void MethodExists_should_accept_a_second_parameter_of_type_string()
    {
        var expected = typeof(string);
        var parameter = GetParameter(methodName, 1);
        var actual = parameter.ParameterType;
        var message = methodName + " should accept an string parameter.";
        Assert.AreEqual(expected, actual, message);
    }

    [TestMethod]
    public void MethodExists_should_accept_a_second_parameter_named_methodName()
    {
        var expected = "methodName";
        var parameter = GetParameter(methodName, 1);
        var actual = parameter.Name;
        var message = methodName + " should accept a parameter named " + expected + ".";
        Assert.AreEqual(expected, actual, message);
    }

    [TestMethod]
    public void MethodExists_should_return_a_methodInfo()
    {
        var expected = typeof(MethodInfo);
        var method = GetMethod(methodName);
        var actual = method.ReturnType;
        var messege = methodName + " should return a MethodInfo.";
        Assert.AreEqual(expected, actual, messege);
    }

    [TestMethod]
    public void MethodExists_should_return_the_correct_methodInfo_with_a_valid_methodName()
    {
        var expected = methodName;
        var methodInfo = InvokeMethodExists(methodName);
        var actual = methodInfo.Name;
        var messege = methodName + " should return the correct method with a valid methodName.";
        Assert.AreEqual(expected, actual, messege);
    }

    [TestMethod]
    public void MethodExists_should_throw_an_AssertFailedException_with_an_invalid_methodName()
    {
        var expectedType = typeof(AssertFailedException);
        var value = TryInvokeMethodExistsWithInvalidName();
        var message = " should throw an AssertFailedException with an invalid methodName";
        Assert.IsInstanceOfType(value, expectedType, message);
    }

    [TestMethod]
    public void MethodExists_error_message_should_be_method_should_exist()
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
        var type = GetType(typeName);
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