using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAPPERJOIN
{
    class Program
    {
        static void Main(string[] args)
        {
            DbManager db = new DbManager();
            var list = db.JoinSingleChild();
            foreach (var item in list)
            {
                Console.WriteLine("{0}\n {1}\n {2}\n {3}\n {4}\n {5}\n", item.Name, item.Country, item.Address,item.Shop[0].Name, item.Shop[0].Url, item.Shop[0].ShopId);
            }
            var list1 = db.MultiJoin();
            foreach (var item in list1)
            {
                Console.WriteLine("{0}\n {1}\n {2}\n {3}\n {4}\n {5}\n", item.Name, item.Country, item.Address, item.Shop[0].Name, item.Shop[0].Url, item.Shop[0].ShopId);
            }
            Console.ReadKey();
        }
    }
}
