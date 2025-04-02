namespace TesteCarrefour.Entity;

public class Consolidado(decimal totalCredito,
                   decimal totalDebito,
                   DateTime data)
{
    public decimal TotalCredito { get; set; } = totalCredito;
    public decimal TotalDebito { get; set; } = totalDebito;
    public decimal Saldo { get => TotalCredito - TotalDebito; }
    public DateTime Data { get; set; } = data;
}
