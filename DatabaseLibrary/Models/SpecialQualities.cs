using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DatabaseLibrary.Models
{
    public partial class SpecialQualities : IMaphawksDatabaseTable
    {
        [Required]
        #nullable disable
        public string LocationId { get; set; }

        [DisplayName("Restricted Access")]
        public string RestrictedAccess { get; set; }

        [DisplayName("Accepts Deposit")]
        public string AcceptDeposit { get; set; }

        [DisplayName("Accepts Cash")]
        public string AcceptCash { get; set; }
        
        [DisplayName("Envelope Required")]
        public string EnvelopeRequired { get; set; }
        
        [DisplayName("On Military Base")]
        public string OnMilitaryBase { get; set; }
        
        [DisplayName("On Premise")]
        public string OnPremise { get; set; }
        
        [DisplayName("Surcharge")]
        public string Surcharge { get; set; }
        
        [DisplayName("Access")]        
        public string Access { get; set; }
        
        [DisplayName("Access Notes")]
        public string AccessNotes { get; set; }
        
        [DisplayName("Installation Type")]
        public string InstallationType { get; set; }
        
        [DisplayName("Handicap Access")]
        public string HandicapAccess { get; set; }
        
        [DisplayName("Cashless")]
        public string Cashless { get; set; }
        
        [DisplayName("Drive Thru Only")]
        public string DriveThruOnly { get; set; }
        
        [DisplayName("Limited Transactions")]
        public string LimitedTransactions { get; set; }
        
        [DisplayName("Military ID Required")]
        public string MilitaryIdRequired { get; set; }
        
        [DisplayName("Self Service Device")]
        public string SelfServiceDevice { get; set; }
        
        [DisplayName("Self Service Only")]
        public string SelfServiceOnly { get; set; }



        // NFC attributes
        [Display(Name = "Coin Star")]
        #nullable enable
        public string? CoinStar { get; set; }

        [Display(Name = "Teller Services")]
        #nullable enable
        public string? TellerServices { get; set; }

        [Display(Name = "24 Hour Express Box")]
        #nullable enable
        public string? _24hourExpressBox { get; set; }

        [Display(Name = "Partner Credit Union")]
        #nullable enable
        public string? PartnerCreditUnion { get; set; }

        [Display(Name = "Member Consultant")]
        #nullable enable
        public string? MemberConsultant { get; set; }

        [Display(Name = "Instant Debit Card Replacement")]
        #nullable enable
        public string? InstantDebitCardReplacement { get; set; }

        // Location Object. Used for joins.
        public virtual Locations Location { get; set; }


        /// <summary>
        /// Determines if all the properties of  a SpecialQualities
        /// record is null.
        /// </summary>
        /// 
        /// 
        /// <returns> True if all properties are null, otherwise False </returns>
        public bool AllPropertiesAreNull()
        {
            var result = AcceptCash ??
                         AcceptDeposit ??
                         Access ??
                         AccessNotes ??
                         Cashless ??
                         DriveThruOnly ??
                         EnvelopeRequired ??
                         InstallationType ??
                         LimitedTransactions ??
                         MilitaryIdRequired ??
                         OnMilitaryBase ??
                         OnPremise ??
                         RestrictedAccess ??
                         SelfServiceDevice ??
                         SelfServiceOnly ??
                         Surcharge ?? null;



            return result is null;
            
        }
    }
}
