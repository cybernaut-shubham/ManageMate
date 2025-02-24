using ManageMate.DAL.Models;
using ManageMate.DAL.Repositories.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;

namespace ManageMate.DAL.Repositories
{
    public class ProductRepository: IProductRepository
    {
        private readonly ManageMateDbContext _context;
        public ProductRepository(ManageMateDbContext dbContext)
        {
            _context = dbContext;
        }

        public async Task<Product> AddProduct(Product product)
        {
            var productExists = await _context.Products.AsNoTracking().FirstOrDefaultAsync(x => x.ProductID == product.ProductID) != null;
            if (productExists)
            {
                return null!;
            }
            product.CreatedAt = DateTime.UtcNow;

            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();

            return product;
        }

        public async Task<Product> DecrementStock(int id, int quantity)
        {
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                product.Quantity -= quantity;
                await _context.SaveChangesAsync();
            }
            return product;
        }

        public IEnumerable<Product> GetAllProducts()
        {
            var productList = _context.Products.AsNoTracking();
            return productList;
        }

        public async Task<Product> GetProductWithProductId(int id)
        {
            var product = await _context.Products.FindAsync(id);
            return product;
        }

        public async Task<Product> IncrementStock(int id, int quantity)
        {
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                product.Quantity += quantity;
                await _context.SaveChangesAsync();
            }
            return product;
        }

        public async Task<Product> UpdateProductById(int id, Product product)
        {
            var productToUpdate = await _context.Products.FindAsync(id);
            if (productToUpdate != null)
            {
                productToUpdate.Quantity = product.Quantity;
                productToUpdate.Price = product.Price;
                productToUpdate.Name = product.Name;
                await _context.SaveChangesAsync();
            }
            return productToUpdate;
        }
    }
}