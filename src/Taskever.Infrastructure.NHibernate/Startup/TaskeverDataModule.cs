using System;
using System.Configuration;
using System.Data.Common;
using System.Reflection;
using System.Web.WebSockets;
using Abp.Configuration.Startup;
using Abp.Modules;
using Abp.NHibernate;
using FluentNHibernate.Cfg.Db;
using Microsoft.Extensions.DependencyInjection;
using Castle.Windsor.MsDependencyInjection;
using FluentMigrator.Runner;
using FluentNHibernate.Conventions.Helpers;
using NHibernate.Tool.hbm2ddl;

namespace Taskever.Startup
{
    [DependsOn(typeof(AbpNHibernateModule),typeof(TaskeverCoreModule))]
    public class TaskeverDataModule : AbpModule
    {
        public override void PreInitialize()
        {
            var connStr = ConfigurationManager.ConnectionStrings["Taskever"].ConnectionString;

            Configuration.DefaultNameOrConnectionString = "Taskever";

            Configuration.Modules.AbpNHibernate().FluentConfiguration
                .Database(MsSqlConfiguration.MsSql2012.ConnectionString(connStr))
                .Mappings(m =>
                    m.FluentMappings
                        .Conventions.Add(
                            DynamicInsert.AlwaysTrue(),
                            DynamicUpdate.AlwaysTrue()
                        )
                        .AddFromAssembly(Assembly.GetExecutingAssembly())
                ).ExposeConfiguration(cfg => new SchemaExport(cfg).Execute(true, true, false, IocManager.Resolve<DbConnection>(), Console.Out));


            var services = new ServiceCollection()
                .AddFluentMigratorCore().ConfigureRunner(rb => rb
                    // Add SQLite support to FluentMigrator
                    .AddSQLite()
                    // Set the connection string
                    .WithGlobalConnectionString(connStr)
                    // Define the assembly containing the migrations
                    .ScanIn(Assembly.GetExecutingAssembly()).For.Migrations())
                // Enable logging to console in the FluentMigrator way
                .AddLogging(lb => lb.AddFluentMigratorConsole());
            // Build the service provider
            IocManager.IocContainer.AddServices(services);

        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}