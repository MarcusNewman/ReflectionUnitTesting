using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Reflection;
using System;

[TestClass]
public class ReflectionAssertBaseUnitTests
{
    public string assemblyName = "MGN.ReflectionAssert";
    public string typeName = "ReflectionAssert";

    [TestMethod]
    public void MGN_ReflectionAssert_assembly_should_exist()
    {
        var assembly = GetAssembly(assemblyName);
        var message = assemblyName + " assembly should exist.";
        Assert.IsNotNull(assembly, message);
    }

    [TestMethod]
    public void ReflectionAssert_type_should_exist()
    {
        var type = GetType(typeName);
        var message = typeName + " type should exist.";
        Assert.IsNotNull(type, message);
    }

    public Assembly GetAssembly(string assemblyName)
    {
        var path = string.Format("..\\..\\..\\bin\\Debug\\{0}.dll", assemblyName);        
        return Assembly.LoadFrom(path); 
    }

    public Type GetType(string typeName)
    {
        var assembly = GetAssembly(assemblyName);
        return assembly.GetType(typeName);
    }

    public MethodInfo GetMethod(string methodName)
    {
        var classType = GetType(typeName);
        return classType.GetMethod(methodName);
    }

    public ParameterInfo[] GetParameters(string methodName)
    {
        var methodInfo = GetMethod(methodName);
        return methodInfo.GetParameters();
    }

    public ParameterInfo GetParameter(string methodName, int parameter = 0)
    {
        var parameters = GetParameters(methodName);
        if (parameter > parameters.Length) return null;
        return parameters[parameter];
    }
}