using adminconsole.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Reflection;
using DatabaseLibrary.Models;


namespace adminconsoletest
{
    [TestClass]
    public class DailyHoursModelTest
    {

        // Tests default instantiation of DailyHours
        [TestMethod]
        public void DailyHours_Default_Should_Pass()
        {
            // Arrange
            var result = new DailyHours();

            // Act


            //Assert
            Assert.IsNotNull(result);   // DailyHours object exists

            PropertyInfo[] properties = typeof(DailyHours).GetProperties(); // All properties of DailyHours is null by default
            foreach (PropertyInfo property in properties)
            {
                var actual = property.GetValue(result);
                Assert.IsNull(actual);
            }
        }







        // Tests DailyHours Get and Set Properties
        [TestMethod]
        public void DailyHours_Set_Properties_Should_Pass()
        {
            // Arrange
            var result = new DailyHours();

            // Act
            PropertyInfo[] properties = typeof(DailyHours).GetProperties();
            foreach (PropertyInfo property in properties)
            {
                if (property.PropertyType.Name.Equals("Locations"))
                {
                    property.SetValue(result, new Locations());
                }

                else
                {
                    property.SetValue(result, property.Name);
                }
            }



            //Assert
            Assert.IsNotNull(result);   // DailyHours object exists

            foreach (PropertyInfo property in properties)
            {
                if (property.PropertyType.Name.Equals("Locations"))
                {
                    Assert.AreEqual("Locations", property.PropertyType.Name);
                }


                else
                {
                    var expected = property.Name;
                    var actual = property.GetValue(result);
                    Assert.AreEqual(expected, actual);
                }
            }
        }
    }
}
