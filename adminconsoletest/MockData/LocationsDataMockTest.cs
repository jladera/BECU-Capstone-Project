using adminconsole.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace adminconsoletest
{
    [TestClass]
    public class LocationsDataMockTest
    {

        // Tests Constructor
        [TestMethod]
        public void LocationsDataMock_Constructor_Should_Pass()
        {
            // Arrange
            
            
            // Act
            var dataMock = new LocationsDataMock();



            // Assert
            Assert.IsNotNull(dataMock);
        }






        // Tests null parameter for GetOneLocation
        [TestMethod]
        public void LocationsDataMock_GetOneLocation_WhereClause_Param_Is_Null_Should_Pass()
        {
            // Arrange
            var dataMock = new LocationsDataMock();


            // Act
            var result = dataMock.GetOneLocation(null);


            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(result.LocationId, "a91be80e-ed05-4157-bb95-aa3494663d2a");
        }







        // Tests null parameters for StringsAreEqual
        [TestMethod]
        public void LocationsDataMock_StringsAreEqual_Params_Are_Null_Should_Pass()
        {
            // Arrange
            var dataMock = new LocationsDataMock();


            // Act
            var result = dataMock.StringsAreEqual(null, null);


            // Assert
            Assert.IsTrue(result);
        }



        // Tests for a null parameter for StringsAreEqual
        [TestMethod]
        public void LocationsDataMock_StringsAreEqual_One_Param_Is_Null_Should_Pass()
        {
            // Arrange
            var dataMock = new LocationsDataMock();


            // Act
            var result1 = dataMock.StringsAreEqual(null, "test");
            var result2 = dataMock.StringsAreEqual("test", null);


            // Assert
            Assert.IsFalse(result1);
            Assert.IsFalse(result2);
        }
    }
}
