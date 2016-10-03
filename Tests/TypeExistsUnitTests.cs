﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Reflection;
using System;

[TestClass]
public class TypeExistsUnitTests : ReflectionAssertBaseUnitTests
{
    string typeName = "TypeExists";

    [TestMethod]
    public void TypeExistsTypeShouldExist()
    {
        var type = GetType(namespaceName, typeName);
        var message = typeName + " type should exist.";
        Assert.IsNotNull(type, message);
    }
}