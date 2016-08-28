using System;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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
        /// <returns>The correct assembly.</returns>
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
        /// <returns>The correct type.</returns>
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
        /// <exception cref="AssertFailedException">Thrown if the method does not exist.</exception>
        /// <returns>The correct method.</returns>
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

        /// <summary>
        /// Method extention method that tests for the number of parameters using reflection.
        /// </summary>
        /// <param name="method">Extension parameter for the calling method.</param>
        /// <param name="numberOfParameters">The expected number of parameters.</param>
        /// <exception cref="AssertFailedException">Thrown if the method does not accept the corrent number of parameters.</exception>
        /// <returns>The correct number of parameters.</returns>
        public static int ShouldTakeANumberOfParameters(this MethodInfo method, int numberOfParameters)
        {
            var parameters = method.GetParameters();
            if (parameters.Length != numberOfParameters)
            {
                var msg = String.Format(method.Name + " should take " + numberOfParameters + " parameter{0}.", numberOfParameters == 1 ? "" : "s");
                throw new AssertFailedException(msg);
            }
            return parameters.Length;
        }
    }
}