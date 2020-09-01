namespace adminconsole.Models
{
    public enum AlterRecordInfoEnum
    {

        // Create a new record in a table
        // ex. If Location #1 has no SpecialQualities, but Admin adds a SpecialQuality when 
        // editing the Location Info.
        Create = 0,

        // Update a record
        Update = 1,

        // Delete a row from a table. Not used on Locations table as rows in Locations table 
        // only ever get Soft Deleted.
        // Delete rows from other tables when all fields (other than LocationId) are null.
        Delete = 2,

        // Is only for Locations records.
        // Sets SoftDelete from 1 to 0
        Recover = 3

    }
}
