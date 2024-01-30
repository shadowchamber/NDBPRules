using Microsoft.Dynamics.AX.Metadata.XppCompiler;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Xml.Linq;

namespace NDBPRules.Metadata.DiagnosticItems
{
    [DataContract]
    public class ViewConfigurationKeyMissing : CustomDiagnosticItem
    {
        public const string DiagnosticMoniker = "NDViewConfigurationKeyMissing";

        public ViewConfigurationKeyMissing(
            string path,
            string elementType,
            TextPosition textPosition,
            string invalidChars)
            : base(path, elementType, textPosition, DiagnosticType.BestPractices, Severity.Warning, DiagnosticMoniker, Messages.ViewConfigurationKeyMissing, invalidChars)
        {

        }

        public ViewConfigurationKeyMissing(XElement element)
            : base(element)
        {
        }

        /// <summary>
        /// Hydrate the diagnostic item from the given XML element.
        /// </summary>
        /// <param name="itemSpecificNode">The XML element containing the diagnostic.</param>
        protected override void ReadItemSpecificFields(XElement itemSpecificNode)
        {
            //base.ReadCustomField(itemSpecificNode);
        }

        /// <summary>
        /// Write the state into the given XML element.
        /// </summary>
        /// <param name="itemSpecificNode">The element into which the state is persisted.</param>
        protected override void WriteItemSpecificFields(XElement itemSpecificNode)
        {
            //this.WriteCustomField(itemSpecificNode, InvalidCharsKey, this.InvalidChars);
        }
    }
}
