using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Configuration;
using System.Data;
using System.Data.Common;

namespace DAPPERJOIN
{
    public class DbManager
    {
        private readonly string ConnectionString;

        public DbManager()
        {
            ConnectionString = ConfigurationManager.ConnectionStrings["cnnString"].ConnectionString;
        }

        public IDbConnection GetOpenConnection()
        {
            var factory = DbProviderFactories.GetFactory("System.Data.SqlClient");
            var connection = factory.CreateConnection();
            connection.ConnectionString = ConnectionString;
            connection.Open();
            return connection;
        }

        // one to one
        public List<Account> JoinSingleChild()
        {
            using (IDbConnection db = GetOpenConnection())
            {
                var resultList = db.Query<Account, Shop, Account>(@"
                    SELECT a.Name, a.Address, a.Country, a.ShopId,
                            s.ShopId, s.Name, s.Url
                    FROM Account a
                    INNER JOIN Shop s ON s.ShopId = a.ShopId                    
                    ", (a, s) =>
                     {
                         if (a.Shop == null)
                         {
                             a.Shop = new List<Shop>();

                         }
                         a.Shop.Add(s);
                         return a;
                     },
                     splitOn: "ShopId"
                     ).ToList();
                return resultList;
            }
        }

        // one to many
        public List<Account> MultiJoin()
        {
            using (IDbConnection db = GetOpenConnection())
            {
                var lookup = new Dictionary<int, Account>();
                var query = @"SELECT a.*,
                            s.*
                    FROM Account a
                    INNER JOIN ShopAccount sa ON sa.ShopId = a.Id   
                 INNER JOIN Shop s ON s.ShopId = sa.ShopId ";
                return db.Query<Account, Shop, Account>(query, (a, s) =>
                {
                    Account acc;
                    if (!lookup.TryGetValue(a.Id, out acc))
                        lookup.Add(a.Id, acc = a);

                    if (acc.Shop == null)
                        acc.Shop = new List<Shop>();

                    acc.Shop.Add(s);

                    return acc;
                },
                splitOn: "ACCOUNTID, ShopId").ToList();


            }
        }
    }
}
