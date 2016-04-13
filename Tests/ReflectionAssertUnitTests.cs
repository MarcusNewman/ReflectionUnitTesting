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
        string methodName = "AssemblyShouldExist";

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
            var methodInfo = MethodShouldExist(methodName);
            Assert.IsNotNull(methodInfo, message: methodName + " method should exist");
        }

        private MethodInfo MethodShouldExist(string methodName)
        {
            var classType = ClassShouldExist(className);
            var methodInfo = classType.GetMethod(methodName);
            return methodInfo;
        }

        [TestMethod]
        public void AssemblyShouldExistShouldAcceptAStringArgumentNamedAssemblyName()
        {
            var methodInfo = MethodShouldExist(methodName);
            var parameters = methodInfo.GetParameters();
            Assert.AreEqual(expected: 1, actual: parameters.Length);
            var parameter = parameters[0];
            Assert.AreEqual(expected: typeof(string), actual: parameter.ParameterType);
            Assert.AreEqual(expected: "assemblyName", actual: parameter.Name);
        }

        [TestMethod]
        public void AssemblyShouldExistShouldBeStaticAndReturnAnAssemblyWithAValidAssemblyName()
        {
            var methodInfo = MethodShouldExist(methodName: methodName);
            var isStatic = methodInfo.IsStatic;
            Assert.IsTrue(condition: isStatic);
            var assembly = methodInfo.Invoke(null, new object[] { assemblyName });
            Assert.IsInstanceOfType(value: assembly, expectedType: typeof(Assembly));
        }

        [TestMethod]
        public void AssemblyShouldExistShouldThrowAnAssertFailedExceptionWithAnInvalidAssemblyName()
        {
            var methodInfo = MethodShouldExist(methodName: methodName);
            Exception actualException = null;
            var invalidAssemblyName = "InvalidAssemblyName";
            try
            {
                methodInfo.Invoke(null, new object[] { invalidAssemblyName });
            }
            catch (TargetInvocationException exception)
            {               
                actualException = exception.InnerException;
            }
            Assert.IsInstanceOfType(value: actualException, expectedType: typeof(AssertFailedException));
            Assert.AreEqual(expected: invalidAssemblyName + " assembly should exist.", actual: actualException.Message);
            Assert.IsNotNull(value: actualException.InnerException);
        }
    }
}
