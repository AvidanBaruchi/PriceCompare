using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PricesEntitiesModel;
using System.IO.Compression;
using System.Xml;
using System.Xml.Linq;

namespace DbInit
{
    class DbInitiator
    {
        private string _dataPath = null;

        public DbInitiator()
        {
        }

        public void ClearDb()
        {
            using (var context = new PricesContext())
            {
                context.Prices.RemoveRange(context.Prices);
                context.Items.RemoveRange(context.Items);
                context.Stores.RemoveRange(context.Stores);
                context.Chains.RemoveRange(context.Chains);

                try
                {
                    context.SaveChanges();
                }
                catch (Exception e)
                {
                    Console.WriteLine("Unable to clea db" + e.Message);
                }
            }
        }

        public DbInitiator(string dataPath) : this()
        {
            _dataPath = dataPath;
        }

        public void initChains()
        {
            using (var db = new PricesContext())
            {
                db.Chains.Add(new Chain("7290700100008", "ColBo Hazi Hinam"));
                db.Chains.Add(new Chain("7290633800006", "Coop"));
                db.Chains.Add(new Chain("7290492000005", "Dor Alon"));
                db.Chains.Add(new Chain("7290055755557", "Eden Teva Market"));
                db.Chains.Add(new Chain("7290876100000", "Fresh Market"));
                db.Chains.Add(new Chain("7290785400000", "Keshet Taamim"));
                db.Chains.Add(new Chain("7290661400001", "Machsanei HaShuk"));
                db.Chains.Add(new Chain("7290058179503", "Machsanei Lahav"));
                db.Chains.Add(new Chain("7290055700007", "Mega"));
                db.Chains.Add(new Chain("7290058140886", "Rami Levi Shivuk Shikma"));
                db.Chains.Add(new Chain("7290027600007", "Shufersal"));
                db.Chains.Add(new Chain("7290873900009", "Super Dosh"));
                db.Chains.Add(new Chain("7290803800003", "Supershuk Yohananof"));
                db.Chains.Add(new Chain("7290873255550", "Tiv Taam"));
                db.Chains.Add(new Chain("7290696200003", "Victory"));
                db.Chains.Add(new Chain("7290725900003", "Yeinot Bitan"));

                try
                {
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }

        public void InitStores()
        {
            CheckPath();

            DirectoryInfo dir = new DirectoryInfo(_dataPath);

            var stores = dir.EnumerateFiles("*Stores*", SearchOption.AllDirectories)
                .ToList();

            foreach (var storeXmlPath in stores)
            {
                var element = XElement.Load(storeXmlPath.FullName);

                NormalizeXml(element);

                var chainId = element.Descendants("chainid").ToArray()[0].Value;

                List<Store> storesList = element
                .Descendants("store")
                .Select((store) =>
                {
                    Store storeEntity = new Store();

                    storeEntity.StoreId = store.Descendants("storeid").First().Value;
                    storeEntity.Name = store.Descendants("storename").First().Value;
                    storeEntity.Address = store.Descendants("address").First().Value;
                    storeEntity.City = store.Descendants("city").First().Value;

                    var storeType = int.Parse(store.Descendants("storetype").First().Value);
                    storeEntity.StoreType = (StoreType)storeType;
                    storeEntity.ChainId = chainId;

                    return storeEntity;
                })
                .ToList();

                using (var db = new PricesContext())
                {
                    var chain = db.Chains.First(c => c.ChainId == chainId);

                    foreach (var store in storesList)
                    {
                        store.Chain = chain;
                        chain.Stores.Add(store);
                    }

                    try
                    {
                        db.SaveChanges();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine($"Cant Save stores {e.Message}");
                    }
                }
            }
        }

        public void InitPrices()
        {
            CheckPath();

            DirectoryInfo dir = new DirectoryInfo(_dataPath);

            var priceFullPaths = dir.EnumerateFiles("*PriceFull*", SearchOption.AllDirectories)
                .ToList();

            foreach (var priceFullPath in priceFullPaths)
            {
                var element = XElement.Load(priceFullPath.FullName);

                NormalizeXml(element);
                var chainId = element.Descendants("chainid").First().Value;
                var storeId = element.Descendants("storeid").First().Value;

                List<Price> prices = element
                .Descendants("item")
                .Where(el => el.Descendants("itemcode").First().Value.Length == 13)
                .Select((item) =>
                {
                    var price = new Price();
                    var itemEntity = CreateItem(item);

                    price.Item = itemEntity;
                    price.PriceValue = double.Parse(item.Descendants("itemprice").First().Value);

                    return price;
                })
                .ToList();

                using (var db = new PricesContext())
                {
                    var storeEntity = db.Stores.First(s => s.ChainId == chainId && s.StoreId == storeId);

                    foreach (var price in prices)
                    {
                        price.Item = AddOrUpdateItem(db, price.Item);
                        price.ItemCode = price.Item.ItemCode;
                        price.Store = storeEntity;
                    }
                    
                    db.Prices.AddRange(prices);

                    try
                    {
                        db.SaveChanges();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine($"Cant Save Prices {e.Message}");
                    }
                }
            }
        }

        private Item CreateItem(XElement itemElement)
        {
            var item = new Item();
            var itemCode = itemElement.Descendants("itemcode").First().Value;

            item.ItemCode = itemCode;
            item.ManufacturerItemDescription = itemElement.Descendants("manufactureritemdescription").First().Value;
            item.ManufacturerName = itemElement.Descendants("manufacturername").First().Value;
            item.Name = itemElement.Descendants("itemname").First().Value;

            return item;
        }

        private void NormalizeXml(XElement element)
        {
            foreach (var node in element.DescendantsAndSelf())
            {
                node.Name = node.Name.ToString().ToLower();
            }
        }

        private Item AddOrUpdateItem(PricesContext db, Item item)
        {
            var found = db.Items.Find(item.ItemCode);

            if (found == null)
            {
                db.Items.Add(item);

                return item;
            }
            else
            {
                found.Name = item.Name;
                found.ManufacturerItemDescription = item.ManufacturerItemDescription;
                found.ManufacturerName = item.ManufacturerName;

                return found;
            }
        }

        private void CheckPath()
        {
            if (!Directory.Exists(_dataPath))
            {
                throw new DirectoryNotFoundException();
            }
        }
    }
}
