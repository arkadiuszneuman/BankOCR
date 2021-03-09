﻿using Autofac;
using BankOCR.Services;

namespace BankOCR.IoC
{
    public class BankOcrModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ParserService>()
                .AsSelf();
        }
    }
}