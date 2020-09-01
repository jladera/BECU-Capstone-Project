using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DatabaseLibrary.Models
{
    public partial class Contacts : IMaphawksDatabaseTable
    {
        [Required]
        #nullable disable
        public string LocationId { get; set; }
        
        [DisplayName("Phone")]
        public string Phone { get; set; }
        
        [DisplayName("Fax")]
        public string Fax { get; set; }
        
        [DisplayName("URL")]
        public string WebAddress { get; set; }


        // Location object used for joins
        public virtual Locations Location { get; set; }





        /// <summary>
        /// Determines if all the properties of  a Contacts
        /// record is null.
        /// </summary>
        /// 
        /// 
        /// <returns> True if all properties are null, otherwise False </returns>
        public bool AllPropertiesAreNull()
        {
            var result = Fax ??
                         Phone ??
                         WebAddress ?? null;

            return result is null;
        }
    }
}
