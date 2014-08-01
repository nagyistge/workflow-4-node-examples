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

        public AddResponse Add(AddRequest request)
        {
            using (var client = new WebClient())
            {
                client.DownloadData(new Uri(this.url + "add?id=" + request.CalculatorId + "&value=" + request.Value));
            }
            return new AddResponse();
        }

        public async Task<AddResponse> AddAsync(AddRequest request)
        {
            using (var client = new WebClient())
            {
                await client.DownloadDataTaskAsync(new Uri(this.url + "add?id=" + request.CalculatorId + "&value=" + request.Value));
            }
            return new AddResponse();
        }

        public SubtractResponse Subtract(SubtractRequest request)
        {
            using (var client = new WebClient())
            {
                client.DownloadData(new Uri(this.url + "subtract?id=" + request.CalculatorId + "&value=" + request.Value));
            }
            return new SubtractResponse();
        }

        public async Task<SubtractResponse> SubtractAsync(SubtractRequest request)
        {
            using (var client = new WebClient())
            {
                await client.DownloadDataTaskAsync(new Uri(this.url + "subtract?id=" + request.CalculatorId + "&value=" + request.Value));
            }
            return new SubtractResponse();
        }

        public MultiplyResponse Multiply(MultiplyRequest request)
        {
            using (var client = new WebClient())
            {
                client.DownloadData(new Uri(this.url + "multiply?id=" + request.CalculatorId + "&value=" + request.Value));
            }
            return new MultiplyResponse();
        }

        public async Task<MultiplyResponse> MultiplyAsync(MultiplyRequest request)
        {
            using (var client = new WebClient())
            {
                await client.DownloadDataTaskAsync(new Uri(this.url + "multiply?id=" + request.CalculatorId + "&value=" + request.Value));
            }
            return new MultiplyResponse();
        }

        public DivideResponse Divide(DivideRequest request)
        {
            using (var client = new WebClient())
            {
                client.DownloadData(new Uri(this.url + "divide?id=" + request.CalculatorId + "&value=" + request.Value));
            }
            return new DivideResponse();
        }

        public async Task<DivideResponse> DivideAsync(DivideRequest request)
        {
            using (var client = new WebClient())
            {
                await client.DownloadDataTaskAsync(new Uri(this.url + "divide?id=" + request.CalculatorId + "&value=" + request.Value));
            }
            return new DivideResponse();
        }
    }
}
