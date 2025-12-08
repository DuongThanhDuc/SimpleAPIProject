using System.Text.Json;
using models;

namespace utilities
{
    public class DataSeeder
    {
        private readonly string dir;
        public DataSeeder()
        {
            dir = Path.Combine(Directory.GetCurrentDirectory(), "Data");
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
        }

        public async Task DataSeeders()
        {
            await Seed("User.json", getUsers());
            await Seed("Product.json", getProducts());
            await Seed("Order.json", getOrders());
            await Seed("OrderDetail.json", getOrderDetails());
        }

        public async Task Seed<T>(string fileName, List<T> data)
        {
            var filePath = Path.Combine(dir, fileName);
            
            if (File.Exists(filePath))
            {
                Console.WriteLine($"{fileName} already exists. Skipping seeding.");
                return;
            }


            var opt = new JsonSerializerOptions { WriteIndented = true };
            var jsonData = JsonSerializer.Serialize(data, opt);

            await File.WriteAllTextAsync(filePath, jsonData);
            Console.WriteLine($"{fileName} created.");
        }

        private List<User> getUsers()
        {
            return new List<User>
            {
                new User
                {
                    ID = 1,
                    Username = "ADMIN",
                    Password = "StoreAdminNo1",
                    Role = "Administrator",
                    Email = "admin@gmail.com",
                    PhoneNumber = "1234567890",
                    Address = "885 Long Beach, California"
                },

                 new User
                {
                    ID = 2,
                    Username = "Miguel",
                    Password = "BrasilNumeroUno1",
                    Role = "Staff",
                    Email = "staff123@gmail.com",
                    PhoneNumber = "1234567890",
                    Address = "500 Arroyo, New Mexico"
                },

                 new User
                {
                    ID = 3,
                    Username = "user",
                    Password = "user123",
                    Role = "User",
                    Email = "user@gmail.com",
                    PhoneNumber = "1234567890",
                    Address = "998 Silent Hill, Texas"
                }
            };
        }

        private List<Product> getProducts()
        {
            return new List<Product>
            {
                new Product
                {
                    ID = 1,
                    Name = "Laptop",
                    Description = "High performance laptop",
                    Price = 1200.00M,
                    StockQuantity = 50
                },
                new Product
                {
                    ID = 2,
                    Name = "Smartphone",
                    Description = "Latest model smartphone",
                    Price = 800.00M,
                    StockQuantity = 100
                }
            };
        }

        private List<Order> getOrders()
        {
            return new List<Order>
            {
                new Order
                {
                    ID = 1,
                    UserID = 3,
                    OrderDate = DateTime.Now,
                    TotalAmount = 2000.00M,
                    Status = "Processing"
                }
            };
        }

        private List<OrderDetail> getOrderDetails()
        {
            return new List<OrderDetail>
            {
                new OrderDetail
                {
                    ID = 1,
                    OrderID = 1,
                    ProductID = 1,
                    Quantity = 1,
                    UnitPrice = 1200.00M
                },
                new OrderDetail
                {
                    ID = 2,
                    OrderID = 1,
                    ProductID = 2,
                    Quantity = 1,
                    UnitPrice = 800.00M
                }
            };
        }
        
    }
}