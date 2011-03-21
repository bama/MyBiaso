using System;

namespace MyBiaso.Core.Model {
    
    /// <summary>
    /// Spiegelt eine Einstellung wieder.
    /// </summary>
    public class Setting {

        /// <summary>
        /// ID des Datensatzes
        /// </summary>
        public virtual Guid Id { get; set; }
        /// <summary>
        /// Schlüssel
        /// </summary>
        public virtual string Key { get; set; }

        /// <summary>
        /// Wert
        /// </summary>
        public virtual string Value { get; set; }
    }
}
