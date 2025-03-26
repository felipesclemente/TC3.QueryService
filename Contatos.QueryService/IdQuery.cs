using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Dapper;
using Npgsql;

namespace Contatos.QueryService
{
    public class IdQuery
    {
        private readonly ILogger<IdQuery> _logger;
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public IdQuery(ILogger<IdQuery> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("Postgres") ?? string.Empty;
        }

        [Function("IdQuery")]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "query/id/{id:int}")] HttpRequest req, int id)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            var queryString = @"SELECT 
                                    c.Id AS ID,
                                    c.NomeCompleto AS NomeCompleto,
                                    c.DDD AS DDD,
                                    c.Telefone AS Telefone,
                                    c.Email AS Email,
                                    r.Zona AS Zona,
                                    r.UF AS UF
                                FROM 
                                    Contato c
                                    INNER JOIN Regiao r ON c.DDD = r.DDD 
                                WHERE c.Id = @id";
            var result = await connection.QueryFirstOrDefaultAsync<Contato>(queryString, new { id });

            return new OkObjectResult(result);
        }
    }
}
