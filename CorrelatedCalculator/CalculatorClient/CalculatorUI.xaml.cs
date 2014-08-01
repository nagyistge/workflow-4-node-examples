using Microsoft.Samples.CorrelatedCalculator.CalculatorClient.ServiceReference1;
using Microsoft.Samples.CorrelatedCalculator.CalculatorClient.ViewModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Microsoft.Samples.CorrelatedCalculator.CalculatorClient
{
    public enum ClientType
    {
        DotNET, Node
    }

    /// <summary>
    /// Interaction logic for CalculatorUI.xaml
    /// </summary>
    public partial class CalculatorUI : UserControl
    {
        int CalculatorId = new Random(DateTime.Now.Millisecond).Next();
        Microsoft.Samples.CorrelatedCalculator.CalculatorClient.ServiceReference1.ICalculator client;

        Brush onBrush = Brushes.LightSteelBlue;
        Brush offBrush = Brushes.GhostWhite;

        bool displayNew = true;
        bool newClient = true;
        string previousOp = "+";

        public ClientType ClientType
        {
            get { return (ClientType)GetValue(ClientTypeProperty); }
            set { SetValue(ClientTypeProperty, value); }
        }

        public static readonly DependencyProperty ClientTypeProperty =
            DependencyProperty.Register("ClientType", typeof(ClientType), typeof(CalculatorUI), new PropertyMetadata(ClientType.DotNET));

        public int? LastCallTime
        {
            get { return (int?)GetValue(LastCallTimeProperty); }
            set { SetValue(LastCallTimeProperty, value); }
        }

        public static readonly DependencyProperty LastCallTimeProperty =
            DependencyProperty.Register("LastCallTime", typeof(int?), typeof(CalculatorUI), new PropertyMetadata(null));

        Microsoft.Samples.CorrelatedCalculator.CalculatorClient.ServiceReference1.ICalculator Client
        {
            get
            {
                if (client == null)
                {
                    this.CalculatorId++;
                    if (ClientType == CalculatorClient.ClientType.DotNET)
                    {
                        client = new Microsoft.Samples.CorrelatedCalculator.CalculatorClient.ServiceReference1.CalculatorClient();
                    }
                    else
                    {
                        client = new WF4NodeClient();
                    }
                    newClient = true;
                }
                return client;
            }
        }

        public CalculatorUI()
        {
            InitializeComponent();
        }

        void UpdateDisplay(object sender, RoutedEventArgs e)
        {
            string input = ((Button)sender).Content.ToString();
            if (displayNew)
            {
                Display.Text = input;
                Display.Background = offBrush;
            }
            else
                Display.Text += input;
            displayNew = false;
        }

        void DoOperation(object sender, RoutedEventArgs e)
        {
            double value = 0;
            string operation = ((Button)sender).Content.ToString();
            if (!previousOp.StartsWith("=") && !double.TryParse(Display.Text, out value))
            {
                MessageBox.Show("Invalid input! Try again.");
                Display.Text = String.Empty;
                return;
            }

            try
            {
                var sw = new Stopwatch();
                sw.Start();
                bool called = false;
                if (!displayNew)
                {
                    switch (previousOp)
                    {
                        case "=": { break; }
                        case "+": { Client.Add(new AddRequest(value, CalculatorId.ToString())); called = true; break; }
                        case "-": { Client.Subtract(new SubtractRequest(value, CalculatorId.ToString())); called = true; break; }
                        case "x": { Client.Multiply(new MultiplyRequest(value, CalculatorId.ToString())); called = true; break; }
                        case "/":
                            {
                                if (value == 0)
                                {
                                    MessageBox.Show("Divide By Zero is not allowed");
                                    value = Client.Equals(CalculatorId.ToString());
                                    Display.Text = value.ToString();
                                    called = true;
                                    break;
                                }
                                else
                                {
                                    Client.Divide(new DivideRequest(value, CalculatorId.ToString()));
                                    called = true;
                                    break;
                                }
                            }
                    }
                }
                if (operation.Equals("="))
                {
                    value = Client.Equals(CalculatorId.ToString());
                    called = true;
                    Display.Text = value.ToString();
                }
                sw.Stop();
                if (called) LastCallTime = (int)sw.ElapsedMilliseconds;
                newClient = false;
            }
            catch (Exception ce)
            {
                MessageBox.Show(ce.Message);
                ClientAbort();
                client = null;
            }

            previousOp = operation;

            Display.Background = onBrush;
            displayNew = true;
        }

        private void ClientAbort()
        {
            var c = Client as ICommunicationObject;
            if (c != null) c.Abort();
        }

        void Reset(object sender, RoutedEventArgs e)
        {
            ResetClient();

            Display.Background = onBrush;
            Display.Text = String.Empty;
            displayNew = true;
        }

        private void ResetClient()
        {
            try
            {
                Client.Reset(CalculatorId.ToString());
                ClientClose();                
            }
            catch (Exception ce)
            {
                MessageBox.Show(ce.Message);
                ClientAbort();
            }

            client = null;
            previousOp = "+";
            newClient = true;
        }

        private void ClientClose()
        {
            var c = Client as ICommunicationObject;
            if (c != null) c.Close();
        }

        public void Release()
        {
            if (!newClient) ResetClient();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            speedTest.DataContext = new SpeedTestViewModel(ClientType);
        }
    }
}
