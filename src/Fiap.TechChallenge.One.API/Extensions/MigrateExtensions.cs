using Fiap.TechChallenge.One.Domain.Ddds;
using Fiap.TechChallenge.One.Infrastructure.Data;

namespace Fiap.TechChallenge.One.API.Extensions;

public static class MigrateExtensions
{
    public static void ApplyMigrations(this IApplicationBuilder app)
    {
        using IServiceScope scope = app.ApplicationServices.CreateScope();

        using ApplicationDbContext context =
            scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        if (context.Ddds.Any())
        {
            return;
        }

        List<Ddd> ddds = [
            Ddd.Criar(Codigo.Criar("61").Value, Estado.Criar("DF", "Distrito Federal").Value),
            Ddd.Criar(Codigo.Criar("62").Value, Estado.Criar("GO", "Goiás").Value),
            Ddd.Criar(Codigo.Criar("64").Value, Estado.Criar("GO", "Goiás").Value),
            Ddd.Criar(Codigo.Criar("65").Value, Estado.Criar("MT", "Mato Grosso").Value),
            Ddd.Criar(Codigo.Criar("66").Value, Estado.Criar("MT", "Mato Grosso").Value),
            Ddd.Criar(Codigo.Criar("67").Value, Estado.Criar("MS", "Mato Grosso do Sul").Value),
            Ddd.Criar(Codigo.Criar("82").Value, Estado.Criar("AL", "Alagoas").Value),
            Ddd.Criar(Codigo.Criar("71").Value, Estado.Criar("BH", "Bahia").Value),
            Ddd.Criar(Codigo.Criar("73").Value, Estado.Criar("BH", "Bahia").Value),
            Ddd.Criar(Codigo.Criar("74").Value, Estado.Criar("BH", "Bahia").Value),
            Ddd.Criar(Codigo.Criar("75").Value, Estado.Criar("BH", "Bahia").Value),
            Ddd.Criar(Codigo.Criar("77").Value, Estado.Criar("BH", "Bahia").Value),
            Ddd.Criar(Codigo.Criar("85").Value, Estado.Criar("CE", "Ceará").Value),
            Ddd.Criar(Codigo.Criar("88").Value, Estado.Criar("CE", "Ceará").Value),
            Ddd.Criar(Codigo.Criar("98").Value, Estado.Criar("MA", "Maranhão").Value),
            Ddd.Criar(Codigo.Criar("99").Value, Estado.Criar("MA", "Maranhão").Value),
            Ddd.Criar(Codigo.Criar("83").Value, Estado.Criar("PB", "Paraiba").Value),
            Ddd.Criar(Codigo.Criar("81").Value, Estado.Criar("PE", "Pernambuco").Value),
            Ddd.Criar(Codigo.Criar("87").Value, Estado.Criar("PE", "Pernambuco").Value),
            Ddd.Criar(Codigo.Criar("86").Value, Estado.Criar("PI", "Piauí").Value),
            Ddd.Criar(Codigo.Criar("89").Value, Estado.Criar("PI", "Piauí").Value),
            Ddd.Criar(Codigo.Criar("84").Value, Estado.Criar("RN", "Rio Grande do Norte").Value),
            Ddd.Criar(Codigo.Criar("79").Value, Estado.Criar("SE", "Sergipe").Value),
            Ddd.Criar(Codigo.Criar("68").Value, Estado.Criar("AC", "Acre").Value),
            Ddd.Criar(Codigo.Criar("96").Value, Estado.Criar("AP", "Amapá").Value),
            Ddd.Criar(Codigo.Criar("92").Value, Estado.Criar("AM", "Amazonas").Value),
            Ddd.Criar(Codigo.Criar("97").Value, Estado.Criar("AM", "Amazonas").Value),
            Ddd.Criar(Codigo.Criar("91").Value, Estado.Criar("PA", "Pará").Value),
            Ddd.Criar(Codigo.Criar("93").Value, Estado.Criar("PA", "Pará").Value),
            Ddd.Criar(Codigo.Criar("94").Value, Estado.Criar("PA", "Pará").Value),
            Ddd.Criar(Codigo.Criar("69").Value, Estado.Criar("RO", "Rondônia").Value),
            Ddd.Criar(Codigo.Criar("95").Value, Estado.Criar("RR", "Roraima").Value),
            Ddd.Criar(Codigo.Criar("63").Value, Estado.Criar("TO", "Tocantis").Value),

            Ddd.Criar(Codigo.Criar("27").Value, Estado.Criar("ES", "Espírito Santo").Value),
            Ddd.Criar(Codigo.Criar("28").Value, Estado.Criar("ES", "Espírito Santo").Value),

            Ddd.Criar(Codigo.Criar("31").Value, Estado.Criar("MG", "Minas Gerais").Value),
            Ddd.Criar(Codigo.Criar("32").Value, Estado.Criar("MG", "Minas Gerais").Value),
            Ddd.Criar(Codigo.Criar("33").Value, Estado.Criar("MG", "Minas Gerais").Value),
            Ddd.Criar(Codigo.Criar("34").Value, Estado.Criar("MG", "Minas Gerais").Value),
            Ddd.Criar(Codigo.Criar("35").Value, Estado.Criar("MG", "Minas Gerais").Value),
            Ddd.Criar(Codigo.Criar("37").Value, Estado.Criar("MG", "Minas Gerais").Value),
            Ddd.Criar(Codigo.Criar("38").Value, Estado.Criar("MG", "Minas Gerais").Value),

            Ddd.Criar(Codigo.Criar("21").Value, Estado.Criar("RJ", "Rio de Janeiro").Value),
            Ddd.Criar(Codigo.Criar("22").Value, Estado.Criar("RJ", "Rio de Janeiro").Value),
            Ddd.Criar(Codigo.Criar("24").Value, Estado.Criar("RJ", "Rio de Janeiro").Value),

            Ddd.Criar(Codigo.Criar("11").Value, Estado.Criar("SP", "São Paulo").Value),
            Ddd.Criar(Codigo.Criar("12").Value, Estado.Criar("SP", "São Paulo").Value),
            Ddd.Criar(Codigo.Criar("13").Value, Estado.Criar("SP", "São Paulo").Value),
            Ddd.Criar(Codigo.Criar("14").Value, Estado.Criar("SP", "São Paulo").Value),
            Ddd.Criar(Codigo.Criar("15").Value, Estado.Criar("SP", "São Paulo").Value),
            Ddd.Criar(Codigo.Criar("16").Value, Estado.Criar("SP", "São Paulo").Value),
            Ddd.Criar(Codigo.Criar("17").Value, Estado.Criar("SP", "São Paulo").Value),
            Ddd.Criar(Codigo.Criar("18").Value, Estado.Criar("SP", "São Paulo").Value),
            Ddd.Criar(Codigo.Criar("19").Value, Estado.Criar("SP", "São Paulo").Value),

            Ddd.Criar(Codigo.Criar("41").Value, Estado.Criar("PR", "Paraná").Value),
            Ddd.Criar(Codigo.Criar("42").Value, Estado.Criar("PR", "Paraná").Value),
            Ddd.Criar(Codigo.Criar("43").Value, Estado.Criar("PR", "Paraná").Value),
            Ddd.Criar(Codigo.Criar("44").Value, Estado.Criar("PR", "Paraná").Value),
            Ddd.Criar(Codigo.Criar("45").Value, Estado.Criar("PR", "Paraná").Value),
            Ddd.Criar(Codigo.Criar("46").Value, Estado.Criar("PR", "Paraná").Value),

            Ddd.Criar(Codigo.Criar("51").Value, Estado.Criar("RS", "Rio Grande do Sul").Value),
            Ddd.Criar(Codigo.Criar("53").Value, Estado.Criar("RS", "Rio Grande do Sul").Value),
            Ddd.Criar(Codigo.Criar("54").Value, Estado.Criar("RS", "Rio Grande do Sul").Value),
            Ddd.Criar(Codigo.Criar("55").Value, Estado.Criar("RS", "Rio Grande do Sul").Value),

            Ddd.Criar(Codigo.Criar("47").Value, Estado.Criar("SC", "Santa Catarina").Value),
            Ddd.Criar(Codigo.Criar("48").Value, Estado.Criar("SC", "Santa Catarina").Value),
            Ddd.Criar(Codigo.Criar("49").Value, Estado.Criar("SC", "Santa Catarina").Value),
            ];

        context.Ddds.AddRange(ddds);

        context.SaveChanges();
    }
}
