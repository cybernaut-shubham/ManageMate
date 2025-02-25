using ManageMate.DAL.Models;

namespace ManageMate.DAL.Repositories.RepositoryInterfaces
{
    public interface IProductRepository
    {
        public IEnumerable<Product> GetAllProducts();

        public Task<Product> GetProductWithProductId(int id);

        public Task<Product> AddProduct(Product product);

        public Task<Product> UpdateProductById(int id, Product product);

        public Task<Product> DecrementStock(int id, int quantity);

        public Task<Product> IncrementStock(int id, int quantity);

        public Task<Product> DeleteProductById(int id);
    }
}