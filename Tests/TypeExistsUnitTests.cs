using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Reflection;
using System;
using System.Runtime.CompilerServices;

[TestClass]
public class TypeExistsUnitTests : ReflectionAssertBaseUnitTests
{
    string methodName = "TypeExists";

    [TestMethod]
    public void TypeExists_method_should_exist()
    {
        var methodInfo = GetMethod(methodName);
        var messege = methodName + " method should exist";
        Assert.IsNotNull(methodInfo, messege);
    }

    [TestMethod]
    public void TypeExists_should_accept_two_parameters()
    {
        var expected = 2;
        var actual = GetParameters(methodName).Length;
        Assert.AreEqual(expected, actual, methodName + " should accept one parameter.");
    }

    [TestMethod]
    public void TypeExists_should_accept_an_extention_parameter()
    {
        var methodInfo = GetMethod(methodName);
        var isExtention = methodInfo.IsDefined(typeof(ExtensionAttribute), false);
        Assert.IsTrue(isExtention, message: methodName + " should accept an extention parameter.");
    }

    [TestMethod]
    public void TypeExists_should_accept_an_assembly_parameter()
    {
        var parameter = GetParameter(methodName);
        var expected = typeof(Assembly);
        var actual = parameter.ParameterType;
        var message = methodName + " should accept an assembly parameter.";
        Assert.AreEqual(expected, actual, message);
    }

    [TestMethod]
    public void TypeExists_should_accept_a_first_parameter_named_assembly()
    {
        var parameter = GetParameter(methodName);
        var expected = "assembly";
        var actual = parameter.Name;
        var message = methodName + " should accept a parameter named " + expected + ".";
        Assert.AreEqual(expected, actual, message);
    }

    [TestMethod]
    public void TypeExists_should_accept_a_second_parameter_of_type_string()
    {
        var parameter = GetParameter(methodName, 1);
        var expected = typeof(string);
        var actual = parameter.ParameterType;
        var message = methodName + " should accept an string parameter.";
        Assert.AreEqual(expected, actual, message);
    }

    [TestMethod]
    public void TypeExists_should_accept_a_second_parameter_named_typeName()
    {
        var parameter = GetParameter(methodName, 1);
        var expected = "typeName";
        var actual = parameter.Name;
        var message = methodName + " should accept a parameter named " + expected + ".";
        Assert.AreEqual(expected, actual, message);
    }

    [TestMethod]
    public void TypeExists_should_return_a_type()
    {
        var expected = typeof(Type);
        var actual = GetMethod(methodName).ReturnType;
        Assert.AreEqual(expected, actual, methodName + " should return a Type.");
    }

    [TestMethod]
    public void TypeExists_should_return_the_correct_type_with_a_valid_typeName()
    {
        var assembly = GetAssembly(assemblyName);
        var methodInfo = GetMethod(methodName: methodName);
        var expected = typeName;
        var actual = ((Type)methodInfo.Invoke(null, new object[] { assembly, typeName })).Name;
        Assert.AreEqual(expected, actual, methodName + " should return the correct type with a valid typeName.");
    }

    [TestMethod]
    public void TypeExists_should_throw_an_AssertFailedException_with_an_invalid_typeName()
    {
        var value = GetTypeWithInvalidName();
        var expectedType = typeof(AssertFailedException);
        Assert.IsInstanceOfType(value, expectedType);
    }

    Exception GetTypeWithInvalidName()
    {
        var assembly = GetAssembly(assemblyName);
        var methodInfo = GetMethod(methodName: methodName);
        Exception actualException = null;
        try
        {
            methodInfo.Invoke(null, new object[] { assembly, "invalidTypeName" });
        }
        catch (TargetInvocationException exception)
        {
            actualException = exception.InnerException;
        }
        return actualException;
    }
}