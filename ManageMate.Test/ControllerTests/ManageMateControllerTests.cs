using ManageMate.DAL.Models;
using ManageMate.Service.Controllers;
using ManageMate.Service.Operations.OperationsInterface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace ManageMate.Test.ControllerTests
{
    [TestClass]
    public class ManageMateControllerTests
    {
        private Mock<IProductOperations> _mockProductOperations;
        private ManageMateController _manageMateController;

        [TestInitialize]
        public void Initialize()
        {
            _mockProductOperations = new Mock<IProductOperations>();
            _manageMateController = new ManageMateController(_mockProductOperations.Object);
        }

        [TestMethod]
        public void GetAllProducts_ReturnsOk_WhenListIsNotNullOrEmpty()
        {
            // Arrange
            _mockProductOperations.Setup(x => x.GetAllProducts()).Returns(MockData.GetAllProductsMock());

            // Act
            var response = _manageMateController.GetAllProducts();

            // Assert
            Assert.IsNotNull(response);
            Assert.IsInstanceOfType(response, typeof(OkObjectResult));
            Assert.AreEqual(StatusCodes.Status200OK, ((OkObjectResult)response).StatusCode);
        }

        [TestMethod]
        public void GetAllProducts_ReturnsNotFound_WhenListIsNullOrEmpty()
        {
            // Arrange
            _mockProductOperations.Setup(x => x.GetAllProducts()).Returns<IEnumerable<Product>>(null!);
            var expectedResult = "No products available at the moment.";
            // Act
            var response = _manageMateController.GetAllProducts();

            // Assert
            Assert.IsNotNull(response);
            Assert.IsInstanceOfType(response, typeof(NotFoundObjectResult));
            Assert.AreEqual(StatusCodes.Status404NotFound, ((NotFoundObjectResult)response).StatusCode);
            Assert.AreEqual(expectedResult, ((NotFoundObjectResult)response).Value!.ToString());
        }

        [TestMethod]
        public void GetProductWithProductId_ReturnsOk_WhenProductExists()
        {
            // Arrange

            _mockProductOperations.Setup(x => x.GetProductWithProductId(It.IsAny<int>())).ReturnsAsync(MockData.GetProductByIdMock());

            // Act
            var response = _manageMateController.GetProductWithProductId(123456).Result;

            // Assert
            Assert.IsNotNull(response);
            Assert.IsInstanceOfType(response, typeof(OkObjectResult));
            Assert.AreEqual(StatusCodes.Status200OK, ((OkObjectResult)response).StatusCode);
        }

        [TestMethod]
        public void GetProductWithProductId_ReturnsNotFound_WhenProductDoesNotExist()
        {
            // Arrange
            _mockProductOperations.Setup(x => x.GetProductWithProductId(It.IsAny<int>())).ReturnsAsync((Product)null!);
            var expectedResult = "Product not found";

            // Act
            var response = _manageMateController.GetProductWithProductId(123456).Result;

            // Assert
            Assert.IsNotNull(response);
            Assert.IsInstanceOfType(response, typeof(NotFoundObjectResult));
            Assert.AreEqual(StatusCodes.Status404NotFound, ((NotFoundObjectResult)response).StatusCode);
            Assert.AreEqual(expectedResult, ((NotFoundObjectResult)response).Value!.ToString());
        }

        [TestMethod]
        public void AddProduct_ReturnsOk_WhenProductIsAdded()
        {
            // Arrange
            var product = new Product()
            {
                Name = "Product1",
                Price = 10.00M,
                Quantity = 10,
                CreatedAt = System.DateTime.UtcNow
            };

            _mockProductOperations.Setup(x => x.AddProduct(It.IsAny<Product>())).ReturnsAsync(MockData.GetProductByIdMock());

            // Act
            var response = _manageMateController.AddProduct(product).Result;

            // Assert
            Assert.IsNotNull(response);
            Assert.IsInstanceOfType(response, typeof(OkObjectResult));
            Assert.AreEqual(StatusCodes.Status200OK, ((OkObjectResult)response).StatusCode);
        }

        [TestMethod]
        public void AddProduct_ReturnsBadRequest_WhenProductIsNull()
        {
            // Arrange
            var product = (Product)null!;
            var expectedResult = "Product is null";

            // Act
            var response = _manageMateController.AddProduct(product).Result;

            // Assert
            Assert.IsNotNull(response);
            Assert.IsInstanceOfType(response, typeof(BadRequestObjectResult));
            Assert.AreEqual(StatusCodes.Status400BadRequest, ((BadRequestObjectResult)response).StatusCode);
            Assert.AreEqual(expectedResult, ((BadRequestObjectResult)response).Value!.ToString());
        }

        [TestMethod]
        public void AddProduct_ReturnsBadRequest_WhenProductAlreadyExists()
        {
            // Arrange
            var product = new Product()
            {
                Name = "Product1",
                Price = 10.00M,
                Quantity = 10,
                CreatedAt = System.DateTime.UtcNow
            };

            var expectedResult = "Product already exists. Please try adding another product.";

            _mockProductOperations.Setup(x => x.AddProduct(It.IsAny<Product>())).ReturnsAsync((Product)null!);

            // Act
            var response = _manageMateController.AddProduct(product).Result;

            // Assert
            Assert.IsNotNull(response);
            Assert.IsInstanceOfType(response, typeof(BadRequestObjectResult));
            Assert.AreEqual(StatusCodes.Status400BadRequest, ((BadRequestObjectResult)response).StatusCode);
            Assert.AreEqual(expectedResult, ((BadRequestObjectResult)response).Value!.ToString());
        }

        [TestMethod]
        public void UpdateProductById_ReturnsOk_WhenProductIsUpdated()
        {
            // Arrange
            var product = new Product()
            {
                Name = "Product1",
                Price = 10.00M,
                Quantity = 10,
                CreatedAt = System.DateTime.UtcNow
            };

            _mockProductOperations.Setup(x => x.UpdateProductById(It.IsAny<int>(), It.IsAny<Product>())).ReturnsAsync(MockData.GetProductByIdMock());

            // Act
            var response = _manageMateController.UpdateProductById(123456, product).Result;

            // Assert
            Assert.IsNotNull(response);
            Assert.IsInstanceOfType(response, typeof(OkObjectResult));
            Assert.AreEqual(StatusCodes.Status200OK, ((OkObjectResult)response).StatusCode);
        }

        [TestMethod]
        public void UpdateProductById_ReturnsBadRequest_WhenProductIsNull()
        {
            // Arrange
            var product = (Product)null!;
            var expectedResult = "Product is null";

            // Act
            var response = _manageMateController.UpdateProductById(123456, product).Result;

            // Assert
            Assert.IsNotNull(response);
            Assert.IsInstanceOfType(response, typeof(BadRequestObjectResult));
            Assert.AreEqual(StatusCodes.Status400BadRequest, ((BadRequestObjectResult)response).StatusCode);
            Assert.AreEqual(expectedResult, ((BadRequestObjectResult)response).Value!.ToString());
        }

        [TestMethod]
        public void DecrementStock_ReturnsOk_WhenStockIsDecremented()
        {
            // Arrange
            _mockProductOperations.Setup(x => x.DecrementStock(It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(MockData.GetProductByIdMock());

            // Act
            var response = _manageMateController.DecrementStock(123456, 5).Result;

            // Assert
            Assert.IsNotNull(response);
            Assert.IsInstanceOfType(response, typeof(OkObjectResult));
            Assert.AreEqual(StatusCodes.Status200OK, ((OkObjectResult)response).StatusCode);
        }

        [TestMethod]
        public void DecrementStock_ReturnsNotFound_WhenProductDoesNotExist()
        {
            // Arrange
            _mockProductOperations.Setup(x => x.DecrementStock(It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync((Product)null!);
            var expectedResult = "Product not found";

            // Act
            var response = _manageMateController.DecrementStock(123456, 5).Result;

            // Assert
            Assert.IsNotNull(response);
            Assert.IsInstanceOfType(response, typeof(NotFoundObjectResult));
            Assert.AreEqual(StatusCodes.Status404NotFound, ((NotFoundObjectResult)response).StatusCode);
            Assert.AreEqual(expectedResult, ((NotFoundObjectResult)response).Value!.ToString());
        }

        [TestMethod]
        public void IncrementStock_ReturnsOk_WhenStockIsIncremented()
        {
            // Arrange
            _mockProductOperations.Setup(x => x.IncrementStock(It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(MockData.GetProductByIdMock());

            // Act
            var response = _manageMateController.IncrementStock(123456, 5).Result;

            // Assert
            Assert.IsNotNull(response);
            Assert.IsInstanceOfType(response, typeof(OkObjectResult));
            Assert.AreEqual(StatusCodes.Status200OK, ((OkObjectResult)response).StatusCode);
        }

        [TestMethod]
        public void IncrementStock_ReturnsNotFound_WhenProductDoesNotExist()
        {
            // Arrange
            _mockProductOperations.Setup(x => x.IncrementStock(It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync((Product)null!);
            var expectedResult = "Product not found";

            // Act
            var response = _manageMateController.IncrementStock(123456, 5).Result;

            // Assert
            Assert.IsNotNull(response);
            Assert.IsInstanceOfType(response, typeof(NotFoundObjectResult));
            Assert.AreEqual(StatusCodes.Status404NotFound, ((NotFoundObjectResult)response).StatusCode);
            Assert.AreEqual(expectedResult, ((NotFoundObjectResult)response).Value!.ToString());
        }

        [TestMethod]
        public void DeleteProductById_ReturnsOk_WhenProductIsDeleted()
        {
            // Arrange
            _mockProductOperations.Setup(x => x.DeleteProductById(It.IsAny<int>())).ReturnsAsync(MockData.GetProductByIdMock());

            // Act
            var response = _manageMateController.DeleteProductById(123456).Result;

            // Assert
            Assert.IsNotNull(response);
            Assert.IsInstanceOfType(response, typeof(OkObjectResult));
            Assert.AreEqual(StatusCodes.Status200OK, ((OkObjectResult)response).StatusCode);
        }

        [TestMethod]
        public void DeleteProductById_ReturnsNotFound_WhenProductDoesNotExist()
        {
            // Arrange
            _mockProductOperations.Setup(x => x.DeleteProductById(It.IsAny<int>())).ReturnsAsync((Product)null!);
            var expectedResult = "Product not found.";

            // Act
            var response = _manageMateController.DeleteProductById(123456).Result;

            // Assert
            Assert.IsNotNull(response);
            Assert.IsInstanceOfType(response, typeof(NotFoundObjectResult));
            Assert.AreEqual(StatusCodes.Status404NotFound, ((NotFoundObjectResult)response).StatusCode);
            Assert.AreEqual(expectedResult, ((NotFoundObjectResult)response).Value!.ToString());
        }
    }
}