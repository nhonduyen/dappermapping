using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAPPERJOIN
{
    public class Shop
    {
        public int ShopId { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public IList<Account> Account { get; set; }
    }
}
