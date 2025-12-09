using ProjetoLoja.Data;
using ProjetoLoja.Mdl;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjetoLoja.Svc;

public class ProdutoSvc(AppDbContext _dbContext)
{
    public List<Produto> GetAllProdutos()
    {
        var produtos = _dbContext.Produtos;
        if (produtos == null) 
            return []; // eh igual a return new List<Produto>();
        return [.. produtos]; // eh igual a return produtos.ToList();
    }

    public Produto? GetProdutoById(int id)
    {
        var produto = _dbContext.Produtos.Where(a => a.Id == id).FirstOrDefault();
        return produto;
    }

    public Produto CreateNewProduto(Produto newProduto)
    {
        ValidacaoProduto(newProduto);
        newProduto.Id = 0; // so para garantir
        _dbContext.Produtos.Add(newProduto);
        _dbContext.SaveChanges();
        return newProduto;
    }

    public Produto UpdateProdutoById(Produto newProduct)
    {
        ValidacaoProduto(newProduct);

        var oldProduct = _dbContext.Produtos.Where(a => a.Id == newProduct.Id).FirstOrDefault()
            ?? throw new ArgumentException("Produto não encontrado");

        oldProduct.Preco = newProduct.Preco;
        oldProduct.Nome = newProduct.Nome;
        oldProduct.Marca = newProduct.Marca;

        _dbContext.SaveChanges(); // Não precisa do Update(), o EF já rastreia
        return oldProduct;
    }

    public bool DeleteProdutoById(int id)
    {
        var produto = _dbContext.Produtos.FirstOrDefault(p => p.Id == id)
            ?? throw new ArgumentException("Produto não encontrado");

        _dbContext.Produtos.Remove(produto);
        _dbContext.SaveChanges();
        return true;
    }


    private static void ValidacaoProduto(Produto oldProduct)
    {
        if (string.IsNullOrEmpty(oldProduct.Nome))
            throw new ArgumentException("Nome é obrigatório.");

        if (oldProduct.Preco <= 0)
            throw new ArgumentException("Preço deve ser maior que zero.");

        if (oldProduct.Preco > 999999.99)
            throw new ArgumentException("Preço muito alto.");
    }
}
