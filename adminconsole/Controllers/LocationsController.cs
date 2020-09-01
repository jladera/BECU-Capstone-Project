using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using adminconsole.Models;
using adminconsole.Backend;
using DatabaseLibrary.Models;
using System;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Collections.Generic;

namespace adminconsole.Controllers
{
    public class LocationsController : Controller
    {

        private readonly MaphawksContext _context;
        private LocationsBackend backend;


        /// <summary>
        /// Constructor for normal function of application
        /// </summary>
        /// <param name="context"></param>
        public LocationsController(MaphawksContext context, LocationsBackend backend=null)
        {
            _context = context;

            // Fork to allow for mocking out backend
            if (backend != null)
            {
                this.backend = backend;
            } else
            {
                this.backend = new LocationsBackend(_context);
            }
        }


        /// GET:
        /// <summary>Locations objects with joins on Contacts, SpecialQualities, and DailyHours.</summary>
        /// 
        /// <returns> Index View with injected list of Locations objects </returns>
        public async Task<IActionResult> Index(string searchKeyword)
        {
            var results = await backend.GetRangeOfRecords(0, 500);
            
            //var results = await backend.IndexAsync().ConfigureAwait(false);
            //results = results.GetRange(0, 3);
            return View(results);

        }


        /// POST: 
        /// <summary>Searches database for a keyword in the Name, Address, City, or State fields.</summary>
        /// 
        /// <param name="SearchKeyword"> Search keyword or phrase </param>
        /// 
        /// <returns> Redirects to Index if an error, otherwise returns the Search view </returns>
        [HttpPost]
        public ActionResult Search(string SearchKeyword)
        {

            if (string.IsNullOrEmpty(SearchKeyword) ||
                string.IsNullOrWhiteSpace(SearchKeyword))
            {
                return RedirectToAction(nameof(Index));
            }
            var results = _context.Locations
                                .Include(c => c.Contact)
                                .Include(s => s.SpecialQualities)
                                .Include(d => d.DailyHours)
                                .Where(record => record.Name.Contains(SearchKeyword) || record.Address.Contains(SearchKeyword) || record.City.Contains(SearchKeyword) || record.State.Contains(SearchKeyword))
                                .Where(record => record.SoftDelete == false)
                                .ToList();
            return View(results);
        }


        /// POST: 
        /// <summary> Endpoint for Ajax pagin call.</summary>
        /// 
        /// <param name="start_index"> Defines how many records to Skip() </param>
        /// <param name="num_records"> Defines how many records to Take() </param>
        /// 
        /// <returns> Returns records as an HTML string to insert if there are more records, otherwise returns an empty string </returns>
        [HttpPost]
        [Produces("application/json")]
        public async Task<JsonResult> GetRangeOfRecords(int start_index, int num_records)
        {
            string error_msg = null;
            if (start_index < 0)
            {
                error_msg = "Start index cannot be less than 0. Your start index was: " + start_index;
            }

            if (num_records < 1)
            {
                error_msg = "Number of records cannot be less than 1. Your number of records was: " + num_records;
            }

            if (!string.IsNullOrEmpty(error_msg))
            {
                var ret =  new JsonResult(error_msg);
                return ret;
            }

            var result_list = await backend.GetRangeOfRecords(start_index, num_records);

            var result_html = backend.CreateTableRow(result_list, start_index);


            var response = new JsonResult(new
            {
                html = result_html,
                number_records = result_list.Count
            });

            return response;

        }


        /// GET: Locations/Details/5
        /// <summary>Singular Locations object joined with Contacts, SpecialQualities, and DailyHours.</summary>
        ///
        /// <returns> Details View with injected list of Locations objects </returns>
        public async Task<IActionResult> Details(string id)
        {

            if (id == null)
            {
                return NotFound();
            }

            var result = await backend.DetailsAsync(id).ConfigureAwait(false);

            if (result == null)
            {
                return NotFound();
            }

            return View(result);

        }


        /// GET: Locations/Create
        /// <summary>Displays the Create View with the intent of inserting into the Database.</summary>
        /// 
        /// <returns> Create View </returns>
        public IActionResult Create()
        {
            return View();
        }



        /// POST: 
        /// <summary>Receives a LocationsContactsSpecialQualitiesViewModel Object with the intent of inserting into the Database.</summary>
        /// 
        /// <param name="newLocation"> LocationsContactsSpecialQualitiesViewModel Object, instantiated with values provided by the user </param>
        /// 
        /// <returns> Either returns the existing view if there is an error, otherwise returns the Index View </returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("LocationId," +
            "CoopLocationId," +
            "TakeCoopData," +
            "SoftDelete," +
            "Name," +
            "Address," +
            "City," +
            "County," +
            "State," +
            "PostalCode," +
            "Country," +
            "Latitude," +
            "Longitude," +
            "Hours," +
            "RetailOutlet," +
            "LocationType," +
            "Phone," +
            "Fax," +
            "WebAddress," +
            "RestrictedAccess," +
            "AcceptDeposit," +
            "AcceptCash," +
            "EnvelopeRequired," +
            "OnMilitaryBase," +
            "OnPremise," +
            "Surcharge," +
            "Access," +
            "AccessNotes," +
            "InstallationType," +
            "HandicapAccess," +
            "Cashless," +
            "DriveThruOnly," +
            "LimitedTransactions," +
            "MilitaryIdRequired," +
            "SelfServiceDevice," +
            "SelfServiceOnly," +
            "HoursMonOpen," +
            "HoursMonClose," +
            "HoursTueOpen," +
            "HoursTueClose," +
            "HoursWedOpen," +
            "HoursWedClose," +
            "HoursThuOpen," +
            "HoursThuClose," +
            "HoursFriOpen," +
            "HoursFriClose," +
            "HoursSatOpen," +
            "HoursSatClose," +
            "HoursSunOpen," +
            "HoursSunClose," +
            "HoursDtmonOpen," +
            "HoursDtmonClose," +
            "HoursDttueOpen," +
            "HoursDttueClose," +
            "HoursDtwedOpen," +
            "HoursDtwedClose," +
            "HoursDtthuOpen," +
            "HoursDtthuClose," +
            "HoursDtfriOpen," +
            "HoursDtfriClose," +
            "HoursDtsatOpen," +
            "HoursDtsatClose," +
            "HoursDtsunOpen," +
            "HoursDtsunClose")] AllTablesViewModel newLocation)
        {

            if (!ModelState.IsValid)
            {
                var view = View(newLocation);
                view.ViewName = "Create";
                return view;
            }


            var result = backend.Create(newLocation);


            return RedirectToAction(nameof(Index));

        }


        /// GET: Locations/Edit/5
        /// <summary>Allows the user to edit the Location information of a given Locations Object.</summary>
        ///
        /// <param name="id"> string ID of the Locations Object </param>
        /// 
        /// <returns> Returns Not Found if the ID is null or does not exist in the Database, otherwise returns the Index View </returns>
        public async Task<IActionResult> Edit(string id)
        {

            if (id == null)
            {
                return NotFound();
            }

            var locations = await backend.EditAsync(id).ConfigureAwait(false);
            // If error in DB query
            if (locations == null)
            {
                return NotFound();
            }

            // If no location found
            if (locations.locations == null)
            {
                return NotFound();
            }

            return View(locations);

        }


        /// POST: Locations/Edit/5
        /// <summary>Creates the LocationsContactsSpecialQualitiesViewModel Object and attempts to insert into the Database.</summary>
        /// 
        /// <param name="id"> The string ID of a Locations Object </param>
        /// <param name="location"> The Locations Object containing the edited Location data </param>
        /// 
        /// <returns> 
        /// Returns the current View if the ID is null or if there is a Database error when attempting to insert. 
        /// If the ID isn't found in the Database, return a Not Found. Otherwise, returns Index View.
        /// </returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("LocationId," +
            "CoopLocationId," +
            "TakeCoopData," +
            "SoftDelete," +
            "Name," +
            "Address," +
            "City," +
            "County," +
            "State," +
            "PostalCode," +
            "Country," +
            "Latitude," +
            "Longitude," +
            "Hours," +
            "RetailOutlet," +
            "LocationType," +
            "Phone," +
            "Fax," +
            "WebAddress," +
            "RestrictedAccess," +
            "AcceptDeposit," +
            "AcceptCash," +
            "EnvelopeRequired," +
            "OnMilitaryBase," +
            "OnPremise," +
            "Surcharge," +
            "Access," +
            "AccessNotes," +
            "InstallationType," +
            "HandicapAccess," +
            "Cashless," +
            "DriveThruOnly," +
            "LimitedTransactions," +
            "MilitaryIdRequired," +
            "SelfServiceDevice," +
            "SelfServiceOnly," +
            "HoursMonOpen," +
            "HoursMonClose," +
            "HoursTueOpen," +
            "HoursTueClose," +
            "HoursWedOpen," +
            "HoursWedClose," +
            "HoursThuOpen," +
            "HoursThuClose," +
            "HoursFriOpen," +
            "HoursFriClose," +
            "HoursSatOpen," +
            "HoursSatClose," +
            "HoursSunOpen," +
            "HoursSunClose," +
            "HoursDtmonOpen," +
            "HoursDtmonClose," +
            "HoursDttueOpen," +
            "HoursDttueClose," +
            "HoursDtwedOpen," +
            "HoursDtwedClose," +
            "HoursDtthuOpen," +
            "HoursDtthuClose," +
            "HoursDtfriOpen," +
            "HoursDtfriClose," +
            "HoursDtsatOpen," +
            "HoursDtsatClose," +
            "HoursDtsunOpen," +
            "HoursDtsunClose")] AllTablesViewModel location)
        {

            if (location == null) // Edit submit error
            {
                return View(location);
            }

            if (id != location.LocationId) // IDs don't match
            {
                return NotFound();
            }

            if (!ModelState.IsValid) // Invalid model state
            {
                return View(location);
            }


            var result = await backend.EditPostAsync(location).ConfigureAwait(false);
          
            if (!result) // DB update error, retry
            {
                return View(location);
            }

            return RedirectToAction(nameof(Index));

        }


        /// GET: Locations/Delete/5
        /// <summary>Gets the Locations Object that the user wants to Delete. This Locations Object is joined with all other tables.</summary>
        /// 
        /// <param name="id"> The ID of the Locations record the user wants to Delete </param>
        /// 
        /// <returns> Not Found if the ID is null or does not exist in the Database. Otherwise returns the Delete View</returns>
        public async Task<IActionResult> Delete(string id)
        {

            if (id == null)
            {
                return NotFound();
            }

            var locations = await backend.GetLocationAsync(id).ConfigureAwait(false);
            if (locations == null)
            {
                return NotFound();
            }

            return View(locations);

        }


        /// POST: Locations/Delete/5
        /// <summary>Marks the Locations Object's SoftDelete field as True</summary>
        /// 
        /// <param name="id"> The string ID of the Locations Object the user wants to Delete </param>
        /// 
        /// <returns> The Delete View if there was a Database error for user to retry, otherwise returns the Index View </returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {

            var result = await backend.DeleteConfirmedAsync(id).ConfigureAwait(false);
            
            if (!result)
            {
                return RedirectToAction(nameof(Delete));
            }
                
           return RedirectToAction(nameof(Index));

        }

        /// GET
        /// <summary>Locations objects with joins on Contacts, SpecialQualities, and DailyHours.</summary>
        /// 
        /// <returns> Deleted View with injected list of Locations objects </returns>
        public async Task<IActionResult> Deleted()
        {

            //var results = await backend.IndexAsync(true).ConfigureAwait(false);
            var results = await backend.GetRangeOfRecords(0, 25, true);
            return View(results);
        
        }


        /// GET
        /// <summary>Gets deleted record from DB to display</summary>
        /// 
        /// <param name="id"> Location Id</param>
        /// 
        /// <returns> Detail view for Deleted locations </returns>
        [ActionName("DeletedDetails")]
        public async Task<IActionResult> DeletedDetailsAsync(string id)
        {

            if (string.IsNullOrWhiteSpace(id))
            {
                return RedirectToAction(nameof(Deleted));
            }

            Locations location = await backend.DetailsAsync(id).ConfigureAwait(false);


            if (location is null)
            {
                return RedirectToAction(nameof(Deleted));
            }


            return View(location);
        
        }


        /// GET: Locations/Deleted/5
        /// <summary>Gets a previously Deleted Locations Object</summary>
        /// 
        /// <param name="id"> The string ID of the Locations Object record the user wants to Recover </param>
        /// 
        /// <returns> Deleted View if there was a Database error or if the ID is null, otherwise returns the Index View </returns>
        [ActionName("Recover")]
        public async Task<IActionResult> RecoverAsync(string id)
        {

            if (id == null)
            {
                return RedirectToAction(nameof(Deleted));
            }
            var result = await backend.RecoverAsync(id).ConfigureAwait(false);

            if (!result)
            {
                return RedirectToAction(nameof(Deleted));
            }

            return RedirectToAction(nameof(Index));

        }


        public async Task<IActionResult> TestAsync()
        {
            return View();
        }

        [ActionName("TestJson")]
        [HttpGet]
        public async Task<JsonResult> TestJsonAsync()
        {

            var locations = await _context.Locations.Include(c => c.Contact).Include(s => s.SpecialQualities).Include(d => d.DailyHours).ToListAsync().ConfigureAwait(true);

            foreach (var location in locations)
            {
                // Remove any circular references
                if (location.Contact != null)
                {
                    location.Contact.Location = null;
                }
                // Check if SpecialQualities is not null
                if (location.SpecialQualities != null)
                {
                    location.SpecialQualities.Location = null;
                }
                // Check if DailyHours is not null
                if (location.DailyHours != null)
                {
                    location.DailyHours.Location = null;
                }
            }

            var resultObj = new BootstrapTableResponseModel() { total = locations.Count, rows = locations };

            return Json(new { total = locations.Count, rows = locations });
        }
    }
}

