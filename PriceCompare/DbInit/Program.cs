using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbInit
{
    class Program
    {
        static void Main(string[] args)
        {
            DbInitiator initiator = new DbInitiator(@"C:\Users\CodeValue\Desktop\prices data");

            //initiator.ClearDb();
            //initiator.initChains();
            //initiator.InitStores();
            initiator.InitPrices();

            Console.ReadLine();
        }
    }
}
