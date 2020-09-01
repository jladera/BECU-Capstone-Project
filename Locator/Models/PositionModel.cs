using NetTopologySuite;
using NetTopologySuite.Geometries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Locator.Models
{
    // initialize a coordinate data type that is accepted by google map to create a LatLng object
    public class PositionModel
    {
        public double Lat { get; set; } = 0.0;
        public double Lng { get; set; } = 0.0;



        /// <summary>
        /// Convert from String to Position
        /// </summary>
        /// <param name="lat"></param>
        /// <param name="lng"></param>
        public PositionModel(string lat, string lng)
        {
            try
            {
                Lat = Convert.ToDouble(lat);
                Lng = Convert.ToDouble(lng);
            }
            catch (Exception)
            {
                return;
            }
        }

        /// <summary>
        /// Convert from Decimal to Position
        /// </summary>
        /// <param name="lat"></param>
        /// <param name="lng"></param>
        public PositionModel(decimal lat, decimal lng)
        {
            try
            {
                Lat = Convert.ToDouble(lat);
                Lng = Convert.ToDouble(lng);
            }
            catch (Exception)
            {
                return;
            }
        }
    }
}
