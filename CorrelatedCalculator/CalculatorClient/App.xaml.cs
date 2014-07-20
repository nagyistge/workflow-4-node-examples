//----------------------------------------------------------------
// Copyright (c) Microsoft Corporation.  All rights reserved.
//----------------------------------------------------------------

using System.Net;
using System.Windows;

namespace Microsoft.Samples.CorrelatedCalculator.CalculatorClient
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>

    public partial class CalculatorApp : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            ServicePointManager.DefaultConnectionLimit = 10000;
        }
    }
}
