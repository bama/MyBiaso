using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Xml;

namespace MyBiaso.Core.DistanceCalculation.Geocode {
    
    /// <summary>
    /// Führt eine Geokodierung mit Google-Maps durch.
    /// </summary>
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

            // abfragen
            var response =
                QueryGoogle("http://maps.google.com/maps/api/directions/xml?origin=" + AddressToQueryString(origin) +
                            "&destination=" + AddressToQueryString(destination) + "&alternatives=true&sensor=false");

            // Routen abfragen
            var routes = GetRoutes(response, origin, destination);
            // sortieren nach Entfernung // TODO: Dies eventuell durch eine Einstellung beeinflussen
            routes.Sort((a, b) => a.DistanceInMeter.CompareTo(b.DistanceInMeter));
            // erstes Element zurückgeben
            return (routes.Count == 0 ? null : routes[0]);
        }

        /// <summary>
        /// Erstellt aus der Adresse den QueryString.
        /// </summary>
        /// <param name="address">Adresse</param>
        /// <returns>Zeichenkette für die Abfrage</returns>
        private static string AddressToQueryString(Address address) {
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

        /// <summary>
        /// Führt eine Anfrage an Google aus.
        /// </summary>
        /// <param name="uri">Uri</param>
        /// <returns>Dokument der Antwort</returns>
        private static XmlDocument QueryGoogle(string uri) {
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

        
        /// <summary>
        /// Bestimmt die Routen aus dem übergebenen Dokument.
        /// </summary>
        /// <param name="document">Dokument</param>
        /// <param name="origin">Start</param>
        /// <param name="destination">Ziel</param>
        /// <returns>Routen aus dem Dokument</returns>
        private static List<Route> GetRoutes(XmlNode document, Address origin, Address destination) {
            // vorbereiten
            var routes = new List<Route>();

            // Alle Routen bestimmen
            var legNodes = document.SelectNodes("/DirectionsResponse/route/leg");
            // prüfen
            if (legNodes != null) {
                // durchlaufen
                foreach (XmlNode legNode in legNodes) {
                    // bestimmen 
                    var route = GetRouteFromLeg(legNode);
                    // prüfen
                    if (null != route) {
                        // Einfügen in die Liste
                        route.Origin = origin;
                        route.Destination = destination;
                        routes.Add(route);
                    }
                }
            }

            // zurückgeben
            return routes;
        }

        /// <summary>
        /// Erstellt aus dem Knoten die Route. 
        /// </summary>
        /// <param name="leg">Knoten</param>
        /// <returns>Route</returns>
        private static Route GetRouteFromLeg(XmlNode leg) {
            // vorbereiten
            var route = new Route();

            // bestimmen der Distanz (als Knoten)
            var distanceNode = leg.SelectSingleNode("distance/value");
            // prüfen
            if(null != distanceNode) {
                // parsen des Inhalts des Distanzknotens
                long value;
                if (long.TryParse(distanceNode.InnerText, out value)) {
                    // setzen
                    route.DistanceInMeter = value;
                } else {
                    // Route auf null setzen
                    route = null;
                }
            }
            

            // zurückgeben)
            return route;
        }

    }
}
