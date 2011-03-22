using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyBiaso.Core.DistanceCalculation.Geocode;
using MyBiaso.Core.Model;

namespace MyBiaso.Core.DistanceCalculation {
    public class Calculation {

        public static Microsoft.JScript.Vsa.VsaEngine Engine = Microsoft.JScript.Vsa.VsaEngine.CreateEngine();

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
    }
}
