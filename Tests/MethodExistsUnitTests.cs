using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Reflection;
using System;
using System.Runtime.CompilerServices;

[TestClass]
public class MethodExistsUnitTests : ReflectionAssertBaseUnitTests
{
    string methodName = "MethodExists";

    [TestMethod]
    public void MethodExists_method_should_exist()
    {
        var methodInfo = GetMethod(methodName);
        var messege = methodName + " method should exist";
        Assert.IsNotNull(methodInfo, messege);
    }

    [TestMethod]
    public void MethodExists_should_accept_two_parameters()
    {
        var expected = 2;
        var actual = GetParameters(methodName).Length;
        Assert.AreEqual(expected, actual, methodName + " should accept one parameter.");
    }

    [TestMethod]
    public void MethodExists_should_accept_an_extention_parameter()
    {
        var methodInfo = GetMethod(methodName);
        var isExtention = methodInfo.IsDefined(typeof(ExtensionAttribute), false);
        Assert.IsTrue(isExtention, message: methodName + " should accept an extention parameter.");
    }

    [TestMethod]
    public void MethodExists_should_accept_a_type_parameter()
    {
        var parameter = GetParameter(methodName);
        var expected = typeof(Type);
        var actual = parameter.ParameterType;
        var message = methodName + " should accept a type parameter.";
        Assert.AreEqual(expected, actual, message);
    }

    [TestMethod]
    public void MethodExists_should_accept_a_first_parameter_named_type()
    {
        var parameter = GetParameter(methodName);
        var expected = "type";
        var actual = parameter.Name;
        var message = methodName + " should accept a parameter named " + expected + ".";
        Assert.AreEqual(expected, actual, message);
    }

    [TestMethod]
    public void MethodExists_should_accept_a_second_parameter_of_type_string()
    {
        var parameter = GetParameter(methodName, 1);
        var expected = typeof(string);
        var actual = parameter.ParameterType;
        var message = methodName + " should accept an string parameter.";
        Assert.AreEqual(expected, actual, message);
    }

    [TestMethod]
    public void MethodExists_should_accept_a_second_parameter_named_methodName()
    {
        var parameter = GetParameter(methodName, 1);
        var expected = "methodName";
        var actual = parameter.Name;
        var message = methodName + " should accept a parameter named " + expected + ".";
        Assert.AreEqual(expected, actual, message);
    }

    [TestMethod]
    public void MethodExists_should_return_a_methodInfo()
    {
        var expected = typeof(MethodInfo);
        var actual = GetMethod(methodName).ReturnType;
        Assert.AreEqual(expected, actual, methodName + " should return a MethodInfo.");
    }

    [TestMethod]
    public void MethodExists_should_return_the_correct_methodInfo_with_a_valid_methodName()
    {
        var assembly = GetAssembly(assemblyName);
        var methodInfo = GetMethod(methodName: methodName);
        var classType = GetType(typeName);
        var expected = methodName;
        var actual = ((MethodInfo)methodInfo.Invoke(null, new object[] { classType, methodName })).Name;
        Assert.AreEqual(expected, actual, methodName + " should return the correct method with a valid methodName.");
    }

    [TestMethod]
    public void MethodExists_should_throw_an_AssertFailedException_with_an_invalid_methodName()
    {
        var value = GetMethodWithInvalidName();
        var expectedType = typeof(AssertFailedException);
        Assert.IsInstanceOfType(value, expectedType);
    }

    private Exception GetMethodWithInvalidName()
    {
        var assembly = GetAssembly(assemblyName);
        var methodInfo = GetMethod(methodName: methodName);
        var classType = GetType(typeName);
        Exception actualException = null;
        try
        {
            methodInfo.Invoke(null, new object[] { classType, "invalidMethodName" });
        }
        catch (TargetInvocationException exception)
        {
            actualException = exception.InnerException;
        }
        return actualException;
    }
}