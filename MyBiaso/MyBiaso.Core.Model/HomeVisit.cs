using System;

namespace MyBiaso.Core.Model {
    
    /// <summary>
    /// Stellt einen Hausbesuch dar.
    /// </summary>
    public class HomeVisit : Activity {

        /// <summary>
        /// Beginn
        /// </summary>
        public virtual DateTime Begin { get; set; }
        /// <summary>
        /// Ende
        /// </summary>
        public virtual DateTime End { get; set; }
        /// <summary>
        /// Flag, true wenn nach dem Hausbesuch Heimweg angetreten wurde.
        /// </summary>
        public virtual bool DrivenHome { get; set; }

        /// <summary>
        /// Kunde der besucht wurde
        /// </summary>
        public virtual Customer Customer { get; set; }

    }
}
