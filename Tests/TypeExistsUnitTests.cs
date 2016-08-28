using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Reflection;
using System;

[TestClass]
public class TypeExistsUnitTests : ReflectionAssertBaseUnitTests
{
    string methodName = "TypeExists";

    [TestMethod]
    public void TypeExistsMethodShouldExist()
    {
        MethodShouldExist(methodName);
    }

    [TestMethod]
    public void TypeExistsShouldTakeThreeParameters()
    {
        MethodShouldTakeNumberOfParameters(methodName, 3);
    }

    [TestMethod]
    public void TypeExistsShouldAcceptAnExtentionParameter()
    {
        var methodInfo = GetMethod(methodName);
        var type = typeof(System.Runtime.CompilerServices.ExtensionAttribute);
        var isExtention = methodInfo.IsDefined(type, false);
        var message = methodName + " should accept an extention parameter.";
        Assert.IsTrue(isExtention, message);
    }

    [TestMethod]
    public void TypeExistsShouldAcceptAnAssemblyParameter()
    {
        var parameter = GetParameter(methodName);
        var expected = typeof(Assembly);
        var actual = parameter.ParameterType;
        var message = methodName + " should accept an assembly parameter.";
        Assert.AreEqual(expected, actual, message);
    }

    [TestMethod]
    public void TypeExistsShouldAcceptAFirstParameterNamedAssembly()
    {
        var parameter = GetParameter(methodName);
        var expected = "assembly";
        var actual = parameter.Name;
        var message = methodName + " should accept a parameter named " + expected + ".";
        Assert.AreEqual(expected, actual, message);
    }

    [TestMethod]
    public void TypeExistsShouldAcceptASecondParameterOfTypeString()
    {
        var parameter = GetParameter(methodName, 2);
        var expected = typeof(string);
        var actual = parameter.ParameterType;
        var message = methodName + " should accept an string parameter.";
        Assert.AreEqual(expected, actual, message);
    }

    [TestMethod]
    public void TypeExistsShouldAcceptASecondParameterNamedNamespaceName()
    {
        var parameter = GetParameter(methodName, 2);
        var expected = "namespaceName";
        var actual = parameter.Name;
        var message = methodName + " should accept a parameter named " + expected + ".";
        Assert.AreEqual(expected, actual, message);
    }
    [TestMethod]
    public void TypeExistsShouldAcceptAThirdParameterOfTypeString()
    {
        var parameter = GetParameter(methodName, 3);
        var expected = typeof(string);
        var actual = parameter.ParameterType;
        var message = methodName + " should accept an string parameter.";
        Assert.AreEqual(expected, actual, message);
    }

    [TestMethod]
    public void TypeExistsShouldAcceptAThirdParameterNamedTypeName()
    {
        var parameter = GetParameter(methodName, 3);
        var expected = "typeName";
        var actual = parameter.Name;
        var message = methodName + " should accept a parameter named " + expected + ".";
        Assert.AreEqual(expected, actual, message);
    }

    [TestMethod]
    public void TypeExistsShouldReturnAType()
    {
        var expected = typeof(Type);
        var actual = GetMethod(methodName).ReturnType;
        Assert.AreEqual(expected, actual, methodName + " should return a Type.");
    }

    [TestMethod]
    public void TypeExistsShouldReturnTheCorrectTypeWithAValidTypeName()
    {
        var expected = typeName;
        var type = InvokeTypeExists(namespaceName, typeName);
        var actual = type.Name;
        var messege = methodName + " should return the correct type with a valid typeName.";
        Assert.AreEqual(expected, actual, messege);
    }

    [TestMethod]
    public void TypeExistsShouldThrowAnAssertFailedExceptionWithAnInvalidTypeName()
    {
        var value = TryInvokeTypeExistsWithInvalidName();
        var expectedType = typeof(AssertFailedException);
        var message = methodName + " should throw an assert failed exception with an invalid typeName.";
        Assert.IsInstanceOfType(value, expectedType, message);
    }

    [TestMethod]
    public void TypeExistsErrorMessageShouldBeTypeShouldExist()
    {
        var actualException = TryInvokeTypeExistsWithInvalidName();
        var actual = actualException.Message;
        var expected = invalidName + " type should exist.";
        var message = methodName + " error message should be " + expected;
        Assert.AreEqual(expected, actual, message);
    }

    Type InvokeTypeExists(string invokedNamespaceName, string invokedTypeName)
    {
        var methodInfo = GetMethod(methodName);
        var assembly = GetAssembly(assemblyName);
        var parameters = new object[] { assembly, invokedNamespaceName, invokedTypeName };
        return (Type)methodInfo.Invoke(null, parameters);
    }

    Exception TryInvokeTypeExistsWithInvalidName()
    {
        Exception actualException = null;
        try
        {
            InvokeTypeExists(namespaceName, invalidName);
        }
        catch (TargetInvocationException exception)
        {
            //Return InnerException to skip the target of the invocation layer.
            actualException = exception.InnerException;
        }
        return actualException;
    }
}