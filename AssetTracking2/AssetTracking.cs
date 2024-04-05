using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;
//using EntityRelationships;
//using Microsoft.EntityFrameworkCore;

namespace AssetTracking2
{
    internal class AssetTracking
    {
        public static List<AssetsBLL> sortedList;
        public static List<AssetsBLL> assetList = [];

        public static void Load()
        {
            MyDbContext Context = new MyDbContext();
          
            // select laptop computers
            var query = from assets in Context.Assets
                        join laptops in Context.LaptopComputers
                        on assets.Id equals laptops.AssetsId
                        select new {
                            Id = assets.Id,
                            Type=laptops.Type, 
                            Model=laptops.Model, 
                            Brand=assets.Brand, 
                            Office=assets.Office, 
                            PriceInUSD=assets.PriceInUSD, 
                            PurchaseDate=assets.PurchaseDate};

            // create objects and add them to the list.
            foreach (var item in query)
            {
                //Constructor (int Id, string type, string brand, string model, string office, DateTime purchaseDate, double price)
                assetList.Add(new LaptopComputersBLL(item.Id, item.Type, item.Brand, item.Model, item.Office, item.PurchaseDate, item.PriceInUSD));
               
            }

            // select mobile phones
            query = from assets in Context.Assets
                        join mobiles in Context.MobilePhones
                        on assets.Id equals mobiles.AssetsId
                        select new
                        {
                            Id = mobiles.Id,
                            Type = mobiles.Type,
                            Model = mobiles.Model,
                            Brand = assets.Brand,
                            Office = assets.Office,
                            PriceInUSD = assets.PriceInUSD,
                            PurchaseDate = assets.PurchaseDate
                        };

            // create objects and add them to the list.
            foreach (var item in query)
            {
                assetList.Add(new MobilePhonesBLL(item.Id, item.Type, item.Brand, item.Model, item.Office, item.PurchaseDate, item.PriceInUSD));
            }

        }
        /*
         var id = 1;
    var query =
   from post in database.Posts
   join meta in database.Post_Metas on post.ID equals meta.Post_ID
   where post.ID == id
   select new { Post = post, Meta = meta }; */

        //var letters = from letter in fruit group letter by letter into y select new {character=y.Key };
        //Engine MyEngine = Context.Engines.Include(x => x.Car).FirstOrDefault(x => x.Id == 1);



        //Car MyCar1 = Context.Cars.FirstOrDefault(x => x.Id == 1);


        //MyCar1.Model = "Avensis";
        //MyCar1.Year = 2024;
        //Context.Update(MyCar1);
        //Context.SaveChanges();
        /*
        public static List<Asset> assets = [
            new Computer("Computer", "MacBook", "SD", "Sweden", Convert.ToDateTime("2021-06-14"), 1200),
            new Phone("Mobile", "Samsung", "SD", "USA", Convert.ToDateTime("2022-03-12"), 200),
            new Phone("Mobile", "Motorolla", "HD", "India", Convert.ToDateTime("2020-12-22"), 300),
            new Computer("Computer", "Asus", "HD", "India", Convert.ToDateTime("2021-04-16"), 500),
            new Computer("Computer", "HP", "Note", "Sweden", Convert.ToDateTime("2021-03-17"), 500),
            new Phone("Mobile", "Apple", "iPhone12", "Sweden", Convert.ToDateTime("2020-03-19"), 699),
            new Computer("Computer", "Lenovo", "IdeaPad", "India", Convert.ToDateTime("2023-07-17"), 999),
            new Computer("Computer", "Dell", "ThinkPad", "USA", Convert.ToDateTime("2014-06-17"), 499),
            new Phone("Mobile", "Apple", "iPhone14", "USA", Convert.ToDateTime("2021-05-10"), 999),
            new Computer("Computer", "Lenovo", "AZ22", "Sweden", Convert.ToDateTime("2021-08-12"), 599),
            new Phone("Mobile", "Apple", "iPhone11", "USA", Convert.ToDateTime("2019-05-19"), 599),
            new Phone("Mobile", "Apple", "iPhone13", "Sweden", Convert.ToDateTime("2016-07-10"), 799),
            new Computer("Computer", "Lenovo", "SD", "India", Convert.ToDateTime("2022-05-17"), 400),
            new Phone("Mobile", "Apple", "iPhone15", "Sweden", Convert.ToDateTime("2021-01-10"), 1999),
            new Phone("Mobile", "Samsung", "Galaxy", "sweden", Convert.ToDateTime("23-10-10"), 2100),
        ];*/

        /// <summary>
        /// Display a menu of options.
        /// </summary>
        public static void DisplayMenu()
        {
            Console.WriteLine("\nMenu:");
            Console.WriteLine("(1) Show an asset list sorted by Type and Date.");
            Console.WriteLine("(2) Show an asset list sorted by Office and Date.");
            Console.WriteLine("(3) Add a new asset.");
            Console.WriteLine("(4) Delete an asset.");
            Console.WriteLine("Enter \"Q\" to quit\n");
        }

        /// <summary>
        /// Add a new asset to the list that can be either computer or phone.
        /// </summary>
        public static void AddAsset()
        {
            try
            {
                MyDbContext Context = new MyDbContext();

                string type, model, brand, office, input;
                double priceInUSD;

                Console.WriteLine("Add a new asset by entering the following: ");

                Console.Write("Type (Phone/Computer): ");
                type = Console.ReadLine();

                Console.Write("Model: ");
                model = Console.ReadLine();

                Console.Write("Brand: ");
                brand = Console.ReadLine();

                Console.Write("Office (Sweden/USA/India): ");
                office = Console.ReadLine();

                Console.Write("Purchase Date (year-month-day): ");
                input = Console.ReadLine();

                if (!DateTime.TryParse(input, out DateTime date))
                {
                    throw new FormatException();
                }

                Console.Write("Price in USD: ");
                input = Console.ReadLine();

                if (!double.TryParse(input, out double price))
                {
                    throw new FormatException();
                }

                Assets asset1;
                LaptopComputers computers;
                MobilePhones mobilePhones;

                bool error = false;

                switch (type.ToLower())
                {
                    case "computer":
                        asset1 = new Assets();

                        computers = new LaptopComputers { Assets = asset1 }; // add foreign key.

                        asset1.PriceInUSD= price;
                        asset1.Brand = brand;
                        asset1.Office = office;
                        asset1.PurchaseDate = date;

                        computers.Model = model;
                        computers.Type = type;
                     
                        Context.Assets.Add(asset1);
                        Context.LaptopComputers.Add(computers);
                        Context.SaveChanges();

                        // adding to list
                        assetList.Add(new LaptopComputersBLL(computers.Id, type, brand, model, office, date, price));
                        break;
                    case "phone":
                        asset1 = new Assets();

                        mobilePhones = new MobilePhones { Assets = asset1 }; // add foreign key.
                          
                        asset1.PriceInUSD = price;
                        asset1.Brand = brand;
                        asset1.Office = office;
                        asset1.PurchaseDate = date;

                        mobilePhones.Model = model;
                        mobilePhones.Type = type;
                    
                        Context.Assets.Add(asset1);
                        Context.MobilePhones.Add(mobilePhones);
                        Context.SaveChanges();

                        // adding to list
                        assetList.Add(new MobilePhonesBLL(mobilePhones.Id, type, brand, model, office, date, price));
                        break;
                    default:
                        error = true;
                        break;
                }

                if (error)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Asset could not be added. Try again!\n");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("The new asset was added successfully!\n");
                }

                Console.ResetColor();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error:" + ex.Message);
            }
            return;
        }

        /// <summary>
        /// Remove an asset
        /// </summary>
        public static void Remove()
        {
            try
            {
                // A list with numbers.
                ShowOrderedList();

                Console.WriteLine("Select a number to remove this item.");
                string input = Console.ReadLine();

                if (!int.TryParse(input, out int selectedIndex))
                {
                    throw new FormatException();
                }

                // select item to delete.
                var myItem = assetList[selectedIndex];
                Console.WriteLine(myItem.Type);
                MyDbContext Context = new MyDbContext();

                Assets asset;
                bool error = false;

                switch (myItem.Type.ToLower())
                {
                    case "computer":
                        LaptopComputers computer = Context.LaptopComputers.SingleOrDefault(x => x.Id == myItem.Id); // Get entity by the primary key.         
                        asset = Context.Assets.SingleOrDefault(x => x.Id == computer.AssetsId); // Get entity by the foreign key, which is the primary key in the other entity.

                        Context.Remove(computer);
                        Context.Remove(asset);
                        assetList.Remove(myItem);
                        Context.SaveChanges();   
                        break;
                    case "phone":
                        MobilePhones phone = Context.MobilePhones.SingleOrDefault(x => x.Id == myItem.Id); // Get entity by the primary key.
                        asset = Context.Assets.SingleOrDefault(x => x.Id == phone.AssetsId); // Get entity by the foreign key, which is the primary key in the other entity.

                        Context.Remove(phone);
                        Context.Remove(asset);
                        assetList.Remove(myItem);
                        Context.SaveChanges();
                        break;
                    default:
                        error = true;
                        break;
                }

                if (error)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Asset could not be removed. Try again!\n");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("The asset was removed successfully!\n");
                }

                Console.ResetColor();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// Show a list of assets. Can be sorted by office and date or by type and date if the parameter is left out.
        /// </summary>
        /// <param name="orderByOffice"></param>
        public static void ShowList(bool orderByOffice = false)
        {
            if (orderByOffice)
            {
                sortedList = assetList.OrderBy(asset => asset.Office).ThenBy(asset => asset.PurchaseDate).ToList();
            }
            else
            {
                sortedList = assetList.OrderBy(asset => asset.GetType().Name).ThenBy(asset => asset.PurchaseDate).ToList();
            }

            Console.WriteLine("\n");
            Console.WriteLine("Type".PadRight(12) + "Brand".PadRight(12) + "Model".PadRight(12) + "Office".PadRight(10) + "Purchase Date".PadRight(15) + "Price in USD".PadRight(15) + "Currency".PadRight(12) + "Local price today");
            Console.WriteLine("----".PadRight(12) + "-----".PadRight(12) + "-----".PadRight(12) + "------".PadRight(10) + "-------------".PadRight(15) + "------------".PadRight(15) + "--------".PadRight(12) + "----------------");

            foreach (AssetsBLL asset in sortedList)
            {
                asset.Print();
            }

            Console.WriteLine("----------------------------------------------------------------------------------------------------------");
        }



        /// <summary>
        /// Show a list with numbers.
        /// </summary>
        public static void ShowOrderedList()
        {
            Console.WriteLine("\n");
            Console.WriteLine("Type".PadRight(12) + "Brand".PadRight(12) + "Model".PadRight(12) + "Office".PadRight(10) + "Purchase Date".PadRight(15) + "Price in USD".PadRight(15) + "Currency".PadRight(12) + "Local price today");
            Console.WriteLine("----".PadRight(12) + "-----".PadRight(12) + "-----".PadRight(12) + "------".PadRight(10) + "-------------".PadRight(15) + "------------".PadRight(15) + "--------".PadRight(12) + "----------------");

            for (int n = 0; n < assetList.Count(); n++)
            { 
                assetList[n].Print(n);
            }

            Console.WriteLine("----------------------------------------------------------------------------------------------------------");
        }
    }
}
