using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NetTopologySuite;
using DatabaseLibrary.Models;
using Locator.Models;

namespace Locator.Models
{
    public partial class CleanLocationModel
    {

        // attributes with legit values get new html tags built
        public string BuildSubTitleDisplayTag(string Key, string Display, string Title)
        {
            var subTitle = string.Format(@"<p class=""subTitle {0} empty""></p>", Key);

            if (!string.IsNullOrEmpty(Title))
            {
                subTitle = string.Format(@"<p class=""subTitle {0}"" value='{2}'> {1}: {2} </p>", Key, Display, Title);
            }

            return subTitle;
        }

        public string GetSubTitleDisplayStrings()
        {
            // start an empty string
            var subTitle = "";

            // declare attribute variables w/arguments
            subTitle += BuildSubTitleDisplayTag("Hours", "Hours", Hours);
            subTitle += BuildSubTitleDisplayTag("RetailOutlet", "Retail Outlet", RetailOutlet);
            subTitle += BuildSubTitleDisplayTag("InstallationType", "Installation Type", InstallationType);
            subTitle += BuildSubTitleDisplayTag("AccessNotes", "Notes", AccessNotes);


            return subTitle;
        }





        //<option class=""list-display {0}"" value=""{2}""></option>
        /// <summary>
        /// Use for Card Creation to improve performance for ajax
        /// </summary>

        // attributes with legit values get new html tags built
        public string BuildListBlockDisplayTag(string Key, string Label, string Value)
        {
            var listBlock = string.Format(@"<li class=""list-group-item""> {1}: <span class=""{0}"">{2}</span></li>", Key, Label, Value);

            return listBlock;
        }

        public string BuildDefaultListBlockDisplayTag(string Key)
        {
            var defaultListBlock = string.Format(@"<li class=""list-group-item {0} empty"">{0}</li>", Key);

            return defaultListBlock;
        }

        public string CreateBuildListBlockIfYes(string Key, string Label, string Value)
        {
            if (Value.Equals("Yes"))
            {
                return BuildListBlockDisplayTag(Key, Label, Value);
            }
            return BuildDefaultListBlockDisplayTag(Key);
        }

        public string CreateBuildListBlockIfNo(string Key, string Label, string Value)
        {
            if (Value.Equals("No"))
            {
                return BuildListBlockDisplayTag(Key, Label, Value);
            }
            return BuildDefaultListBlockDisplayTag(Key);
        }

        public string GetListDisplayStrings()
        {
            // start an empty string
            var listBlock = "";

            // declare attribute variables w/arguments
            listBlock += CreateBuildListBlockIfYes("HandicapAccess", "Handicap Access", HandicapAccess.ToTitle());
            listBlock += CreateBuildListBlockIfNo("Surcharge", "Surcharge", Surcharge.ToTitle());
            listBlock += CreateBuildListBlockIfYes("DriveThruOnly", "Drive Thru Only", DriveThruOnly.ToTitle());
            listBlock += CreateBuildListBlockIfYes("AcceptsDeposits", "Accepts Deposits", AcceptDeposit.ToTitle());
            listBlock += CreateBuildListBlockIfYes("AcceptsCash", "Accepts Cash", AcceptCash.ToTitle());
            listBlock += CreateBuildListBlockIfYes("Cashless", "Cashless", Cashless.ToTitle());
            listBlock += CreateBuildListBlockIfYes("SelfServiceDevice", "Self Service Device", SelfServiceDevice.ToTitle());
            listBlock += CreateBuildListBlockIfYes("SelfServiceOnly", "Self Service Only", SelfServiceOnly.ToTitle());
            listBlock += CreateBuildListBlockIfYes("OnMilitaryBase", "On Military Base", OnMilitaryBase.ToTitle());
            listBlock += CreateBuildListBlockIfYes("MilitaryIDRequired", "Military ID Required", MilitaryIdRequired.ToTitle());
            listBlock += CreateBuildListBlockIfYes("RestrictedAccess", "Restricted Access", RestrictedAccess.ToTitle());
            listBlock += CreateBuildListBlockIfYes("CoinStar", "CoinStar", CoinStar.ToTitle());
            listBlock += CreateBuildListBlockIfYes("TellerServices", "Teller Services", TellerServices.ToTitle());
            listBlock += CreateBuildListBlockIfYes("_24hourExpressBox", "24 Hour Express Box", _24hourExpressBox.ToTitle());
            listBlock += CreateBuildListBlockIfNo("PartnerCreditUnion", "Partner Credit Union", PartnerCreditUnion.ToTitle());
            listBlock += CreateBuildListBlockIfYes("MemberConsultant", "Member Consultant", MemberConsultant.ToTitle());
            listBlock += CreateBuildListBlockIfYes("InstantDebitCardReplacement", "Instant Debit Card Replacement", InstantDebitCardReplacement.ToTitle());


            return listBlock;
        }






        // attributes with legit values get new html tags built
        public string BuildFooterBlockQuoteDisplayTag(string Key, string Label, string Value)
        {
            var footerBlockQuote = string.Format(@"<blockquote class=""blockquote my-1 pt-1""><h5 class=""blockquote-footer {0}""><cite title =""{2}"">{1}: {2}</cite></h5></blockquote>", Key, Label, Value);

            return footerBlockQuote;
        }

        // default footer blockquote tag
        public string BuildDefaultFooterBlockQuoteDisplayTag(string Key)
        {
            var defaultFooterBlockQuote = string.Format(@"<blockquote><h5 class=""blockquote-footer {0} empty""></h5></blockquote>", Key);

            return defaultFooterBlockQuote;
        }

        // check for unknown value and create appropriate tag
        public string CreateFooterBlockQuoteDisplayTag(string Key, string Label, string Value)
        {
            if (LocationTypeCode != null)
            {
                return BuildFooterBlockQuoteDisplayTag(Key, Label, Value);
            }
            return BuildDefaultFooterBlockQuoteDisplayTag(Key);
        }

        // convert installation type into footer blockquote
        public string GetFooterBlockQuoteDisplayStrings()
        {
            var footerBlock = "";

            footerBlock += CreateFooterBlockQuoteDisplayTag("LocationType", "Location Type", LocationTypeCode);

            return footerBlock;
        }
    }
}
