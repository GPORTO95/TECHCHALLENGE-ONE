using Fiap.TechChallenge.One.Infrastructure.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Testcontainers.MsSql;

namespace Api.IntegrationTests.Abstractions;

public class FunctionalTestWebAppFactory : WebApplicationFactory<Program>, IAsyncLifetime
{
    private readonly MsSqlContainer _msSqlContainer = new MsSqlBuilder().Build();

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureTestServices(services =>
        {
            services.RemoveAll(typeof(DbContextOptions<ApplicationDbContext>));

            services.AddDbContext<ApplicationDbContext>(options =>
                options
                    .UseSqlServer(_msSqlContainer.GetConnectionString()));

            CreateTables();
        });
    }

    private void CreateTables()
    {
        string sql =
            """
            CREATE TABLE [Ddds] (
                [Id] uniqueidentifier NOT NULL,
                [Codigo] nvarchar(450) NOT NULL,
                [DescricaoEstado] nvarchar(100) NOT NULL,
                [SiglaEstado] nvarchar(2) NOT NULL,
                CONSTRAINT [PK_Ddds] PRIMARY KEY ([Id])
            );

            CREATE TABLE [Contatos] (
                [Id] uniqueidentifier NOT NULL,
                [DddId] uniqueidentifier NOT NULL,
                [Email] nvarchar(100) NOT NULL,
                [Nome] nvarchar(200) NOT NULL,
                [Telefone] nvarchar(9) NOT NULL,
                CONSTRAINT [PK_Contatos] PRIMARY KEY ([Id]),
                CONSTRAINT [FK_Contatos_Ddds_DddId] FOREIGN KEY ([DddId]) REFERENCES [Ddds] ([Id]) ON DELETE CASCADE
            );
            """;

        
        SqlConnection cnn = new(_msSqlContainer.GetConnectionString());
        cnn.Open();
        
        SqlCommand cmd = new(sql, cnn);

        cmd.ExecuteScalar();
        cmd.Dispose();
        cnn.Close();
    }


    public Task InitializeAsync()
        => _msSqlContainer.StartAsync();

    public Task DisposeAsync()
        => _msSqlContainer.DisposeAsync().AsTask();
}
