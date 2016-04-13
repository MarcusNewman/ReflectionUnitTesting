using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Reflection;

namespace MGN.ReflectionAssert.Tests
{
    [TestClass]
    public class ReflectionAssertUnitTests
    {
        string assemblyName = "MGN.ReflectionAssert";

        [TestMethod]
        public void AssemblyShouldExist()
        {

            var assembly = AssemblyShouldExist(assemblyName);
            Assert.IsNotNull(assembly, message: string.Format("Assembly {0}.dll should exist.", assemblyName));
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
            var assembly = AssemblyShouldExist(assemblyName);
            var className = assemblyName + ".ReflectionAssert";
            var classType = assembly.GetType(className);
            Assert.IsNotNull(classType);
        }
    }
}
