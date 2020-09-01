using adminconsole.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using DatabaseLibrary.Models;


namespace adminconsoletest
{
    [TestClass]
    public class AllTablesViewModelTest
    {
        [TestMethod]
        public void AllTablesViewModel_Default_Should_Pass()
        {

            // Arrange

            // Act
            var result = new AllTablesViewModel();

            // Assert
            Assert.IsNotNull(result);

        }

        [TestMethod]
        public void AllTablesViewModel_Constructor_With_Context_Parameter_Should_Pass()
        {

            // Arrange
            MaphawksContext context = new MaphawksContext();

            // Act
            var result = new AllTablesViewModel();

            // Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.locations);

        }


        [TestMethod]
        public void AllTablesViewModel_Constructor_With_Null_Context_Parameter_Should_Pass()
        {

            // Arrange

            // Act
            var result = new AllTablesViewModel(null);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.locations);

        }

        [TestMethod]
        public void AllTablesViewModel_Get_Property_Defaults_Should_Pass()
        {

            // Arrange
            var result = new AllTablesViewModel();

            // Act
            var s = result.SoftDelete;
            // Assert
            // Locations
            Assert.IsNull(result.Address);
            Assert.IsNull(result.City);
            Assert.IsNull(result.CoopLocationId);
            Assert.IsNull(result.Country);
            Assert.IsNull(result.County);
            Assert.IsNull(result.Hours);
            Assert.IsNotNull(result.Latitude);
            Assert.IsNull(result.LocationId);
            Assert.IsNotNull(result.LocationType);
            Assert.IsNotNull(result.Longitude);
            Assert.IsNull(result.Name);
            Assert.IsNull(result.PostalCode);
            Assert.IsNull(result.RetailOutlet);
            Assert.AreEqual(BooleanEnum.N, result.SoftDelete);
            Assert.IsNotNull(result.State);
            Assert.AreEqual(BooleanEnum.N, result.TakeCoopData);

            // Contacts
            Assert.IsNull(result.Phone);
            Assert.IsNull(result.Fax);
            Assert.IsNull(result.WebAddress);

            // Special Qualities
            Assert.IsNull(result.AcceptCash);
            Assert.IsNull(result.AcceptDeposit);
            Assert.IsNull(result.Access);
            Assert.IsNull(result.AccessNotes);
            Assert.IsNull(result.Cashless);
            Assert.IsNull(result.DriveThruOnly);
            Assert.IsNull(result.EnvelopeRequired);
            Assert.IsNull(result.HandicapAccess);
            Assert.IsNull(result.InstallationType);
            Assert.IsNull(result.LimitedTransactions);
            Assert.IsNotNull(result.locations);
            Assert.AreEqual(0, result.locations.Capacity);
            Assert.IsNull(result.MilitaryIdRequired);
            Assert.IsNull(result.OnMilitaryBase);
            Assert.IsNull(result.OnPremise);
            Assert.IsNull(result.RestrictedAccess);
            Assert.IsNull(result.SelfServiceDevice);
            Assert.IsNull(result.SelfServiceOnly);
            Assert.IsNotNull(result.State);
            Assert.IsNull(result.Surcharge);

            // Hours Per Day Of The Week
            Assert.IsNull(result.HoursDtfriClose);
            Assert.IsNull(result.HoursDtfriOpen);
            Assert.IsNull(result.HoursDtmonClose);
            Assert.IsNull(result.HoursDtmonOpen);
            Assert.IsNull(result.HoursDtsatClose);
            Assert.IsNull(result.HoursDtsatOpen);
            Assert.IsNull(result.HoursDtsunClose);
            Assert.IsNull(result.HoursDtsunOpen);
            Assert.IsNull(result.HoursDtthuClose);
            Assert.IsNull(result.HoursDtthuOpen);
            Assert.IsNull(result.HoursDttueClose);
            Assert.IsNull(result.HoursDttueOpen);
            Assert.IsNull(result.HoursDtwedClose);
            Assert.IsNull(result.HoursDtwedOpen);
            Assert.IsNull(result.HoursFriClose);
            Assert.IsNull(result.HoursFriOpen);
            Assert.IsNull(result.HoursMonClose);
            Assert.IsNull(result.HoursMonOpen);
            Assert.IsNull(result.HoursSatClose);
            Assert.IsNull(result.HoursSatOpen);
            Assert.IsNull(result.HoursSunClose);
            Assert.IsNull(result.HoursSunOpen);
            Assert.IsNull(result.HoursThuClose);
            Assert.IsNull(result.HoursThuOpen);
            Assert.IsNull(result.HoursTueClose);
            Assert.IsNull(result.HoursTueOpen);
            Assert.IsNull(result.HoursWedClose);
            Assert.IsNull(result.HoursWedOpen);

        }

        [TestMethod]
        public void AllTablesViewModel_Set_Property_Should_Pass()
        {

            // Arrange
            var result = new AllTablesViewModel();

            // Act
            // Locations
            result.AcceptCash = BooleanEnum.Y;
            result.AcceptDeposit = BooleanEnum.Y;
            result.Access = BooleanEnum.Y;
            result.AccessNotes = "Lobby";
            result.Address = "362 Oxford Dr.";
            result.Cashless = BooleanEnum.Y;
            result.City = "Starkville";
            result.CoopLocationId = "WA9820-174920573";
            result.Country = "US";
            result.County = "King County";
            result.DriveThruOnly = BooleanEnum.Y;
            result.EnvelopeRequired = BooleanEnum.Y;
            result.Fax = "8058451931";
            result.HandicapAccess = BooleanEnum.Y;
            result.Hours = "24 HOURS ACCESS";
            result.HoursDtfriClose = "9";
            result.HoursDtfriOpen = "9";
            result.HoursDtmonClose = "9";
            result.HoursDtmonOpen = "9";
            result.HoursDtsatClose = "9";
            result.HoursDtsatOpen = "9";
            result.HoursDtsunClose = "9";
            result.HoursDtsunOpen = "9";
            result.HoursDtthuClose = "9";
            result.HoursDtthuOpen = "9";
            result.HoursDttueClose = "9";
            result.HoursDttueOpen = "9";
            result.HoursDtwedClose = "9";
            result.HoursDtwedOpen = "9";
            result.HoursFriClose = "9";
            result.HoursFriOpen = "9";
            result.HoursMonClose = "9";
            result.HoursMonOpen = "9";
            result.HoursSatClose = "9";
            result.HoursSatOpen = "9";
            result.HoursSunClose = "9";
            result.HoursSunOpen = "9";
            result.HoursThuClose = "9";
            result.HoursThuOpen = "9";
            result.HoursTueClose = "9";
            result.HoursTueOpen = "9";
            result.HoursWedClose = "9";
            result.HoursWedOpen = "9";
            result.InstallationType = "Walk-Up";
            result.Latitude = 13.3108M;
            result.LimitedTransactions = BooleanEnum.Y;
            result.LocationId = "11170401-4112-43c1-aa4e-f73370e1014a";
            result.locations = new List<Locations>();
            result.LocationType = LocationTypeEnum.A;
            result.Longitude = -132.8851M;
            result.MilitaryIdRequired = BooleanEnum.Y;
            result.Name = "BECU";
            result.OnMilitaryBase = BooleanEnum.Y;
            result.OnPremise = BooleanEnum.Y;
            result.Phone = "4896771019";
            result.PostalCode = "39759";
            result.RestrictedAccess = BooleanEnum.Y;
            result.RetailOutlet = "Northgate";
            result.SelfServiceDevice = BooleanEnum.Y;
            result.SelfServiceOnly = BooleanEnum.Y;
            result.SoftDelete = BooleanEnum.Y;
            result.State = StateEnum.MS;
            result.Surcharge = BooleanEnum.Y;
            result.TakeCoopData = BooleanEnum.Y;
            result.WebAddress = "https://trypap.com/";

            // Assert
            // Locations
            Assert.AreEqual(BooleanEnum.Y, result.AcceptCash);
            Assert.AreEqual(BooleanEnum.Y, result.AcceptDeposit);
            Assert.AreEqual(BooleanEnum.Y, result.Access);
            Assert.AreEqual("Lobby", result.AccessNotes);
            Assert.AreEqual("362 Oxford Dr.", result.Address);
            Assert.AreEqual(BooleanEnum.Y, result.Cashless);
            Assert.AreEqual("Starkville", result.City);
            Assert.AreEqual("WA9820-174920573", result.CoopLocationId);
            Assert.AreEqual("US", result.Country);
            Assert.AreEqual("King County", result.County);
            Assert.AreEqual(BooleanEnum.Y, result.DriveThruOnly);
            Assert.AreEqual(BooleanEnum.Y, result.EnvelopeRequired);
            Assert.AreEqual("8058451931", result.Fax);
            Assert.AreEqual(BooleanEnum.Y, result.HandicapAccess);
            Assert.AreEqual("24 HOURS ACCESS", result.Hours);
            Assert.AreEqual("9", result.HoursDtfriClose);
            Assert.AreEqual("9", result.HoursDtfriOpen);
            Assert.AreEqual("9", result.HoursDtmonClose);
            Assert.AreEqual("9", result.HoursDtmonOpen);
            Assert.AreEqual("9", result.HoursDtsatClose);
            Assert.AreEqual("9", result.HoursDtsatOpen);
            Assert.AreEqual("9", result.HoursDtsunClose);
            Assert.AreEqual("9", result.HoursDtsunOpen);
            Assert.AreEqual("9", result.HoursDtthuClose);
            Assert.AreEqual("9", result.HoursDtthuOpen);
            Assert.AreEqual("9", result.HoursDttueClose);
            Assert.AreEqual("9", result.HoursDttueOpen);
            Assert.AreEqual("9", result.HoursDtwedClose);
            Assert.AreEqual("9", result.HoursDtwedOpen);
            Assert.AreEqual("9", result.HoursFriClose);
            Assert.AreEqual("9", result.HoursFriOpen);
            Assert.AreEqual("9", result.HoursMonClose);
            Assert.AreEqual("9", result.HoursMonOpen);
            Assert.AreEqual("9", result.HoursSatClose);
            Assert.AreEqual("9", result.HoursSatOpen);
            Assert.AreEqual("9", result.HoursSunClose);
            Assert.AreEqual("9", result.HoursSunOpen);
            Assert.AreEqual("9", result.HoursThuClose);
            Assert.AreEqual("9", result.HoursThuOpen);
            Assert.AreEqual("9", result.HoursTueClose);
            Assert.AreEqual("9", result.HoursTueOpen);
            Assert.AreEqual("9", result.HoursWedClose);
            Assert.AreEqual("9", result.HoursWedOpen);
            Assert.AreEqual("Walk-Up", result.InstallationType);
            Assert.AreEqual(13.3108M, result.Latitude);
            Assert.AreEqual(BooleanEnum.Y, result.LimitedTransactions);
            Assert.AreEqual("11170401-4112-43c1-aa4e-f73370e1014a", result.LocationId);
            Assert.IsNotNull(result.locations);
            Assert.AreEqual(0, result.locations.Capacity);
            Assert.AreEqual(LocationTypeEnum.A, result.LocationType);
            Assert.AreEqual(-132.8851M, result.Longitude);
            Assert.AreEqual(BooleanEnum.Y, result.MilitaryIdRequired);
            Assert.AreEqual("BECU", result.Name);
            Assert.AreEqual(BooleanEnum.Y, result.OnMilitaryBase);
            Assert.AreEqual(BooleanEnum.Y, result.OnPremise);
            Assert.AreEqual("4896771019", result.Phone);
            Assert.AreEqual("39759", result.PostalCode);
            Assert.AreEqual(BooleanEnum.Y, result.RestrictedAccess);
            Assert.AreEqual("Northgate", result.RetailOutlet);
            Assert.AreEqual(BooleanEnum.Y, result.SelfServiceDevice);
            Assert.AreEqual(BooleanEnum.Y, result.SelfServiceOnly);
            Assert.AreEqual(BooleanEnum.Y, result.SoftDelete);
            Assert.AreEqual(StateEnum.MS, result.State);
            Assert.AreEqual(BooleanEnum.Y, result.Surcharge);
            Assert.AreEqual(BooleanEnum.Y, result.TakeCoopData);
            Assert.AreEqual("https://trypap.com/", result.WebAddress);

        }


        [TestMethod]
        public void AllTablesViewModel_GetNewLocation_Null_Parameter_Should_Pass()
        {

            // Arrange

            // Act
            var result = AllTablesViewModel.GetNewLocation(null);
            // Assert
            Assert.IsNull(result);

        }

        [TestMethod]
        public void AllTablesViewModel_GetNewContact_Null_Parameter_Should_Pass()
        {

            // Arrange

            // Act
            var result = AllTablesViewModel.GetNewContact(null);
            // Assert
            Assert.IsNull(result);

        }

        [TestMethod]
        public void AllTablesViewModel_GetNewContact_NonNull_Parameter_Should_Pass()
        {

            // Arrange
            var viewModel = new AllTablesViewModel();

            viewModel.AcceptCash = BooleanEnum.Y;
            viewModel.AcceptDeposit = BooleanEnum.Y;
            viewModel.Access = BooleanEnum.Y;
            viewModel.AccessNotes = "Lobby";
            viewModel.Address = "362 Oxford Dr.";
            viewModel.Cashless = BooleanEnum.Y;
            viewModel.City = "Starkville";
            viewModel.CoopLocationId = "WA9820-174920573";
            viewModel.Country = "US";
            viewModel.County = "King County";
            viewModel.DriveThruOnly = BooleanEnum.Y;
            viewModel.EnvelopeRequired = BooleanEnum.Y;
            viewModel.Fax = "8058451931";
            viewModel.HandicapAccess = BooleanEnum.Y;
            viewModel.Hours = "24 HOURS ACCESS";
            viewModel.HoursDtfriClose = "9";
            viewModel.HoursDtfriOpen = "9";
            viewModel.HoursDtmonClose = "9";
            viewModel.HoursDtmonOpen = "9";
            viewModel.HoursDtsatClose = "9";
            viewModel.HoursDtsatOpen = "9";
            viewModel.HoursDtsunClose = "9";
            viewModel.HoursDtsunOpen = "9";
            viewModel.HoursDtthuClose = "9";
            viewModel.HoursDtthuOpen = "9";
            viewModel.HoursDttueClose = "9";
            viewModel.HoursDttueOpen = "9";
            viewModel.HoursDtwedClose = "9";
            viewModel.HoursDtwedOpen = "9";
            viewModel.HoursFriClose = "9";
            viewModel.HoursFriOpen = "9";
            viewModel.HoursMonClose = "9";
            viewModel.HoursMonOpen = "9";
            viewModel.HoursSatClose = "9";
            viewModel.HoursSatOpen = "9";
            viewModel.HoursSunClose = "9";
            viewModel.HoursSunOpen = "9";
            viewModel.HoursThuClose = "9";
            viewModel.HoursThuOpen = "9";
            viewModel.HoursTueClose = "9";
            viewModel.HoursTueOpen = "9";
            viewModel.HoursWedClose = "9";
            viewModel.HoursWedOpen = "9";
            viewModel.InstallationType = "Walk-Up";
            viewModel.Latitude = 13.3108M;
            viewModel.LimitedTransactions = BooleanEnum.Y;
            viewModel.LocationId = "11170401-4112-43c1-aa4e-f73370e1014a";
            viewModel.locations = new List<Locations>();
            viewModel.LocationType = LocationTypeEnum.A;
            viewModel.Longitude = -132.8851M;
            viewModel.MilitaryIdRequired = BooleanEnum.Y;
            viewModel.Name = "BECU";
            viewModel.OnMilitaryBase = BooleanEnum.Y;
            viewModel.OnPremise = BooleanEnum.Y;
            viewModel.Phone = "4896771019";
            viewModel.PostalCode = "39759";
            viewModel.RestrictedAccess = BooleanEnum.Y;
            viewModel.RetailOutlet = "Northgate";
            viewModel.SelfServiceDevice = BooleanEnum.Y;
            viewModel.SelfServiceOnly = BooleanEnum.Y;
            viewModel.SoftDelete = BooleanEnum.Y;
            viewModel.State = StateEnum.MS;
            viewModel.Surcharge = BooleanEnum.Y;
            viewModel.TakeCoopData = BooleanEnum.Y;
            viewModel.WebAddress = null;

            // Act
            var result = AllTablesViewModel.GetNewContact(viewModel);

            // Assert
            Assert.AreEqual(viewModel.LocationId, result.LocationId);
            Assert.AreEqual(viewModel.Phone, result.Phone);
            Assert.AreEqual(viewModel.Fax, result.Fax);
            Assert.AreEqual(viewModel.WebAddress, result.WebAddress);
            Assert.IsNull(result.Location);

        }

        [TestMethod]
        public void AllTablesViewModel_GetNewContact_All_Contact_Properties_Are_Null_Parameter_Should_Pass()
        {

            // Arrange
            var viewModel = new AllTablesViewModel();

            viewModel.AcceptCash = BooleanEnum.Y;
            viewModel.AcceptDeposit = BooleanEnum.Y;
            viewModel.Access = BooleanEnum.Y;
            viewModel.AccessNotes = "Lobby";
            viewModel.Address = "362 Oxford Dr.";
            viewModel.Cashless = BooleanEnum.Y;
            viewModel.City = "Starkville";
            viewModel.CoopLocationId = "WA9820-174920573";
            viewModel.Country = "US";
            viewModel.County = "King County";
            viewModel.DriveThruOnly = BooleanEnum.Y;
            viewModel.EnvelopeRequired = BooleanEnum.Y;
            viewModel.Fax = null;
            viewModel.HandicapAccess = BooleanEnum.Y;
            viewModel.Hours = "24 HOURS ACCESS";
            viewModel.HoursDtfriClose = "9";
            viewModel.HoursDtfriOpen = "9";
            viewModel.HoursDtmonClose = "9";
            viewModel.HoursDtmonOpen = "9";
            viewModel.HoursDtsatClose = "9";
            viewModel.HoursDtsatOpen = "9";
            viewModel.HoursDtsunClose = "9";
            viewModel.HoursDtsunOpen = "9";
            viewModel.HoursDtthuClose = "9";
            viewModel.HoursDtthuOpen = "9";
            viewModel.HoursDttueClose = "9";
            viewModel.HoursDttueOpen = "9";
            viewModel.HoursDtwedClose = "9";
            viewModel.HoursDtwedOpen = "9";
            viewModel.HoursFriClose = "9";
            viewModel.HoursFriOpen = "9";
            viewModel.HoursMonClose = "9";
            viewModel.HoursMonOpen = "9";
            viewModel.HoursSatClose = "9";
            viewModel.HoursSatOpen = "9";
            viewModel.HoursSunClose = "9";
            viewModel.HoursSunOpen = "9";
            viewModel.HoursThuClose = "9";
            viewModel.HoursThuOpen = "9";
            viewModel.HoursTueClose = "9";
            viewModel.HoursTueOpen = "9";
            viewModel.HoursWedClose = "9";
            viewModel.HoursWedOpen = "9";
            viewModel.InstallationType = "Walk-Up";
            viewModel.Latitude = 13.3108M;
            viewModel.LimitedTransactions = BooleanEnum.Y;
            viewModel.LocationId = "11170401-4112-43c1-aa4e-f73370e1014a";
            viewModel.locations = new List<Locations>();
            viewModel.LocationType = LocationTypeEnum.A;
            viewModel.Longitude = -132.8851M;
            viewModel.MilitaryIdRequired = BooleanEnum.Y;
            viewModel.Name = "BECU";
            viewModel.OnMilitaryBase = BooleanEnum.Y;
            viewModel.OnPremise = BooleanEnum.Y;
            viewModel.Phone = null;
            viewModel.PostalCode = "39759";
            viewModel.RestrictedAccess = BooleanEnum.Y;
            viewModel.RetailOutlet = "Northgate";
            viewModel.SelfServiceDevice = BooleanEnum.Y;
            viewModel.SelfServiceOnly = BooleanEnum.Y;
            viewModel.SoftDelete = BooleanEnum.Y;
            viewModel.State = StateEnum.MS;
            viewModel.Surcharge = BooleanEnum.Y;
            viewModel.TakeCoopData = BooleanEnum.Y;
            viewModel.WebAddress = null;

            // Act
            var result = AllTablesViewModel.GetNewContact(viewModel);

            // Assert
            Assert.IsNull(result);

        }

        [TestMethod]
        public void AllTablesViewModel_GetNewSpecialQualities_Null_Parameter_Should_Pass()
        {

            // Arrange
            
            // Act
            var result = AllTablesViewModel.GetNewSpecialQualities(null);

            // Assert
            Assert.IsNull(result);

        }

        [TestMethod]
        public void AllTablesViewModel_GetNewSpecialQualities_NonNull_Parameter_Should_Pass()
        {

            // Arrange
            var viewModel = new AllTablesViewModel();

            viewModel.AcceptCash = BooleanEnum.Y;
            viewModel.AcceptDeposit = BooleanEnum.Y;
            viewModel.Access = BooleanEnum.Y;
            viewModel.AccessNotes = "Lobby";
            viewModel.Address = "362 Oxford Dr.";
            viewModel.Cashless = BooleanEnum.Y;
            viewModel.City = "Starkville";
            viewModel.CoopLocationId = "WA9820-174920573";
            viewModel.Country = "US";
            viewModel.County = "King County";
            viewModel.DriveThruOnly = BooleanEnum.Y;
            viewModel.EnvelopeRequired = BooleanEnum.Y;
            viewModel.Fax = "8058451931";
            viewModel.HandicapAccess = BooleanEnum.Y;
            viewModel.Hours = "24 HOURS ACCESS";
            viewModel.HoursDtfriClose = "9";
            viewModel.HoursDtfriOpen = "9";
            viewModel.HoursDtmonClose = "9";
            viewModel.HoursDtmonOpen = "9";
            viewModel.HoursDtsatClose = "9";
            viewModel.HoursDtsatOpen = "9";
            viewModel.HoursDtsunClose = "9";
            viewModel.HoursDtsunOpen = "9";
            viewModel.HoursDtthuClose = "9";
            viewModel.HoursDtthuOpen = "9";
            viewModel.HoursDttueClose = "9";
            viewModel.HoursDttueOpen = "9";
            viewModel.HoursDtwedClose = "9";
            viewModel.HoursDtwedOpen = "9";
            viewModel.HoursFriClose = "9";
            viewModel.HoursFriOpen = "9";
            viewModel.HoursMonClose = "9";
            viewModel.HoursMonOpen = "9";
            viewModel.HoursSatClose = "9";
            viewModel.HoursSatOpen = "9";
            viewModel.HoursSunClose = "9";
            viewModel.HoursSunOpen = "9";
            viewModel.HoursThuClose = "9";
            viewModel.HoursThuOpen = "9";
            viewModel.HoursTueClose = "9";
            viewModel.HoursTueOpen = "9";
            viewModel.HoursWedClose = "9";
            viewModel.HoursWedOpen = "9";
            viewModel.InstallationType = "Walk-Up";
            viewModel.Latitude = 13.3108M;
            viewModel.LimitedTransactions = BooleanEnum.Y;
            viewModel.LocationId = "11170401-4112-43c1-aa4e-f73370e1014a";
            viewModel.locations = new List<Locations>();
            viewModel.LocationType = LocationTypeEnum.A;
            viewModel.Longitude = -132.8851M;
            viewModel.MilitaryIdRequired = BooleanEnum.Y;
            viewModel.Name = "BECU";
            viewModel.OnMilitaryBase = BooleanEnum.Y;
            viewModel.OnPremise = BooleanEnum.Y;
            viewModel.Phone = "4896771019";
            viewModel.PostalCode = "39759";
            viewModel.RestrictedAccess = BooleanEnum.Y;
            viewModel.RetailOutlet = "Northgate";
            viewModel.SelfServiceDevice = BooleanEnum.Y;
            viewModel.SelfServiceOnly = BooleanEnum.Y;
            viewModel.SoftDelete = BooleanEnum.Y;
            viewModel.State = StateEnum.MS;
            viewModel.Surcharge = BooleanEnum.Y;
            viewModel.TakeCoopData = BooleanEnum.Y;
            viewModel.WebAddress = null;

            // Act
            var result = AllTablesViewModel.GetNewSpecialQualities(viewModel);


            // Assert
            Assert.AreEqual(viewModel.AcceptCash.ToString(), result.AcceptCash);
            Assert.AreEqual(viewModel.AcceptDeposit.ToString(), result.AcceptDeposit);
            Assert.AreEqual(viewModel.Access.ToString(), result.Access);
            Assert.AreEqual(viewModel.AccessNotes, result.AccessNotes);
            Assert.AreEqual(viewModel.Cashless.ToString(), result.Cashless);
            Assert.AreEqual(viewModel.DriveThruOnly.ToString(), result.DriveThruOnly);
            Assert.AreEqual(viewModel.EnvelopeRequired.ToString(), result.EnvelopeRequired);
            Assert.AreEqual(viewModel.HandicapAccess.ToString(), result.HandicapAccess);
            Assert.AreEqual(viewModel.InstallationType.ToString(), result.InstallationType);
            Assert.AreEqual(viewModel.LimitedTransactions.ToString(), result.LimitedTransactions);
            Assert.AreEqual(viewModel.LocationId.ToString(), result.LocationId);
            Assert.AreEqual(viewModel.MilitaryIdRequired.ToString(), result.MilitaryIdRequired);
            Assert.AreEqual(viewModel.OnMilitaryBase.ToString(), result.OnMilitaryBase);
            Assert.AreEqual(viewModel.OnPremise.ToString(), result.OnPremise);
            Assert.AreEqual(viewModel.RestrictedAccess.ToString(), result.RestrictedAccess);
            Assert.AreEqual(viewModel.SelfServiceDevice.ToString(), result.SelfServiceDevice);
            Assert.AreEqual(viewModel.SelfServiceOnly.ToString(), result.SelfServiceOnly);
            Assert.AreEqual(viewModel.Surcharge.ToString(), result.Surcharge);
            Assert.IsNull(result.Location);

        }

        [TestMethod]
        public void AllTablesViewModel_GetNewSpecialQualities_All_Properties_Are_Null_Parameter_Should_Pass()
        {
            // Arrange
            var viewModel = new AllTablesViewModel();

            viewModel.AcceptCash = null;
            viewModel.AcceptDeposit = null;
            viewModel.Access = null;
            viewModel.AccessNotes = null;
            viewModel.Address = "362 Oxford Dr.";
            viewModel.Cashless = null;
            viewModel.City = "Starkville";
            viewModel.CoopLocationId = "WA9820-174920573";
            viewModel.Country = "US";
            viewModel.County = "King County";
            viewModel.DriveThruOnly = null;
            viewModel.EnvelopeRequired = null;
            viewModel.Fax = "8058451931";
            viewModel.HandicapAccess = null;
            viewModel.Hours = "24 HOURS ACCESS";
            viewModel.HoursDtfriClose = "9";
            viewModel.HoursDtfriOpen = "9";
            viewModel.HoursDtmonClose = "9";
            viewModel.HoursDtmonOpen = "9";
            viewModel.HoursDtsatClose = "9";
            viewModel.HoursDtsatOpen = "9";
            viewModel.HoursDtsunClose = "9";
            viewModel.HoursDtsunOpen = "9";
            viewModel.HoursDtthuClose = "9";
            viewModel.HoursDtthuOpen = "9";
            viewModel.HoursDttueClose = "9";
            viewModel.HoursDttueOpen = "9";
            viewModel.HoursDtwedClose = "9";
            viewModel.HoursDtwedOpen = "9";
            viewModel.HoursFriClose = "9";
            viewModel.HoursFriOpen = "9";
            viewModel.HoursMonClose = "9";
            viewModel.HoursMonOpen = "9";
            viewModel.HoursSatClose = "9";
            viewModel.HoursSatOpen = "9";
            viewModel.HoursSunClose = "9";
            viewModel.HoursSunOpen = "9";
            viewModel.HoursThuClose = "9";
            viewModel.HoursThuOpen = "9";
            viewModel.HoursTueClose = "9";
            viewModel.HoursTueOpen = "9";
            viewModel.HoursWedClose = "9";
            viewModel.HoursWedOpen = "9";
            viewModel.InstallationType = null;
            viewModel.Latitude = 13.3108M;
            viewModel.LimitedTransactions = null;
            viewModel.LocationId = "11170401-4112-43c1-aa4e-f73370e1014a";
            viewModel.locations = new List<Locations>();
            viewModel.LocationType = LocationTypeEnum.A;
            viewModel.Longitude = -132.8851M;
            viewModel.MilitaryIdRequired = null;
            viewModel.Name = "BECU";
            viewModel.OnMilitaryBase = null;
            viewModel.OnPremise = null;
            viewModel.Phone = "4896771019";
            viewModel.PostalCode = "39759";
            viewModel.RestrictedAccess = null;
            viewModel.RetailOutlet = "Northgate";
            viewModel.SelfServiceDevice = null;
            viewModel.SelfServiceOnly = null;
            viewModel.SoftDelete = BooleanEnum.Y;
            viewModel.State = StateEnum.MS;
            viewModel.Surcharge = null;
            viewModel.TakeCoopData = BooleanEnum.Y;
            viewModel.WebAddress = null;

            // Act
            var result = AllTablesViewModel.GetNewSpecialQualities(viewModel);


            // Assert
            Assert.IsNull(result);

        }


        // Tests null parameter. Should return null.
        [TestMethod]
        public void AllTablesViewModel_GetNewDailyHours_Null_Parameter_Should_Pass()
        {
            // Arrange
            var viewModel = new AllTablesViewModel();

            // Act
            var result = AllTablesViewModel.GetNewDailyHours(null);

            // Assert
            Assert.IsNull(result);

        }

        // Tests ConvertStringToBooleanEnum via ViewModel's InstantiateViewModelPropertiesWithOneLocation
        [TestMethod]
        public void AllTablesViewModel_ConvertStringToBooleanEnum_Parameter_No_Should_Pass()
        {
            // Arrange
            var mockData = new LocationsDataMock();
            var location = mockData.GetAllViewModelList()[0];
            var viewModel = new AllTablesViewModel();


            var properties = typeof(SpecialQualities).GetProperties(); // All properties of DailyHours is null by default
            var specialQualitiesPropertyNameList = new List<string>();
            foreach (var property in properties)
            {
                if (property.Name.Equals("LocationId") ||
                    property.Name.Equals("AccessNotes") ||
                    property.Name.Equals("InstallationType") ||
                    property.Name.Equals("Location") ||
                    property.Name.Equals("CoinStar") ||
                    property.Name.Equals("TellerServices") ||
                    property.Name.Equals("_24hourExpressBox") ||
                    property.Name.Equals("PartnerCreditUnion") ||
                    property.Name.Equals("MemberConsultant") ||
                    property.Name.Equals("InstantDebitCardReplacement"))
                {
                    continue;
                }

                property.SetValue(location.SpecialQualities, "N");
                specialQualitiesPropertyNameList.Add(property.Name);
            }
            // Act
            viewModel.InstatiateViewModelPropertiesWithOneLocation(location);


            // Assert
            properties = typeof(AllTablesViewModel).GetProperties();
            foreach (var property in properties)
            {
                if (!specialQualitiesPropertyNameList.Contains(property.Name))
                {
                        continue;
                }

                    Assert.AreEqual(BooleanEnum.N, property.GetValue(viewModel));
            }

        }


        // Tests ConvertStringToBooleanEnum via ViewModel's InstantiateViewModelPropertiesWithOneLocation
        [TestMethod]
        public void AllTablesViewModel_ConvertStringToBooleanEnum_Parameter_Null_Should_Pass()
        {
            // Arrange
            var mockData = new LocationsDataMock();
            var location = mockData.GetAllViewModelList()[0];
            var viewModel = new AllTablesViewModel();


            var properties = typeof(SpecialQualities).GetProperties(); // All properties of DailyHours is null by default
            var specialQualitiesPropertyNameList = new List<string>();
            foreach (var property in properties)
            {
                if (property.Name.Equals("LocationId") ||
                    property.Name.Equals("AccessNotes") ||
                    property.Name.Equals("InstallationType") ||
                    property.Name.Equals("Location") ||
                    property.Name.Equals("CoinStar") ||
                    property.Name.Equals("TellerServices") ||
                    property.Name.Equals("_24hourExpressBox") ||
                    property.Name.Equals("PartnerCreditUnion") ||
                    property.Name.Equals("MemberConsultant") ||
                    property.Name.Equals("InstantDebitCardReplacement"))
            {
                    continue;
                }

                property.SetValue(location.SpecialQualities, null);
                specialQualitiesPropertyNameList.Add(property.Name);
            }
            // Act
            viewModel.InstatiateViewModelPropertiesWithOneLocation(location);


            // Assert
            properties = typeof(AllTablesViewModel).GetProperties();
            foreach (var property in properties)
            {
                if (!specialQualitiesPropertyNameList.Contains(property.Name))
                {
                    continue;
                }
                    
                Assert.AreEqual(BooleanEnum.NULL, property.GetValue(viewModel));
            }

        }


        // Tests ConvertStringToBooleanEnum via ViewModel's InstantiateViewModelPropertiesWithOneLocation
        [TestMethod]
        public void AllTablesViewModel_ConvertStringToBooleanEnum_Parameter_Yes_Should_Pass()
        {
            // Arrange
            var mockData = new LocationsDataMock();
            var location = mockData.GetAllViewModelList()[0];
            var viewModel = new AllTablesViewModel();


            var properties = typeof(SpecialQualities).GetProperties(); // All properties of DailyHours is null by default
            var specialQualitiesPropertyNameList = new List<string>();
            foreach (var property in properties)
            {
                if (property.Name.Equals("LocationId") ||
                    property.Name.Equals("AccessNotes") ||
                    property.Name.Equals("InstallationType") ||
                    property.Name.Equals("Location") ||
                    property.Name.Equals("CoinStar") ||
                    property.Name.Equals("TellerServices") ||
                    property.Name.Equals("_24hourExpressBox") ||
                    property.Name.Equals("PartnerCreditUnion") ||
                    property.Name.Equals("MemberConsultant") ||
                    property.Name.Equals("InstantDebitCardReplacement"))
                {
                    continue;
                }

                property.SetValue(location.SpecialQualities, "Y");
                specialQualitiesPropertyNameList.Add(property.Name);
            }
            // Act
            viewModel.InstatiateViewModelPropertiesWithOneLocation(location);


            // Assert
            properties = typeof(AllTablesViewModel).GetProperties();
            foreach (var property in properties)
            {
                if (!specialQualitiesPropertyNameList.Contains(property.Name))
                {
                    continue;
                }
                
                Assert.AreEqual(BooleanEnum.Y, property.GetValue(viewModel));
            }

        }

        // Tests ConvertStringToStateEnum
        [TestMethod]
        public void AllTablesViewModel_ConvertStringToStateEnum_All_States_Should_Pass()
        {

            // Arrange
            var values = Enum.GetValues(typeof(StateEnum));


            // Act

            // Assert
            foreach (var value in values)
            {
                Assert.AreEqual(value, AllTablesViewModel.ConvertStringToStateEnum(value.ToString()));
            }
            
        }


        // Tests InstantiateViewModelPropertiesWithOneLocation where parameter is null
        [TestMethod]
        public void AllTablesViewModel_InstantiateViewModelropertiesWithOneLocation_Null_Parameter_Should_Pass()
        {
            // Arrange
            var viewModel = new AllTablesViewModel();
            var mockData = new LocationsDataMock();
            viewModel.locations = mockData.GetAllViewModelList();

            // Act
            var result = viewModel.InstatiateViewModelPropertiesWithOneLocation();

            // Assert
            Assert.IsTrue(result);

        }


        // Tests ConvertBooleanEnumToString via ViewModel's GetNewSpecialQualities
        [TestMethod]
        public void AllTablesViewModel_ConvertBooleanEnumToString_Parameter_No_Should_Pass()
        {

            // Arrange
            var mockData = new LocationsDataMock();
            var location = mockData.GetAllViewModelList()[0];
            var viewModel = new AllTablesViewModel();

            var viewModelProperties = typeof(AllTablesViewModel).GetProperties();
            var specialQualitiesPropertyNameList = new List<string>();

            foreach (var property in typeof(SpecialQualities).GetProperties())
            {

                if (property.Name.Equals("LocationId") ||
                    property.Name.Equals("AccessNotes") ||
                    property.Name.Equals("InstallationType") ||
                    property.Name.Equals("Location") ||
                    property.Name.Equals("CoinStar") ||
                    property.Name.Equals("TellerServices") ||
                    property.Name.Equals("_24hourExpressBox") ||
                    property.Name.Equals("PartnerCreditUnion") ||
                    property.Name.Equals("MemberConsultant") ||
                    property.Name.Equals("InstantDebitCardReplacement"))
                {
                    continue;
                }

                specialQualitiesPropertyNameList.Add(property.Name);
            }


            foreach (var property in viewModelProperties)
            {

                if (!specialQualitiesPropertyNameList.Contains(property.Name))
                {
                    continue;
                }

                property.SetValue(viewModel, BooleanEnum.N);

            }

            // Act
            var result = AllTablesViewModel.GetNewSpecialQualities(viewModel);

            // Assert
            foreach (var property in typeof(SpecialQualities).GetProperties())
            {

                if (specialQualitiesPropertyNameList.Contains(property.Name))
                {
                    Assert.AreEqual("N", property.GetValue(result));
                }

            }

        }
    }
}
