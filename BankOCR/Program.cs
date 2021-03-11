using System;
using System.Threading;
using System.Threading.Tasks;
using Autofac;
using BankOCR.IoC;
using BankOCR.Services;
using CommandLine;

namespace BankOCR
{
    public class Program
    {
        private class Options
        {
            [Option('d', "data", HelpText = "Data file path", Default = "default-data.txt")]
            public string DataFile { get; set; }
            
            [Option('r', "result", HelpText = "Result file path", Default = "results.txt")]
            public string ResultFile { get; set; }
        }
        
        public static async Task Main(string[] args)
        {
            var cts = new CancellationTokenSource();
            Console.CancelKeyPress += (s, e) =>
            {
                cts.Cancel();
                e.Cancel = true;
            };

            await Run(args, cts.Token);

            async Task Run(string[] args, CancellationToken cancellationToken)
            {
                await Parser.Default.ParseArguments<Options>(args)
                    .WithParsedAsync(async o =>
                    {
                        var builder = new ContainerBuilder();
                        builder.RegisterModule<BankOcrModule>();
                        await using var container = builder.Build();

                        var ocrService = container.Resolve<OcrService>();
                        
                        try
                        {
                            var dataFileName = o.DataFile;
                            var resultsFileName = o.ResultFile;

                            Console.WriteLine($"Scanning file {dataFileName}");
                            await ocrService.ScanFile(dataFileName, resultsFileName, cancellationToken);
                            Console.WriteLine($"Results has been written to {resultsFileName}");
                        }
                        catch (Exception e)
                        {
                            //in normal life we should have ILogger here
                            Console.WriteLine(e.Message);
                        }
                    });
            }
        }
    }
}