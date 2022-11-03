using Microsoft.Extensions.Configuration;
using System;
using System.Linq;

namespace EF_Core_ConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine(GetWallet(5));

            //var wallet = new Wallet
            //{
            //    Holder = "Salah M.",
            //    Balance = 5600
            //};
            //insert(wallet);

            //update(2, 4000);
            //Delete(15);

            //Transaction(2000);
            //PrintAllWallets();
            //QueringData();

            TestNewSwitchStatement();
            Console.ReadKey();
        }
        public static void TestNewSwitchStatement()
        {
            string firstName = "hazem";
            string jobTitle = string.Empty;
            switch (firstName)
            {
                case "Daniel":
                    jobTitle = "system analyst";
                    break;
                case "hazem":
                    jobTitle = "Database admin";
                    break;
                case "nagaty":
                    jobTitle = "java developer";
                    break;
                default:
                    jobTitle = "junior software engineer";
                    break;
            }
            //new switch code
            jobTitle = firstName switch
            {
                "Daniel" => "system analyst",
                "hazem" => "Database admin",
                "nagaty" => "java developer",
                _ => "junior software engineer"
            };
            Console.WriteLine(firstName + " is a "+ jobTitle);
        }
        public static void Transaction(decimal amount)
        {
            using (var dbContext = new DigitalConcurrencyDbContext())
            {
                using (var transaction = dbContext.Database.BeginTransaction())
                {
                    var walletFrom = dbContext.Wallets.Single(w => w.Id == 2);
                    var walletTo = dbContext.Wallets.Single(w => w.Id == 3);
                    
                    //withdraw
                    
                    
                    if (walletFrom.Balance > amount)
                    {
                        walletFrom.Balance -= amount;
                        dbContext.SaveChanges();
                        //deposite
                        walletTo.Balance += amount;
                        transaction.Commit();
                    }
                    else
                    {
                        Console.WriteLine("cannot complete transaction");
                    }
                }
            }
        }
        public static void QueringData()
        {
            using (var dbContext = new DigitalConcurrencyDbContext())
            {
                var result = dbContext.Wallets.Where(w => w.Balance >= 10000);
                foreach (var item in result)
                {
                    Console.WriteLine(item);
                }
            }
        }
        public static void Delete(int id)
        {
            using (var dbContext = new DigitalConcurrencyDbContext())
            {
                var wallet = dbContext.Wallets.SingleOrDefault(x => x.Id == id);
                dbContext.Wallets.Remove(wallet);
                dbContext.SaveChanges();
            }
        }
        public static void update(int id, decimal _newBalance)
        {
            using (var dbContext = new DigitalConcurrencyDbContext())
            {
                var walletToUpdate = dbContext.Wallets.SingleOrDefault(x => x.Id == id);
                walletToUpdate.Balance = _newBalance;
                dbContext.SaveChanges();    
            }
        }
        public static void insert(Wallet wallet)
        {
            using (var dbContext = new DigitalConcurrencyDbContext())
            {
                dbContext.Wallets.Add(wallet);
                dbContext.SaveChanges();
            }
        }
        public static Wallet GetWallet(int id)
        {
            var wal = new Wallet();
            using (var dbContext = new DigitalConcurrencyDbContext())
            {
                wal = dbContext.Wallets.FirstOrDefault(x => x.Id == id);
            }
            return wal;
        }
        public static void PrintAllWallets()
        {
            using (var dbContext = new DigitalConcurrencyDbContext())
            {
                foreach (var wallet in dbContext.Wallets)
                {
                    Console.WriteLine(wallet);
                }
            }
        }
    }
}
