using adminconsole.Backend;
using adminconsole.Controllers;
using adminconsole.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Threading.Tasks;
using DatabaseLibrary.Models;


namespace adminconsoletest
{
    [TestClass]
    public class LocationsControllerTest
    {


        MaphawksContext context = (new Mock<MaphawksContext>()).Object;

        #region ConstructorTests
        /// <summary>
        /// Ensures Constructor called when officially running the application creates a LocationsController object
        /// </summary>
        [TestMethod]
        public void LocationsController_Production_Constructor_Should_Pass()
        {
            // Arrange 
            var result = new LocationsController(context);

            // Act

            // Assert
            Assert.IsNotNull(result);

        }




        /// <summary>
        /// Ensures Constructor called when test running the application creates a LocationsController object
        /// </summary>
        [TestMethod]
        public void LocationsController_Testing_Constructor_Should_Pass()
        {
            // Arrange 
            var mockBackend = new Mock<LocationsBackend>(context);
            var result = new LocationsController(context, mockBackend.Object);

            // Act

            // Assert
            Assert.IsNotNull(result);

        }

        #endregion
        #region IndexTests
        /// <summary>
        /// Ensure the Default Index page on the controller returns and is not null
        /// </summary>
        [TestMethod]
        public void LocationsController_Index_Get_Default_Should_Pass()
        {
            // Arrange
            var dataMock = new LocationsDataMock();
            var backendMock = new Mock<LocationsBackend>(context);

            backendMock.Setup(backend => backend.IndexAsync(false)).Returns(Task.FromResult(dataMock.GetAllViewModelList()));
            var myController = new LocationsController(context, backendMock.Object);


            // Act
            var result = myController.Index();


            // Assert
            Assert.IsNotNull(result);
        }
        #endregion IndexTests





        #region CreateTests



        /// <summary>
        /// Ensure the Get Create Method on the controller returns and is not null
        /// </summary>
        [TestMethod]
        public void Locations_Create_Get_Default_Should_Pass()
        {
            // Arrange
            var mock = new Mock<LocationsBackend>(context);
            var dataMock = new LocationsDataMock();
            var myController = new LocationsController(context, mock.Object);



            // Act
            var result = myController.Create();



            // Assert
            Assert.IsNotNull(result);
        }


        /// <summary>
        /// Ensure the Post Create Method on the controller returns and is not null
        /// </summary>
        [TestMethod]
        public void LocationsController_Create_Post_Valid_Model_Should_Pass()
        {
            // Arrange
            var mock = new Mock<LocationsBackend>(context);
            mock.Setup(backend => backend.Create(It.IsAny<AllTablesViewModel>())).Returns(true);
            var controller = new LocationsController(context, mock.Object);

            // Get mock data as view model
            var dataMock = new LocationsDataMock();
            var dataRaw = dataMock.GetAllViewModelList()[0];
            var data = new AllTablesViewModel(dataRaw);


            // Act
            var result = controller.Create(data) as RedirectToActionResult;



            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Index", result.ActionName);
        }





        /// <summary>
        /// Ensure the Post Create Method on the controller re-returns for edit and is not null
        /// </summary>
        [TestMethod]
        public void LocationsController_Create_Post_Invalid_Model_Should_Send_Back_For_Edit()
        {
            // Arrange
            var mock = new Mock<LocationsBackend>(context);
            var controller = new LocationsController(context, mock.Object);



            // Get mock data as view model
            var dataMock = new LocationsDataMock();
            var dataRaw = dataMock.GetAllViewModelList()[0];
            var data = new AllTablesViewModel(dataRaw);



            // Make ModelState Invalid
            controller.ModelState.AddModelError("test", "test");



            // Act
            var result = controller.Create(data) as ViewResult;



            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Create Post", result.ViewName);
        }



        /// <summary>
        /// Ensure the Create Method Post on the controller returns and is not null
        /// </summary>
        /*
        [TestMethod]
        public void Clinic_Create_Post_Default_Should_Fail()
        {
            // Arrange
            var myController = new ClinicController(HttpContextHelper.GetHttpContext().Object);
            var myData = new ClinicModel();

            
            
            // Act
            var result = myController.Create(myData);



            // Assert

            Assert.IsNotNull(result);

            Assert.IsNull(myData.OrganizationID);

        }
        */


        #endregion CreateTests
    }
}
