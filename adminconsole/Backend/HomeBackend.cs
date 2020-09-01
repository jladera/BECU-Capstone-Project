namespace adminconsole.Backend
{
    /// <summary>
    /// Used to fake the login page, 
    /// </summary>
    public class HomeBackend
    {
        private const string U = "jake";
        private const string P = "isamazing";

        public bool Login(string username, string password)
        {

            if (username is null)
            {
                return false;
            }


            if (password is null)
            {
                return false;
            }

            if (!username.Trim().Equals(U))
            {
                return false;
            }


            if (!password.Trim().Equals(P))
            {
                return false;
            }

            return true;

        }
    }
}
