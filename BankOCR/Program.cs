using System;
using System.Threading;
using System.Threading.Tasks;
using Autofac;
using BankOCR.IoC;
using BankOCR.Services;

var cts = new CancellationTokenSource();
Console.CancelKeyPress += (s, e) =>
{
    cts.Cancel();
    e.Cancel = true;
};

await Run(args, cts.Token);

async Task Run(string[] args, CancellationToken cancellationToken)
{
    var builder = new ContainerBuilder();
    builder.RegisterModule<BankOcrModule>();
    await using var container = builder.Build();
    
    var ocrService = container.Resolve<OcrService>();

    try
    {
        var dataFileName = "default-data.txt";
        var resultsFileName = "results.txt";
        
        Console.WriteLine($"Scanning file {dataFileName}");
        await ocrService.ScanFile(dataFileName, resultsFileName, cancellationToken);
        Console.WriteLine($"Results has been written to {resultsFileName}");
    }
    catch (Exception e)
    {
        //in normal life we should have ILogger here
        Console.WriteLine(e.Message);
    }
}