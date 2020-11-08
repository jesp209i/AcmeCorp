using AcmeCorp.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AcmeCorp.Infrastructure
{
    public class ProductFetcher : IProductFetcher
    {
        private readonly ICollection<Product> _products = new List<Product>();
        public ProductFetcher()
        {
            Generate100SerialNumbers();            
        }
        public async Task<Product> GetProduct(string serialNumber)
        {
            await Task.Delay(42);
            return _products.FirstOrDefault(x => x.SerialNumber == serialNumber);
        }

        public async Task<ICollection<Product>> GetProducts()
        {
            await Task.Delay(42);
            return _products;
        }
        private void Generate100SerialNumbers()
        {
            var randomSeed = 42424242; // seed that generates the same numbers each time the solution is run
            var rand = new Random(randomSeed);
            string[] superlatives = {"Great", "Super", "So-so", "Not Too Shabby", "Mid-range", "Wild", "Ugly, but Good", "Weird", "Friendly", "Tough", "Spicy" };
            string[] names1 = { "Hairdryer", "Lawnmower", "Laptop", "Flat Iron", "EZ-Bake Oven" };
            var id = 1;
            do
            {
                var product = new Product { 
                    SerialNumber = $"ac-{rand.Next(1000000, 9999999)}", 
                    Name = $"{superlatives[rand.Next(superlatives.Length)]} {names1[rand.Next(names1.Length)]}", 
                    Id = id };
                if (!_products.Contains(product))
                {
                    _products.Add(product);
                    id++;
                }
            } while (_products.Count < 100);
        }
    }
}