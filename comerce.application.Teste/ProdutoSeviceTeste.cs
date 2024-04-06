using comerce.aplication.services;
using comerce.data.Contract;
using comerce.domain.model;
using comerce.domain.ModelView;
using commerce.mock;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace commerce.Application.Teste
{
    public class ProdutoSeviceTeste
    {
        [Fact]
        public async Task Get_ReturnsOkResult_WhenEntityExists()
        {
            // Arrange
            var produto = new ComerceMock().NovoProduto();

            var mockRepository = new Mock<IProdutoRepository>();
            var produtoService = new ProdutoService(mockRepository.Object);

            mockRepository.Setup(repo => repo.Get(produto.Id)).Returns(produto);

            // Act
            var result = await produtoService.Get(produto.Id);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedEntity = Assert.IsType<Produto>(okResult.Value);
            Assert.Equal(produto, returnedEntity);
        }

        [Fact]
        public async Task GetByName_ReturnsOkResult_WhenEntityExists()
        {
            // Arrange
            var produto = new ComerceMock().NovoProduto();

            var mockRepository = new Mock<IProdutoRepository>();
            var produtoService = new ProdutoService(mockRepository.Object);
            var lista = new List<Produto>() { produto };

            mockRepository.Setup(repo => repo.GetByName(produto.Nome)).Returns(lista);

            // Act
            var result = await produtoService.GetByName(produto.Nome);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedEntities = Assert.IsAssignableFrom<IEnumerable<Produto>>(okResult.Value);
            var returnedEntity = Assert.Single(returnedEntities);
            Assert.Equal(produto.Nome, returnedEntity.Nome);
            Assert.Equal(produto.Valor, returnedEntity.Valor);
            Assert.Equal(produto.Estoque, returnedEntity.Estoque);
        }

        [Fact]
        public async Task GetBySort_ReturnsOkResult_WhenEntityExists()
        {
            // Arrange
            var mockRepository = new Mock<IProdutoRepository>();
            var produtoService = new ProdutoService(mockRepository.Object);
            var sorts = new List<EstockModelView> { new EstockModelView { PropertyName = "Nome", Direction = "asc" } };
            var mockEntities = new List<Produto>
            {
                new Produto("Produto1", 10.00, 5),
                new Produto("Produto2", 20.00, 10)
            };

            mockRepository.Setup(repo => repo.GetBySort(sorts)).Returns(mockEntities);

            // Act
            var result = await produtoService.GetBySort(sorts);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedEntities = Assert.IsAssignableFrom<List<Produto>>(okResult.Value);
            Assert.Equal(mockEntities, returnedEntities);
        }

        [Fact]
        public async Task Add_ReturnsNoContentResult_WhenDtoIsValid()
        {
            // Arrange
            var mockRepository = new Mock<IProdutoRepository>();
            var produtoService = new ProdutoService(mockRepository.Object);
            var produtoModelView = new ProdutoModelView { Nome = "Produto Teste", Valor = 50.00, Estoque = 10 };

            // Act
            var result = await produtoService.Add(produtoModelView);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task Delete_ReturnsNoContentResult_WhenEntityExists()
        {
            // Arrange
            var produto = new ComerceMock().NovoProduto();

            var mockRepository = new Mock<IProdutoRepository>();
            var produtoService = new ProdutoService(mockRepository.Object);

            mockRepository.Setup(repo => repo.Get(produto.Id)).Returns(produto);

            // Act
            var result = await produtoService.Delete(produto.Id);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task Update_ReturnsOkResult_WhenDtoIsValidAndEntityExists()
        {
            // Arrange
            var produto = new ComerceMock().NovoProduto();

            var mockRepository = new Mock<IProdutoRepository>();
            var produtoService = new ProdutoService(mockRepository.Object);
            var produtoModelView = new ProdutoModelView { Nome = "Produto Atualizado", Valor = 100.00, Estoque = 20 };

            mockRepository.Setup(repo => repo.Get(produto.Id)).Returns(produto);

            // Act
            var result = await produtoService.Update(produto.Id, produtoModelView);

            // Assert
            Assert.IsType<OkResult>(result);
            Assert.Equal(produtoModelView.Nome, produto.Nome);
            Assert.Equal(produtoModelView.Valor, produto.Valor);
            Assert.Equal(produtoModelView.Estoque, produto.Estoque);
        }
    }
}