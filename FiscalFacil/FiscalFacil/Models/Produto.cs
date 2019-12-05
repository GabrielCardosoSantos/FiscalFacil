
using SQLite;

namespace FiscalFacil
{
    public class Produto
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int IdNota { get; set; }
        public int IdLocal { get; set; }
        public string Descricao { get; set; }
        public string TipoUnidade { get; set; }
        public decimal Qtd { get; set; }
        public decimal ValorUnidade { get; set; }
        public decimal ValorPago { get; set; }
    }
}
