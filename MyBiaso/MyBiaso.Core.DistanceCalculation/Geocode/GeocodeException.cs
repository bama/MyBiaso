using System;

namespace MyBiaso.Core.DistanceCalculation.Geocode {

    /// <summary>
    /// Ausnahme wird ausgelöst, wenn die Geokodierung fehlschlägt.
    /// </summary>
    public class GeocodeException:Exception {
        
        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="message">Nachricht</param>
        /// <param name="exception">Ausnahme</param>
        public GeocodeException(string message, Exception exception)
            : base(message, exception) {

        }
    }
}
