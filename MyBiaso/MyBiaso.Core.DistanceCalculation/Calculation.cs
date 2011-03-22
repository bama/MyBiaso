using System;
using System.Collections.Generic;
using MyBiaso.Core.DistanceCalculation.Geocode;
using MyBiaso.Core.Model;

namespace MyBiaso.Core.DistanceCalculation {
    
    public class Calculation {

        /// <summary>
        /// Berechnet die Distanz zwischen zwei Adressen.
        /// </summary>
        /// <param name="from">Von</param>
        /// <param name="to">Zu</param>
        /// <exception cref="ArgumentNullException">Wird ausgelöst, wenn <paramref name="from"/> oder <paramref name="to"/> null sind.</exception>
        /// <returns>Distanz zwischen den beiden Adressen</returns>
        public long CalculateDistance(Address from, Address to) {
            if(null == from) throw new ArgumentNullException("from");
            if(null == to) throw new ArgumentNullException("to");



            try {
                var geocode = new GoogleMapsGeocode();
                var route =
                    geocode.CalculateRoute(from, to);
                return route.DistanceInMeter;
            } catch(Exception e) {
                var message = e.Message;
            }

            return 0;
        }

        /// <summary>
        /// Berechnet die zurückgelegte Distanz für den angegebenen Hausbesuch.
        /// </summary>
        /// <param name="homeVisit">Hausbesuch</param>
        /// <param name="visitBefore">Hausbesuch zuvor</param>
        /// <returns>zurückgelegte Distanz in Metern</returns>
        public long CalculateDistanceTravelled(HomeVisit homeVisit, HomeVisit visitBefore) {
            if(null == homeVisit) throw new ArgumentNullException("homeVisit");

            // standardmässig vom Start zuhause ausgehen
            var origin = GetHomeAddress();
            // überprüfen, ob der Besuch zuvor existiert und dieser nicht zuhause abgeschlossen wurde (und ein Kunde existiert)
            if((null != visitBefore) && (!visitBefore.DrivenHome) && (null != visitBefore.Customer)) {
                // Hausadresse des Kunden als Startpunkt verwenden
                origin = GetCustomerAddress(visitBefore.Customer);
            }

            // zunächst die Distanz berechnen vom Start zum Kunden
            var distanceTravelled = CalculateDistance(origin, GetCustomerAddress(homeVisit.Customer));
            
            // dann überprüfen, ob anschließend nach Hause gefahren wurde
            if(homeVisit.DrivenHome) {
                // dann kommt noch zusätzlich die Strecke für die Fahrt nach Hause dazu
                distanceTravelled += CalculateDistance(GetCustomerAddress(homeVisit.Customer), GetHomeAddress());
            }

            // zurückgeben
            return distanceTravelled;
        }

        /// <summary>
        /// Liefert die Heimatadresse zurück.
        /// </summary>
        /// <returns>Heimatadresse</returns>
        private static Address GetHomeAddress() {
            // TODO: Dies in die Einstellungen auslagern
            return new Address {City = "Berlin", Street = "Unter den Linden", Housenumber = "1", ZipCode = "10117"};
        }

        /// <summary>
        /// Erstellt aus dem Kunden ein Adressobjekt.
        /// </summary>
        /// <param name="customer">Kunde</param>
        /// <returns>Adresse</returns>
        private static Address GetCustomerAddress(Customer customer) {
            return new Address
            {
                City = customer.City,
                Housenumber = customer.Housenumber,
                Street = customer.Street,
                ZipCode = customer.ZipCode
            };
        }
    }
}
