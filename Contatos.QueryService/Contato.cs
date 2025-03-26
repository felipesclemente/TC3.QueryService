namespace Contatos.QueryService
{
    public record Contato
    {
        public long? Id { get; init; }
        public string? NomeCompleto { get; init; }
        public int? DDD { get; init; }
        public int? Telefone { get; init; }
        public string? Email { get; init; }
        public string? Zona { get; init; }
        public string? UF { get; init; }

        public override string ToString()
        {
            return $"Contato ID {Id}, Nome Completo: {NomeCompleto}, DDD: {DDD}, Telefone: {Telefone}, Email: {Email}, Região: {Zona}, UF: {UF}.";
        }
    }
}
