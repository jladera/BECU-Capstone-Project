using adminconsole.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using DatabaseLibrary.Models;


namespace adminconsole.Backend
{

    public class LocationsBackend
    {

        private DatabaseHelper db;

        /// <summary>
        /// Constructor used for unit testing. Ensures we don't actually query the database.
        /// </summary>
        /// 
        /// <param name="mockDb">DatabaseHelper mock object</param>
        public LocationsBackend(IDatabaseHelper mockDb)
        {

            db = (DatabaseHelper)mockDb;

        }



        /// <summary>
        /// Constructor with DB Context for Live Dependency Injection
        /// </summary>
        /// 
        /// <param name="context"> DB Context Object </param>
        public LocationsBackend(MaphawksContext context)
        {

            db = new DatabaseHelper(context);

        }


        /// <summary>
        /// Gets all Locations Objects with a join on all tables on LocationId. 
        /// If deleted == false: fetches live records (SoftDelete != true)
        /// If deleted == true: fetched deleted records (SoftDelete == true)
        /// </summary>
        /// 
        /// <param name="deleted"> Lets you choose between deleted or live Locations records </param>
        /// 
        /// <returns> Returns List of Locations Objects </returns>
        public virtual async Task<List<Locations>> IndexAsync(bool deleted = false)
        {

            var locations_list = new List<Locations>();

            locations_list = await db.ReadMultipleRecordsAsync(deleted).ConfigureAwait(false); // Select * join all tables


            foreach (var location in locations_list)
            {
                ConvertDbStringsToEnums(location);
            }
            return locations_list;

        }


        /// <summary>
        /// Gets a range of records from the database
        /// </summary>
        /// <param name="start_index"> Number of records to Skip() </param>
        /// <param name="num_records"> Number of records to Take() </param>
        /// <param name="isDeleted"> SoftDelete=true or SoftDelete=false </param>
        /// <returns> Locations List Object </returns>
        public virtual async Task<List<Locations>> GetRangeOfRecords(int start_index, int num_records, bool isDeleted=false)
        {
            var locations_list = await db.GetRangeOfRecords(start_index, num_records, isDeleted).ConfigureAwait(false);

            return locations_list;
        }


        /// <summary>
        /// Gets a single Locations record with a join on all tables on LocationId
        /// </summary>
        /// 
        /// <param name="id">  The string ID of the Location record requested by the user </param>
        /// 
        /// <returns> Returns a single Locations Object </returns>
        public virtual async Task<Locations> DetailsAsync(string id)
        {

            var resultLocation = await db.ReadOneRecordAsync(id).ConfigureAwait(false);
            if (resultLocation == null)
            {
                return null;
            }

            ConvertDbStringsToEnums(resultLocation);
            return resultLocation;

        }


        /// <summary>
        /// Creates new Locations, Contacts, SpecialQualities, DailyHours.
        /// </summary>
        /// 
        /// <param name="newLocation"> ViewModel with properties corresponding to the fields for each table </param>
        /// 
        /// <returns> 
        /// False: If newLocation is null or there was an error when attempting to insert
        /// True: It newLocation is successfully inserted into the Database
        /// </returns>
        public virtual bool Create(AllTablesViewModel newLocation)
        {

            if (newLocation == null) // Non-valid ViewModel Object
            {
                return false;
            }

             // Ensures we don't end up with any duplicate LocationIds
            while (db.LocationIdNotUnique(newLocation.LocationId))
            {
                newLocation.LocationId = Guid.NewGuid().ToString();
            }

            Locations location = AllTablesViewModel.GetNewLocation(newLocation);
            Contacts contact = AllTablesViewModel.GetNewContact(newLocation);
            SpecialQualities specialQuality = AllTablesViewModel.GetNewSpecialQualities(newLocation);
            DailyHours dailyHours = AllTablesViewModel.GetNewDailyHours(newLocation);

            try
            {
                db.AlterRecordInfo(AlterRecordInfoEnum.Create, location);

                if (contact != null)
                {
                    db.AlterRecordInfo(AlterRecordInfoEnum.Create, contact);
                }

                if (specialQuality != null)
                {
                    db.AlterRecordInfo(AlterRecordInfoEnum.Create, specialQuality);
                }

                if (dailyHours != null)
                {
                    db.AlterRecordInfo(AlterRecordInfoEnum.Create, dailyHours);
                }

                db.SaveChanges();
            }
            catch (Exception)
            {
                return false;
            }

            return true;

        }


        /// <summary>
        /// Gets a single Locations Object from the Database given an LocationId.
        /// Used by LocationsController: Edit (Post), Delete (Get)
        /// </summary>
        /// 
        /// <param name="id"> The string ID of a Locations Object </param>
        /// 
        /// <returns> If the record doesn't exist returns NULL, otherwise returns the Locations Object </returns>
        public virtual async Task<Locations> GetLocationAsync(string id)
        {

            if (id == null)
            {
                return null;
            }

            Locations location;

            
            location = await db.ReadOneRecordAsync(id).ConfigureAwait(false);

            if (location != null)
            {
                ConvertDbStringsToEnums(location);
            }

            return location;

        }



        /// <summary>
        /// Gets the Location Object given a LocationId for the User to Edit upon
        /// </summary>
        /// 
        /// <param name="id"> The string ID of the Locations Object the user wants to Edit </param>
        /// 
        /// <returns> 
        /// NULL: If Location record does not exist with the given ID
        /// Locations Object: If the record was found in the Database
        /// </returns>
        public virtual async Task<AllTablesViewModel> EditAsync(string id)
        {

            if (id == null)
            {
                return null;
            }


            AllTablesViewModel locationViewModel = null;

            var location = await db.ReadOneRecordAsync(id).ConfigureAwait(false); // Get Location from Database

            if (location != null)
            {
                locationViewModel = new AllTablesViewModel(location);
            }


            return locationViewModel;

        }


        /// <summary>
        /// Updates the Location Record in the Database
        /// </summary>
        /// 
        /// <param name="newLocation"> ViewModel Object containing the data for the fields of all the tables provided by the user </param>
        /// 
        /// <returns>
        /// True: If successfully updates Location record in DB
        /// False: If newLocation is null, of if there was a Database Update error
        /// </returns>
        public virtual async Task<bool> EditPostAsync(AllTablesViewModel newLocation)
        {

            if (newLocation == null)
            {
                return false;
            }

            // Get each table's Object
            Locations location = AllTablesViewModel.GetNewLocation(newLocation);
            location.Contact = AllTablesViewModel.GetNewContact(newLocation);
            location.SpecialQualities = AllTablesViewModel.GetNewSpecialQualities(newLocation);
            location.DailyHours = AllTablesViewModel.GetNewDailyHours(newLocation);

            bool result = false; // Value to be returned

            try
            {
                Locations response = await db.ReadOneRecordAsync(newLocation.LocationId).ConfigureAwait(true);

                if (response is null)  // Location does not exist
                {
                    return false;
                }

                db._AddDeleteRow(response.Contact, location.Contact);
                db._AddDeleteRow(response.SpecialQualities, location.SpecialQualities);
                db._AddDeleteRow(response.DailyHours, location.DailyHours);


                response = location;

                result = db.AlterRecordInfo(AlterRecordInfoEnum.Update, response);

                return result;
            }
            catch (Exception e)
            {
                return false;
            }

        }



        /// <summary>
        /// Soft Deletes a Locations Object.
        /// </summary>
        /// 
        /// <param name="id"> The string ID of the Locations Object the user wants to Delete </param>
        /// 
        /// <returns>
        /// True: If successfully updates SoftDelete field to True
        /// False: If Database error or if the LocationId does not exist in Database
        /// </returns>
        public virtual async Task<bool> DeleteConfirmedAsync(string id)
        {

            if (id == null)
            {
                return false;
            }

            Locations locations;

            locations = await db.ReadOneRecordAsync(id).ConfigureAwait(false);
           

            if (locations == null) // Record not found
            {
                return false;
            }

            locations.SoftDelete = true;

            try
            {
                db.AlterRecordInfo(AlterRecordInfoEnum.Update, locations);
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }


        /// <summary>
        /// Recovers a deleted Locations record
        /// </summary>
        /// 
        /// <param name="id"> The string ID for the Locations Object the user wants to recover </param>
        /// 
        /// <returns>
        /// True: If the Locations record's SoftDelete field was successfully changed to False
        /// False: If there was a Database error when trying to Update the Locations record
        /// </returns>
        public virtual async Task<bool> RecoverAsync(string id)
        {

            if (id == null)
            {
                return false;
            }
            Locations location = new Locations();


            // Get the record
            location = await db.ReadOneRecordAsync(id).ConfigureAwait(false);

            if (location is null)  // Location doesn't exist
            {
                return false;
            }

            // Change SoftDelete value to False
            location.SoftDelete = false;

            try
            {
                db.AlterRecordInfo(AlterRecordInfoEnum.Update, location);
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }




        /// <summary>
        /// Converts Database State and LocationType strings to their corresponding Enum values so that we can use these values in the ViewModel.
        /// </summary>
        /// 
        /// <param name="location"> A Locations Object </param>
        /// 
        /// <returns> The Updated Location. </returns>
        private static Locations ConvertDbStringsToEnums(Locations location)
        {

            var state = AllTablesViewModel.ConvertStringToStateEnum(location.State);
            var locationType = AllTablesViewModel.ConvertStringToLocationTypeEnum(location.LocationType);

            location.State = state.GetType().GetMember(state.ToString()).First().GetCustomAttribute<DisplayAttribute>().Name;

            location.LocationType = locationType.GetType().GetMember(locationType.ToString()).First().GetCustomAttribute<DisplayAttribute>().Name;

            return location;

        }


        /// <summary>
        /// Creates HTML table row from model data. Intended to allow for table plugin to register 
        /// and export added records, but plugin does not act accordingly
        /// </summary>
        /// 
        /// <param name="locations"> A Locations List Object </param>
        /// <param name="start_index"> Ensures we keep our <tr data-index="{some_number}"> increasing in sequential order </param>
        /// 
        /// <returns> Location rows as a string of HTML </returns>
        public string CreateTableRow(List<Locations> locations, int start_index)
        {
            string returnString = "";
            string tdOpen = @"<td>";
            string tdClose = @"</td>";
            string tdOpenDisplayNone = @"<td class=""d-none"">";
            string nullPlaceholder = null;

            foreach (var location in locations)
            {
                // Prevents an infinite loop from forming
                if (location.Contact != null)
                {
                    location.Contact.Location = null;
                }
                
                if (location.SpecialQualities != null)
                {
                    location.SpecialQualities.Location = null;
                }
                
                if (location.DailyHours != null)
                {
                    location.DailyHours.Location = null;
                }

                location.Point = null;

                returnString = returnString + string.Format(@"<tr data-index = ""{0}"" >", start_index);
                returnString = returnString + tdOpen;
                returnString = returnString + string.Format(@"<a href = ""/Locations/Edit/{0}""> Edit </a> | ", location.LocationId);
                returnString = returnString + string.Format(@"<a href = ""/Locations/Details/{0}""> Details </a> | ", location.LocationId);
                returnString = returnString + string.Format(@"<a href = ""/Locations/Delete/{0}""> Delete </a> |", location.LocationId);
                returnString = returnString + tdClose;
                returnString = returnString + tdOpen;
                if (location.Name.ToLower().Equals("becu"))
                {
                    returnString = returnString + @"<img src=""/media/BEC-Logo-Icon-pms.png"">" + location.Name;
                } else
                {
                    returnString = returnString + @"<img src=""/media/logo_co-op.png"" style=""height: 40px;"">" + location.Name;
                }
                returnString = returnString + tdClose;
                returnString = returnString + tdOpen + location.LocationType + tdClose;
                returnString = returnString + tdOpen + location.Address + tdClose;
                returnString = returnString + tdOpen + location.City + tdClose;
                returnString = returnString + tdOpenDisplayNone + location.PostalCode + tdClose;
                returnString = returnString + CreateTableDataNullableField(location.County);
                returnString = returnString + tdOpen + location.State + tdClose;
                returnString = returnString + CreateTableDataNullableField(location.Country);
                returnString = returnString + tdOpenDisplayNone + location.Latitude + tdClose;
                returnString = returnString + tdOpenDisplayNone + location.Longitude + tdClose;
                returnString = returnString + CreateTableDataNullableField(location.RetailOutlet);
                returnString = returnString + CreateTableDataNullableField(location.Hours);
                returnString = location.Contact == null ? returnString + tdOpen + tdClose : returnString + tdOpen + location.Contact.Phone + tdClose;
                returnString = location.Contact == null ? returnString + CreateTableDataNullableField(nullPlaceholder) : returnString + CreateTableDataNullableField(location.Contact.Fax);
                returnString = location.Contact == null ? returnString + tdOpen + tdClose : returnString + tdOpen + string.Format(@"<a href=""{0}\"">{0}</a>", location.Contact.WebAddress);
                returnString = location.SpecialQualities == null ? returnString + CreateTableDataNullableField(nullPlaceholder) : returnString + CreateTableDataNullableField(location.SpecialQualities.RestrictedAccess);
                returnString = location.SpecialQualities == null ? returnString + CreateTableDataNullableField(nullPlaceholder) : returnString + CreateTableDataNullableField(location.SpecialQualities.AcceptDeposit);
                returnString = location.SpecialQualities == null ? returnString + CreateTableDataNullableField(nullPlaceholder) : returnString + CreateTableDataNullableField(location.SpecialQualities.EnvelopeRequired);
                returnString = location.SpecialQualities == null ? returnString + CreateTableDataNullableField(nullPlaceholder) : returnString + CreateTableDataNullableField(location.SpecialQualities.OnPremise);
                returnString = location.SpecialQualities == null ? returnString + CreateTableDataNullableField(nullPlaceholder) : returnString + CreateTableDataNullableField(location.SpecialQualities.Access);
                returnString = location.SpecialQualities == null ? returnString + CreateTableDataNullableField(nullPlaceholder) : returnString + CreateTableDataNullableField(location.SpecialQualities.InstallationType);
                returnString = location.SpecialQualities == null ? returnString + CreateTableDataNullableField(nullPlaceholder) : returnString + CreateTableDataNullableField(location.SpecialQualities.DriveThruOnly);
                returnString = location.SpecialQualities == null ? returnString + CreateTableDataNullableField(nullPlaceholder) : returnString + CreateTableDataNullableField(location.SpecialQualities.LimitedTransactions);
                returnString = location.SpecialQualities == null ? returnString + CreateTableDataNullableField(nullPlaceholder) : returnString + CreateTableDataNullableField(location.SpecialQualities.HandicapAccess);
                returnString = location.SpecialQualities == null ? returnString + CreateTableDataNullableField(nullPlaceholder) : returnString + CreateTableDataNullableField(location.SpecialQualities.AcceptCash);
                returnString = location.SpecialQualities == null ? returnString + CreateTableDataNullableField(nullPlaceholder) : returnString + CreateTableDataNullableField(location.SpecialQualities.Cashless);
                returnString = location.SpecialQualities == null ? returnString + CreateTableDataNullableField(nullPlaceholder) : returnString + CreateTableDataNullableField(location.SpecialQualities.SelfServiceOnly);
                returnString = location.SpecialQualities == null ? returnString + CreateTableDataNullableField(nullPlaceholder) : returnString + CreateTableDataNullableField(location.SpecialQualities.Surcharge);
                returnString = location.SpecialQualities == null ? returnString + CreateTableDataNullableField(nullPlaceholder) : returnString + CreateTableDataNullableField(location.SpecialQualities.OnMilitaryBase);
                returnString = location.SpecialQualities == null ? returnString + CreateTableDataNullableField(nullPlaceholder) : returnString + CreateTableDataNullableField(location.SpecialQualities.MilitaryIdRequired);
                returnString = location.SpecialQualities == null ? returnString + CreateTableDataNullableField(nullPlaceholder) : returnString + CreateTableDataNullableField(location.SpecialQualities.SelfServiceDevice);
                returnString = location.SpecialQualities == null ? returnString + CreateTableDataNullableField(nullPlaceholder) : returnString + CreateTableDataNullableField(location.SpecialQualities.AccessNotes);
                returnString = location.DailyHours == null ? returnString + CreateTableDataNullableField(nullPlaceholder) : returnString + CreateTableDataNullableField(location.DailyHours.HoursMonOpen);
                returnString = location.DailyHours == null ? returnString + CreateTableDataNullableField(nullPlaceholder) : returnString + CreateTableDataNullableField(location.DailyHours.HoursMonClose);
                returnString = location.DailyHours == null ? returnString + CreateTableDataNullableField(nullPlaceholder) : returnString + CreateTableDataNullableField(location.DailyHours.HoursTueOpen);
                returnString = location.DailyHours == null ? returnString + CreateTableDataNullableField(nullPlaceholder) : returnString + CreateTableDataNullableField(location.DailyHours.HoursTueClose);
                returnString = location.DailyHours == null ? returnString + CreateTableDataNullableField(nullPlaceholder) : returnString + CreateTableDataNullableField(location.DailyHours.HoursWedOpen);
                returnString = location.DailyHours == null ? returnString + CreateTableDataNullableField(nullPlaceholder) : returnString + CreateTableDataNullableField(location.DailyHours.HoursWedClose);
                returnString = location.DailyHours == null ? returnString + CreateTableDataNullableField(nullPlaceholder) : returnString + CreateTableDataNullableField(location.DailyHours.HoursThuOpen);
                returnString = location.DailyHours == null ? returnString + CreateTableDataNullableField(nullPlaceholder) : returnString + CreateTableDataNullableField(location.DailyHours.HoursThuClose);
                returnString = location.DailyHours == null ? returnString + CreateTableDataNullableField(nullPlaceholder) : returnString + CreateTableDataNullableField(location.DailyHours.HoursFriOpen);
                returnString = location.DailyHours == null ? returnString + CreateTableDataNullableField(nullPlaceholder) : returnString + CreateTableDataNullableField(location.DailyHours.HoursFriClose);
                returnString = location.DailyHours == null ? returnString + CreateTableDataNullableField(nullPlaceholder) : returnString + CreateTableDataNullableField(location.DailyHours.HoursSatOpen);
                returnString = location.DailyHours == null ? returnString + CreateTableDataNullableField(nullPlaceholder) : returnString + CreateTableDataNullableField(location.DailyHours.HoursSatClose);
                returnString = location.DailyHours == null ? returnString + CreateTableDataNullableField(nullPlaceholder) : returnString + CreateTableDataNullableField(location.DailyHours.HoursSunOpen);
                returnString = location.DailyHours == null ? returnString + CreateTableDataNullableField(nullPlaceholder) : returnString + CreateTableDataNullableField(location.DailyHours.HoursSunClose);
                returnString = location.DailyHours == null ? returnString + CreateTableDataNullableField(nullPlaceholder) : returnString + CreateTableDataNullableField(location.DailyHours.HoursDtmonOpen);
                returnString = location.DailyHours == null ? returnString + CreateTableDataNullableField(nullPlaceholder) : returnString + CreateTableDataNullableField(location.DailyHours.HoursDtmonClose);
                returnString = location.DailyHours == null ? returnString + CreateTableDataNullableField(nullPlaceholder) : returnString + CreateTableDataNullableField(location.DailyHours.HoursDttueOpen);
                returnString = location.DailyHours == null ? returnString + CreateTableDataNullableField(nullPlaceholder) : returnString + CreateTableDataNullableField(location.DailyHours.HoursDttueClose);
                returnString = location.DailyHours == null ? returnString + CreateTableDataNullableField(nullPlaceholder) : returnString + CreateTableDataNullableField(location.DailyHours.HoursDtwedOpen);
                returnString = location.DailyHours == null ? returnString + CreateTableDataNullableField(nullPlaceholder) : returnString + CreateTableDataNullableField(location.DailyHours.HoursDtwedClose);
                returnString = location.DailyHours == null ? returnString + CreateTableDataNullableField(nullPlaceholder) : returnString + CreateTableDataNullableField(location.DailyHours.HoursDtthuOpen);
                returnString = location.DailyHours == null ? returnString + CreateTableDataNullableField(nullPlaceholder) : returnString + CreateTableDataNullableField(location.DailyHours.HoursDtthuClose);
                returnString = location.DailyHours == null ? returnString + CreateTableDataNullableField(nullPlaceholder) : returnString + CreateTableDataNullableField(location.DailyHours.HoursDtfriOpen);
                returnString = location.DailyHours == null ? returnString + CreateTableDataNullableField(nullPlaceholder) : returnString + CreateTableDataNullableField(location.DailyHours.HoursDtfriClose);
                returnString = location.DailyHours == null ? returnString + CreateTableDataNullableField(nullPlaceholder) : returnString + CreateTableDataNullableField(location.DailyHours.HoursDtsatOpen);
                returnString = location.DailyHours == null ? returnString + CreateTableDataNullableField(nullPlaceholder) : returnString + CreateTableDataNullableField(location.DailyHours.HoursDtsatClose);
                returnString = location.DailyHours == null ? returnString + CreateTableDataNullableField(nullPlaceholder) : returnString + CreateTableDataNullableField(location.DailyHours.HoursDtsunOpen);
                returnString = location.DailyHours == null ? returnString + CreateTableDataNullableField(nullPlaceholder) : returnString + CreateTableDataNullableField(location.DailyHours.HoursDtsunClose);
                start_index = start_index + 1;
                returnString = returnString + @"</tr>";
            }
            return returnString;
        }


        /// <summary>
        /// Takes a hidden data column and populates it with an empty string (if null) or the field value
        /// </summary>
        /// <typeparam name="T"> Database field, occasionally string, occationally bit </typeparam>
        /// <param name="field"> Database field value </param>
        /// <returns> Table column HTML string </returns>
        private string CreateTableDataNullableField<T>(T field)
        {
            string tdClose = @"</td>";
            string tdOpenDisplayNone = @"<td class=""d-none"">";
            if (field is null)
            {
                return tdOpenDisplayNone + tdClose;
            }
            else
            {
                return tdOpenDisplayNone + field + tdClose;
            }
        }

    }
       
}
