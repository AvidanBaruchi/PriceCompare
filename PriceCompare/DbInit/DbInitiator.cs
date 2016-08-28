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
        private DbContext db = null;
        private string _dataPath = null;

        public DbInitiator()
        {
            
        }

        public DbInitiator(string dataPath)
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

            foreach (var directory in dir.EnumerateDirectories())
            {
                var stores = directory.GetFiles("*Stores*");

                if (stores.Length != 1)
                {
                    return;
                }

                var storeFile = stores[0];
                
                if (storeFile.Extension == "zip")
                {
                    try
                    {
                        using (var archive = new ZipArchive(storeFile.OpenRead()))
                        {
                            var xmlFile = archive.Entries[0];

                            var xElement = XElement.Load(xmlFile.Open());
                        }
                    }
                    catch (Exception)
                    {
                     
                    }
                }
            }
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
