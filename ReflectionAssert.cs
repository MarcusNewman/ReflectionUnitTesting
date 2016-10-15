using System;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace ReflectionUnitTesting
{
    /// <summary>
    /// Methods for unit testing with reflection.
    /// </summary>
    public static class ReflectionAssert
    {
        /// <summary>
        /// Tests for the existance of an assembly using reflection. 
        /// </summary>
        /// <param name="assemblyName">The name of the assembly to be tested.</param>
        /// <exception cref="AssertFailedException">Thrown if the assembly does not exist.</exception>
        /// <returns>The correct assembly if it exists.</returns>
        public static Assembly AssemblyExists(string assemblyName)
        {
            var path = "..\\..\\..\\bin\\Debug\\" + assemblyName;
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
        /// Assembly extension method that tests for the existance of a type using reflection. 
        /// </summary>
        /// <param name="assembly">Extension parameter for the calling assembly.</param>
        /// <param name="namespaceName">The namespace of the class to be tested.</param>
        /// <param name="typeName">The name of the class to be tested.</param>
        /// <exception cref="AssertFailedException">Thrown if the type does not exist.</exception>
        /// <returns>The correct type if it exists.</returns>
        public static Type TypeExists(this Assembly assembly, string namespaceName, string typeName)
        {
            var name = namespaceName + '.' + typeName;
            var type = assembly.GetType(name);
            if (type == null)
            {
                var msg = typeName + " type should exist.";
                throw new AssertFailedException(msg);
            }
            return type;
        }

        /// <summary>
        /// Type extention method that tests for the existance of a method using reflection.
        /// </summary>
        /// <param name="type">Extension parameter for the calling class.</param>
        /// <param name="methodName">The name of the method to be tested.</param>
        /// <param name="shouldBeStatic">Should the method be static?</param>
        /// <exception cref="AssertFailedException">Thrown if the method does not exist or if the properties don't match.</exception>
        /// <returns>The correct method if it exists.</returns>
        public static MethodInfo MethodExists(this Type type, string methodName, bool? shouldBeStatic = null, Type shouldReturnType = null, bool? shouldBeAnExtentionMethod = null, List<Tuple<Type, string>> parameterTypesAndNames = null)
        {
            var methodInfo = type.GetMethod(methodName);
            var msg = "";
            if (methodInfo == null)
            {
                msg = methodName + " method should exist.";
                throw new AssertFailedException(msg);
            }
            if (shouldBeStatic.HasValue)
            {
                var isStatic = methodInfo.IsStatic;
                if (shouldBeStatic != isStatic)
                {
                    msg = string.Format(methodName + " should {0}be static.", shouldBeStatic.Value ? "" : "not ");
                    throw new AssertFailedException(msg);
                }
            }

            if (shouldReturnType != null)
            {
                var returnType = methodInfo.ReturnType;
                if (shouldReturnType != returnType)
                {
                    msg = methodName + " return type should be: " + shouldReturnType.Name;
                    throw new AssertFailedException(msg);
                }
            }

            if (shouldBeAnExtentionMethod.HasValue)
            {
                var extensionAttributeType = typeof(System.Runtime.CompilerServices.ExtensionAttribute);
                var isExtention = methodInfo.IsDefined(extensionAttributeType, false);
                if (shouldBeAnExtentionMethod != isExtention)
                {
                    msg = string.Format(methodName + " should {0}be an extention method.", shouldBeAnExtentionMethod.Value ? "" : "not ");
                    throw new AssertFailedException(msg);
                }
            }

            if (parameterTypesAndNames != null)
            {
                var expectedParameterLength = parameterTypesAndNames.Count;
                var actualParameters = methodInfo.GetParameters();
                if (expectedParameterLength != actualParameters.Length)
                {
                    msg = string.Format(methodName + " should take {0} {1}.", expectedParameterLength, "parameter");
                    throw new AssertFailedException(msg);
                }
                var counter = 0;
                foreach (var parameter in parameterTypesAndNames)
                {
                    if (parameter.Item1 != actualParameters[counter].ParameterType)
                    {
                        msg = string.Format(methodName + " should take a parameter of type: {0}.", parameter.Item1.Name);
                        throw new AssertFailedException(msg);
                    }
                    if (parameter.Item2 != actualParameters[counter].Name)
                    {
                        msg = string.Format(methodName + " should take a parameter named: {0}.", parameter.Item2);
                        throw new AssertFailedException(msg);
                    }
                    counter++;
                }
            }
            return methodInfo;            
        }
    }
}