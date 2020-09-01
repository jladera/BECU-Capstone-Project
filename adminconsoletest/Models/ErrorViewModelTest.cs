using adminconsole.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace adminconsoletest
{
    [TestClass]
    public class ErrorViewModelTest
    {

        // Ensures default values of Class instantiation are intact
        [TestMethod]
        public void ErrorViewModelTest_Default_Should_Pass()
        {
            // Arrange
            var errorViewModel = new ErrorViewModel();

            // Act

            // Assert
            Assert.IsNull(errorViewModel.RequestId);
            Assert.IsFalse(errorViewModel.ShowRequestId);
        }



        // Ensures Set of class variables are intact
        [TestMethod]
        public void ErrorViewModelTest_Set_RequestId_Should_Pass()
        {
            // Arrange
            var errorViewModel = new ErrorViewModel();
            var expected = "Error";

            // Act
            errorViewModel.RequestId = expected;

            // Assert
            Assert.IsNotNull(errorViewModel.RequestId);
            Assert.AreEqual(expected, errorViewModel.RequestId);
            Assert.IsTrue(errorViewModel.ShowRequestId);
        }
    }
}
