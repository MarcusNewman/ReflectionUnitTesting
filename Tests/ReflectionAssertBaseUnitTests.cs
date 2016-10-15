using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Reflection;
using System;
using System.Collections.Generic;

[TestClass]
public class ReflectionAssertBaseUnitTests
{
    public string assemblyName = "ReflectionUnitTesting.dll";
    public string namespaceName = "ReflectionUnitTesting";
    public string typeName = "ReflectionAssert";
    public string invalidName = "InvalidName";


    public Assembly GetAssembly()
    {
        var path = "..\\..\\..\\bin\\Debug\\" + assemblyName;
        return Assembly.LoadFrom(path);
    }

    public Type GetType(string typeName, bool? shouldBeStatic = null)
    {
        var assembly = GetAssembly();
        var fullTypeName = namespaceName + '.' + typeName;
        var type = assembly.GetType(fullTypeName);

        var message = typeName + " type should exist.";
        Assert.IsNotNull(type, message);

        if (shouldBeStatic.HasValue)
        {
            var isStatic = type.IsAbstract && type.IsSealed;
            message = string.Format(typeName + " should {0}be static.", shouldBeStatic.Value ? "" : "not ");
            Assert.AreEqual(isStatic, shouldBeStatic.Value, message);
        }

        return type;
    }

    public MethodInfo GetMethod(string methodName, bool? shouldBeStatic = null, Type expectedReturnType = null, bool? shouldBeAnExtentionMethod = null, List<Tuple<Type, string>> parameterTypesAndNames = null)
    {
        var methodInfo = GetType(typeName).GetMethod(methodName);
        var message = methodName + " method should exist.";
        Assert.IsNotNull(methodInfo, message);

        if (shouldBeStatic.HasValue)
        {
            var isStatic = methodInfo.IsStatic;
            message = string.Format(methodName + " should {0}be static.", shouldBeStatic.Value ? "" : "not ");
            Assert.AreEqual(isStatic, shouldBeStatic.Value, message);
        }

        if (expectedReturnType != null)
        {
            var returnType = methodInfo.ReturnType;
            message = methodName + " return type should be: " + expectedReturnType.Name;
            Assert.AreEqual(expectedReturnType, returnType, message);
        }

        if (shouldBeAnExtentionMethod.HasValue)
        {
            var extensionAttributeType = typeof(System.Runtime.CompilerServices.ExtensionAttribute);
            var isExtention = methodInfo.IsDefined(extensionAttributeType, false);
            message = string.Format(methodName + " should {0}be an extention method.", shouldBeAnExtentionMethod.Value ? "" : "not ");
            Assert.AreEqual(shouldBeAnExtentionMethod.Value, isExtention, message);
        }

        if (parameterTypesAndNames != null)
        {
            var expectedParameterLength = parameterTypesAndNames.Count;
            var actualParameters = methodInfo.GetParameters();
            message = string.Format(methodName + " should take {0} {1}.", expectedParameterLength, "parameter");
            Assert.AreEqual(expectedParameterLength, actualParameters.Length, message);
            var counter = 0;
            foreach (var parameter in parameterTypesAndNames)
            {
                message = string.Format(methodName + " should take a parameter of type: {0}.", parameter.Item1.Name);
                Assert.AreEqual(parameter.Item1, actualParameters[counter].ParameterType, message);

                message = string.Format(methodName + " should take a parameter named: {0}.", parameter.Item2);
                Assert.AreEqual(parameter.Item2, actualParameters[counter].Name, message);
                counter++;
            }
        }
        return methodInfo;
    }
}