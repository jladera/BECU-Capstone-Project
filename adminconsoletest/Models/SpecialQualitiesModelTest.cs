using adminconsole.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DatabaseLibrary.Models;


namespace adminconsoletest
{
    [TestClass]
    public class SpecialQualitiesModelTest
    {
        [TestMethod]
        public void SpecialQualitiesModel_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = new SpecialQualities();

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void SpecialQualitiesModel_Get_Property_Defaults_Should_Pass()
        {
            // Arrange
            var result = new SpecialQualities();

            // Act

            // Assert
            Assert.IsNull(result.Location);
            Assert.IsNull(result.LocationId);
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
            Assert.IsNull(result.MilitaryIdRequired);
            Assert.IsNull(result.OnMilitaryBase);
            Assert.IsNull(result.OnPremise);
            Assert.IsNull(result.RestrictedAccess);
            Assert.IsNull(result.SelfServiceDevice);
            Assert.IsNull(result.SelfServiceOnly);
            Assert.IsNull(result.Surcharge);
        }

        [TestMethod]
        public void SpecialQualitiesModel_Set_Property_Should_Pass()
        {
            // Arrange
            var result = new SpecialQualities();

            // Act
            result.Location = new Locations();
            result.LocationId = "location id";
            result.AcceptCash = "Y";
            result.AcceptDeposit = "Y";
            result.Access = "Y"; 
            result.AccessNotes = "additional";
            result.Cashless = "Y";
            result.DriveThruOnly = "Y";
            result.EnvelopeRequired = "Y";
            result.HandicapAccess = "Y";
            result.InstallationType = "installation type";
            result.LimitedTransactions = "Y";
            result.MilitaryIdRequired = "Y";
            result.OnMilitaryBase = "Y";
            result.OnPremise = "Y";
            result.RestrictedAccess = "Y"; 
            result.SelfServiceDevice = "Y";
            result.SelfServiceOnly = "Y";
            result.Surcharge = "Y";
            


            // Assert
            Assert.IsNotNull(result.Location);
            Assert.AreEqual("location id", result.LocationId);
            Assert.AreEqual("Y", result.AcceptCash);
            Assert.AreEqual("Y", result.AcceptDeposit);
            Assert.AreEqual("Y", result.Access);
            Assert.AreEqual("additional", result.AccessNotes);
            Assert.AreEqual("Y", result.Cashless);
            Assert.AreEqual("Y", result.DriveThruOnly);
            Assert.AreEqual("Y", result.EnvelopeRequired);
            Assert.AreEqual("Y", result.HandicapAccess);
            Assert.AreEqual("installation type", result.InstallationType);
            Assert.AreEqual("Y", result.LimitedTransactions);
            Assert.AreEqual("Y", result.MilitaryIdRequired);
            Assert.AreEqual("Y", result.OnMilitaryBase);
            Assert.AreEqual("Y", result.OnPremise);
            Assert.AreEqual("Y", result.RestrictedAccess);
            Assert.AreEqual("Y", result.SelfServiceDevice);
            Assert.AreEqual("Y", result.SelfServiceOnly);
            Assert.AreEqual("Y", result.Surcharge);
        }
    }
}
