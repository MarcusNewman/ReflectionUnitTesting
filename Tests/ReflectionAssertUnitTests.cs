using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public class ReflectionAssertUnitTests : ReflectionAssertBaseUnitTests
{
    [TestMethod]
    public void ReflectionUnitTestingDllAssemblyShouldExist()
    {
        var assembly = GetAssembly();
        var message = assemblyName + " assembly should exist.";
        Assert.IsNotNull(assembly, message);
    }

    [TestMethod]
    public void ReflectionAssertTypeShouldExist()
    {
        GetType(
            typeName: typeName,
            shouldBeStatic: true
            );
    }
}