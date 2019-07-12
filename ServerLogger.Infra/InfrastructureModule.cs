﻿using System;
using Autofac;
using ConsoleApp.Core.Interfaces.Gateways.Repositories.Logs;
using ServerLogger.Infra.Data;

namespace ServerLogger.Infra
{
    public class InfrastructureModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ServerLoggerRepository>().As<IServerLoggerRepository>().InstancePerLifetimeScope();
        }
    }
}
