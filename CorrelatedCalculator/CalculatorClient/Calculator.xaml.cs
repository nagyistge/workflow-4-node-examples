//----------------------------------------------------------------
// Copyright (c) Microsoft Corporation.  All rights reserved.
//----------------------------------------------------------------

using System;
using System.Diagnostics;
using System.ServiceModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Microsoft.Samples.CorrelatedCalculator.CalculatorClient
{
    /// <summary>
    /// Interaction logic for CalculatorWindow.xaml
    /// </summary>
    
    public partial class CalculatorWindow : Window
    {
        public CalculatorWindow()
        {
            InitializeComponent();
        }

        private void OnExit(object sender, EventArgs e)
        {
            netUI.Release();
            nodeUI.Release();
        }
    }
}
