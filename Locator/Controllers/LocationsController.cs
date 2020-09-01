using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DatabaseLibrary.Models;
using Locator.Backend;
using Locator.Models;
using System.Collections.Generic;
using System.Linq;
using NetTopologySuite;
using NetTopologySuite.Geometries;
using Newtonsoft.Json;
using System;

namespace Locator.Controllers
{
    public class LocationsController : Controller
    {
        private readonly MaphawksContext _context;
        private LocationsBackend backend;



        public LocationsController(MaphawksContext context, LocationsBackend backend = null)
        {
            _context = context;

            // Fork to allow for mocking out backend
            if (backend != null)
            {
                this.backend = backend;
            }
            else
            {
                this.backend = new LocationsBackend(_context);
            }
        }



        public async Task<IActionResult> Index()
        {
            var cleanResults = await GetCleanViewModel();

            return View(cleanResults);
        }



        [Produces("application/json")]
        public async Task<JsonResult> CardJson()
        {

            var cleanResults = await GetCleanViewModel();

            // get the user's location values in the session cookie
            var Latitude = Request.Cookies["latitude"];
            var Longitude = Request.Cookies["longitude"];

            // if the user blocks cookies then use the tukwila headquarters as default coordinates
            if (string.IsNullOrEmpty(Latitude))
            {
                Latitude = "47.490209";
            }
            if (string.IsNullOrEmpty(Longitude))
            {
                Longitude = "-122.272126";
            }

            // create a new latlng object from the assigned location values
            var point = new PositionModel(Latitude, Longitude);

            // create and object that can pass the user location along with the list of atms to the ajax via json
            var data = new
            {
                point,
                cleanResults.CleanLocationList
            };

            return new JsonResult(data);
        }




        public async Task<CleanLocationViewModel> GetCleanViewModel()
        {
            // get the raw un-parsed values from the locations model
            var dirtyResults = await backend.IndexAsync().ConfigureAwait(false);
            // get the parsed values to eliminate undefined values after processing in the CleanLocationModel
            var cleanResults = new CleanLocationViewModel(dirtyResults);

            // get the user's location values in the session cookie
            var Latitude = Request.Cookies["latitude"];
            var Longitude = Request.Cookies["longitude"];

            // if the user blocks cookies then use the tukwila headquarters as default coordinates
            if (string.IsNullOrEmpty(Latitude))
            {
                Latitude = "47.490209";
            }
            if (string.IsNullOrEmpty(Longitude))
            {
                Longitude = "-122.272126";
            }

            // create a new latlng object from the assigned location values
            var point = new PositionModel(Latitude, Longitude);

            // assign user coordinates from the latlng object
            var geometryFactory = NtsGeometryServices.Instance.CreateGeometryFactory(srid: 4326);
            Point userPoint = geometryFactory.CreatePoint(new Coordinate(point.Lat, point.Lng));

            // update distance value based on user coordinates and reset geometry factory to null before sending
            foreach (var value in cleanResults.CleanLocationList)
            {
                value.MyPoint = geometryFactory.CreatePoint(new Coordinate(value.Position.Lat, value.Position.Lng));
                value.MyDistance = value.MyPoint.Distance(userPoint);
                value.MyPoint = null;
            }

            // sort the clean results list by distance and reduce by range
            cleanResults.CleanLocationList = cleanResults.CleanLocationList.OrderBy(x => x.MyDistance).ToList().GetRange(0, 16);


            return cleanResults;
        }
    }
}