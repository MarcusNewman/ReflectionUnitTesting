using System;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

/// <summary>
/// Methods for unit testing with reflection.
/// </summary>
public static class ReflectionAssert
{
    /// <summary>
    /// Tests for the existance of an assembly using reflection. 
    /// </summary>
    /// <param name="assemblyName">The name of the assembly you are testing.</param>
    /// <returns></returns>
    public static Assembly AssemblyExists(string assemblyName)
    {
        var path = string.Format("..\\..\\..\\bin\\Debug\\{0}.dll", assemblyName);
        try
        {
            return Assembly.LoadFrom(path);
        }
        catch (Exception exception)
        {
            var msg = assemblyName + " assembly should exist.";
            throw new AssertFailedException(msg, exception);
        }
    }

    /// <summary>
    /// Tests for the existance of a type using reflection. 
    /// </summary>
    /// <param name="assembly">Extension parameter for the calling assembly.</param>
    /// <param name="typeName">The name of the class you are testing.</param>
    /// <returns></returns>
    public static Type TypeExists(this Assembly assembly, string typeName)
    {
        var type = assembly.GetType(typeName);
        if (type == null)
        {
            var msg = typeName + " type should exist.";
            throw new AssertFailedException(msg);
        }
        return type;
    }

    /// <summary>
    /// Tests for the existance of a method using reflection.
    /// </summary>
    /// <param name="type">Extension parameter for the calling class.</param>
    /// <param name="methodName">The name of the method you are testing.</param>
    /// <returns></returns>
    public static MethodInfo MethodExists(this Type type, string methodName)
    {
        var methodInfo = type.GetMethod(methodName);
        if (methodInfo == null)
        {
            var msg = methodName + " method should exist.";
            throw new AssertFailedException(msg);
        }
        return methodInfo;
    }
}

