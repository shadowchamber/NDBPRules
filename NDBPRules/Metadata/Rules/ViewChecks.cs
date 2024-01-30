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
       ViewConfigurationKeyMissing.DiagnosticMoniker, 
       typeof(Messages),
       ViewConfigurationKeyMissing.DiagnosticMoniker + "Description",
       BestPracticeCheckerTargets.View)]
    public class ViewChecks : BestPracticeMetadataChecker
    {
        public override void RunChecksOn(Microsoft.Dynamics.AX.Metadata.Core.MetaModel.INamedObject metaObject)
        {
            AxView view = metaObject as AxView;

            // only check views
            if (view != null)
            {
                if (string.IsNullOrEmpty(view.ConfigurationKey))
                {
                    ViewConfigurationKeyMissing diagnostic = new ViewConfigurationKeyMissing(
                    ModelElementPathBuilder.CreatePathForTable(view.Name),
                    "View",
                    null,
                    string.Empty);

                    this.ExtensionContext.AddErrorMessage(diagnostic);
                }
            }
        }
    }
}