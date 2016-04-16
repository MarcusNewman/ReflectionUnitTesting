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
        string methodName = "Assembly_should_exist";

        [TestMethod]
        public void MGN_ReflectionAssert_assembly_should_exist()
        {
            var assembly = GetAssembly(assemblyName);
            var message = assemblyName + " assembly should exist.";
            Assert.IsNotNull(assembly, message);
        }

        Assembly GetAssembly(string assemblyName)
        {
            var path = string.Format("..\\..\\..\\bin\\Debug\\{0}.dll", assemblyName);
            Assembly assembly = null;
            try { assembly = Assembly.LoadFrom(path); }
            catch { }
            return assembly;
        }

        [TestMethod]
        public void ReflectionAssert_type_should_exist()
        {
            var type = GetType(typeName);
            var message = typeName + " type should exist.";
            Assert.IsNotNull(type, message);
        }

        Type GetType(string typeName)
        {
            var assembly = GetAssembly(assemblyName);
            return assembly.GetType(assemblyName + '.' + typeName);
        }

        [TestMethod]
        public void Assembly_should_exist_should_exist()
        {
            var methodInfo = GetMethod(methodName);
            var messege = methodName + " method should exist";
            Assert.IsNotNull(methodInfo, messege);
        }

        private MethodInfo GetMethod(string methodName)
        {
            var classType = GetType(typeName);
            return classType.GetMethod(methodName);
        }

        [TestMethod]
        public void Assembly_should_exist_should_accept_one_parameter()
        {
            var parameters = GetParameters();
            Assert.AreEqual(expected: 1, actual: parameters.Length, message: methodName + " should accept one parameter.");
        }

        private ParameterInfo[] GetParameters()
        {
            var methodInfo = GetMethod(methodName);
            return methodInfo.GetParameters();
        }

        [TestMethod]
        public void Assembly_should_exist_should_accept_a_string_parameter()
        {
            var parameter = GetParameter();
            Assert.AreEqual(expected: typeof(string), actual: parameter.ParameterType, message: methodName + " should accept a string parameter.");
        }

        private ParameterInfo GetParameter()
        {
            var parameters = GetParameters();
            return parameters[0];
        }

        [TestMethod]
        public void Assembly_should_exist_should_accept_a_parameter_named_assemblyName()
        {
            var parameter = GetParameter();
            var expected = "assemblyName";
            var actual = parameter.Name;
            var message = methodName + " should accept a parameter named " + expected + ".";
            Assert.AreEqual(expected, actual, message);
        }

        [TestMethod]
        public void Assembly_should_exist_should_be_static()
        {
            var methodInfo = GetMethod(methodName: methodName);
            var isStatic = methodInfo.IsStatic;
            Assert.IsTrue(isStatic, methodName + " should be static.");
        }

        [TestMethod]
        public void Assembly_should_exist_should_return_an_assembly()
        {
            var methodInfo = GetMethod(methodName: methodName);

            Assert.AreEqual(methodInfo.ReturnType, typeof(Assembly), methodName + " should return an assembly.");
        }

        [TestMethod]
        public void Assembly_should_exist_should_return_an_assembly_with_a_valid_assembly_name()
        {
            var methodInfo = GetMethod(methodName: methodName);
            var isStatic = methodInfo.IsStatic;
            Assert.IsTrue(condition: isStatic);
            var assembly = methodInfo.Invoke(null, new object[] { assemblyName });
            Assert.IsInstanceOfType(value: assembly, expectedType: typeof(Assembly));
        }

        [TestMethod]
        public void AssemblyShouldExistShouldThrowAnAssertFailedExceptionWithAnInvalidAssemblyName()
        {
            var methodInfo = GetMethod(methodName: methodName);
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
            var methodInfo = GetMethod(methodName: "TypeShouldExist");
            Assert.IsNotNull(methodInfo, message: methodName + " method should exist.");
        }

        [TestMethod]
        public void TypeShouldExistMethodShouldBeStatic()
        {
            var methodInfo = GetMethod(methodName: "TypeShouldExist");
            var isStatic = methodInfo.IsStatic;
            Assert.IsTrue(condition: isStatic, message: methodName + " method should be static.");
        }

        [TestMethod]
        public void TypeShouldExistShouldAcceptAnAssemblyAndStringArgumentsNamedAssemblyAndTypeName()
        {
            var methodName = "TypeShouldExist";
            var methodInfo = GetMethod(methodName);
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
