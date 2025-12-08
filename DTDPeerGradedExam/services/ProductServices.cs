using models;
using utilities;

namespace services
{
    public class ProductServices : FileService
    {
        private const string FileName = "Product.json";

        public List<Product> LoadProducts()
        {
            return LoadFile<Product>(FileName);
        }

        public Product? GetProductByID(int id)
        {
            return LoadProducts().FirstOrDefault(p => p.ID == id); 
        }

        public void AddProduct(Product product)
        {
            var products = LoadProducts();
            products.Add(product);
            SaveFile<Product>(FileName, products);
        }

        public void UpdateProduct(Product product)
        {
            var products = LoadProducts();
            var existing = LoadProducts().FirstOrDefault(p => p.ID == product.ID);

            if (existing == null)
            {
                return;
            }

            existing.Name = product.Name;
            existing.Description = product.Description;
            existing.Price = product.Price;
            existing.StockQuantity = product.StockQuantity;


            SaveFile<Product>(FileName, products);
        }

        public void DeleteProduct(int id)
        {
            var products = LoadProducts();
            var productToDelete = products.FirstOrDefault(p => p.ID == id);
            if (productToDelete == null)
            {
                return;
            }
            products.Remove(productToDelete);
            SaveFile<Product>(FileName, products);
        }
    }
}