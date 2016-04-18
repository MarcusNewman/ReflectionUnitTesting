using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Reflection;
using System;
using System.Runtime.CompilerServices;

[TestClass]
public class ReflectionAssertUnitTests
{
    string assemblyName = "MGN.ReflectionAssert";
    string typeName = "ReflectionAssert";
    string methodName = "AssemblyExists";
    string invalidAssemblyName = "InvalidAssemblyName";

    [TestMethod]
    public void MGN_ReflectionAssert_assembly_should_exist()
    {
        var assembly = GetAssembly(assemblyName);
        var message = assemblyName + " assembly should exist.";
        Assert.IsNotNull(assembly, message);
    }

    Assembly GetAssembly(string assemblyName)
    {
        var path = string.Format("..\\..\\..\\bin\\Debug\\{0}.dll", assemblyName);
        Assembly assembly = null;
        try { assembly = Assembly.LoadFrom(path); }
        catch { }
        return assembly;
    }

    [TestMethod]
    public void ReflectionAssert_type_should_exist()
    {
        var type = GetType(typeName);
        var message = typeName + " type should exist.";
        Assert.IsNotNull(type, message);
    }

    Type GetType(string typeName)
    {
        var assembly = GetAssembly(assemblyName);
        return assembly.GetType(typeName);
    }

    [TestMethod]
    public void AssemblyExists_should_exist()
    {
        var methodInfo = GetMethod(methodName);
        var messege = methodName + " method should exist";
        Assert.IsNotNull(methodInfo, messege);
    }

    private MethodInfo GetMethod(string methodName)
    {
        var classType = GetType(typeName);
        return classType.GetMethod(methodName);
    }

    [TestMethod]
    public void AssemblyExists_should_accept_one_parameter()
    {
        var parameters = GetParameters(methodName);
        Assert.AreEqual(expected: 1, actual: parameters.Length, message: methodName + " should accept one parameter.");
    }

    private ParameterInfo[] GetParameters(string methodName)
    {
        var methodInfo = GetMethod(methodName);
        return methodInfo.GetParameters();
    }

    [TestMethod]
    public void AssemblyExists_should_accept_a_string_parameter()
    {
        var parameter = GetParameter(methodName);
        Assert.AreEqual(expected: typeof(string), actual: parameter.ParameterType, message: methodName + " should accept a string parameter.");
    }

    private ParameterInfo GetParameter(string methodName)
    {
        var parameters = GetParameters(methodName);
        return parameters[0];
    }

    [TestMethod]
    public void AssemblyExists_should_accept_a_parameter_named_assemblyName()
    {
        var parameter = GetParameter(methodName);
        var expected = "assemblyName";
        var actual = parameter.Name;
        var message = methodName + " should accept a parameter named " + expected + ".";
        Assert.AreEqual(expected, actual, message);
    }

    [TestMethod]
    public void AssemblyExists_should_be_static()
    {
        var methodInfo = GetMethod(methodName: methodName);
        var isStatic = methodInfo.IsStatic;
        Assert.IsTrue(isStatic, methodName + " should be static.");
    }

    [TestMethod]
    public void AssemblyExists_should_return_an_assembly()
    {
        var methodInfo = GetMethod(methodName: methodName);
        Assert.AreEqual(methodInfo.ReturnType, typeof(Assembly), methodName + " should return an assembly.");
    }

    [TestMethod]
    public void AssemblyExists_should_return_the_correct_assembly_with_a_valid_assembly_name()
    {
        var methodInfo = GetMethod(methodName: methodName);
        var assembly = (Assembly)methodInfo.Invoke(null, new object[] { assemblyName });
        Assert.AreEqual(assembly.GetName().Name, assemblyName, methodName + " should return the correct assembly with a valid assemblyName.");
    }

    [TestMethod]
    public void AssemblyExists_should_throw_an_AssertFailedException_with_an_invalid_assemblyName()
    {
        var actualException = GetAssemblyWithInvalidName();
        Assert.IsInstanceOfType(value: actualException, expectedType: typeof(AssertFailedException));
    }

    private Exception GetAssemblyWithInvalidName()
    {
        var methodInfo = GetMethod(methodName: methodName);
        Exception actualException = null;
        try
        {
            methodInfo.Invoke(null, new object[] { invalidAssemblyName });
        }
        catch (TargetInvocationException exception)
        {
            actualException = exception.InnerException;
        }
        return actualException;
    }

    [TestMethod]
    public void AssemblyExists_error_message_should_be_assembly_should_exist()
    {
        var actualException = GetAssemblyWithInvalidName();
        Assert.AreEqual(expected: invalidAssemblyName + " assembly should exist.", actual: actualException.Message);
    }

    [TestMethod]
    public void AssemblyExists_error_should_contain_the_InnerException()
    {
        var actualException = GetAssemblyWithInvalidName();
        Assert.IsNotNull(value: actualException.InnerException);
    }

    [TestMethod]
    public void TypeExists_method_should_exist()
    {
        var methodName = "TypeExists";
        var methodInfo = GetMethod(methodName);
        var messege = methodName + " method should exist";
        Assert.IsNotNull(methodInfo, messege);
    }

    [TestMethod]
    public void TypeExists_should_accept_one_parameter()
    {
        var methodName = "TypeExists";
        var parameters = GetParameters(methodName);
        Assert.AreEqual(expected: 1, actual: parameters.Length, message: methodName + " should accept one parameter.");
    }

    [TestMethod]
    public void TypeExists_should_accept_an_extention_parameter()
    {
        var methodName = "TypeExists";
        var methodInfo = GetMethod(methodName);
        var isExtention = methodInfo.IsDefined(typeof(ExtensionAttribute), false);
        Assert.IsTrue(isExtention, message: methodName + " should accept an extention parameter.");
    }

    [TestMethod]
    public void TypeExists_should_accept_a_parameter_named_typeName()
    {
        var methodName = "TypeExists";
        var parameter = GetParameter(methodName);
        var expected = "typeName";
        var actual = parameter.Name;
        var message = methodName + " should accept a parameter named " + expected + ".";
        Assert.AreEqual(expected, actual, message);
    }

    [TestMethod]
    public void TypeExists_should_be_static()
    {
        var methodName = "TypeExists";
        var methodInfo = GetMethod(methodName: methodName);
        var isStatic = methodInfo.IsStatic;
        Assert.IsTrue(isStatic, methodName + " should be static.");
    }
}

