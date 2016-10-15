using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Reflection;
using System;
using System.Collections.Generic;

[TestClass]
public class MethodExistsUnitTests : ReflectionAssertBaseUnitTests
{
    string methodName = "MethodExists";

    [TestMethod]
    public void MethodShouldExist()
    {
        GetMethod();
    }

    public MethodInfo GetMethod()
    {
        return GetMethod(
            methodName: methodName,
            shouldBeStatic: true,
            expectedReturnType: typeof(MethodInfo),
            shouldBeAnExtentionMethod: true,
            parameterTypesAndNames: new List<Tuple<Type, string>>() {
                Tuple.Create(typeof(Type), "type"),
                Tuple.Create(typeof(string), "methodName"),
                Tuple.Create(typeof(bool?), "shouldBeStatic"),
                Tuple.Create(typeof(Type), "shouldReturnType"),
                Tuple.Create(typeof(bool?), "shouldBeAnExtentionMethod"),
                Tuple.Create(typeof(List<Tuple<Type, string>>), "parameterTypesAndNames") });
    }

    [TestMethod]
    public void ShouldReturnTheCorrectMethodInfoWithAValidMethodName()
    {
        var expected = methodName;
        var methodInfo = InvokeMethodExists(methodName);
        var actual = methodInfo.Name;
        var messege = methodName + " should return the correct method with a valid methodName.";
        Assert.AreEqual(expected, actual, messege);
    }

    [TestMethod]
    public void ShouldThrowAnAssertFailedExceptionWithAnInvalidMethodName()
    {
        var expectedType = typeof(AssertFailedException);
        var value = TryInvokeMethodExists(invalidName);
        var message = " should throw an AssertFailedException with an invalid methodName.";
        Assert.IsInstanceOfType(value, expectedType, message);
    }

    [TestMethod]
    public void ErrorMessageShouldBeMethodShouldExist()
    {
        var actualException = TryInvokeMethodExists(invalidName);
        var actual = actualException.Message;
        var expected = invalidName + " method should exist.";
        var message = methodName + " error message should be " + expected;
        Assert.AreEqual(expected, actual, message);
    }

    [TestMethod]
    public void ShouldFailIfShouldBeStaticDoesntMatch()
    {
        var shouldBeStatic = false;
        var expectedType = typeof(AssertFailedException);
        var value = TryInvokeMethodExists(methodName, shouldBeStatic);
        var message = " should throw an AssertFailedException if shouldBeStatic doesn't match.";
        Assert.IsInstanceOfType(value, expectedType, message);

        var actual = value.Message;
        var expected = string.Format(methodName + " should {0}be static.", shouldBeStatic ? "" : "not ");
        message = methodName + " error message should be " + expected;
        Assert.AreEqual(expected, actual, message);
    }

    [TestMethod]
    public void ShouldFailIfReturnTypeDoesntMatch()
    {
        var returnType = typeof(void);
        var expectedType = typeof(AssertFailedException);
        var value = TryInvokeMethodExists(methodName, returnType: returnType);
        var message = " should throw an AssertFailedException if returnType doesn't match.";
        Assert.IsInstanceOfType(value, expectedType, message);

        var actual = value.Message;
        var expected = string.Format(methodName + " return type should be: " + returnType.Name);
        message = methodName + " error message should be " + expected;
        Assert.AreEqual(expected, actual, message);
    }

    [TestMethod]
    public void ShouldFailIfShouldBeAnExtensionMethodDoesntMatch()
    {
        var shouldBeAnExtensionMethod = false;
        var expectedType = typeof(AssertFailedException);
        var value = TryInvokeMethodExists(methodName, shouldBeAnExtensionMethod: shouldBeAnExtensionMethod);
        var message = " should throw an AssertFailedException if shouldBeAnExtensionMethod doesn't match.";
        Assert.IsInstanceOfType(value, expectedType, message);

        var actual = value.Message;
        var expected = string.Format(methodName + " should {0}be an extention method.", shouldBeAnExtensionMethod ? "" : "not ");
        message = methodName + " error message should be " + expected;
        Assert.AreEqual(expected, actual, message);
    }

    [TestMethod]
    public void ShouldFailIfParamaterCountDoesntMatch()
    {
        var parameterTypesAndNames = new List<Tuple<Type, string>>() {
            Tuple.Create(typeof(Type), "type"),
            Tuple.Create(typeof(string), "methodName"),
            Tuple.Create(typeof(bool?), "shouldBeStatic"),
            Tuple.Create(typeof(Type), "shouldReturnType"),
            Tuple.Create(typeof(bool?), "shouldBeAnExtentionMethod") };
        var expectedType = typeof(AssertFailedException);
        var value = TryInvokeMethodExists(methodName, parameterTypesAndNames: parameterTypesAndNames);
        var message = " should throw an AssertFailedException if parameter counts don't match.";
        Assert.IsInstanceOfType(value, expectedType, message);

        var actual = value.Message;
        var expected = string.Format(methodName + " should take {0} {1}.", parameterTypesAndNames.Count, "parameter");
        message = methodName + " error message should be " + expected;
        Assert.AreEqual(expected, actual, message);
    }

    [TestMethod]
    public void ShouldFailIfAParamaterTypeDoesntMatch()
    {
        var parameterTypesAndNames = new List<Tuple<Type, string>>() {
            Tuple.Create(typeof(Type), "type"),
            Tuple.Create(typeof(string), "methodName"),
            Tuple.Create(typeof(bool?), "shouldBeStatic"),
            Tuple.Create(typeof(Type), "shouldReturnType"),
            Tuple.Create(typeof(bool?), "shouldBeAnExtentionMethod"),
            Tuple.Create(typeof(List<Tuple<Type, bool>>), "parameterTypesAndNames") };
        var expectedType = typeof(AssertFailedException);
        var value = TryInvokeMethodExists(methodName, parameterTypesAndNames: parameterTypesAndNames);
        var message = " should throw an AssertFailedException if a parameter type doesn't match.";
        Assert.IsInstanceOfType(value, expectedType, message);

        var actual = value.Message;
        var expected = string.Format(methodName + " should take a parameter of type: {0}.", parameterTypesAndNames[5].Item1.Name);
        message = methodName + " error message should be " + expected;
        Assert.AreEqual(expected, actual, message);
    }

    [TestMethod]
    public void ShouldFailIfAParamaterNameDoesntMatch()
    {
        var parameterTypesAndNames = new List<Tuple<Type, string>>() {
            Tuple.Create(typeof(Type), "type"),
            Tuple.Create(typeof(string), "methodName"),
            Tuple.Create(typeof(bool?), "shouldBeStatic"),
            Tuple.Create(typeof(Type), "shouldReturnType"),
            Tuple.Create(typeof(bool?), "shouldBeAnExtentionMethod"),
            Tuple.Create(typeof(List<Tuple<Type, string>>), "invalidParameterTypesAndNames") };
        var expectedType = typeof(AssertFailedException);
        var value = TryInvokeMethodExists(methodName, parameterTypesAndNames: parameterTypesAndNames);
        var message = " should throw an AssertFailedException if a parameter name doesn't match.";
        Assert.IsInstanceOfType(value, expectedType, message);

        var actual = value.Message;
        var expected = string.Format(methodName + " should take a parameter named: {0}.", parameterTypesAndNames[5].Item2);
        message = methodName + " error message should be " + expected;
        Assert.AreEqual(expected, actual, message);
    }

    MethodInfo InvokeMethodExists(string invokedMethodName, bool? shouldBeStatic = null, Type returnType = null, bool? shouldBeAnExtensionMethod = null, List<Tuple<Type, string>> parameterTypesAndNames = null)
    {
        var methodInfo = GetMethod(methodName);
        var type = GetType(typeName);
        var parameters = new object[] { type, invokedMethodName, shouldBeStatic, returnType, shouldBeAnExtensionMethod, parameterTypesAndNames };
        return (MethodInfo)methodInfo.Invoke(null, parameters);
    }

    Exception TryInvokeMethodExists(string invokedMethodName, bool? shouldBeStatic = null, Type returnType = null, bool? shouldBeAnExtensionMethod = null, List<Tuple<Type, string>> parameterTypesAndNames = null)
    {
        Exception actualException = null;
        try
        {
            InvokeMethodExists(invokedMethodName, shouldBeStatic, returnType, shouldBeAnExtensionMethod, parameterTypesAndNames);
        }
        catch (TargetInvocationException exception)
        {
            //Return InnerException to skip the target of the invocation layer.
            actualException = exception.InnerException;
        }
        return actualException;
    }
}