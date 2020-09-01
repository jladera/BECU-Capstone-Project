namespace DatabaseLibrary.Models
{
    
    public interface IMaphawksDatabaseTable 
    {

        //public Tables 

        /// <summary>
        /// Will be used to determine if all properties are null in Table Classes
        /// </summary>
        /// 
        /// 
        /// <returns> True if all are null, otherwise False </returns>
        public abstract bool AllPropertiesAreNull();


        
  
    }
}


public enum Table
{
    None = 0,

    Locations = 1,

    Contacts = 2,

    Special_Qualities = 3,

    Daily_Hours = 4
}
