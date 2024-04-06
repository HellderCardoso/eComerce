using comerce.Api.Controllers;
using comerce.aplication.contract;
using comerce.domain.ModelView;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace commerce.API.Test
{
    public class ProdutoControllerTest
    {
        [Fact]
        public async Task Get_ReturnsCorrectActionResult()
        {
            // Arrange
            var id = 1;
            var mockProdutoService = new Mock<IProdutoService>();
            var produtoController = new ProdutoController(mockProdutoService.Object);
            var expectedResult = new OkObjectResult("Resultado esperado");

            mockProdutoService.Setup(service => service.Get(id)).ReturnsAsync(expectedResult);

            // Act
            var result = await produtoController.Get(id);

            // Assert
            Assert.IsType<OkObjectResult>(result);
            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public async Task GetBySort_ReturnsCorrectActionResult()
        {
            // Arrange
            var mockProdutoService = new Mock<IProdutoService>();
            var produtoController = new ProdutoController(mockProdutoService.Object);
            var sorts = new List<EstockModelView> { new EstockModelView { PropertyName = "Nome", Direction = "asc" } };
            var expectedResult = new OkObjectResult("Resultado esperado");

            mockProdutoService.Setup(service => service.GetBySort(sorts)).ReturnsAsync(expectedResult);

            // Act
            var result = await produtoController.GetBySort(sorts);

            // Assert
            Assert.IsType<OkObjectResult>(result);
            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public async Task GetByName_ReturnsCorrectActionResult()
        {
            // Arrange
            var name = "Test Product";
            var mockProdutoService = new Mock<IProdutoService>();
            var produtoController = new ProdutoController(mockProdutoService.Object);
            var expectedResult = new OkObjectResult("Resultado esperado");

            mockProdutoService.Setup(service => service.GetByName(name)).ReturnsAsync(expectedResult);

            // Act
            var result = await produtoController.GetByName(name);

            // Assert
            Assert.IsType<OkObjectResult>(result);
            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public async Task Post_ReturnsCorrectActionResult()
        {
            // Arrange
            var mockProdutoService = new Mock<IProdutoService>();
            var produtoController = new ProdutoController(mockProdutoService.Object);
            var dto = new ProdutoModelView { Nome = "Test Product", Valor = 50.00, Estoque = 10 };
            var expectedResult = new NoContentResult();

            mockProdutoService.Setup(service => service.Add(dto)).ReturnsAsync(expectedResult);

            // Act
            var result = await produtoController.Post(dto);

            // Assert
            Assert.IsType<NoContentResult>(result);
            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public async Task Update_ReturnsCorrectActionResult()
        {
            // Arrange
            var id = 1;
            var mockProdutoService = new Mock<IProdutoService>();
            var produtoController = new ProdutoController(mockProdutoService.Object);
            var dto = new ProdutoModelView { Nome = "Updated Product", Valor = 100.00, Estoque = 20 };
            var expectedResult = new OkResult();

            mockProdutoService.Setup(service => service.Update(id, dto)).ReturnsAsync(expectedResult);

            // Act
            var result = await produtoController.Update(dto, id);

            // Assert
            Assert.IsType<OkResult>(result);
            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public async Task Delete_ReturnsCorrectActionResult()
        {
            // Arrange
            var id = 1;
            var mockProdutoService = new Mock<IProdutoService>();
            var produtoController = new ProdutoController(mockProdutoService.Object);
            var expectedResult = new NoContentResult();

            mockProdutoService.Setup(service => service.Delete(id)).ReturnsAsync(expectedResult);

            // Act
            var result = await produtoController.Delete(id);

            // Assert
            Assert.IsType<NoContentResult>(result);
            Assert.Equal(expectedResult, result);
        }
    }
}