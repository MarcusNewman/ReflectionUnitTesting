using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Reflection;
using System;

[TestClass]
public class ShouldTakeANumberOfParametersUnitTests : ReflectionAssertUnitTests
{
    int invalidNumberOfParameters = 10;
    string methodName = "ShouldTakeANumberOfParameters";

    [TestMethod]
    public void ShouldTakeANumberOfParametersMethodShouldExist()
    {
        MethodShouldExist(methodName);
    }

    [TestMethod]
    public void ShouldTakeANumberOfParametersShouldTakeTwoParameters()
    {
        MethodShouldTakeNumberOfParameters(methodName, 2);
    }

    [TestMethod]
    public void ShouldTakeANumberOfParametersShouldAcceptAnExtentionParameter()
    {
        var methodInfo = GetMethod(methodName);
        var type = typeof(System.Runtime.CompilerServices.ExtensionAttribute);
        var isExtention = methodInfo.IsDefined(type, false);
        var message = methodName + " should accept an extention parameter.";
        Assert.IsTrue(isExtention, message);
    }

    [TestMethod]
    public void ShouldTakeANumberOfParametersShouldAcceptAMethodInfoParameter()
    {
        var parameter = GetParameter(methodName);
        var expected = typeof(MethodInfo);
        var actual = parameter.ParameterType;
        var message = methodName + " should accept an MethodInfo parameter.";
        Assert.AreEqual(expected, actual, message);
    }

    [TestMethod]
    public void ShouldTakeANumberOfParametersShouldAcceptAFirstParameterNamedMethod()
    {
        var parameter = GetParameter(methodName);
        var expected = "method";
        var actual = parameter.Name;
        var message = methodName + " should accept a parameter named " + expected + ".";
        Assert.AreEqual(expected, actual, message);
    }

    [TestMethod]
    public void ShouldTakeANumberOfParametersShouldAcceptASecondParameterOfTypeInt()
    {
        var expected = typeof(int);
        var parameter = GetParameter(methodName, 2);
        var actual = parameter.ParameterType;
        var message = methodName + " should accept an int parameter.";
        Assert.AreEqual(expected, actual, message);
    }

    [TestMethod]
    public void ShouldTakeANumberOfParametersShouldAcceptASecondParameterNamedNumberOfParameters()
    {
        var expected = "numberOfParameters";
        var parameter = GetParameter(methodName, 2);
        var actual = parameter.Name;
        var message = methodName + " should accept a parameter named " + expected + ".";
        Assert.AreEqual(expected, actual, message);
    }

    [TestMethod]
    public void ShouldTakeANumberOfParametersShouldReturnAnInt()
    {
        var expected = typeof(int);
        var method = GetMethod(methodName);
        var actual = method.ReturnType;
        var messege = methodName + " should return a int.";
        Assert.AreEqual(expected, actual, messege);
    }

    [TestMethod]
    public void ShouldTakeANumberOfParametersShouldReturnTheCorrectNumberOfParameters()
    {
        var expected = 2;
        var actual = InvokeShouldTakeANumberOfParameters(expected);
        var messege = methodName + " should return the correct number of parameters.";
        Assert.AreEqual(expected, actual, messege);
    }

    int InvokeShouldTakeANumberOfParameters(int numberOfParameters)
    {
        var methodInfo = GetMethod(methodName);
        var parameters = new object[] { methodInfo, numberOfParameters };
        return (int)methodInfo.Invoke(null, parameters);
    }

    [TestMethod]
    public void ShouldTakeANumberOfParametersShouldThrowAnAssertFailedExceptionWithAnInvalidNumberOfParameters()
    {
        var expectedType = typeof(AssertFailedException);
        var value = TryInvokeShouldTakeANumberOfParametersWithInvalidNumberOfParameters();
        var message = methodName + " should throw an AssertFailedException with an invalid number of parameters";
        Assert.IsInstanceOfType(value, expectedType, message);
    }

    Exception TryInvokeShouldTakeANumberOfParametersWithInvalidNumberOfParameters()
    {
        Exception actualException = null;
        try
        {
            InvokeShouldTakeANumberOfParameters(invalidNumberOfParameters);
        }
        catch (TargetInvocationException exception)
        {
            //Return InnerException to skip the target of the invocation layer.
            actualException = exception.InnerException;
        }
        return actualException;
    }

    [TestMethod]
    public void ShouldTakeANumberOfParametersErrorMessageShouldBeShouldTakeInvalidNumberOfParametersParameters()
    {
        var actualException = TryInvokeShouldTakeANumberOfParametersWithInvalidNumberOfParameters();
        var actual = actualException.Message;
        var expected = String.Format("{0} should take {1}.", methodName, Pluralize("parameter", invalidNumberOfParameters));
        var message = methodName + " error message should be " + expected;
        Assert.AreEqual(expected, actual, message);
    }
}