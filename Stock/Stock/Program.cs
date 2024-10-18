using System;

namespace StockAlertSystem
{
    public delegate void OnStockChange(string message);

    public class Companies
    {
        public string CompanyName { get; set; }
        private double _stock;
        public double Stock
        {
            get => _stock;
            set
            {
                _stock = value;
                OnStockChange?.Invoke($"{CompanyName} stock changed to {_stock}");
            }
        }

        public event OnStockChange OnStockChange;
    }

    public class Google : Companies
    {
        public Google()
        {
            CompanyName = "Google";
        }

        public void CheckStock()
        {
            if (Stock > 15)
            {
                Console.WriteLine("Google's stock is above 15. You should consider buying this share.");
            }
        }
    }

    public class Facebook : Companies
    {
        public Facebook()
        {
            CompanyName = "Facebook";
        }

        public void CheckStock()
        {
            if (Stock > 15)
            {
                Console.WriteLine("Facebook's stock is above 15. You should consider buying this share.");
            }
        }
    }

    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Balance { get; set; }

        public bool CanBuy(double price)
        {
            return Balance >= price;
        }

        public void BuyStock(double price)
        {
            if (CanBuy(price))
            {
                Balance -= price;
                Console.WriteLine($"{Name} bought the stock for {price}. Remaining balance: {Balance}");
            }
            else
            {
                Console.WriteLine($"{Name} does not have enough balance to buy the stock.");
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            User user = new User { Id = 1, Name = "Alice", Balance = 100 };

            Google google = new Google();
            Facebook facebook = new Facebook();

            google.OnStockChange += (message) => Console.WriteLine(message);
            facebook.OnStockChange += (message) => Console.WriteLine(message);

            Random random = new Random();

            for (int i = 0; i < 10; i++)
            {
                google.Stock = random.Next(10, 25); 
                google.CheckStock();
                if (google.Stock > 20)
                {
                    Console.WriteLine("Google's stock is above 20. Considering buying.");
                    user.BuyStock(google.Stock);
                }

                facebook.Stock = random.Next(10, 25);
                facebook.CheckStock();
                if (facebook.Stock > 20)
                {
                    Console.WriteLine("Facebook's stock is above 20. Considering buying.");
                    user.BuyStock(facebook.Stock);
                }

                Console.ReadKey();
            }
        }
    }
}
