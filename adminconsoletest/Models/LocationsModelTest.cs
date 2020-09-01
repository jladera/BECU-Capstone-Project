using adminconsole.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DatabaseLibrary.Models;


namespace adminconsoletest
{
    [TestClass]
    public class LocationsModelTest
    {
        [TestMethod]
        public void LocationsModel_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = new Locations();

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void LocationsModel_Get_Property_Defaults_Should_Pass()
        {
            // Arrange
            var result = new Locations();

            // Act

            // Assert
            Assert.IsNull(result.Address);
            Assert.IsNull(result.City);
            Assert.IsNull(result.Contact);
            Assert.IsNull(result.CoopLocationId);
            Assert.IsNull(result.Country);
            Assert.IsNull(result.County);
            Assert.IsNull(result.Hours);
            Assert.IsNull(result.DailyHours);
            Assert.IsNotNull(result.Latitude);
            Assert.IsNull(result.LocationId);
            Assert.IsNull(result.LocationType);
            Assert.IsNotNull(result.Longitude);
            Assert.IsNull(result.Name);
            Assert.IsNull(result.PostalCode);
            Assert.IsNull(result.RetailOutlet);
            Assert.IsFalse(result.SoftDelete);
            Assert.IsNull(result.SpecialQualities);
            Assert.IsNull(result.State);
            Assert.IsFalse(result.TakeCoopData);
        }

        [TestMethod]
        public void LocationsModel_Set_Property_Should_Pass()
        {
            // Arrange
            var result = new Locations();

            // Act
            result.Address = "362 Oxford Dr.";
            result.City = "Starkville";
            result.Contact = new Contacts();
            result.CoopLocationId = "WA9820-174920573";
            result.Country = "US";
            result.County = "King County";
            result.Hours = "24 HOURS ACCESS";
            result.DailyHours = new DailyHours();
            result.Latitude = 13.3108M;
            result.LocationId = "11170401-4112-43c1-aa4e-f73370e1014a";
            result.LocationType = "A";
            result.Longitude = -132.8851M;
            result.Name = "BECU";
            result.PostalCode = "39759";
            result.RetailOutlet = "Northgate";
            result.SoftDelete = true;
            result.SpecialQualities = new SpecialQualities();
            result.State = StateEnum.MS.ToString();
            result.TakeCoopData = true;

            // Assert
            Assert.AreEqual("362 Oxford Dr.", result.Address);
            Assert.AreEqual("Starkville", result.City);
            Assert.IsNotNull(result.Contact);
            Assert.AreEqual("WA9820-174920573", result.CoopLocationId);
            Assert.AreEqual("US", result.Country);
            Assert.AreEqual("King County", result.County);
            Assert.AreEqual("24 HOURS ACCESS", result.Hours);
            Assert.AreEqual(13.3108M, result.Latitude);
            Assert.AreEqual("11170401-4112-43c1-aa4e-f73370e1014a", result.LocationId);
            Assert.AreEqual("A", result.LocationType);
            Assert.AreEqual(-132.8851M, result.Longitude);
            Assert.AreEqual("BECU", result.Name);
            Assert.AreEqual("39759", result.PostalCode);
            Assert.AreEqual("Northgate", result.RetailOutlet);
            Assert.AreEqual(true, result.SoftDelete);
            Assert.IsNotNull(result.SpecialQualities);
            Assert.AreEqual(StateEnum.MS.ToString(), result.State);
            Assert.AreEqual(true, result.TakeCoopData); ;
        }





        [TestMethod]
        public void LocationsModel_Default_AllPropertiesAreNull_Returns_False_Should_Pass()
        {
            // Arrange
            var result = new Locations();

            // Act
            
            // Assert
            Assert.IsNotNull(result);
            Assert.IsFalse(result.AllPropertiesAreNull());
        }
    }
}
