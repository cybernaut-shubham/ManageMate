using ManageMate.DAL.Models;

namespace ManageMate.Test
{
    public static class MockData
    {
        public static IEnumerable<Product> GetAllProductsMock()
        {
            return new List<Product>()
            {
                new Product()
                {
                    ProductID = 123456,
                    Name = "Product1",
                    Price = 10.00M,
                    Quantity = 10,
                    CreatedAt = DateTime.UtcNow
                }
            };
        }

        public static Product GetProductByIdMock()
        {
            return new Product()
            {
                ProductID = 123456,
                Name = "Product1",
                Price = 10.00M,
                Quantity = 10,
                CreatedAt = DateTime.UtcNow
            };
        }
    }
}