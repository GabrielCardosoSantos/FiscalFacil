
using FiscalFacil.Models;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace FiscalFacil
{
    public class Produto
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [ForeignKey(typeof(NotaFiscal))]
        public int IdNota { get; set; }

        [ForeignKey(typeof(Local))]
        public int IdLocal { get; set; }

        [ForeignKey(typeof(Preco))]
        public int IdPreco { get; set; }

        public string Descricao { get; set; }
        public string TipoUnidade { get; set; }
        public decimal Qtd { get; set; }
        
    }
}
