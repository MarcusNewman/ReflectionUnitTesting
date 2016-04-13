using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MGN.ReflectionAssert
{
    public class ReflectionAssert
    {
        public static Assembly AssemblyShouldExist(string assemblyName)
        {
            Assembly assembly = null;
            try
            {
                assembly = GetAssembly(assemblyName);
            }
            catch (Exception ex)
            {
                throw new AssertFailedException(msg: assemblyName + " assembly should exist.", ex: ex);
            }
            return assembly;
        }

        static Assembly GetAssembly(string assemblyName)
        {
            var path = string.Format("..\\..\\..\\bin\\Debug\\{0}.dll", assemblyName);
            return Assembly.LoadFrom(path);
        }
    }
}
