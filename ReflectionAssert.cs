﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

/// <summary>
/// </summary>
public static class ReflectionAssert
{
    /// <summary>
    /// Tests for the existance of an assembly using reflection. 
    /// </summary>
    /// <param name="assemblyName"></param>
    /// <returns></returns>
    public static Assembly AssemblyExists(string assemblyName)
    {
        var path = string.Format("..\\..\\..\\bin\\Debug\\{0}.dll", assemblyName);
        try
        {
            return Assembly.LoadFrom(path);
        }
        catch (Exception ex)
        {
            throw new AssertFailedException(assemblyName + " assembly should exist.", ex);
        }
    }

    public static Type TypeExists(this Assembly assembly, string typeName)
    {
        var type = assembly.GetType(typeName);
        if (type == null) throw new AssertFailedException(typeName + " type should exist.");
        return type;
    }

    public static MethodInfo MethodExists(this Type type, string methodName)
    {
        var methodInfo = type.GetMethod(methodName);
        if (methodInfo == null) throw new AssertFailedException(methodName + " method should exist.");
        return methodInfo;
    }
}

