using GalaSoft.MvvmLight.Command;
using Microsoft.Samples.CorrelatedCalculator.CalculatorClient.ServiceReference1;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Microsoft.Samples.CorrelatedCalculator.CalculatorClient.ViewModel
{
    public class SpeedTestViewModel : MainViewModel
    {
        public SpeedTestViewModel(ClientType clientType)
        {
            this.clientType = clientType;
        }

        ClientType clientType;

        CancellationTokenSource cancel;

        static readonly Random rnd = new Random();

        private int parallelCount = 10;

        public int ParallelCount
        {
            get { return parallelCount; }
            set 
            {
                if (parallelCount != value)
                {
                    parallelCount = value;
                    RaisePropertyChanged(() => ParallelCount);
                }
            }
        }

        private int iterationCount = 1;

        public int IterationCount
        {
            get { return iterationCount; }
            set 
            {
                if (iterationCount != value)
                {
                    iterationCount = value;
                    RaisePropertyChanged(() => IterationCount);
                }
            }
        }

        private bool isRunning;

        public bool IsRunning
        {
            get { return isRunning; }
            private set 
            {
                if (isRunning != value)
                {
                    isRunning = value;
                    RaisePropertyChanged(() => IsRunning);
                    Start.RaiseCanExecuteChanged();
                    Stop.RaiseCanExecuteChanged();
                }
            }
        }

        private double? ellapedMilliseconds;

        public double? EllapsedMilliseconds
        {
            get { return ellapedMilliseconds; }
            set 
            {
                if (ellapedMilliseconds != value)
                {
                    ellapedMilliseconds = value;
                    RaisePropertyChanged(() => EllapsedMilliseconds);
                }
            }
        }

        private double? millisecondsPerCall;

        public double? MillisecondsPerCall
        {
            get { return millisecondsPerCall; }
            set 
            {
                if (millisecondsPerCall != value)
                {
                    millisecondsPerCall = value;
                    RaisePropertyChanged(() => MillisecondsPerCall);
                }
            }
        }

        private int max;

        public int Max
        {
            get { return max; }
            set 
            {
                if (max != value)
                {
                    max = value;
                    RaisePropertyChanged(() => Max);
                }
            }
        }

        private int current;

        public int Current
        {
            get { return current; }
            set 
            {
                if (current != value)
                {
                    current = value;
                    RaisePropertyChanged(() => Current);
                }
            }
        }


        public RelayCommand Start { get; private set; }

        public RelayCommand Stop { get; private set; }

        protected override void Init()
        {
            Start = new RelayCommand(DoStart, () => !IsRunning);
            Stop = new RelayCommand(DoStop, () => IsRunning);
        }

        private async void DoStart()
        {
            cancel = new CancellationTokenSource();
            int id = rnd.Next();
            var tasks = new List<Task>(ParallelCount);
            IsRunning = true;
            EllapsedMilliseconds = MillisecondsPerCall = null;
            var sw = new Stopwatch();
            sw.Start();
            Max = IterationCount * ParallelCount;
            Current = 0;
            int callCount = 0;
            ICalculator client;
            ICommunicationObject co = null;
            if (clientType == ClientType.DotNET)
            {
                client = new Microsoft.Samples.CorrelatedCalculator.CalculatorClient.ServiceReference1.CalculatorClient();
                co = (ICommunicationObject)client;
            }
            else
            {
                client = new WF4NodeClient();
            }
            try
            {
                for (int c = 0; c < IterationCount; c++)
                {
                    for (int i = 0; i < ParallelCount; i++, id++)
                    {
                        Func<Task> call = async () =>
                        {
                            await Calculate(client, id.ToString());
                            Current++;
                            callCount += 6;
                        };
                        tasks.Add(call());
                    }
                    await Task.WhenAll(tasks);
                    tasks.Clear();
                }
                if (co != null) co.Close();
            }
            catch (OperationCanceledException)
            {
                if (co != null) co.Abort();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.ToString());
                if (co != null) co.Abort();
            }
            sw.Stop();
            MillisecondsPerCall = (EllapsedMilliseconds = sw.Elapsed.TotalMilliseconds) / callCount;
            IsRunning = false;
            Max = Current = 0;
            cancel = null;
        }

        private async Task Calculate(ICalculator client, string id)
        {
            cancel.Token.ThrowIfCancellationRequested();
            await client.AddAsync(100.0, id);
            await client.DivideAsync(1.0, id);
            await client.MultiplyAsync(100.0, id);
            await client.SubtractAsync(1.0, id);
            await client.EqualsAsync(id);
            await client.ResetAsync(id);
        }

        private void DoStop()
        {
            if (cancel != null) cancel.Cancel();
        }
    }
}
