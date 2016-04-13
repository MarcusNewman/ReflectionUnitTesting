using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Reflection;
using System;

namespace MGN.ReflectionAssert.Tests
{
    [TestClass]
    public class ReflectionAssertUnitTests
    {
        string assemblyName = "MGN.ReflectionAssert";
        string typeName = "ReflectionAssert";
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
        public void TypeShouldExist()
        {
            var type = TypeShouldExist(typeName);
            Assert.IsNotNull(type, message: typeName + " type should exist.");
        }

        Type TypeShouldExist(string typeName)
        {
            var assembly = AssemblyShouldExist(assemblyName);
            var type = assembly.GetType(assemblyName + '.' + typeName);
            return type;
        }

        [TestMethod]
        public void AssemblyShouldExistMethodShouldExist()
        {
            var methodInfo = MethodShouldExist(methodName);
            Assert.IsNotNull(methodInfo, message: methodName + " method should exist");
        }

        private MethodInfo MethodShouldExist(string methodName)
        {
            var classType = TypeShouldExist(typeName);
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

        [TestMethod]
        public void TypeShouldExistMethodShouldExist()
        {
            var methodInfo = MethodShouldExist(methodName: "TypeShouldExist");        
            Assert.IsNotNull(methodInfo, message: methodName + " method should exist.");
        }

        [TestMethod]
        public void TypeShouldExistMethodShouldBeStatic()
        {
            var methodInfo = MethodShouldExist(methodName: "TypeShouldExist");
            var isStatic = methodInfo.IsStatic;
            Assert.IsTrue(condition:isStatic,message: methodName + " method should be static.");
        }

        [TestMethod]
        public void TypeShouldExistShouldAcceptAnAssemblyAndStringArgumentsNamedAssemblyAndTypeName()
        {
            var methodName = "TypeShouldExist";
            var methodInfo = MethodShouldExist(methodName);
            var parameters = methodInfo.GetParameters();
            Assert.AreEqual(expected: 2, actual: parameters.Length);
            var parameter1 = parameters[0];
            Assert.AreEqual(expected: typeof(Assembly), actual: parameter1.ParameterType);
            Assert.AreEqual(expected: "assembly", actual: parameter1.Name);
            var parameter2 = parameters[1];
            Assert.AreEqual(expected: typeof(string), actual: parameter2.ParameterType);
            Assert.AreEqual(expected: "typeName", actual: parameter2.Name);
        }
    }
}
