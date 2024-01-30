using Microsoft.Dynamics.AX.Framework.BestPractices.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NDBPRules
{
    [BestPracticeRule(
        NotAllowedWordDiagnosticItem.DiagnosticMoniker,
        typeof(Messages),
        NotAllowedWordDiagnosticItem.DiagnosticMoniker + "Description",
        BestPracticeCheckerTargets.Class)]
    public class DemoASTCheck : BestPracticeAstChecker<BestPracticeCheckerPayload>
    {
        private const string NotAllowedWord = "Microsoft";

        // Do not remove this constructor
        public DemoASTCheck()
        {
        }

        /// <summary>
        /// Implement the VisitMethod method. This method will be called
        /// every time a method is encountered in the AST. In this case, 
        /// the name is checked against the words that are not allowed.
        /// </summary>
        /// <param name="payload"></param>
        /// <param name="method"></param>
        /// <returns></returns>
        protected override object VisitMethod(BestPracticeCheckerPayload payload, Microsoft.Dynamics.AX.Metadata.XppCompiler.Method method)
        {
            if (method != null)
            {
                if (method.Name.Contains(NotAllowedWord))
                {
                    // Create a diagnostic message
                    NotAllowedWordDiagnosticItem diagnosticMessage = new NotAllowedWordDiagnosticItem(
                        this.Context,
                        method.MethodPosition,
                        NotAllowedWord);

                    // and add it to the set of reported messages.
                    this.ExtensionContext.AddErrorMessage(diagnosticMessage);
                }
            }

            // Call the base implementation, to allow the sweeping 
            // of the method's subnodes to take place.
            return base.VisitMethod(payload, method);
        }
    }
}