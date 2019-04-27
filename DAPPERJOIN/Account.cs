﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAPPERJOIN
{
    public class Account
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Country { get; set; }
        public int ShopId { get; set; }
        public IList<Shop> Shop { get; set; }
    }
}
