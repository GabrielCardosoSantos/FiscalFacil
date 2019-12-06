using FiscalFacil.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FiscalFacil
{
    public class NotaFiscalModel
    {
        public NotaFiscal Nota { get; set; }
        public Local Local { get; set; }
        public List<ProdutoModel> Produtos { get; set; }

        public NotaFiscalModel(NotaFiscal nota, Local local)
        {
            Nota = nota;
            Local = local;
            Produtos = new List<ProdutoModel>();
        }

        public bool AddProduto(ProdutoModel produto)
        {
            try
            {
                Produtos.Add(produto);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public ProdutoModel GetProduto(string nome)
        {
            return Produtos.Find(n => n.Produto.Descricao == nome);
        }

        public string GetDataEmissao()
        {
            return Nota.DataEmissao.ToString();
        }

        public string GetNomeLocal()
        {
            return Local.Nome;
        }

        public List<ProdutoModel> GetProdutos()
        {
            return Produtos;
        }

        public void ShowNota()
        {
            Console.WriteLine("   ------------------");
            Console.WriteLine("   Id: " + Nota.Id);
            Console.WriteLine("   Id Lugar: " + Nota.IdLocal);
            Console.WriteLine("   Chave Acesso: " + Nota.ChaveAcesso);
            Console.WriteLine("   Data Emissao: " + Nota.DataEmissao);
            Console.WriteLine("   Valor Compra: " + Nota.ValorCompra);
            Console.WriteLine("   Valor Desconto: " + Nota.ValorDesconto);
            Console.WriteLine("   Lugar: " + Local.Nome);

            Console.WriteLine("   ------------------");
            foreach (ProdutoModel p in Produtos)
            {
                //Console.WriteLine("  " + p.Id);
                //Console.WriteLine("  " + p.IdLocal);
                //Console.WriteLine("  " + p.IdNota);
                Console.Write("  " + p.Produto.Descricao);
                Console.Write("  " + p.Produto.Qtd);
                Console.Write("  " + p.Preco.ValorPago);
                Console.WriteLine("  " + p.Preco.ValorUnidade);
            }

            Console.WriteLine("   ------------------");
        }

        public string GetEndereco()
        {
            return Local.Endereco;
        }
    }
}
