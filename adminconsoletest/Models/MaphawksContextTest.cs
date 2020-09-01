using adminconsole.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DatabaseLibrary.Models;


namespace adminconsoletest
{
    [TestClass]
    public class MaphawksContextTest
    {
        [TestMethod]
        public void MaphawksContext_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = new MaphawksContext();

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void MaphawksContext_Get_Property_Defaults_Should_Pass()
        {
            // Arrange
            var result = new MaphawksContext();

            // Act

            // Assert
            Assert.IsNotNull(result.Contacts);
            Assert.IsNotNull(result.DailyHours);
            Assert.IsNotNull(result.Locations);
            Assert.IsNotNull(result.SpecialQualities);
        }

        // Not sure how to test set methods in context or parameterized constructor
        [TestMethod]
        public void MaphawksContext_Set_Property_Should_Pass()
        {
        }
    }
}
