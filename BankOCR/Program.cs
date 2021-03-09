using System;
using System.Threading;
using System.Threading.Tasks;
using Autofac;
using BankOCR.IoC;

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
}