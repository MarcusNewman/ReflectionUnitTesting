using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Reflection;
using System;

[TestClass]
public class AssemblyExistsUnitTests : ReflectionAssertBaseUnitTests
{
    string methodName = "AssemblyExists";

    [TestMethod]
    public void AssemblyExistsMethodShouldExist()
    {
        var methodInfo = GetMethod(methodName);
        var messege = methodName + " method should exist.";
        Assert.IsNotNull(methodInfo, messege);
    }

    [TestMethod]
    public void AssemblyExistsShouldAcceptOneParameter()
    {
        var parameters = GetParameters(methodName);
        var expected = 1;
        var actual = parameters.Length;
        var message = methodName + " should accept one parameter.";
        Assert.AreEqual(expected, actual, message);
    }

    [TestMethod]
    public void AssemblyExistsShouldAcceptAStringParameter()
    {
        var parameter = GetParameter(methodName);
        var expected = typeof(string);
        var actual = parameter.ParameterType;
        var message = methodName + " should accept a string parameter.";
        Assert.AreEqual(expected, actual, message);
    }

    [TestMethod]
    public void AssemblyExistsShouldAcceptAParameterNamedAssemblyName()
    {
        var parameter = GetParameter(methodName);
        var expected = "assemblyName";
        var actual = parameter.Name;
        var message = methodName + " should accept a parameter named " + expected + ".";
        Assert.AreEqual(expected, actual, message);
    }

    [TestMethod]
    public void AssemblyExistsShouldBeStatic()
    {
        var methodInfo = GetMethod(methodName);
        var isStatic = methodInfo.IsStatic;
        var message = methodName + " should be static.";
        Assert.IsTrue(isStatic, message);
    }

    [TestMethod]
    public void AssemblyExistsShouldReturnAnAssembly()
    {
        var methodInfo = GetMethod(methodName);
        var actual = methodInfo.ReturnType;
        var expected = typeof(Assembly);
        var message = methodName + " should return an Assembly.";
        Assert.AreEqual(actual, expected, message);
    }

    [TestMethod]
    public void AssemblyExistsShouldReturnTheCorrectAssemblyWithAValidAssemblyName()
    {
        var assembly = InvokeAssemblyExists(assemblyName);
        var actual = assembly.ManifestModule.Name;
        var expected = assemblyName;
        var message = methodName + " should return the correct assembly with a valid assemblyName.";
        Assert.AreEqual(actual, expected, message);
    }

    [TestMethod]
    public void AssemblyExistsShouldThrowAnAssertFailedExceptionIfTheAssemblyIsNotFound()
    {
        var actualException = TryToInvokeAssemblyExistsWithInvalidName();
        var expectedType = typeof(AssertFailedException);
        var message = methodName + " should throw an AssertFailed exception if the assembly is not found.";
        Assert.IsInstanceOfType(actualException, expectedType, message);
    }

    [TestMethod]
    public void AssemblyExistsErrorMessageShouldBeAssemblyShouldExist()
    {
        var actualException = TryToInvokeAssemblyExistsWithInvalidName();
        var actual = actualException.Message;
        var expected = invalidName + " assembly should exist.";
        var message = methodName + " error message should be " + expected;
        Assert.AreEqual(expected, actual, message);
    }

    [TestMethod]
    public void AssemblyExistsErrorShouldContainTheInnerException()
    {
        var actualException = TryToInvokeAssemblyExistsWithInvalidName();
        var value = actualException.InnerException;
        var message = methodName + " error should contain the inner exception.";
        Assert.IsNotNull(value, message);
    }

    Assembly InvokeAssemblyExists(string assemblyName)
    {
        var methodInfo = GetMethod(methodName);
        var parameters = new object[] { assemblyName };
        return (Assembly)methodInfo.Invoke(null, parameters);
    }

    Exception TryToInvokeAssemblyExistsWithInvalidName()
    {
        Exception result = null;
        try
        {
            InvokeAssemblyExists(invalidName);
        }
        catch (Exception exception)
        {
            //Return InnerException to skip the target of the invocation layer.
            result = exception.InnerException;
        }
        return result;
    }

}