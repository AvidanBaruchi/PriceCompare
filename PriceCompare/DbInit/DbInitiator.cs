using System;
using System.Collections.Generic;
using System.Data.Entity;
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
            //ClearDb();
        }

        private void ClearDb()
        {
            using (var context = new PricesContext())
            {
                context.Chains.RemoveRange(context.Chains);
                context.Stores.RemoveRange(context.Stores);
                context.Items.RemoveRange(context.Items);
                context.Prices.RemoveRange(context.Prices);

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
            //var xElement = XElement.Load(xmlFile.Open());

            var stores = dir.EnumerateFiles("*Stores*", SearchOption.AllDirectories)
                //.Select(file => XElement.Load(file.Name))
                .ToList();

            Parallel.ForEach(stores, (storeFile) =>
            {
                var element = XElement.Load(storeFile.FullName);

                var isUpperCased = element.Descendants("CHAINID").ToArray().Any();

                if (isUpperCased) return;

                var chainId = element.Descendants("ChainId").ToArray()[0].Value;

                IEnumerable<Store> storesList =
                element.Descendants("Store")
                    .Select((store) =>
                    {
                        Store storeEntity = new Store();

                        storeEntity.Name = store.Descendants("StoreName").First().Value;
                        storeEntity.Address = store.Descendants("Address").First().Value;
                        storeEntity.City = store.Descendants("City").First().Value;

                        var storeType = int.Parse(store.Descendants("StoreType").First().Value);
                        storeEntity.StoreType = (StoreType) storeType;

                        return storeEntity;
                    });

                using (var db = new PricesContext())
                {
                    var x = db.Chains.ToList();
                    var chain = db.Chains.First(c => c.ChainId == chainId);

                    foreach (var store in storesList)
                    {
                        store.Chain = chain;
                        chain.Stores.Add(store);
                    }
                    //chain.Stores.Add(storesList);
                }
            });
        }

        public void InitItems()
        {
            CheckPath();
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
