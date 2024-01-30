using Microsoft.Dynamics.AX.Metadata.XppCompiler;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Xml.Linq;

namespace NDBPRules
{
    /// <summary>
    /// Class that describes the error where invalid characters are
    /// found in a field's help text.
    /// </summary>

    [DataContract]
    public class InvalidCharactersDiagnosticItem : CustomDiagnosticItem
    {
        private const string InvalidCharsKey = "chars";
        public const string DiagnosticMoniker = "InvalidCharacters";

        public InvalidCharactersDiagnosticItem(string path, string elementType, TextPosition textPosition, string invalidChars)
            : base(path, elementType, textPosition, DiagnosticType.BestPractices, Severity.Warning, DiagnosticMoniker, Messages.InvalidCharacters, invalidChars)
        {
            // Validate parameters
            if (string.IsNullOrWhiteSpace(invalidChars))
            {
                throw new ArgumentNullException("invalidChars");
            }

            this.InvalidChars = invalidChars;
        }

        [DataMember]
        public string InvalidChars { get; private set; }

        // Serialization support.
        public InvalidCharactersDiagnosticItem(XElement element)
            : base(element)
        {
        }

        /// <summary>
        /// Hydrate the diagnostic item from the given XML element.
        /// </summary>
        /// <param name="itemSpecificNode">The XML element containing the diagnostic.</param>
        protected override void ReadItemSpecificFields(XElement itemSpecificNode)
        {
            this.InvalidChars = base.ReadCustomField(itemSpecificNode, InvalidCharsKey);
        }

        /// <summary>
        /// Write the state into the given XML element.
        /// </summary>
        /// <param name="itemSpecificNode">The element into which the state is persisted.</param>
        protected override void WriteItemSpecificFields(XElement itemSpecificNode)
        {
            this.WriteCustomField(itemSpecificNode, InvalidCharsKey, this.InvalidChars);
        }
    }
}