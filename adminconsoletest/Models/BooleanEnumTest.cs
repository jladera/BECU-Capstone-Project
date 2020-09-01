using adminconsole.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace adminconsoletest
{
    [TestClass]
    public class BooleanEnumTest
    {

        // Confirms assumptions regarding the number of BooleanEnum types and their values
        [TestMethod]
        public void Test_BooleanEnum_Types()
        {
            // Arrange
            var values = Enum.GetValues(typeof(BooleanEnum));

            // Act


            // Assert
            Assert.AreEqual(3, values.Length);


            Assert.AreEqual(BooleanEnum.N, values.GetValue(0));
            Assert.AreEqual(BooleanEnum.Y, values.GetValue(1));
            Assert.AreEqual(BooleanEnum.NULL, values.GetValue(2));



            Assert.AreEqual(0, BooleanEnum.N.GetHashCode());
            Assert.AreEqual(1, BooleanEnum.Y.GetHashCode());
            Assert.AreEqual(2, BooleanEnum.NULL.GetHashCode());



            Assert.AreEqual("Not Set", BooleanEnumExtensions.GetDisplayName(BooleanEnum.NULL));
            Assert.AreEqual("No", BooleanEnumExtensions.GetDisplayName(BooleanEnum.N));
            Assert.AreEqual("Yes", BooleanEnumExtensions.GetDisplayName(BooleanEnum.Y));
        }
    }
}
