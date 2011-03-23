using System;
using System.Globalization;

namespace MyBiaso.Core.Model {
    
    /// <summary>
    /// Sammlung von Werten für das Reporting (Stellt eine Zeile dar).
    /// </summary>
    public class ReportingValues {

        /// <summary>
        /// Zeispanne (Bsp. April 2011)
        /// </summary>
        public String TimeSpan { get {
            if (Month != 0)
                return CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(Month) + " " + Year;

            if (Quarter != 0)
                return Quarter + ". Quartal " + Year;

            return "Jahr " + Year;
        } }

        /// <summary>
        /// Anzahl Kundenbesuche in der Zeitspanne
        /// </summary>
        public long CustomerVisits { get; set; }

        /// <summary>
        /// Zurückgelegte Distanz in der Zeitspanne
        /// </summary>
        public float DistanceTravelled { get; set; }

        /// <summary>
        /// Jahr
        /// </summary>
        public int Year { get; set; }
        /// <summary>
        /// Monat
        /// </summary>
        public int Month { get; set; }
        /// <summary>
        /// Quarter
        /// </summary>
        public int Quarter { get; set; }
    }
}
