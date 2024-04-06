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
                            Id = laptops.Id,
                            Type=laptops.Type, 
                            Model=laptops.Model, 
                            Brand=assets.Brand, 
                            Office=assets.Office, 
                            PriceInUSD=assets.PriceInUSD, 
                            PurchaseDate=assets.PurchaseDate};

            assetList.Clear(); // Make sure it's empty.

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
       
        /// <summary>
        /// Display a menu of options.
        /// </summary>
        public static void DisplayMenu()
        {
            Console.WriteLine("\nMenu:");
            Console.WriteLine("(1) Show an asset list sorted by Type and Date");
            Console.WriteLine("(2) Show an asset list sorted by Office and Date");
            Console.WriteLine("(3) Add a new asset");
            Console.WriteLine("(4) Delete an asset");
            Console.WriteLine("(5) Update asset");
            Console.WriteLine("Enter \"Q\" to quit\n");
        }

        /// <summary>
        /// Add a new asset to the list that can be either computer or mobile.
        /// </summary>
        public static void AddAsset()
        {
            try
            {
                MyDbContext Context = new MyDbContext();

                string type, model, brand, office, input;
                double priceInUSD;

                Console.WriteLine("Add a new asset by entering the following: ");

                Console.Write("Type (Mobile/Computer): ");
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
                    case "mobile":
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
        /// Update and set new properties for an asset.
        /// </summary>
        public static void UpdateAsset()
        {
            try
            {
                // A list with numbers to select one of the list items.
                ShowOrderedList();

                Console.WriteLine("Select a number to update this item.");
                string input = Console.ReadLine();

                if (!int.TryParse(input, out int selectedIndex))
                {
                    throw new FormatException();
                }

                // select item to update.
                var myItem = assetList[selectedIndex];

                MyDbContext Context = new MyDbContext();

                // The properties to update.
                string model, brand, office;
                double priceInUSD;

                Console.WriteLine("Update the asset by entering the following: ");

                Console.Write("Brand: ");
                brand = Console.ReadLine();

                Console.Write("Model: ");
                model = Console.ReadLine();

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

                Assets asset;
                bool error = false;

                switch (myItem.Type.ToLower())
                {
                    case "computer":
                        LaptopComputers computer = Context.LaptopComputers.SingleOrDefault(x => x.Id == myItem.Id); // Get entity by the primary key.                                                                                    

                        asset = Context.Assets.SingleOrDefault(x => x.Id == computer.AssetsId); // Get entity by the foreign key, which is the primary key in the other entity.

                        asset.PriceInUSD = price;
                        asset.Brand = brand;
                        asset.Office = office;
                        asset.PurchaseDate = date;

                        computer.Model = model;
                     
                        Context.Assets.Update(asset);
                        Context.LaptopComputers.Update(computer);
                        
                        Context.SaveChanges();
                        break;
                    case "mobile":
                        MobilePhones phone = Context.MobilePhones.SingleOrDefault(x => x.Id == myItem.Id); // Get entity by the primary key.
                        asset = Context.Assets.SingleOrDefault(x => x.Id == phone.AssetsId); // Get entity by the foreign key, which is the primary key in the other entity.

                        asset.PriceInUSD = price;
                        asset.Brand = brand;
                        asset.Office = office;
                        asset.PurchaseDate = date;

                        phone.Model = model;
           
                        Context.Assets.Update(asset);
                        Context.MobilePhones.Update(phone);
                       
                        Context.SaveChanges();   
                        break;
                    default:
                        error = true;
                        break;
                }

                if (error)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Asset could not be updated. Try again!\n");
                }
                else
                {
                    Load(); // Load updates from the database.
                    ShowOrderedList(); // Show list with the updated item.
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("The new asset was updated successfully!\n");
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
                    case "mobile":
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
                    ShowOrderedList(); // Show the updated list.
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
            Console.WriteLine("  Type".PadRight(14) + "Brand".PadRight(12) + "Model".PadRight(12) + "Office".PadRight(10) + "Purchase Date".PadRight(15) + "Price in USD".PadRight(15) + "Currency".PadRight(12) + "Local price today");
            Console.WriteLine("  ----".PadRight(14) + "-----".PadRight(12) + "-----".PadRight(12) + "------".PadRight(10) + "-------------".PadRight(15) + "------------".PadRight(15) + "--------".PadRight(12) + "----------------");

            for (int n = 0; n < assetList.Count; n++)
            { 
                assetList[n].Print(n);
            }

            Console.WriteLine("----------------------------------------------------------------------------------------------------------");
        }
    }
}
