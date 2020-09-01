using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DatabaseLibrary.Models;
using Locator.Models;
using NetTopologySuite;
using NetTopologySuite.Geometries;
using GeoAPI.Geometries;



namespace Locator.Models
{
    public partial class CleanLocationModel
    {

        public CleanLocationModel(Locations data)
        {
            // assign locations model attributes
            if (data != null)
            {
                if (!string.IsNullOrEmpty(data.LocationId))
                {
                    LocationId = data.LocationId;
                }
                if (!string.IsNullOrEmpty(data.Name))
                {
                    Name = data.Name;
                }
                if (!string.IsNullOrEmpty(data.LocationType))
                {
                    LocationType = LocationTypeEnumHelper.StringToEnum(data.LocationType);
                    LocationTypeCode = LocationType.ToTitle();
                }
                if (!string.IsNullOrEmpty(data.Address))
                {
                    Address = data.Address;
                }
                if (!string.IsNullOrEmpty(data.City))
                {
                    City = data.City;
                }
                if (!string.IsNullOrEmpty(data.PostalCode))
                {
                    PostalCode = data.PostalCode;
                }
                if (!string.IsNullOrEmpty(data.State))
                {
                    State = StateEnumHelper.StringToEnum(data.State);
                    StateTitle = State.ToTitle();
                }
                if (!string.IsNullOrEmpty(data.Country))
                {
                    Country = data.Country;
                }
                if (!string.IsNullOrEmpty(data.RetailOutlet))
                {
                    RetailOutlet = data.RetailOutlet;
                }
                if (!string.IsNullOrEmpty(data.Hours))
                {
                    Hours = data.Hours;
                }
                if (!data.Latitude.Equals(null))
                {
                    Latitude = data.Latitude;
                }
                if (!data.Longitude.Equals(null))
                {
                    Longitude = data.Longitude;
                }


                // create a lat lng object for google map
                if ((!Latitude.Equals(null)) && (!Longitude.Equals(null)))
                {
                    Position = new PositionModel(Latitude, Longitude);
                }
            }



            // assign contact model attributes
            if (data.Contact != null)
            {
                if (!string.IsNullOrEmpty(data.Contact.Phone))
                {
                    Phone = data.Contact.Phone;
                }

                if (!string.IsNullOrEmpty(data.Contact.WebAddress))
                {
                    var protocol = "http";
                    var secureProtocol = "https://";
                    var commercialDomain = ".com";
                    var netDomain = ".net";
                    var nonProfitDomain = ".org";
                    if (data.Contact.WebAddress.Contains(commercialDomain) || data.Contact.WebAddress.Contains(netDomain) || data.Contact.WebAddress.Contains(nonProfitDomain))
                    {
                        if (!data.Contact.WebAddress.Contains(protocol))
                        {
                            WebAddress = secureProtocol + data.Contact.WebAddress;
                        }
                        else
                        {
                            WebAddress = data.Contact.WebAddress;
                        }

                    }
                }
            }


            // assign special qualities model attributes
            if (data.SpecialQualities != null)
            {
                if (!string.IsNullOrEmpty(data.SpecialQualities.HandicapAccess))
                {
                    HandicapAccess = BoolEnumHelper.StringToEnum(data.SpecialQualities.HandicapAccess);
                }
                if (!string.IsNullOrEmpty(data.SpecialQualities.Surcharge))
                {
                    Surcharge = BoolEnumHelper.StringToEnum(data.SpecialQualities.Surcharge);
                }
                if (!string.IsNullOrEmpty(data.SpecialQualities.DriveThruOnly))
                {
                    DriveThruOnly = BoolEnumHelper.StringToEnum(data.SpecialQualities.DriveThruOnly);
                }
                if (!string.IsNullOrEmpty(data.SpecialQualities.RestrictedAccess))
                {
                    RestrictedAccess = BoolEnumHelper.StringToEnum(data.SpecialQualities.RestrictedAccess);
                }
                if (!string.IsNullOrEmpty(data.SpecialQualities.AcceptDeposit))
                {
                    AcceptDeposit = BoolEnumHelper.StringToEnum(data.SpecialQualities.AcceptDeposit);
                }
                if (!string.IsNullOrEmpty(data.SpecialQualities.AcceptCash))
                {
                    AcceptCash = BoolEnumHelper.StringToEnum(data.SpecialQualities.AcceptCash);
                }
                if (!string.IsNullOrEmpty(data.SpecialQualities.Cashless))
                {
                    Cashless = BoolEnumHelper.StringToEnum(data.SpecialQualities.Cashless);
                }
                if (!string.IsNullOrEmpty(data.SpecialQualities.SelfServiceDevice))
                {
                    SelfServiceOnly = BoolEnumHelper.StringToEnum(data.SpecialQualities.SelfServiceDevice);
                }
                if (!string.IsNullOrEmpty(data.SpecialQualities.SelfServiceOnly))
                {
                    SelfServiceOnly = BoolEnumHelper.StringToEnum(data.SpecialQualities.SelfServiceOnly);
                }
                if (!string.IsNullOrEmpty(data.SpecialQualities.OnMilitaryBase))
                {
                    OnMilitaryBase = BoolEnumHelper.StringToEnum(data.SpecialQualities.OnMilitaryBase);
                }
                if (!string.IsNullOrEmpty(data.SpecialQualities.MilitaryIdRequired))
                {
                    MilitaryIdRequired = BoolEnumHelper.StringToEnum(data.SpecialQualities.MilitaryIdRequired);
                }
                if (!string.IsNullOrEmpty(data.SpecialQualities.CoinStar))
                {
                    CoinStar = BoolEnumHelper.StringToEnum(data.SpecialQualities.CoinStar);
                }
                if (!string.IsNullOrEmpty(data.SpecialQualities.TellerServices))
                {
                    TellerServices = BoolEnumHelper.StringToEnum(data.SpecialQualities.TellerServices);
                }
                if (!string.IsNullOrEmpty(data.SpecialQualities._24hourExpressBox))
                {
                    _24hourExpressBox = BoolEnumHelper.StringToEnum(data.SpecialQualities._24hourExpressBox);
                }
                if (!string.IsNullOrEmpty(data.SpecialQualities.PartnerCreditUnion))
                {
                    PartnerCreditUnion = BoolEnumHelper.StringToEnum(data.SpecialQualities.PartnerCreditUnion);
                }
                if (!string.IsNullOrEmpty(data.SpecialQualities.MemberConsultant))
                {
                    MemberConsultant = BoolEnumHelper.StringToEnum(data.SpecialQualities.MemberConsultant);
                }
                if (!string.IsNullOrEmpty(data.SpecialQualities.InstantDebitCardReplacement))
                {
                    InstantDebitCardReplacement = BoolEnumHelper.StringToEnum(data.SpecialQualities.InstantDebitCardReplacement);
                }
                if (!string.IsNullOrEmpty(data.SpecialQualities.InstallationType))
                {
                    InstallationType = data.SpecialQualities.InstallationType;
                }
                if (!string.IsNullOrEmpty(data.SpecialQualities.AccessNotes))
                {
                    AccessNotes = data.SpecialQualities.AccessNotes;
                }





                // call builder functions
                SubTitleDisplay = GetSubTitleDisplayStrings();
                ListBlockDisplay = GetListDisplayStrings();
                FooterBlockQuoteDisplay = GetFooterBlockQuoteDisplayStrings();
            }
        }
    }
}
