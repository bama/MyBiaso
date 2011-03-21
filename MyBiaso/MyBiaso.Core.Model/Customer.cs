using System;

namespace MyBiaso.Core.Model {

    /// <summary>
    /// Stellt einen Kunden dar.
    /// </summary>
    public class Customer:IEquatable<Customer> {

        /// <summary>
        /// ID des Kunden
        /// </summary>
        public virtual Guid Id { get; set; }
        /// <summary>
        /// Kundennummer
        /// </summary>
        public virtual string CustomerNumber { get; set; }
        /// <summary>
        /// Vorname
        /// </summary>
        public virtual string Firstname { get; set; }
        /// <summary>
        /// Nachname
        /// </summary>
        public virtual string Lastname { get; set; }

        /// <summary>
        /// Telefonnummer
        /// </summary>
        public virtual string Phone { get; set; }
        /// <summary>
        /// Straße
        /// </summary>
        public virtual string Street { get; set; }
        /// <summary>
        /// Hausnummer
        /// </summary>
        public virtual string Housenumber { get; set; }
        /// <summary>
        /// PLZ
        /// </summary>
        public virtual string ZipCode { get; set; }
        /// <summary>
        /// Ort
        /// </summary>
        public virtual string City { get; set; }

        /// <summary>
        /// Püft ob der übergebene Kunde identisch ist.
        /// </summary>
        /// <param name="other">Anderer Kunde</param>
        /// <returns>True, wenn der Kunde identisch ist</returns>
        public virtual bool Equals(Customer other) {
            return (null != other &&  Id == other.Id);
        }
    }
}
