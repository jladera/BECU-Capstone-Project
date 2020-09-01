using NetTopologySuite.Geometries;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DatabaseLibrary.Models
{
    public partial class Locations : IMaphawksDatabaseTable
    {
        /// <summary>
        /// LocationId
        /// </summary>
        [Required]
        public string LocationId { get; set; }




        /// <summary>
        /// Co-Op's LocationId 
        /// </summary>
        public string CoopLocationId { get; set; }





        /// <summary>
        /// Indicates if location record gets updated with every 
        /// batch execution
        /// </summary>
        [Required]
        [DisplayName("Take Co-Op Data")]
        public bool TakeCoopData { get; set; }





        /// <summary>
        /// Indicates not to show to external members, but can
        /// be viewed by System Admins. Also allows System Admin
        /// to recover the location record.
        /// </summary>
        [Required]
        public bool SoftDelete { get; set; }






        /// <summary>
        /// Institution Name
        /// </summary>
        [Required]
        [DisplayName("Institution")]
        public string Name { get; set; }





        /// <summary>
        /// Street address
        /// </summary>
        [Required]
        [DisplayName("Address")]
        public string Address { get; set; }





        /// <summary>
        /// City Name
        /// </summary>
        [Required]
        [DisplayName("City")]
        public string City { get; set; }






        /// <summary>
        /// County name (if provided)
        /// </summary>
        [DisplayName("County")]
        public string County { get; set; }






        /// <summary>
        /// 2 letter State code
        /// </summary>
        [Required]
        [DisplayName("State")]
        public string State { get; set; }





        /// <summary>
        /// Postal code
        /// </summary>
        [Required]
        [DisplayName("Zipcode")]
        public string PostalCode { get; set; }







        /// <summary>
        /// 2 letter Country code
        /// 
        /// Should be blank or US for now
        /// </summary>
        [DisplayName("Country")]
        public string Country { get; set; }







        /// <summary>
        /// Latitude
        /// </summary>
        [Required]
        [DisplayName("Latitude")]
        public decimal Latitude { get; set; }








        /// <summary>
        /// Longitude
        /// </summary>
        [Required]
        [DisplayName("Longitude")]
        public decimal Longitude { get; set; }







        /// <summary>
        /// Provided by Co-Op. 
        /// 
        /// Is either 24rs Access or Business Hours Access
        /// </summary>
        [DisplayName("24hrs / Business Hours Access")]
        public string Hours { get; set; }





        /// <summary>
        /// Retail Outlet name
        /// 
        /// ex. Safeway or _________ Mall
        /// </summary>
        [DisplayName("Retail Outlet")]
        public string RetailOutlet { get; set; }






        /// <summary>
        /// Location type.
        /// 
        /// Either A (ATM) or S (Shared Branch)
        /// </summary>
        [Required]
        [DisplayName("Location Type")]
        public string LocationType { get; set; }




        /// <summary>
        /// Used to return Location query results in sorted order from a given latitude/longitude
        /// 
        /// Un-used in this application, but used in externally-facing mapping service
        /// </summary>
        public Geometry Point { get; set; }





        /// <summary>
        /// 
        /// Locations record can never have all null values as it
        /// is the main table which requires multiple fields to contain
        /// values.
        /// 
        /// </summary>
        /// 
        /// 
        /// 
        /// 
        /// <returns> 
        /// 
        /// Always return false as per model definition it will never allow
        /// a Location record to be created that will have all null values.
        /// 
        /// </returns>
        public bool AllPropertiesAreNull()
        {
            return false;
        }





        #region Objects Used for Joins
        public virtual Contacts Contact { get; set; }
        public virtual SpecialQualities SpecialQualities { get; set; }
        public virtual DailyHours DailyHours { get; set; }
        #endregion

        
    }
}
