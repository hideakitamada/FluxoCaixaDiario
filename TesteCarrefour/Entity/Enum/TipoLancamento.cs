using System.Text.Json.Serialization;

namespace TesteCarrefour.Entity.Enum;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum TipoLancamento
{
    Credito,
    Debito
}