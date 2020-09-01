using adminconsole.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DatabaseLibrary.Models;

namespace adminconsole.Backend
{
    public class DatabaseHelper : IDatabaseHelper
    {
        private MaphawksContext context;


        /// <summary>
        /// Constructor used for real deployment of app
        /// </summary>
        /// <param name="context"></param>
        public DatabaseHelper(MaphawksContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// Reads one record from the database given its Location ID
        /// </summary>
        /// 
        /// <param name="referenceId"> The LocationId of the record to retrieve from the database </param>
        /// 
        /// <returns> A location object if the record exists, otherwise null </returns>
         public virtual async Task<Locations> ReadOneRecordAsync(string referenceId)
        {

            if (string.IsNullOrEmpty(referenceId) &&
                string.IsNullOrWhiteSpace(referenceId))
            {
                return null;
            }


            var result = await context.Locations
                            .Include(c => c.Contact)
                            .Include(s => s.SpecialQualities)
                            .Include(h => h.DailyHours)
                            .AsNoTracking()
                            .Where(record => record.LocationId.Equals(referenceId))
                            .FirstAsync()
                            .ConfigureAwait(false);

            return result;

        }


        /// <summary>
        /// Queries database for records in a certain  range
        /// </summary>
        /// 
        /// <param name="start_index"> Number of records to Skip() </param>
        /// <param name="num_records"> Number of records to Take() </param>
        /// <param name="isDeleted"> SoftDelete=true (get deleted records) or SoftDelete=false (get live records) </param>
        /// 
        /// <returns> Locations List Object (with related record from other tables nested in each Locations object) </returns>
        public virtual async Task<List<Locations>> GetRangeOfRecords(int start_index, int num_records, bool isDeleted=false)
        {

            var result = await context.Locations
                            .Include(c => c.Contact)
                            .Include(s => s.SpecialQualities)
                            .Include(h => h.DailyHours)
                            .AsNoTracking()
                            .Where(record => record.SoftDelete == isDeleted)
                            .Skip(start_index)
                            .Take(num_records)
                            .ToListAsync()
                            .ConfigureAwait(false);

            return result;

        }


        /// <summary>
        /// Reads all records from database. 
        /// </summary>
        /// 
        /// <param name="isDeleted"> If true, return soft deleted records, otherwise return existing records </param>
        /// 
        /// <returns> List<Locations> object if any records in databse, otherwise returns null </returns>
        public virtual async Task<List<Locations>> ReadMultipleRecordsAsync(bool isDeleted = false)
        {

            var result = await context.Locations
                         .Include(c => c.Contact)
                         .Include(s => s.SpecialQualities)
                         .Include(h => h.DailyHours)
                         .AsNoTracking()
                         .Where(record => record.SoftDelete == isDeleted)
                         .ToListAsync()
                         .ConfigureAwait(false);


            return result;

        }







        /// <summary>
        /// Alters location record information, whether it be through: 
        ///     CREATING a new record,
        ///     UPDATING a prexisting record,
        ///     DELETING a preexisting record, or
        ///     RECOVERING a delete Locations record
        /// </summary>
        ///
        /// <param name="context"> MaphawksContext class object </param>
        /// <param name="action"> AlterRecordInfoEnum type. Create, Update, Delete or Recover</param>
        /// <param name="table"> Maphawks Database table (model class) </param>
        /// 
        /// <returns> Returns true on success, otherwise returns false </returns>
        public  virtual bool AlterRecordInfo(AlterRecordInfoEnum action, IMaphawksDatabaseTable table)
        {
            if (table is null)
            {
                return false;
            }


            var table_type = GetTable(table);


            switch (table_type)
            {

                case Table.Locations:
                    return AlterLocations(action, table);


                case Table.Contacts:
                    return AlterContacts(action, table);


                case Table.Special_Qualities:
                    return AlterSpecialQualities(action, table);


                case Table.Daily_Hours:
                    return AlterDailyHours(action, table);


                default:
                    return false;

            }

        }


        /// <summary>
        /// Performs Create, Update and Delete actions on DailyHours table
        /// </summary>
        /// 
        /// <param name="action"> AlterRecordInfoEnum type. Create, Update, Delete or Recover</param>
        /// <param name="table"> Maphawks Database table (model class) </param>
        /// 
        /// <returns>False if error, true otherwise</returns>
        private bool AlterDailyHours(AlterRecordInfoEnum action, IMaphawksDatabaseTable table)
        {
            var daily_hours_record = (DailyHours)table;

            if (action is AlterRecordInfoEnum.Create)
            {

                try
                {
                    context.Add(daily_hours_record);
                    context.SaveChanges();
                    return true;
                }
                catch (DbUpdateException)
                {
                    return false;
                }

            }


            if (action is AlterRecordInfoEnum.Update)
            {

                try
                {
                    context.Update(daily_hours_record);
                    context.SaveChanges();
                    return true;
                }
                catch (DbUpdateException)
                {
                    return false;
                }
            }


            if (action is AlterRecordInfoEnum.Delete)
            {
                try
                {
                    context.Remove(context.DailyHours.Single(c => c.LocationId.Equals(daily_hours_record.LocationId)));
                    context.SaveChanges();
                    return true;
                }
                catch (DbUpdateException)
                {
                    return false;
                }
            }


            return false;
        }


        /// <summary>
        /// Performs Create, Update, and Delete actions on SpecialQualities table
        /// </summary>
        /// 
        /// <param name="action"> AlterRecordInfoEnum type. Create, Update, Delete or Recover</param>
        /// <param name="table"> Maphawks Database table (model class) </param>
        /// 
        /// <returns>False if error, true otherwise</returns>
        private bool AlterSpecialQualities(AlterRecordInfoEnum action, IMaphawksDatabaseTable table)
        {

            var special_qualities_record = (SpecialQualities)table;

            if (action is AlterRecordInfoEnum.Create)
            {

                try
                {
                    context.Add(special_qualities_record);
                    context.SaveChanges();
                    return true;
                }
                catch (DbUpdateException)
                {
                    return false;
                }

            }


            if (action is AlterRecordInfoEnum.Update)
            {

                try
                {
                    context.Update(special_qualities_record);
                    context.SaveChanges();
                    return true;
                }
                catch (DbUpdateException)
                {
                    return false;
                }
            }


            if (action is AlterRecordInfoEnum.Delete)
            {
                try
                {
                    context.Remove(context.SpecialQualities.Single(c => c.LocationId.Equals(special_qualities_record.LocationId)));
                    context.SaveChanges();
                    return true;
                }
                catch (DbUpdateException)
                {
                    return false;
                }
            }


            return false;

        }


        /// <summary>
        /// Performs Create, Update, and Delete actions on Contacts table
        /// </summary>
        /// 
        /// <param name="action"> AlterRecordInfoEnum type. Create, Update, Delete or Recover</param>
        /// <param name="table"> Maphawks Database table (model class) </param>
        /// 
        /// <returns></returns>
        private bool AlterContacts(AlterRecordInfoEnum action, IMaphawksDatabaseTable table)
        {
            var contact_record = (Contacts)table;

            if (action is AlterRecordInfoEnum.Create)
            {

                try
                {
                    context.Add(contact_record);
                    context.SaveChanges();
                    return true;
                }
                catch (DbUpdateException)
                {
                    return false;
                }

            }


            if (action is AlterRecordInfoEnum.Update)
            {

                try
                {
                    context.Update(contact_record);
                    context.SaveChanges();
                    return true;
                }
                catch (DbUpdateException)
                {
                    return false;
                }
            }


            if (action is AlterRecordInfoEnum.Delete)
            {
                try
                {
                    context.Remove(context.Contacts.Single(c => c.LocationId.Equals(contact_record.LocationId)));
                    context.SaveChanges();
                    return true;
                }
                catch (DbUpdateException)
                {
                    return false;
                }
            }

            return false;
        }


        /// <summary>
        /// Performs Create, Update, and Delete actions on Locations table
        /// </summary>
        /// 
        /// <param name="action"> AlterRecordInfoEnum type. Create, Update, Delete or Recover</param>
        /// <param name="table"> Maphawks Database table (model class) </param>
        /// 
        /// <returns>Fals if error, true otherwise.</returns>
        private bool AlterLocations(AlterRecordInfoEnum action, IMaphawksDatabaseTable table)
        {

            var locations_record = (Locations)table;

            if (action is AlterRecordInfoEnum.Create)
            {

                try
                {
                    context.Add(locations_record);
                    context.SaveChanges();
                    return true;
                }
                catch (DbUpdateException)
                {
                    return false;
                }

            }


            if (action is AlterRecordInfoEnum.Update)
            {

                try
                {
                    context.Update(locations_record);
                    context.SaveChanges();
                    return true;
                }
                catch (DbUpdateException)
                {
                    return false;
                }
            }


            if (action is AlterRecordInfoEnum.Delete)
            {
                try
                {
                    locations_record.SoftDelete = true;
                    context.Update(locations_record);
                    context.SaveChanges();
                    return true;
                }
                catch (DbUpdateException)
                {
                    return false;
                }
            }


            if (action is AlterRecordInfoEnum.Recover)
            {
                try
                {
                    locations_record.SoftDelete = false;
                    context.Update(locations_record);
                    context.SaveChanges();
                    return true;
                }
                catch (DbUpdateException)
                {
                    return false;
                }
            }


            return false;

        }



        /// <summary>
        /// Determines child type of IMaphawksDatabaseTable
        /// </summary>
        /// 
        /// <param name="record">Represents a row in a table</param>
        /// 
        /// <returns>Table enum type corresponding to a database table, if table type is invalid, returns Table.None</returns>
        public  Table GetTable(IMaphawksDatabaseTable record)
        {

            if (!(record as Locations is null))
            {
                return Table.Locations;
            }


            if (!(record as Contacts is null))
            {
                return Table.Contacts;
            }


            if (!(record as SpecialQualities is null))
            {
                return Table.Special_Qualities;
            }


            if (!(record as DailyHours is null))
            {
                return Table.Daily_Hours;
            }


            return Table.None;

        }


        /// <summary>
        /// Adds a row in Contacts, SpecialQualities, or DailyHours table if one didn't exist before an edit.
        /// Deletes a row in Contacts, SpecialQualities, or DailyHours table if after an edit an entire row's fields are null.
        /// </summary>
        /// 
        /// <param name="referenceRow"> The row before the edit </param>
        /// <param name="editedRow"> The row after the edit </param>
        public virtual void _AddDeleteRow(IMaphawksDatabaseTable referenceRow, IMaphawksDatabaseTable editedRow)
        {

            if (referenceRow is null && editedRow is null ||        // Don't need to add a new row
                !(referenceRow is null) && !(editedRow is null))
            {
                return;
            }


            // Add row
            if (referenceRow is null && !(editedRow is null))
            {
                var table = GetTable(editedRow);

                switch (table)
                {

                    case (Table.Contacts):

                        AlterRecordInfo(AlterRecordInfoEnum.Create, (Contacts)editedRow);
                        return;

                    case (Table.Special_Qualities):

                        AlterRecordInfo(AlterRecordInfoEnum.Create, (SpecialQualities)editedRow);
                        return;

                    case (Table.Daily_Hours):

                        AlterRecordInfo(AlterRecordInfoEnum.Create, (DailyHours)editedRow);
                        return;

                    default:
                        return;
                }

            }


            // Delete row
            if (!(referenceRow is null) && editedRow is null) 
            {
                var table = GetTable(referenceRow);

                switch (table)
                {

                    case (Table.Contacts):

                        AlterRecordInfo(AlterRecordInfoEnum.Delete, (Contacts)referenceRow);
                        return;

                    case (Table.Special_Qualities):

                        AlterRecordInfo(AlterRecordInfoEnum.Delete, (SpecialQualities)referenceRow);
                        return;

                    case (Table.Daily_Hours):

                        AlterRecordInfo(AlterRecordInfoEnum.Delete, (DailyHours)referenceRow);
                        return;

                    default:
                        return;
                }

            }

        }






        
        /// <summary>
        /// Determines if a GUID is already associated with a Location record. 
        /// </summary>
        /// 
        /// <param name="id"> Database field LocationId </param>
        /// 
        /// <returns> Returns true if the LocationId is already associated with a Location record, false otherwise </returns>
        public virtual bool LocationIdNotUnique(string id)
        {

            var idExists = context.Locations.Where(x => x.LocationId.Equals(id)).Any();

            return idExists;

        }

        /// <summary>
        /// Commits changes made to Database
        /// </summary>
        /// 
        /// <returns>True if successful, false otherwise</returns>
        public virtual bool SaveChanges()
        {

            try
            {
                context.SaveChanges();
                return true;
            } catch (Exception)
            {
                return false;
            }

        }
    }
}