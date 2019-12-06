using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace FiscalFacil.Models
{
    public class Preco
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [ForeignKey(typeof(Produto))]
        public int IdProduto { get; set; }

        public DateTime DataPreco { get; set; }
        public decimal ValorPago { get; set; }
        public decimal ValorUnidade { get; set; }
    }
}
