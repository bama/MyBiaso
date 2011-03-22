namespace MyBiaso.Core.DistanceCalculation {

    /// <summary>
    /// Route
    /// </summary>
    public class Route {

        /// <summary>
        /// Ursprung
        /// </summary>
        public Address Origin { get; set; }

        /// <summary>
        /// Ziel
        /// </summary>
        public Address Destination { get; set; }

        /// <summary>
        /// Distanz in metern
        /// </summary>
        public long DistanceInMeter { get; set; } 

    }
}
