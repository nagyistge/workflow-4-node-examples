using Microsoft.Samples.CorrelatedCalculator.CalculatorClient.ServiceReference1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Samples.CorrelatedCalculator.CalculatorClient
{
    public class WF4NodeClient : ICalculator
    {
        public WF4NodeClient() : this(Properties.Settings.Default.WF4NodeCalcUrl)
        {
        }

        public WF4NodeClient(string url)
        {
            this.url = url;
            if (!this.url.EndsWith("/")) this.url += "/";
        }

        string url;

        public void Add(double Value, string CalculatorId)
        {
            using (var client = new WebClient())
            {
                client.DownloadData(new Uri(this.url + "add?id=" + CalculatorId + "&value=" + Value));
            }
        }

        public async Task AddAsync(double Value, string CalculatorId)
        {
            using (var client = new WebClient())
            {
                await client.DownloadDataTaskAsync(new Uri(this.url + "add?id=" + CalculatorId + "&value=" + Value));
            }
        }

        public void Subtract(double Value, string CalculatorId)
        {
            using (var client = new WebClient())
            {
                client.DownloadData(new Uri(this.url + "subtract?id=" + CalculatorId + "&value=" + Value));
            }
        }

        public async Task SubtractAsync(double Value, string CalculatorId)
        {
            using (var client = new WebClient())
            {
                await client.DownloadDataTaskAsync(new Uri(this.url + "subtract?id=" + CalculatorId + "&value=" + Value));
            }
        }

        public void Multiply(double Value, string CalculatorId)
        {
            using (var client = new WebClient())
            {
                client.DownloadData(new Uri(this.url + "multiply?id=" + CalculatorId + "&value=" + Value));
            }
        }

        public async Task MultiplyAsync(double Value, string CalculatorId)
        {
            using (var client = new WebClient())
            {
                await client.DownloadDataTaskAsync(new Uri(this.url + "multiply?id=" + CalculatorId + "&value=" + Value));
            }
        }

        public void Divide(double Value, string CalculatorId)
        {
            using (var client = new WebClient())
            {
                client.DownloadData(new Uri(this.url + "divide?id=" + CalculatorId + "&value=" + Value));
            }
        }

        public async Task DivideAsync(double Value, string CalculatorId)
        {
            using (var client = new WebClient())
            {
                await client.DownloadDataTaskAsync(new Uri(this.url + "divide?id=" + CalculatorId + "&value=" + Value));
            }
        }

        public double Equals(string CalculatorId)
        {
            using (var client = new WebClient())
            {
                return double.Parse(client.DownloadString(new Uri(this.url + "equals?id=" + CalculatorId)));
            }
        }

        public async Task<double> EqualsAsync(string CalculatorId)
        {
            using (var client = new WebClient())
            {
                return double.Parse(await client.DownloadStringTaskAsync(new Uri(this.url + "equals?id=" + CalculatorId)));
            }
        }

        public void Reset(string CalculatorId)
        {
            using (var client = new WebClient())
            {
                client.DownloadData(new Uri(this.url + "reset?id=" + CalculatorId));
            }
        }

        public async Task ResetAsync(string CalculatorId)
        {
            using (var client = new WebClient())
            {
                await client.DownloadDataTaskAsync(new Uri(this.url + "reset?id=" + CalculatorId));
            }
        }
    }
}
