using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Xml;

namespace MyBiaso.Core.DistanceCalculation.Geocode {
    public class GoogleMapsGeocode {

        /// <summary>
        /// Berechnet die Route von <paramref name="origin"/> nach <paramref name="destination"/>.
        /// </summary>
        /// <param name="origin">Start</param>
        /// <param name="destination">Ziel</param>
        /// <returns>Route</returns>
        public Route CalculateRoute(Address origin, Address destination) {
            if(null == origin) throw new ArgumentNullException("origin");
            if(null == destination) throw new ArgumentNullException("destination");

            var response =
                QueryGoogle("http://maps.google.com/maps/api/directions/xml?origin=" + AddressToQueryString(origin) +
                            "&destination=" + AddressToQueryString(destination) + "&sensor=false");

            var node = response.SelectSingleNode("/DirectionsResponse/status");
            if(String.Equals("OK", node.InnerText, StringComparison.InvariantCultureIgnoreCase)) {
                var result = new Route {Origin = origin, Destination = destination};
                var distance = response.SelectSingleNode("/DirectionsResponse/route/leg/distance/value");

                long value;
                if(long.TryParse(distance.InnerText, out value)) {
                    result.DistanceInMeter = value;
                    return result;
                } 
            }


            return null;

        }

        public Location CalculateGeocode(Address address) {
            if(null == address) throw new ArgumentNullException("address");

            var response = QueryGoogle("http://maps.google.com/maps/api/geocode/xml?address=" + AddressToQueryString(address) + "&sensor=false");

            var node = response.SelectSingleNode("/GeocodeResponse/status");
            if(String.Equals("OK", node.InnerText, StringComparison.InvariantCultureIgnoreCase)) {
                var latitude = response.SelectSingleNode("/GeocodeResponse/result/geometry/location/lat");
                var longitude = response.SelectSingleNode("/GeocodeResponse/result/geometry/location/lng");

                var result = new Location();
                result.Latitude = latitude.InnerXml;
                result.Longitude = longitude.InnerXml;
                return result;
            } else {
                // Fehler bei der Abfrage -> ignorieren
                return new Location();
            }
        }



        private string AddressToQueryString(Address address) {
            var queryString = new StringBuilder();

            queryString.Append(address.Street);
            queryString.Append("+");
            queryString.Append(address.Housenumber);
            queryString.Append("+");
            queryString.Append(address.ZipCode);
            queryString.Append("+");
            queryString.Append(address.City);

            return queryString.ToString();
        }

        private XmlDocument QueryGoogle(string uri) {
            try {
                var request = WebRequest.Create(uri);
                var response = request.GetResponse();
                var reader = new StreamReader(response.GetResponseStream());
                var responseContent = reader.ReadToEnd();
                reader.Close();
                var result = new XmlDocument();
                result.LoadXml(responseContent);
                return result;
            } catch (Exception exception) {
                throw new GeocodeException("Fehler bei der Codierung mittels Google Maps API.", exception);
            }
        }
    }


    public class GeocodeException : Exception {
        public GeocodeException(string message, Exception exception):base(message, exception) {
            
        }
    }
}
