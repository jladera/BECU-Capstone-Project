using adminconsole.Backend;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace adminconsoletest
{
    [TestClass]
    public class HomeBackendTest
    {



        /// <summary>
        /// Login successful
        /// </summary>
        [TestMethod]
        public void HomeBackend_Login_Correct_Credentials_Should_Pass()
        {
            string U = "jake";
            string P = "isamazing";

            var backend = new HomeBackend();

            var result = backend.Login(U, P);

            Assert.IsTrue(result);
        }






        /// <summary>
        /// Login unsuccessful due to null username
        /// </summary>
        [TestMethod]
        public void HomeBackend_Login_Null_Username_Should_Not_Pass()
        {
            string P = "isamazing";

            var backend = new HomeBackend();

            var result = backend.Login(null, P);

            Assert.IsFalse(result);
        }







        /// <summary>
        /// Login unsuccessful due to null password
        /// </summary>
        [TestMethod]
        public void HomeBackend_Login_Null_Password_Should_Not_Pass()
        {
            string U = "jake";

            var backend = new HomeBackend();

            var result = backend.Login(U, null);

            Assert.IsFalse(result);
        }







        /// <summary>
        /// Login unsuccessful due to incorrect username
        /// </summary>
        [TestMethod]
        public void HomeBackend_Login_Incorrect_Username_Should_Pass()
        {
            string U = "jakes";
            string P = "isamazing";

            var backend = new HomeBackend();

            var result = backend.Login(U, P);

            Assert.IsFalse(result);
        }







        /// <summary>
        /// Login unsuccessful due to incorrect password
        /// </summary>
        [TestMethod]
        public void HomeBackend_Login_Incorrect_Password_Should_Pass()
        {
            string U = "jake";
            string P = "isam azing";

            var backend = new HomeBackend();

            var result = backend.Login(U, P);

            Assert.IsFalse(result);
        }

    }
}
