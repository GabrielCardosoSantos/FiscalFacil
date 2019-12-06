using System;
using System.Collections.Generic;
using System.Text;

namespace FiscalFacil.Models
{
    public class ProdutoModel
    {
        public Produto Produto { get; set; }
        public Preco Preco { get; set; }


        public ProdutoModel(Produto produto, Preco preco)
        {
            Produto = produto;
            Preco = preco;
        }
            
    }
}
