using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using DatabaseLibrary.Models;


namespace adminconsole.Models
{
    public partial class AllTablesViewModel
    {
        #region Locations Properties
        [Required]
        public string LocationId { get; set; }

        [DisplayName("Co-Op ID")]
        #nullable enable
        public string? CoopLocationId { get; set; }

        [DisplayName("Allow Co-Op Overwrite")]
        [EnumDataType(typeof(BooleanEnum))]
        [Required]
        public BooleanEnum TakeCoopData { get; set; }

        [DisplayName("Delete")]
        [EnumDataType(typeof(BooleanEnum))]
        [Required]
        public BooleanEnum SoftDelete { get; set; }

        [DisplayName("Institution")]
        #nullable enable
        public string? Name { get; set; }

        [Required]
        [DisplayName("Type")]
        [EnumDataType(typeof(LocationTypeEnum))]
        public LocationTypeEnum LocationType { get; set; }

        [Required]
        [DisplayName("Street")]
        public string Address { get; set; }

        [Required]
        [DisplayName("City")]
        public string City { get; set; }

        [Required]
        [DisplayName("Zipcode")]
        public string PostalCode { get; set; }

        [DisplayName("County")]
        #nullable enable
        public string? County { get; set; }

        [Required]
        [DisplayName("State")]
        [EnumDataType(typeof(StateEnum))]
        public StateEnum State { get; set; }

        [DisplayName("Country")]
        #nullable enable
        public string? Country { get; set; }

        [Required]
        [DisplayName("Latitude")]
        public decimal Latitude { get; set; } = 0.0M;

        [Required]
        [DisplayName("Longitude")]
        public decimal Longitude { get; set; } = 0.0M;

        [DisplayName("Retail Outlet")]
        #nullable enable
        public string? RetailOutlet { get; set; }

        [DisplayName("Hours")]
        #nullable enable
        public string? Hours { get; set; }
        #endregion

        #region Contacts Properties
        [DisplayName("Phone")]
        public string? Phone { get; set; }

        [DisplayName("Fax")]
        public string? Fax { get; set; }

        [DisplayName("Web Address")]
        public string? WebAddress { get; set; }
        #endregion

        #region Special Qualities Properties
        [DisplayName("Restricted Access")]
        #nullable enable
        public BooleanEnum? RestrictedAccess { get; set; }

        [DisplayName("Accepts Deposits")]
        #nullable enable
        public BooleanEnum? AcceptDeposit { get; set; }

        [DisplayName("Envelope Required")]
        #nullable enable
        public BooleanEnum? EnvelopeRequired { get; set; }

        [DisplayName("On Premise")]
        #nullable enable
        public BooleanEnum? OnPremise { get; set; }

        [DisplayName("Access")]
        #nullable enable
        public BooleanEnum? Access { get; set; }

        [DisplayName("Installation Type")]
        #nullable enable
        public string? InstallationType { get; set; }

        [DisplayName("Drive Thru Only")]
        #nullable enable
        public BooleanEnum? DriveThruOnly { get; set; }

        [DisplayName("Limited Transactions")]
        #nullable enable
        public BooleanEnum? LimitedTransactions { get; set; }

        [DisplayName("Handicap Access")]
        #nullable enable
        public BooleanEnum? HandicapAccess { get; set; }

        [DisplayName("Accepts Cash")]
        #nullable enable
        public BooleanEnum? AcceptCash { get; set; }

        [DisplayName("Cashless")]
        #nullable enable
        public BooleanEnum? Cashless { get; set; }

        [DisplayName("Self Service Only")]
        #nullable enable
        public BooleanEnum? SelfServiceOnly { get; set; }

        [DisplayName("Surcharge")]
        #nullable enable
        public BooleanEnum? Surcharge { get; set; }

        [DisplayName("On Military Base")]
        #nullable enable
        public BooleanEnum? OnMilitaryBase { get; set; }

        [DisplayName("Military ID Required")]
        #nullable enable
        public BooleanEnum? MilitaryIdRequired { get; set; }

        [DisplayName("Self Service Device")]
        #nullable enable
        public BooleanEnum? SelfServiceDevice { get; set; }

        [DisplayName("Additional Detail")]
        #nullable enable
        [StringLength(100)]
        public string? AccessNotes { get; set; }


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
        #endregion

        #region Hours Per Day Of The Week Properties
        [DisplayName("Mon Open")]
        public string? HoursMonOpen { get; set; }

        [DisplayName("Mon Close")]
        public string? HoursMonClose { get; set; }

        [DisplayName("Tues Open")]
        public string? HoursTueOpen { get; set; }

        [DisplayName("Tues Close")]
        public string? HoursTueClose { get; set; }

        [DisplayName("Wed Open")]
        public string? HoursWedOpen { get; set; }

        [DisplayName("Wed Close")]
        public string? HoursWedClose { get; set; }

        [DisplayName("Thu Open")]
        public string? HoursThuOpen { get; set; }

        [DisplayName("Thu Close")]
        public string? HoursThuClose { get; set; }

        [DisplayName("Fri Open")]
        public string? HoursFriOpen { get; set; }

        [DisplayName("Fri Close")]
        public string? HoursFriClose { get; set; }

        [DisplayName("Sat Open")]
        public string? HoursSatOpen { get; set; }

        [DisplayName("Sat Close")]
        public string? HoursSatClose { get; set; }

        [DisplayName("Sun Open")]
        public string? HoursSunOpen { get; set; }

        [DisplayName("Sun Close")]
        public string? HoursSunClose { get; set; }

        [DisplayName("Mon Open Drive-Thru")]
        public string? HoursDtmonOpen { get; set; }

        [DisplayName("Mon Close Drive-Thru")]
        public string? HoursDtmonClose { get; set; }

        [DisplayName("Tues Open Drive-Thru")]
        public string? HoursDttueOpen { get; set; }

        [DisplayName("Tues Close Drive-Thru")]
        public string? HoursDttueClose { get; set; }

        [DisplayName("Wed Open Drive-Thru")]
        public string? HoursDtwedOpen { get; set; }

        [DisplayName("Wed Close Drive-Thru")]
        public string? HoursDtwedClose { get; set; }

        [DisplayName("Thu Open Drive-Thru")]
        public string? HoursDtthuOpen { get; set; }

        [DisplayName("Thu Close Drive-Thru")]
        public string? HoursDtthuClose { get; set; }

        [DisplayName("Fri Open Drive-Thru")]
        public string? HoursDtfriOpen { get; set; }

        [DisplayName("Fri Close Drive-Thru")]
        public string? HoursDtfriClose { get; set; }

        [DisplayName("Sat Open Drive-Thru")]
        public string? HoursDtsatOpen { get; set; }

        [DisplayName("Sat Close Drive-Thru")]
        public string? HoursDtsatClose { get; set; }

        [DisplayName("Sun Open Drive-Thru")]
        public string? HoursDtsunOpen { get; set; }

        [DisplayName("Sun Close Drive-Thru")]
        public string? HoursDtsunClose { get; set; }
        #endregion

        public List<Locations> locations { get; set;  }
    }
}