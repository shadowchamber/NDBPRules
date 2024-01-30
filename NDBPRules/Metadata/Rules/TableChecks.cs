using NDBPRules.Metadata.DiagnosticItems;
using Microsoft.Dynamics.AX.Framework.BestPractices.Extensions;
using Microsoft.Dynamics.AX.Metadata.Core.MetaModel;
using Microsoft.Dynamics.AX.Metadata.MetaModel;
using Microsoft.Dynamics.AX.Metadata.Upgrade.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NDBPRules.Metadata.Rules
{
    [BestPracticeRule(
       TableCacheLookupMissing.DiagnosticMoniker,
       typeof(Messages),
       TableCacheLookupMissing.DiagnosticMoniker + "Description",
       BestPracticeCheckerTargets.Table)]
    [BestPracticeRule(
       TableConfigurationKeyMissing.DiagnosticMoniker,
       typeof(Messages),
       TableConfigurationKeyMissing.DiagnosticMoniker + "Description",
       BestPracticeCheckerTargets.Table)]
    [BestPracticeRule(
       TableGroupMissing.DiagnosticMoniker,
       typeof(Messages),
       TableGroupMissing.DiagnosticMoniker + "Description",
       BestPracticeCheckerTargets.Table)]
    public class TableChecks : BestPracticeMetadataChecker
    {
        public override void RunChecksOn(Microsoft.Dynamics.AX.Metadata.Core.MetaModel.INamedObject metaObject)
        {
            AxTable table = metaObject as AxTable;

            // only check tables
            if (table != null)
            {
                if (table.CacheLookup == RecordCacheLevel.None)
                {
                    TableCacheLookupMissing diagnostic = new TableCacheLookupMissing(
                    ModelElementPathBuilder.CreatePathForTable(table.Name),
                    "Table",
                    null,
                    string.Empty);

                    this.ExtensionContext.AddErrorMessage(diagnostic);
                }

                if (string.IsNullOrEmpty(table.ConfigurationKey))
                {
                    TableConfigurationKeyMissing diagnostic = new TableConfigurationKeyMissing(
                    ModelElementPathBuilder.CreatePathForTable(table.Name),
                    "Table",
                    null,
                    string.Empty);

                    this.ExtensionContext.AddErrorMessage(diagnostic);
                }

                if (table.TableGroup == TableGroup.Miscellaneous)
                {
                    TableGroupMissing diagnostic = new TableGroupMissing(
                    ModelElementPathBuilder.CreatePathForTable(table.Name),
                    "Table",
                    null,
                    string.Empty);

                    this.ExtensionContext.AddErrorMessage(diagnostic);
                }
            }
        }
    }
}