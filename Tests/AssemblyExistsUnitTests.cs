using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Reflection;
using System;
using System.Collections.Generic;

[TestClass]
public class AssemblyExistsUnitTests : ReflectionAssertBaseUnitTests
{
    string methodName = "AssemblyExists";

    [TestMethod]
    public void AssemblyExistsMethodShouldExist()
    {
        GetAssemblyExistsMethod();
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
        var methodInfo = GetAssemblyExistsMethod();
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

    public MethodInfo GetAssemblyExistsMethod()
    {
        return GetMethod(
                    methodName: methodName,
                    shouldBeStatic: true,
                    shouldReturnType: typeof(Assembly),
                    parameterTypesAndNames: new List<Tuple<Type, string>>() { Tuple.Create(typeof(string), "assemblyName") }
                    );
    }
}