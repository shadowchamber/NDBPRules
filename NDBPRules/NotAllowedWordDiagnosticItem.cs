using Microsoft.Dynamics.AX.Metadata.XppCompiler;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Xml.Linq;

namespace NDBPRules
{
    /// <summary>
    /// Class that describes the error where invalid words are
    /// found in a method name.
    /// </summary>
    [DataContract]
    public class NotAllowedWordDiagnosticItem : CustomDiagnosticItem
    {
        private const string NotAllowedWordKey = "Word";
        public const string DiagnosticMoniker = "NotAllowedWord";

        public NotAllowedWordDiagnosticItem(string path, string elementType, TextPosition textPosition, string notAllowedWord)
            : base(path, elementType, textPosition, DiagnosticType.BestPractices, Severity.Warning, DiagnosticMoniker, Messages.NotAllowedWord, notAllowedWord)
        {
            // Validate parameters.
            if (string.IsNullOrWhiteSpace(notAllowedWord))
            {
                throw new ArgumentNullException("notAllowedWord");
            }

            this.NotAllowedWord = notAllowedWord;
        }

        public NotAllowedWordDiagnosticItem(Stack<Ast> context, TextPosition textPosition, string notAllowedWord)
            : base(context, textPosition, DiagnosticType.BestPractices, Severity.Warning, DiagnosticMoniker, Messages.NotAllowedWord, notAllowedWord)
        {
            // Validate parameters.
            if (string.IsNullOrWhiteSpace(notAllowedWord))
            {
                throw new ArgumentNullException("notAllowedWord");
            }

            this.NotAllowedWord = notAllowedWord;
        }


        [DataMember]
        public string NotAllowedWord { get; private set; }

        // Serialization support
        public NotAllowedWordDiagnosticItem(XElement element)
            : base(element)
        {
        }

        /// <summary>
        /// Hydrate the diagnostic item from the given XML element.
        /// </summary>
        /// <param name="itemSpecificNode">The XML element containing the diagnostic.</param>
        protected override void ReadItemSpecificFields(System.Xml.Linq.XElement itemSpecificNode)
        {
            this.NotAllowedWord = base.ReadCustomField(itemSpecificNode, NotAllowedWordKey);
        }

        /// <summary>
        /// Write the state into the given XML element.
        /// </summary>
        /// <param name="itemSpecificNode">The element into which the state is persisted.</param>
        protected override void WriteItemSpecificFields(System.Xml.Linq.XElement itemSpecificNode)
        {
            this.WriteCustomField(itemSpecificNode, NotAllowedWordKey, this.NotAllowedWord);
        }
    }
}