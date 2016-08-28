using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Reflection;
using System;

[TestClass]
public class ReflectionAssertBaseUnitTests
{
    public string assemblyName = "ReflectionUnitTesting.dll";
    public string namespaceName = "ReflectionUnitTesting";
    public string typeName = "ReflectionAssert";
    public string invalidName = "InvalidName";

    [TestMethod]
    public void ReflectionUnitTestingDllAssemblyShouldExist()
    {
        var assembly = GetAssembly(assemblyName);
        var message = assemblyName + " assembly should exist.";
        Assert.IsNotNull(assembly, message);
    }

    [TestMethod]
    public void ReflectionAssertTypeShouldExist()
    {
        var type = GetType(namespaceName, typeName);
        var message = typeName + " type should exist.";
        Assert.IsNotNull(type, message);
    }

    public void MethodShouldExist(String methodName)
    {
        var methodInfo = GetMethod(methodName);
        var messege = methodName + " method should exist.";
        Assert.IsNotNull(methodInfo, messege);
    }

    public void MethodShouldTakeNumberOfParameters(string methodName, int numberOfParameters)
    {
        var parameters = GetParameters(methodName);
        var actual = parameters.Length;
        string message = methodName + " should take " + Pluralize("parameter", numberOfParameters);
        Assert.AreEqual(numberOfParameters, actual, message);
    }

    public string Pluralize(string itemName, int count)
    {
        return String.Format("{0} {1}{2}", count, itemName, count == 1 ? "" : "s");
    }

    public Assembly GetAssembly(string assemblyName)
    {
        var path = "..\\..\\..\\bin\\Debug\\" + assemblyName;
        return Assembly.LoadFrom(path);
    }

    public Type GetType(string namespaceName, string typeName)
    {
        var assembly = GetAssembly(assemblyName);
        var fullTypeName = namespaceName + '.' + typeName;
        return assembly.GetType(fullTypeName);
    }

    public MethodInfo GetMethod(string methodName)
    {
        var classType = GetType(namespaceName, typeName);
        return classType.GetMethod(methodName);
    }

    public ParameterInfo[] GetParameters(string methodName)
    {
        var methodInfo = GetMethod(methodName);
        return methodInfo.GetParameters();
    }

    public ParameterInfo GetParameter(string methodName, int parameter = 1)
    {
        parameter--;
        var parameters = GetParameters(methodName);
        if (parameter > parameters.Length) return null;
        return parameters[parameter];
    }
}