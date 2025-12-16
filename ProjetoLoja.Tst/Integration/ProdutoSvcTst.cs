using Microsoft.EntityFrameworkCore;
using ProjetoLoja.Data;
using ProjetoLoja.Mdl;
using ProjetoLoja.Svc;

namespace ProjetoLoja.Tst.Integration
{
    public class ProdutoSvcTst
    {
        private readonly ProdutoSvc _svc;
        private readonly AppDbContext _dbContext;

        public ProdutoSvcTst()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) // Nome único para cada teste
                .Options;

            _dbContext = new AppDbContext(options);
            _svc = new ProdutoSvc(_dbContext);
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

            var resultado = _svc.CreateNewProduto(produto);

            // Assert
            Assert.NotNull(resultado);
            Assert.True(resultado.Id > 0); // Verifica se foi gerado um ID
            Assert.Equal(nomeProduto, resultado.Nome);
            Assert.Equal(precoProduto, resultado.Preco);
            Assert.Equal(marcaProduto, resultado.Marca);
        }

        [Fact]
        public void Cria_retorna_todos_os_produtos_e_seleciona_1()
        {
            for(int i = 1; i < 5; i++)
            {
                switch (i)
                {
                    case 1: Criar_Produtos_Corretamente("Notebook I5", 5499.90, "Dell");
                        break;
                        
                    case 2: Criar_Produtos_Corretamente("Bolacha Maria", 2.99, "");
                        break;
                        
                    case 3: Criar_Produtos_Corretamente("Filtro de água", 349.90, "Eletrolux");
                        break;

                    default: Criar_Produtos_Corretamente("Monitor Gamer Curvo Odyssey 34’’", (2104.92*i), "Samsung");
                        break;
                }
            }

            var produtosDoBanco = _svc.GetAllProdutos();
            var produtoNaMao = produtosDoBanco.Where(a => a.Id > 0).FirstOrDefault();
            var produtoDoBanco = _svc.GetProdutoById(2);

            Assert.NotNull(produtosDoBanco);
            Assert.True(produtosDoBanco.Count > 0);
            Assert.NotNull(produtoNaMao);
            Assert.NotNull(produtoDoBanco);
            Assert.True(produtoNaMao.Id != produtoDoBanco.Id);
        }

        [Fact]
        public void CRUD_Completo()
        {

            for (int i = 1; i < 5; i++)
            {
                switch (i)
                {
                    case 1:
                        Criar_Produtos_Corretamente("Notebook I5", 5499.90, "Dell");
                        break;

                    case 2:
                        Criar_Produtos_Corretamente("Bolacha Maria", 2.99, "");
                        break;

                    case 3:
                        Criar_Produtos_Corretamente("Filtro de água", 349.90, "Eletrolux");
                        break;

                    default:
                        Criar_Produtos_Corretamente("Monitor Gamer Curvo Odyssey 34’’", (2104.92 * i), "Samsung");
                        break;
                }
            }

            var produtosDoBanco = _svc.GetAllProdutos();
            Assert.NotNull(produtosDoBanco);

            var primeiroProduto = produtosDoBanco.FirstOrDefault();
            Assert.NotNull(primeiroProduto);

            var produtoDoBanco = _svc.GetProdutoById(primeiroProduto.Id);
            Assert.NotNull(produtoDoBanco);

            produtoDoBanco.Marca = "Marca Diabo";
            produtoDoBanco.Preco = 3.33;

            var produtoEditado = _svc.UpdateProduto(produtoDoBanco);
            Assert.True(produtoDoBanco.Marca == produtoEditado.Marca);
            Assert.True(produtoDoBanco.Preco == produtoEditado.Preco);

            var deletado = _svc.DeleteProdutoById(produtoDoBanco.Id);
            Assert.True(deletado);

            Assert.Throws<ArgumentNullException>(() => _svc.GetProdutoById(produtoEditado.Id));
        }
    }
}
