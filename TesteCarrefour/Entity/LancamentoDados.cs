using TesteCarrefour.Entity.Enum;

namespace TesteCarrefour.Entity
{
    public class LancamentoDados
    {
        public decimal Valor { get; set; }
        public DateTime DataLancamento { get; set; }
        public TipoLancamento Tipo { get; set; } // Crédito ou Débito
    }
}
