using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DatabaseLibrary.Models;
using Locator.Models;
using Locator.Backend;

namespace Locator.Backend
{
    public class DatabaseHelper : IDatabaseHelper
    {

        private MaphawksContext context;

        public DatabaseHelper(MaphawksContext context)
        {
            this.context = context;
        }

        public virtual async Task<List<Locations>> ReadMultipleRecordsAsync()
        {
            var result = await context.Locations
                         .Include(c => c.Contact)
                         .Include(s => s.SpecialQualities)
                         .Include(h => h.DailyHours)
                         .AsNoTracking()
                         .ToListAsync()
                         .ConfigureAwait(false);


            return result;
        }
    }
}
