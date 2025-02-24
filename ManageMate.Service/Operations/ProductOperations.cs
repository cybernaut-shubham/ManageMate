using ManageMate.DAL.Models;
using ManageMate.DAL.Repositories.RepositoryInterfaces;
using ManageMate.Service.Operations.OperationsInterface;

namespace ManageMate.Service.Operations
{
    public class ProductOperations : IProductOperations
    {
        private readonly IProductRepository _productRepository;
        public ProductOperations(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task<Product> AddProduct(Product product)
        {
            return await _productRepository.AddProduct(product);
        }

        public async Task<Product> DecrementStock(int id, int quantity)
        {
            return await _productRepository.DecrementStock(id, quantity);
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return _productRepository.GetAllProducts();
        }

        public async Task<Product> GetProductWithProductId(int id)
        {
            return await _productRepository.GetProductWithProductId(id);
        }

        public async Task<Product> IncrementStock(int id, int quantity)
        {
            return await _productRepository.IncrementStock(id, quantity);
        }

        public async Task<Product> UpdateProductById(int id, Product product)
        {
            return await _productRepository.UpdateProductById(id, product);
        }
    }
}
