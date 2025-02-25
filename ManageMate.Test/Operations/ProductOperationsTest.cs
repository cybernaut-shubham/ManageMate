using ManageMate.DAL.Models;
using ManageMate.DAL.Repositories.RepositoryInterfaces;
using ManageMate.Service.Operations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace ManageMate.Test.Operations
{
    [TestClass]
    public class ProductOperationsTest
    {
        private Mock<IProductRepository> _productRepositoryMock;
        private ProductOperations _productOperations;

        [TestInitialize]
        public void Initialize()
        {
            _productRepositoryMock = new Mock<IProductRepository>();
            _productOperations = new ProductOperations(_productRepositoryMock.Object);
        }

        [TestMethod]
        public void GetAllProducts_ReturnsProductList_WhenProductsExist()
        {
            //Arrange
            _productRepositoryMock.Setup(x => x.GetAllProducts()).Returns(MockData.GetAllProductsMock());

            //Act
            var response = _productOperations.GetAllProducts();

            //Assert
            Assert.IsNotNull(response);
        }

        [TestMethod]
        public void GetAllProducts_ReturnsNull_WhenNoProductsExist()
        {
            //Arrange
            _productRepositoryMock.Setup(x => x.GetAllProducts()).Returns < IEnumerable <Product>>(null!);

            //Act
            var response = _productOperations.GetAllProducts();

            //Assert
            Assert.IsNull(response);
        }

        [TestMethod]
        public void GetProductWithProductId_ReturnsProduct_WhenProductExists()
        {
            //Arrange
            _productRepositoryMock.Setup(x => x.GetProductWithProductId(It.IsAny<int>())).ReturnsAsync(MockData.GetProductByIdMock());

            //Act
            var response = _productOperations.GetProductWithProductId(123342).Result;

            //Assert
            Assert.IsNotNull(response);
        }

        [TestMethod]
        public void GetProductWithProductId_ReturnsNull_WhenProductDoesNotExist()
        {
            //Arrange
            _productRepositoryMock.Setup(x => x.GetProductWithProductId(It.IsAny<int>())).ReturnsAsync((Product)null!);

            //Act
            var response = _productOperations.GetProductWithProductId(123245).Result;

            //Assert
            Assert.IsNull(response);
        }

        [TestMethod]
        public void AddProduct_ReturnsProduct_WhenProductIsAdded()
        {
            //Arrange
            _productRepositoryMock.Setup(x => x.AddProduct(It.IsAny<Product>())).ReturnsAsync(MockData.GetProductByIdMock());

            //Act
            var response = _productOperations.AddProduct(new Product()).Result;

            //Assert
            Assert.IsNotNull(response);
        }

        [TestMethod]
        public void AddProduct_ReturnsNull_WhenProductAlreadyExists()
        {
            //Arrange
            _productRepositoryMock.Setup(x => x.AddProduct(It.IsAny<Product>())).ReturnsAsync((Product)null!);

            //Act
            var response = _productOperations.AddProduct(new Product()).Result;

            //Assert
            Assert.IsNull(response);
        }

        [TestMethod]
        public void UpdateProductById_ReturnsProduct_WhenProductIsUpdated()
        {
            //Arrange
            _productRepositoryMock.Setup(x => x.UpdateProductById(It.IsAny<int>(), It.IsAny<Product>())).ReturnsAsync(MockData.GetProductByIdMock());

            //Act
            var response = _productOperations.UpdateProductById(1234, new Product()).Result;

            //Assert
            Assert.IsNotNull(response);
        }

        [TestMethod]
        public void UpdateProductById_ReturnsNull_WhenProductDoesNotExist()
        {
            //Arrange
            _productRepositoryMock.Setup(x => x.UpdateProductById(It.IsAny<int>(), It.IsAny<Product>())).ReturnsAsync((Product)null!);

            //Act
            var response = _productOperations.UpdateProductById(1234, new Product()).Result;

            //Assert
            Assert.IsNull(response);
        }

        [TestMethod]
        public void DecrementStock_ReturnsProduct_WhenStockIsDecremented()
        {
            //Arrange
            _productRepositoryMock.Setup(x => x.DecrementStock(It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(MockData.GetProductByIdMock());

            //Act
            var response = _productOperations.DecrementStock(1234, 2).Result;

            //Assert
            Assert.IsNotNull(response);
        }

        [TestMethod]
        public void DecrementStock_ReturnsNull_WhenProductDoesNotExist()
        {
            //Arrange
            _productRepositoryMock.Setup(x => x.DecrementStock(It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync((Product)null!);

            //Act
            var response = _productOperations.DecrementStock(1234, 2).Result;

            //Assert
            Assert.IsNull(response);
        }

        [TestMethod]
        public void IncrementStock_ReturnsProduct_WhenStockIsIncremented()
        {
            //Arrange
            _productRepositoryMock.Setup(x => x.IncrementStock(It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(MockData.GetProductByIdMock());

            //Act
            var response = _productOperations.IncrementStock(1234, 2).Result;

            //Assert
            Assert.IsNotNull(response);
        }

        [TestMethod]
        public void IncrementStock_ReturnsNull_WhenProductDoesNotExist()
        {
            //Arrange
            _productRepositoryMock.Setup(x => x.IncrementStock(It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync((Product)null!);

            //Act
            var response = _productOperations.IncrementStock(1234, 2).Result;

            //Assert
            Assert.IsNull(response);
        }

        [TestMethod]
        public void DeleteProductById_ReturnsProduct_WhenProductIsDeleted()
        {
            //Arrange
            _productRepositoryMock.Setup(x => x.DeleteProductById(It.IsAny<int>())).ReturnsAsync(MockData.GetProductByIdMock());

            //Act
            var response = _productOperations.DeleteProductById(1234).Result;

            //Assert
            Assert.IsNotNull(response);
        }

        [TestMethod]
        public void DeleteProductById_ReturnsNull_WhenProductDoesNotExist()
        {
            //Arrange
            _productRepositoryMock.Setup(x => x.DeleteProductById(It.IsAny<int>())).ReturnsAsync((Product)null!);

            //Act
            var response = _productOperations.DeleteProductById(1234).Result;

            //Assert
            Assert.IsNull(response);
        }
    }
}
