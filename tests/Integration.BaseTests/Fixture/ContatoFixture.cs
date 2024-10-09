using System.Data.SqlClient;

namespace Integration.BaseTests.Fixture;

public class ContatoFixture(string connectionString)
{
    private readonly string _connectionString = connectionString;

    public async Task CriarContato(
        Guid contatoId,
        string nome,
        string email,
        string telefone,
        string ddd)
    {
        const string sql = $"""
            INSERT INTO Contatos (
                Id, 
                DddId, 
                Email, 
                Nome, 
                Telefone
            )
            VALUES (
                @ContatoId,
                (SELECT Id FROM Ddds WHERE Codigo = @Ddd),
                @Email,
                @Nome,
                @Telefone
            )
            """;

        using SqlConnection connection = new(_connectionString);

        try
        {
            await connection.OpenAsync();

            using SqlCommand command = new(sql, connection);

            command.Parameters.AddWithValue("@Nome", nome);
            command.Parameters.AddWithValue("@Email", email);
            command.Parameters.AddWithValue("@Telefone", telefone);
            command.Parameters.AddWithValue("@Ddd", ddd);
            command.Parameters.AddWithValue("@ContatoId", contatoId);

            await command.ExecuteScalarAsync();

            await connection.CloseAsync();
        }
        catch(Exception e)
        {
            throw e;
        }
    }
}
