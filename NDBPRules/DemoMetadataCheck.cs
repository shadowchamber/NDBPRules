using Microsoft.Dynamics.AX.Framework.BestPractices.Extensions;
using Microsoft.Dynamics.AX.Metadata.MetaModel;
using Microsoft.Dynamics.AX.Metadata.Upgrade.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NDBPRules
{
    [BestPracticeRule(
       InvalidCharactersDiagnosticItem.DiagnosticMoniker,
       typeof(Messages),
       InvalidCharactersDiagnosticItem.DiagnosticMoniker + "Description",
       BestPracticeCheckerTargets.Table | BestPracticeCheckerTargets.View)]
    public class DemoMetadataCheck : BestPracticeMetadataChecker
    {
        private const string InvalidCharsCollection = "^+{}|";

        /// <summary>
        /// This method is called with the top level artifacts that need
        /// to be checked. In this implementation, we are only interested
        /// in tables - everything else is ignored. 
        /// </summary>
        /// <param name="metaObject">A metadata instance to check for BP violations.</param>
        public override void RunChecksOn(Microsoft.Dynamics.AX.Metadata.Core.MetaModel.INamedObject metaObject)
        {
            AxTable table = metaObject as AxTable;

            // only check tables
            if (table != null)
            {
                // Traverse the fields of the table.
                if (table.Fields != null)
                {
                    foreach (AxTableField field in table.Fields)
                    {
                        this.VisitField(table.Name, field);
                    }
                }
            }
        }

        /// <summary>
        /// Implementation of the field checker
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="field"></param>
        private void VisitField(string tableName, AxTableField field)
        {
            if (string.IsNullOrEmpty(field.HelpText))
            {
                // No help text for the field, so nothing to check.
                return;
            }

            StringBuilder badChars = new StringBuilder();
            foreach (char c in field.HelpText)
            {
                if (InvalidCharsCollection.Contains(c))
                {
                    badChars.Append(c);
                }
            }

            if (badChars.Length != 0)
            {
                // Found one or more bad characters in the help text.
                // Build a diagnostic ...
                InvalidCharactersDiagnosticItem diagnostic = new InvalidCharactersDiagnosticItem(
                    ModelElementPathBuilder.CreatePathForTableField(tableName, field.Name),
                    "Table Field",
                    null,
                    badChars.ToString());

                // ... and report the error.
                this.ExtensionContext.AddErrorMessage(diagnostic);
            }
        }
    }
}