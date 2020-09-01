using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using DatabaseLibrary.Models;
using Locator.Models;
using Locator.Backend;

namespace Locator.Backend
{
    public interface IDatabaseHelper
    {

        public Task<List<Locations>> ReadMultipleRecordsAsync();
    }
}
