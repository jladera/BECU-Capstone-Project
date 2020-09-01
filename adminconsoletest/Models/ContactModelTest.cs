using adminconsole.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DatabaseLibrary.Models;


namespace adminconsoletest
{
    [TestClass]
    public class ContactModelTest
    {
        [TestMethod]
        public void ContactModel_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = new Contacts();

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void ContactModel_Get_Property_Defaults_Should_Pass()
        {
            // Arrange
            var result = new Contacts();

            // Act

            // Assert
            Assert.IsNull(result.Location);
            Assert.IsNull(result.LocationId);
            Assert.IsNull(result.Phone);
            Assert.IsNull(result.Fax);
            Assert.IsNull(result.WebAddress);
        }

        [TestMethod]
        public void ContactModel_Set_Property_Should_Pass()
        {
            // Arrange
            var result = new Contacts();

            // Act
            result.Location = new Locations();
            result.LocationId = "location id";
            result.Phone = "phone";
            result.Fax = "fax";
            result.WebAddress = "web address";

            // Assert
            Assert.IsNotNull(result.Location);
            Assert.AreEqual("location id", result.LocationId);
            Assert.AreEqual("phone", result.Phone);
            Assert.AreEqual("fax", result.Fax);
            Assert.AreEqual("web address", result.WebAddress);
        }
    }
}
