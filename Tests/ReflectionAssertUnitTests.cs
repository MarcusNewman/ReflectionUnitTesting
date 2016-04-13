using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Reflection;
using System;

namespace MGN.ReflectionAssert.Tests
{
    [TestClass]
    public class ReflectionAssertUnitTests
    {
        string assemblyName = "MGN.ReflectionAssert";
        string className = "ReflectionAssert";

        [TestMethod]
        public void AssemblyShouldExist()
        {
            var assembly = AssemblyShouldExist(assemblyName);
            Assert.IsNotNull(assembly, message: assemblyName + " assembly should exist.");
        }

        Assembly AssemblyShouldExist(string assemblyName)
        {
            var path = string.Format("..\\..\\..\\bin\\Debug\\{0}.dll", assemblyName);
            Assembly assembly = null;
            try
            {
                assembly = Assembly.LoadFrom(path);
            }
            catch
            {
            }
            return assembly;
        }

        [TestMethod]
        public void ClassShouldExist()
        {
            var classType = ClassShouldExist(className);
            Assert.IsNotNull(classType, message: className + " class should exist.");
        }

        Type ClassShouldExist(string className)
        {
            var assembly = AssemblyShouldExist(assemblyName);
            var classType = assembly.GetType(assemblyName + '.' + className);
            return classType;
        }

        [TestMethod]
        public void AssemblyShouldExistMethodShouldExist()
        {
            var methodName = "AssemblyShouldExist";
            var methodInfo = MethodShouldExist(methodName);
            Assert.IsNotNull(methodInfo, message: methodName + " method should exist");
        }

        private MethodInfo MethodShouldExist(string methodName)
        {
            var classType = ClassShouldExist(className);
            var methodInfo = classType.GetMethod(methodName);
            return methodInfo;
        }
    }
}
