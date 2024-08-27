using Fiap.TechChallenge.Exclusao.API.Events;
using Fiap.TechChallenge.Infrastructure.Data;
using Fiap.TechChallenge.Infrastructure.MessageBroker;
using Integration.BaseTests.Extensions;
using MassTransit;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Testcontainers.MsSql;
using Testcontainers.RabbitMq;

namespace Fiap.TechChallenge.Exclusao.IntegrationTests.Abstractions;

public class FunctionalTestWebAppFactory : WebApplicationFactory<Program>, IAsyncLifetime
{
    public readonly MsSqlContainer _msSqlContainer = new MsSqlBuilder().Build();
    private readonly RabbitMqContainer _rabbitMqContainer = new RabbitMqBuilder()
        .WithUsername("guest")
        .WithPassword("guest")
        .Build();

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureTestServices(async services =>
        {
            services.RemoveAll(typeof(DbContextOptions<ApplicationDbContext>));
            services.RemoveAll(typeof(MessageBrokerSettings));

            services.AddDbContext<ApplicationDbContext>(options =>
                options
                    .UseSqlServer(_msSqlContainer.GetConnectionString()));

            services.AddSingleton<MessageBrokerSettings>(new MessageBrokerSettings
            {
                Host = _rabbitMqContainer.GetConnectionString()
            });

            await _msSqlContainer.ExecScriptAsync(ScriptInitialExtensions.CreateTables());
        });
    }

    public Task InitializeAsync()
    {
        _msSqlContainer.StartAsync();
        return _rabbitMqContainer.StartAsync();
    }

    public Task DisposeAsync()
    { 
        _msSqlContainer.DisposeAsync().AsTask();
        return _rabbitMqContainer.DisposeAsync().AsTask();
    }

}
