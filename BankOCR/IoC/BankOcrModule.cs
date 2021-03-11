using Autofac;
using BankOCR.Repositories;
using BankOCR.Services;
using BankOCR.Services.Possibilities;

namespace BankOCR.IoC
{
    public class BankOcrModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<OcrService>()
                .AsSelf();
            
            builder.RegisterType<DigitParserService>()
                .As<IDigitParserService>();

            builder.RegisterType<NumberParserService>()
                .As<INumberParserService>();

            builder.RegisterType<AccountValidatorService>()
                .As<IAccountValidatorService>();
            
            builder.RegisterType<AccountPossibilitiesFinderService>()
                .As<IAccountPossibilitiesFinderService>();
            
            builder.RegisterType<SignPossibilitiesFinder>()
                .As<ISignPossibilitiesFinder>();
            
            builder.RegisterType<AccountsFileRepository>()
                .As<IAccountsFileRepository>();
        }
    }
}