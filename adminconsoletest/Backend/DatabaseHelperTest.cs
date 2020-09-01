using adminconsole.Backend;
using adminconsole.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DatabaseLibrary.Models;

namespace adminconsoletest
{
    [TestClass]
    public class DatabaseHelperTest
    {

        LocationsDataMock mockData = new LocationsDataMock();
        MaphawksContext mockContext = new MaphawksContext();

        /// <summary>
        /// Test DatabaseHelper Constructor
        /// </summary>
        [TestMethod]
        public void DatabaseHelper_Constructor_Should_Pass()
        {
            // Arrange
            var mock = new Mock<MaphawksContext>();

            
            // Act
            var db = new DatabaseHelper(mock.Object);



            // Assert
            Assert.IsNotNull(db);
        }
    }
}
