using SQLite;
using System;

namespace FiscalFacil
{
    public class NotaFiscal
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int IdLocal { get; set; }
        public DateTime DataEmissao { get; set; }
        public string ChaveAcesso { get; set; }
        public decimal ValorCompra { get; set; }
        public decimal ValorDesconto { get; set; }
    }
}
