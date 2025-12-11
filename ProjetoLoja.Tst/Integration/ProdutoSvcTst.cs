using Microsoft.EntityFrameworkCore;
using ProjetoLoja.Data;
using ProjetoLoja.Mdl;
using ProjetoLoja.Svc;

namespace ProjetoLoja.Tst.Integration
{
    public class ProdutoSvcTst
    {
        private readonly ProdutoSvc svc;
        private readonly AppDbContext dbContext;

        public ProdutoSvcTst()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) // Nome único para cada teste
                .Options;

            dbContext = new AppDbContext(options);
            svc = new ProdutoSvc(dbContext);
        }

        [Theory]
        [InlineData("Notebook I5", 5499.90, "Dell")]
        [InlineData("Bolacha Maria", 2.99, "")]
        [InlineData("Filtro de água", 349.90, "Eletrolux")]
        public void Criar_Produtos_Corretamente(string nomeProduto, double precoProduto, string? marcaProduto)
        {
            Produto produto = new Produto
            {
                Nome = nomeProduto,
                Preco = precoProduto,
                Marca = marcaProduto
            };

            var resultado = svc.CreateNewProduto(produto);

            // Assert
            Assert.NotNull(resultado);
            Assert.True(resultado.Id > 0); // Verifica se foi gerado um ID
            Assert.Equal(nomeProduto, resultado.Nome);
            Assert.Equal(precoProduto, resultado.Preco);
            Assert.Equal(marcaProduto, resultado.Marca);
        }


    }
}
