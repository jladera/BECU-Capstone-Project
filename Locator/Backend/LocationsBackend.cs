using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DatabaseLibrary.Models;
using Locator.Models;

namespace Locator.Backend
{
    public class LocationsBackend
    {
        private DatabaseHelper db;
        //private int TakeSize;
        //private int TakeIndex;
        //private PositionModel point;

        public LocationsBackend(MaphawksContext context)
        {
            db = new DatabaseHelper(context);
        }

        public virtual async Task<List<Locations>> IndexAsync()
        {

            var locations_list = await db.ReadMultipleRecordsAsync().ConfigureAwait(false); // Select * join all tables

            return locations_list;
        }
    }
}
